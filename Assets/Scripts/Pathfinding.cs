using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using CodeMonkey.Utils;
using DG.Tweening;
using Unity.VisualScripting;
using UnityEngine;
using Path = DG.Tweening.Plugins.Core.PathCore.Path;

public class Pathfinding
{
    private const int MOVE_STRAIGHT_COST = 10;
    private const int MOVE_DIAGONAL_COST = 14;

    public Grid grid;
    private Car currentCar;
    private PathNode currentNode;
    private List<PathNode> openList;
    private List<PathNode> closedList;

    public Pathfinding(int width, int height)
    {
        grid = new Grid(width, height, 10f, Vector3.zero);
        grid.GenerateTiles();
    }

    public List<PathNode> FindPath(int startX, int startY, int endX, int endY)
    {
        PathNode startNode = grid.GetGridObject(startX, startY);
        PathNode endNode = grid.GetGridObject(endX, endY);

        currentCar = startNode.car;

        openList = new List<PathNode> { startNode };
        closedList = new List<PathNode>();

        for (int x = 0; x < grid.GetWidth(); x++)
        {
            for (int y= 0; y < grid.GetHeight(); y++)
            {
                PathNode pathNode = grid.GetGridObject(x, y);
                pathNode.gCost = int.MaxValue;
                pathNode.CalculateFCost();
                pathNode.cameFromNode = null;
            }
        }

        startNode.gCost = 0;
        startNode.hCost = CalculateDistanceCost(startNode, endNode);
        startNode.CalculateFCost();

        currentNode = startNode;
        currentCar = currentNode.car;
        
        while (openList.Count > 0)
        {
            if (currentNode != null)
            {
                currentNode =  GetLowestFCostNode(openList);
            }

            if (currentNode == endNode)
            {
                return CalculatePath(endNode);
            }
            
            openList.Remove(currentNode);
            closedList.Add(currentNode);


            List<PathNode> neighbourList = GetNeighbourList(currentNode);
            foreach (PathNode neighbourNode in neighbourList)
            {
                if (closedList.Contains(neighbourNode))
                {
                    continue;
                }
                if (currentNode.tile == TileType.Empty)
                {
                    continue;
                }

                int tentativeGCost = currentNode.gCost + CalculateDistanceCost(currentNode, neighbourNode);
                if (tentativeGCost < neighbourNode.gCost) {
                    neighbourNode.cameFromNode = currentNode;
                    neighbourNode.gCost = tentativeGCost;
                    neighbourNode.hCost = CalculateDistanceCost(neighbourNode, endNode);
                    neighbourNode.CalculateFCost();
                }

                if (!openList.Contains(neighbourNode))
                {
                    openList.Add(neighbourNode);
                }
            }
        }

        return null;
    }

    public void StartPath(List<PathNode> path)
    {        
        currentNode = null;
        
        if (path == null)
        {
            Debug.Log("Pathfinding.cs: ShowPath() -> Car can not move");
            return;
        }

        PathNode startNode = path[0];
        PathNode endNode = path[path.Count-1];
        
        Vector3[] vectorlist = new Vector3[path.Count];
        for (int i = 0; i < path.Count; i++)
        {
            GameObject go =  GameObject.Find(path[i].x + " , " + path[i].y);
            vectorlist[i] = new Vector3(go.transform.position.x, 0.5f, go.transform.position.z) ;
        }
        
        string currentTileObjectName = startNode.x + " , " + startNode.y;
        string endTileObjectName = endNode.x + " , " + endNode.y;

        GameObject currentTileObject = GameObject.Find(currentTileObjectName);
        GameObject endTileObject = GameObject.Find(endTileObjectName);


        if (currentCar.targetX == endNode.x && currentCar.targetY == endNode.y)
        {
            CarMovement.MoveCartoParkinglot(currentCar, currentTileObject.transform, endTileObject.transform, vectorlist);
            SetCurrentCarPosition(currentCar.startX, currentCar.startY, currentCar.targetX, currentCar.targetY);
        }
        else
        {
            CarMovement.MoveCar(currentCar, currentTileObject.transform, endTileObject.transform, vectorlist);
            SetCurrentCarPosition(currentCar.startX, currentCar.startY, endNode.x, endNode.y);
        }

    }
    
    private List<PathNode> GetNeighbourList(PathNode currentNode) {
        List<PathNode> neighbourList = new List<PathNode>();
        
        if(currentNode.x - 1 >= 0)
        {
            if (true) // currentCar.carDirection != CarDirection.Right nereye dönükse oraya gitmek için
            {
                PathNode node = GetNode(currentNode.x - 1, currentNode.y);
                if (node.tile == TileType.Way)
                {
                    neighbourList.Add(node);
                }

                else if (node.tile == TileType.Car)
                {
                    if (node.car.targetX == currentCar.targetX && node.car.targetY == currentCar.targetY)
                    {
                        neighbourList.Add(node);
                    }
                }
                else if (node.tile == TileType.ParkingLot)
                {
                    if (currentCar.targetX == node.x && currentCar.targetY == node.y)
                    {
                        neighbourList.Add(node);
                    }
                }
            }
        }
        
        if(currentNode.x + 1 < grid.GetWidth())
        {
            if (true) // currentCar.carDirection != CarDirection.Left
            {
                PathNode node = GetNode(currentNode.x + 1, currentNode.y);
                if (node.tile == TileType.Way)
                {
                    neighbourList.Add(node);
                }
                else if (node.tile == TileType.Car)
                {
                    if (node.car.targetX == currentCar.targetX && node.car.targetY == currentCar.targetY)
                    {
                        neighbourList.Add(node);
                    }
                }
                else if (node.tile == TileType.ParkingLot)
                {
                    if (currentCar.targetX == node.x && currentCar.targetY == node.y)
                    {
                        neighbourList.Add(node);
                    }
                }
            }
        }

        if (currentNode.y - 1 >= 0)
        {
            if (true) // currentCar.carDirection != CarDirection.Up
            {
                PathNode node = GetNode(currentNode.x, currentNode.y - 1);
                if (node.tile == TileType.Way)
                {
                    neighbourList.Add(node);
                }
                else if (node.tile == TileType.Car)
                {
                    if (node.car.targetX == currentCar.targetX && node.car.targetY == currentCar.targetY)
                    {
                        neighbourList.Add(node);
                    }
                }
                else if (node.tile == TileType.ParkingLot)
                {
                    if (currentCar.targetX == node.x && currentCar.targetY == node.y)
                    {
                        neighbourList.Add(node);
                    }
                }
            }
            
        }

        if (currentNode.y + 1 < grid.GetHeight())
        {
            if (true) // currentCar.carDirection != CarDirection.Down
            {
                PathNode node = GetNode(currentNode.x, currentNode.y + 1);
                if (node.tile == TileType.Way)
                {
                    neighbourList.Add(node);
                }
                else if (node.tile == TileType.Car)
                {
                    if (node.car.targetX == currentCar.targetX && node.car.targetY == currentCar.targetY)
                    {
                        neighbourList.Add(node);
                    }
                }
                else if (node.tile == TileType.ParkingLot)
                {
                    if (currentCar.targetX == node.x && currentCar.targetY == node.y)
                    {
                        neighbourList.Add(node);
                    }
                }
            }
        }
        
        return neighbourList;
    }
    
    private PathNode GetNode(int x, int y) {
        return grid.GetGridObject(x, y);
    }
    
    private List<PathNode> CalculatePath(PathNode endNode)
    {
        List<PathNode> path = new List<PathNode>();
        path.Add(endNode);
        PathNode currentNode = endNode;
        while (currentNode.cameFromNode != null)
        {
            path.Add(currentNode.cameFromNode);
            currentNode = currentNode.cameFromNode;
        }
        path.Reverse();
        return path; 
    }
    
    private int CalculateDistanceCost(PathNode a, PathNode b) {
        int xDistance = Mathf.Abs(a.x - b.x);
        int yDistance = Mathf.Abs(a.y - b.y);
        int remaining = Mathf.Abs(xDistance - yDistance);
        return MOVE_DIAGONAL_COST * Mathf.Min(xDistance, yDistance) + MOVE_STRAIGHT_COST * remaining;
    }
    
    private PathNode GetLowestFCostNode(List<PathNode> pathNodeList) {
        PathNode lowestFCostNode = pathNodeList[0];
        for (int i= 1; i < pathNodeList.Count; i ++) {
            if (pathNodeList[i].fCost < lowestFCostNode.fCost) {
                lowestFCostNode = pathNodeList[i];
                
            }
        }
        return lowestFCostNode;
    }

    private void SetCurrentCarPosition(int currentX, int currentY, int newX, int newY)
    {
        PathNode startNode = grid.GetGridObject(currentX, currentY);
        PathNode endNode = grid.GetGridObject(newX, newY);
        
        startNode.tile = TileType.Way;
        startNode.car = null;

        currentCar.startX = newX;
        currentCar.startY = newY;
        
        endNode.tile = TileType.Car;
        endNode.car = currentCar;

    }

}
