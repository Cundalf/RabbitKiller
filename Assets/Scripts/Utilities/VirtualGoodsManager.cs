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

    public List<GameObject> WeaponsAvailable;
    public List<GameObject> PurchasedWeapons;

    public List<GameObject> SkinsAvailable;
    public List<GameObject> PurchasedSkins;

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
        if (GameManager.SharedInstance.ActualGameState == GameManager.GameState.IN_GAME) return;
        if (cant <= 0) return;
        rabbitFeet += cant;
        PlayerPrefs.SetInt("RabbitFeet", rabbitFeet);
    }

    public void UpdateUI()
    {
        if(GameManager.SharedInstance.ActualGameState == GameManager.GameState.MAIN_MENU)
        {
            GetComponent<MainMenuManager>().UpdateRabbitFeet(rabbitFeet);
        }
    }
}
