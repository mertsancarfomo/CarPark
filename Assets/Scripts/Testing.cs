using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class Testing : MonoBehaviour
{
    [Header("Generate Grid")] 
    public GameObject gridObject;
    public int startingLevel;
    
    public static Pathfinding pathfinding;


    void Start()
    {
        LevelManager.currentLevelId = startingLevel;
        pathfinding = LevelManager.NextLevel();
        //pathfinding = new Pathfinding(5, 5);
    }
    
    void Update()
    {
        //InputManager.ClickTile_2Tap(pathfinding);
        //InputManager.ClickTile_1Tap(pathfinding);
        //InputManager.ClickTile_1Tap_3D(pathfinding);
        InputManager.ClickTile_2Tap_3D(pathfinding);
    
        if (LevelManager.levelEnd)
        {
            //Clear Level
            foreach (Transform child in gridObject.transform) {
                Destroy(child.gameObject);
            }
            
            LevelManager.UpdateLevel();
            pathfinding = LevelManager.NextLevel();
        }
    
        if (LevelManager.levelFail)
        {
            //Clear Level
            foreach (Transform child in gridObject.transform) {
                Destroy(child.gameObject);
            }
            
            ScreenManager.instance.GetFailedScreen();
        }
    
        // if (LevelManager.levelEnd)
        // {
        //     currentLevelId += 1;
        //     if (currentLevelId > 12)
        //     {
        //         currentLevelId = 1;
        //     }
        //     foreach (Transform child in grid.transform) {
        //         Destroy(child.gameObject);
        //     }
        //     pathfinding = LevelManager.NextLevel(currentLevelId.ToString());
        //     InputManager.SetCameraPosition();
        // }
    }
    

}
