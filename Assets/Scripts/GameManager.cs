using System.Collections;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    public enum GameState
    {
        MAIN_MENU, IN_GAME, PAUSE, GAME_OVER
    }

<<<<<<< HEAD
    // TODO: No cargar desde disco
    public string[] scenasConfig = new string[] { "Assets/Scenes/Stage/Stage1.unity", "Assets/Scenes/Stage/Stage2.unity"};

    //FIXME: No se esta usando para nada, Cual era el proposito?
    bool sceneLoaded;
    
=======
    public string[] scenasConfig = new string[] { "Assets/Scenes/Stage/Stage1.unity", "Assets/Scenes/Stage/Stage3.unity" };
>>>>>>> mapa_trigal_2.0
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

    public void ChangeGameManager(GameState newGameState)
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
        if (this.nextSceneConfig < scenasConfig.Length) 
        {
            PauseGame();
            SceneManager.LoadScene(this.scenasConfig[this.nextSceneConfig]);
            nextSceneConfig++;
            ResumeGame();
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
        this.scenasConfig = maps;                   
    }

    public void setCurrentMap(int numberCurrenMap)
    {
        this.nextSceneConfig = numberCurrenMap;
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
