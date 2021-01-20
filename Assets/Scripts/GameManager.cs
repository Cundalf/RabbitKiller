using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    public enum GameState
    {
        MAIN_MENU, IN_GAME, PAUSE, GAME_OVER
    }

    [SerializeField]
    private string[] configuredMap;
    private int currentMap = 0;
    private GameState actualGameState;
    public GameState ActualGameState { 
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

    public void nexMap() 
    {
        currentMap++;
        SceneManager.LoadScene(this.configuredMap[currentMap]);
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
}
