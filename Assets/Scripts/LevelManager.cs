using System;
using System.Collections;
using System.Collections.Generic;
using CodeMonkey.Utils;
using Unity.VisualScripting.Antlr3.Runtime;
using UnityEngine;
using Object = UnityEngine.Object;


public class LevelManager
{
    public static LevelData myLevel;
    public static int currentLevelId = 1;
    public static string levelFailId;
    public static bool levelEnd;
    public static bool levelFail;
    public static int carCounter = 0;
    public static int wrongMoveCount = 0;
    
    [System.Serializable]
    public class LevelData
    {
        public string id;
        public int height;
        public int width;

        public Car[] cars;
        public int carCount;
    }
    
    [System.Serializable]
    public class Car
    {
        public int startX;
        public int startY;
        public int targetX;
        public int targetY;
        public string carDirection;
        public int color;
    }

    
    public static void SetLevel(string levelName)
    {
        CarMovement.SetCarColors();
        TextAsset textJSON = (TextAsset)Resources.Load(String.Format("Levels/{0}", levelName), typeof(TextAsset));
        myLevel = JsonUtility.FromJson<LevelData>(textJSON.text);
        myLevel.carCount = myLevel.cars.Length;
    }
    
    public static Pathfinding NextLevel()
    {
        levelEnd = false;
        levelFail = false;
        carCounter = 0;
        wrongMoveCount = 0;
        //ScreenManager.instance.wrongMoveCountText.text = "Wrong Move: " + wrongMoveCount;
        SetLevel("level"+currentLevelId);
        return new Pathfinding(myLevel.width, myLevel.height);
    }
    
    public static void UpdateLevel()
    {
        currentLevelId += 1;
        if (currentLevelId > 12)
        {
            currentLevelId = 1;
        }
    }
    
    public static void SelectLevel(int selectedLevel)
    {
        if (selectedLevel == -1)
        {
            currentLevelId = selectedLevel;
        }
    }
    
    public static void FinishLevel(GameObject gridObject)
    {
        foreach (Transform child in gridObject.transform) {
            Object.Destroy(child.gameObject);
        }
    }
    
    public static void UpdateCarCounter()
    {
        carCounter += 1;
        if (carCounter == myLevel.carCount)
        {
            levelEnd = true;
        }
    }
    
    public static void UpdateWrongMoveCount()
    {
        wrongMoveCount += 1;
        //ScreenManager.instance.wrongMoveCountText.text = "Wrong Move: " + wrongMoveCount;
        CameraManager.instance.StartShake();
        if (wrongMoveCount > 2)
        {
            //ScreenManager.instance.wrongMoveCountText.color = Color.red;
            levelFail = true;
            levelFailId = myLevel.id;
        }
    }

    

}
