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

    private List<SO_Product> virtualGoodsPurchased;
    private SO_Product activeCharacter;
    private SO_Product activeMunition;
    private SO_Product activeWeapon;

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

    public void BuyProduct(SO_Product product)
    {

    }
}
