using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    // Control de estados del juego.
    public enum GameState
    {
        MAIN_MENU,
        IN_GAME,
        PAUSE,
        GAME_OVER,
        STARTING_GAME
    }

    private GameState actualGameState;
    public GameState ActualGameState 
    { 
        get
        {
            return actualGameState;
        }
    }

    // Para controlar los modos de juego
    public enum GameMode
    {
        CAMPAIGN,
        HORDE,
        CLASSIC
    }

    // Para controlar las escenas de una manera practica.
    public enum GameScenes
    {
        MAIN_MENU = 0,
        STAGE1 = 1,
        STAGE2 = 2,
        STAGE3 = 3,
    }

    // Rotaciones de mapa
    // Para controlar por que mapa va la rotacion
    private int currentSceneInRotation = 0;
    [SerializeField]
    private List<MapRotationSO> mapRotations;
    [SerializeField]
    private int currentMapRotationIdx;
    private MapRotationSO currentMapRotation;

    // Player actual
    public GameObject actualPlayer;

    // Cursor in game
    public Texture2D InGameCursor;

    // Singleton
    private static GameManager _sharedInstance = null;

    public static GameManager sharedInstance
    {
        get
        {
            return _sharedInstance;
        }
    }
    
    private void Awake()
    {
        if (_sharedInstance != null && _sharedInstance != this)
        {
            Destroy(gameObject);
            return;
        }

        _sharedInstance = this;
        DontDestroyOnLoad(this);
    }

    private void Start()
    {
        if (mapRotations.Count == 0)
        {
            Debug.LogWarning("No se cargo ninguna rotacion de mapa");
        }
        else
        {
            currentMapRotation = mapRotations[currentMapRotationIdx];
        }
    }

    public void changeGameState(GameState newGameState)
    {
        actualGameState = newGameState;

        switch(actualGameState)
        {
            case GameState.MAIN_MENU:
                VirtualGoodsManager.SharedInstance.UpdateUI();
                break;
            case GameState.IN_GAME:
                Cursor.SetCursor(InGameCursor, Vector2.zero, CursorMode.Auto);
                break;
            case GameState.GAME_OVER:
                Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
                break;
        }
    }

    //TODO: Pasar a Virtual Goods.
    public int GetCantRabbitFeet(int rabbitDead)
    {
        int cant = 0;
        for(int i = 1; i <= rabbitDead; i++)
        {
            cant += Random.Range(0, 3);
        }

        return cant;
    }

    public void changeMap() 
    {
        if (currentSceneInRotation <= mapRotations.Count)
        {
            currentSceneInRotation++;
            SceneManager.LoadScene((int) currentMapRotation.scenes[currentSceneInRotation]);
            changeGameState(GameState.STARTING_GAME);
        }
    }



    public void pauseGame()
    {
        actualGameState = GameState.PAUSE;
        Time.timeScale = 0;
    }

    public void resumeGame()
    {
        actualGameState = GameState.IN_GAME;
        Time.timeScale = 1;
    }

    public void exitGame()
    {
        Application.Quit();
    }

    //? estos test van a necesitar rework?
    public void setCurrentMap(int numberCurrenMap)
    {
        currentSceneInRotation = numberCurrenMap;
    }

    public string getNameCurrentScene() 
    {
        return SceneManager.GetActiveScene().name;
    }

    public Scene getActiveScene() 
    {
        return SceneManager.GetActiveScene();
    }

}
