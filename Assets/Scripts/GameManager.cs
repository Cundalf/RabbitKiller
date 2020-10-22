﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public enum GameState
    {
        MAIN_MENU, IN_GAME, PAUSE
    }
    
    private GameState actualGameState;
    public GameState GetGameState
    {
        get
        {
            return actualGameState;
        }
    }

    // Singleton
    private static GameManager sharedInstance = null;

    public static GameManager SharedInstance
    {
        get
        {
            return sharedInstance;
        }
    }

    private void Awake()
    {
        if (sharedInstance != null && sharedInstance != this)
        {
            Destroy(gameObject);
        }

        sharedInstance = this;
        DontDestroyOnLoad(this);
    }

    public void PauseGame()
    {
        Debug.Log("Juego pausado");
        actualGameState = GameState.PAUSE;
        Time.timeScale = 0;
    }

    public void ResumeGame()
    {
        Debug.Log("Juego Renaudado");
        actualGameState = GameState.IN_GAME;
        Time.timeScale = 1;
    }

}
