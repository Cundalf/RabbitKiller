using System.Collections;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    public enum GameState
    {
        MAIN_MENU, IN_GAME, PAUSE, GAME_OVER, STARTING_GAME
    }

    //? No va el Stage2?
    public string[] scenasConfig = new string[] { "Stage1", "Stage3" };

    //FIXME: No se esta usando para nada, Cual era el proposito?
    bool sceneLoaded;
   
    [SerializeField]
    public int nextSceneConfig = 0;
    private GameState actualGameState;
    public GameState ActualGameState 
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
            return;
        }

        sharedInstance = this;
        DontDestroyOnLoad(this);
    }

    public void ChangeGameState(GameState newGameState)
    {
        actualGameState = newGameState;

        switch(actualGameState)
        {
            case GameState.MAIN_MENU:
                VirtualGoodsManager.SharedInstance.UpdateUI();
                break;
        }
    }

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
        if (nextSceneConfig < scenasConfig.Length) 
        {
            SceneManager.LoadScene(scenasConfig[nextSceneConfig]);
            nextSceneConfig++;
            ChangeGameState(GameState.STARTING_GAME);
        }
    }

    public void PauseGame()
    {
        actualGameState = GameState.PAUSE;
        Time.timeScale = 0;
    }

    public void ResumeGame()
    {
        actualGameState = GameState.IN_GAME;
        Time.timeScale = 1;
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    public void setMaps(string[] maps) 
    {
        scenasConfig = maps;                   
    }

    public void setCurrentMap(int numberCurrenMap)
    {
        nextSceneConfig = numberCurrenMap;
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
