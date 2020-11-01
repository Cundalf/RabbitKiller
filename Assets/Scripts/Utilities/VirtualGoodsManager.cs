using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VirtualGoodsManager : MonoBehaviour
{
    private int rabbitFeet;
    public int RabbitFeet
    {
        get
        {
            return rabbitFeet;
        }
    }

    private static VirtualGoodsManager sharedInstance = null;

    public static VirtualGoodsManager SharedInstance
    {
        get
        {
            return sharedInstance;
        }
    }

    public List<GameObject> audios;
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

    private void Start()
    {
        if(PlayerPrefs.HasKey("RabbitFeet"))
        {
            rabbitFeet = PlayerPrefs.GetInt("RabbitFeet");
        }
    }

    public void AddRabbitFeet(int cant)
    {
        if (cant <= 0) return;
        rabbitFeet += cant;
        PlayerPrefs.SetInt("RabbitFeet", rabbitFeet);
    }

    private void UpdateUI()
    {
        if(GameManager.SharedInstance.actualGameState == GameManager.GameState.MAIN_MENU)
        {
            GetComponent<MainMenuManager>().UpdateRabbitFeet(rabbitFeet);
        }

        if (GameManager.SharedInstance.actualGameState == GameManager.GameState.IN_GAME)
        {
            GetComponent<MainMenuManager>().UpdateRabbitFeet(rabbitFeet);
        }
    }
}
