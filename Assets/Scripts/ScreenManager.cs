using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScreenManager : MonoBehaviour
{
    public static ScreenManager instance;
    public TMP_Text wrongMoveCountText;
    public GameObject levelFailedScreen;

    public void Awake()
    {
        instance = this;
    }

    void Start()
    {
        levelFailedScreen.SetActive(false);
    }
    

    public void GetFailedScreen()
    {
        levelFailedScreen.SetActive(true);
    }
    
    public void TryAgainButton()
    {
        wrongMoveCountText.color = Color.white;
        levelFailedScreen.SetActive(false);
        Testing.pathfinding = LevelManager.NextLevel();
        // LevelManager.NextLevel();
    }
}
