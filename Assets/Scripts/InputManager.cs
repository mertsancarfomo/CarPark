using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    private static bool isFirstClick = true;
    private static Transform clickedTileObject;
    private static PathNode clickedCar;
    
     public static void ClickTile_2Tap(Pathfinding pathfinding)
     {
            if (Input.GetMouseButtonDown(0))
            {
                RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
         
                if(hit.collider != null )
                {
                    if (isFirstClick)
                    {
                        if (hit.transform.childCount == 2) // 1'den fazlaysa arabaya tıklamıştır
                        {
                            clickedTileObject = hit.transform;
                            clickedTileObject.GetChild(0).GetComponent<SpriteRenderer>().color = Color.green;

                            int x = int.Parse(hit.collider.transform.gameObject.name.Split(",")[0]);
                            int y = int.Parse(hit.collider.transform.gameObject.name.Split(",")[1]);

                            clickedCar = pathfinding.grid.GetGridObject(x, y);
                                
                            Debug.Log("Testing.cs: ClickTile() -> CAR SELECTED");
                            
                            // try
                            // {
                            //     int x = int.Parse(hit.collider.transform.gameObject.name.Split(",")[0]);
                            //     int y = int.Parse(hit.collider.transform.gameObject.name.Split(",")[1]);
                            //
                            //     clickedCar = pathfinding.grid.GetGridObject(x, y);
                            //     
                            //     Debug.Log("Testing.cs: ClickTile() -> CAR SELECTED");
                            //     
                            //     // List<PathNode> path = pathfinding.FindPath(node.x, node.y, node.car.targetX, node.car.targetY);
                            //     // pathfinding.StartPath(path);
                            // }
                            // catch
                            // {
                            //     Debug.Log("Testing.cs: ClickTile() -> There is no car");
                            // }
                            isFirstClick = false;
                        }

                    }
                    else
                    {
                        if (hit.transform.childCount == 1)
                        {
                            clickedTileObject.GetChild(0).GetComponent<SpriteRenderer>().color = new Color(231, 230, 230, 255);

                            int x = int.Parse(hit.collider.transform.gameObject.name.Split(",")[0]);
                            int y = int.Parse(hit.collider.transform.gameObject.name.Split(",")[1]);
                            
                            PathNode targetNode = pathfinding.grid.GetGridObject(x, y);
                            
                            List<PathNode> path = pathfinding.FindPath(clickedCar.x, clickedCar.y, targetNode.x, targetNode.y);
                            pathfinding.StartPath(path);
                            
                            Debug.Log("Testing.cs: ClickTile() -> CAR MOVE");
                        }
                        else
                        {
                            clickedTileObject.GetChild(0).GetComponent<SpriteRenderer>().color = new Color(231, 230, 230, 255);
                        }
                        // try
                        // {
                        //     int x = int.Parse(hit.collider.transform.gameObject.name.Split(",")[0]);
                        //     int y = int.Parse(hit.collider.transform.gameObject.name.Split(",")[1]);
                        //     
                        //     PathNode targetNode = pathfinding.grid.GetGridObject(x, y);
                        //     
                        //     List<PathNode> path = pathfinding.FindPath(clickedCar.x, clickedCar.y, targetNode.x, targetNode.y);
                        //     pathfinding.StartPath(path);
                        //     
                        //     Debug.Log("Testing.cs: ClickTile() -> CAR MOVE");
                        // }
                        // catch
                        // {
                        //     Debug.Log("Testing.cs: ClickTile() -> There is a car");
                        // }
                        isFirstClick = true;

                    }
                    
                }
            }
     }
     
     public static void ClickTile_1Tap(Pathfinding pathfinding)
     {
         if (Input.GetMouseButtonDown(0))
         {
             RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);

             if (hit.collider != null)
             {
                 if (hit.transform.childCount == 2)
                 {
                     int x = int.Parse(hit.collider.transform.gameObject.name.Split(",")[0]);
                     int y = int.Parse(hit.collider.transform.gameObject.name.Split(",")[1]);
                     
                     PathNode carNode = pathfinding.grid.GetGridObject(x, y);
                     
                     List<PathNode> path = pathfinding.FindPath(carNode.car.startX, carNode.car.startY, carNode.car.targetX, carNode.car.targetY);
                     if (path == null)
                     {
                         LevelManager.UpdateWrongMoveCount();
                         return;
                     }
                     pathfinding.StartPath(path);
                     
                     Debug.Log("Testing.cs: ClickTile() -> CAR MOVE");
                     
                 }
                 else
                 {
                     Debug.Log("Testing.cs: ClickTile() -> There is no car");
                 }

             }
         }
     }
     
     public static void ClickTile_1Tap_3D(Pathfinding pathfinding)
     {
         if (Input.GetMouseButtonDown(0))
         {
             RaycastHit hit; 
             Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

             if (Physics.Raycast (ray,out hit,100.0f))
             {
                 if (hit.transform.childCount == 2)
                 {
                     int x = int.Parse(hit.collider.transform.gameObject.name.Split(",")[0]);
                     int y = int.Parse(hit.collider.transform.gameObject.name.Split(",")[1]);
                     
                     PathNode carNode = pathfinding.grid.GetGridObject(x, y);
                     
                     List<PathNode> path = pathfinding.FindPath(carNode.car.startX, carNode.car.startY, carNode.car.targetX, carNode.car.targetY);
                     if (path == null)
                     {
                         LevelManager.UpdateWrongMoveCount();
                         return;
                     }
                     pathfinding.StartPath(path);
                     
                     Debug.Log("Testing.cs: ClickTile() -> CAR MOVE");
                     
                 }
                 else
                 {
                     Debug.Log("Testing.cs: ClickTile() -> There is no car");
                 }

             }
         }
     }
     
     public static void SetCameraPosition()
     {
         // int cameraSize = 1;
         // if (LevelManager.myLevel.height < LevelManager.myLevel.width)
         // {
         //     cameraSize = LevelManager.myLevel.height * 10;
         // }
         // else
         // {
         //     cameraSize = LevelManager.myLevel.width * 10;
         // }
         //
         Camera.main.transform.localPosition = new Vector3(LevelManager.myLevel.width * 5, LevelManager.myLevel.height * 5, -10f);
         Camera.main.GetComponent<Camera>().orthographicSize = (LevelManager.myLevel.width  * 7) + (LevelManager.myLevel.height * 7);

     }
}
