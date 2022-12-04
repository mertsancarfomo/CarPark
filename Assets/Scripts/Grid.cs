using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using CodeMonkey.Utils;
using UnityEngine;
using Random = UnityEngine.Random;

public class Grid {
    
    private int width;
    private int height;
    private float cellSize;
    private Vector3 originPosition;
    private PathNode[,] gridArray;
    
    public List<Car> cars = new List<Car>();
    public List<ParkingLot> parkingLots = new List<ParkingLot>();

    public Grid(int width, int height, float cellSize, Vector3 originPosition) {
        
        this.width = width; 
        this.height = height;
        this.cellSize = cellSize;
        this.originPosition = originPosition;
        
        gridArray = new PathNode[this.width, this.height];

        foreach (LevelManager.Car carData in LevelManager.myLevel.cars)
        {
            ColorUtility.TryParseHtmlString(CarMovement.carColors[carData.color], out var carColor); 
            ParkingLot parkingLot = new ParkingLot(carData.targetX, carData.targetY, carColor);
            parkingLots.Add(new ParkingLot(carData.targetX, carData.targetY, carColor));
            Car car = new Car(carData.startX, carData.startY, parkingLot, (CarDirection) Enum.Parse(typeof(CarDirection), carData.carDirection, true));
            cars.Add(car);
        }

        for (int x = 0; x < gridArray.GetLength(0); x++) {
            for (int y = 0; y < gridArray.GetLength(1); y++) {
                if (x == 0 || x == gridArray.GetLength(0) - 1)
                {
                    gridArray[x, y] = new PathNode(x, y);
                    gridArray[x, y].tile = TileType.Empty;
                    continue;
                }
                if (y == 0 || y == gridArray.GetLength(1) - 1)
                {
                    gridArray[x, y] = new PathNode(x, y);
                    gridArray[x, y].tile = TileType.Empty;
                    continue;
                }
                gridArray[x, y] = new PathNode(x, y);
            }
        }

        foreach (Car car in cars)
        {
            gridArray[car.startX, car.startY] = new PathNode(car.startX, car.startY, car);
        }
        
        foreach (ParkingLot parkingLot in parkingLots)
        {
            gridArray[parkingLot.x, parkingLot.y] = new PathNode(parkingLot.x, parkingLot.y, parkingLot);
        }
        
    }

    public void GenerateTiles()
    {
        for (int x = 0; x < gridArray.GetLength(0); x++) {
            for (int y = 0; y < gridArray.GetLength(1); y++)
            {
                PathNode node = GetGridObject(x, y);
                
                if (node == null || node.tile == TileType.Empty)
                {
                    continue;
                }
                UtilsClass.CreateTile(gridArray[x, y]?.ToString(), node,  GameObject.Find("Grid").transform, GetWorldPosition(x, y) + new Vector3(cellSize, cellSize) * .5f, 30, Color.white, TextAnchor.MiddleCenter);
            }
        }
    }

    public int GetWidth() {
        return width;
    }

    public int GetHeight() {
        return height;
    }

    public float GetCellSize() {
        return cellSize;
    }

    public Vector3 GetWorldPosition(int x, int y) {
        return new Vector3(x, 0, y) * cellSize + originPosition;
    }

    public void GetXY(Vector3 worldPosition, out int x, out int y) {
        x = Mathf.FloorToInt((worldPosition - originPosition).x / cellSize);
        y = Mathf.FloorToInt((worldPosition - originPosition).y / cellSize);
    }
    
    public PathNode GetGridObject(int x, int y) {
        if (x >= 0 && y >= 0 && x < width && y < height) {
            return gridArray[x, y];
        } else {
            return default(PathNode);
        }
    }


}
