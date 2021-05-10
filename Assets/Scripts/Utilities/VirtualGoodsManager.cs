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

    private const string healthPackID = "C01";
    private const string premiumAmmoID = "C00";

    // Cantidad de curaciones
    private int healthPack;
    // Cantidad de municion premium
    private int premiumAmmo;

    // Skins compradas
    private List<string> skinsPurchased;
    
    // Armas activas
    private List<string> weaponsPurchased;

    private string activeCharacterID;
    private string activeWeaponID;

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
        LoadData();
    }

    private void LoadData()
    {
        if (PlayerPrefs.HasKey("RabbitFeet"))
        {
            rabbitFeet = PlayerPrefs.GetInt("RabbitFeet");
        }
        else
        {
            rabbitFeet = 0;
        }

        if (PlayerPrefs.HasKey("HealthPackPurchased"))
        {
            healthPack = PlayerPrefs.GetInt("HealthPackPurchased");
        }
        else
        {
            healthPack = 0;
        }

        if (PlayerPrefs.HasKey("PremiumAmmoPurchased"))
        {
            premiumAmmo = PlayerPrefs.GetInt("PremiumAmmoPurchased");
        }
        else
        {
            premiumAmmo = 0;
        }
    }

    private void SaveData()
    {
        PlayerPrefs.SetInt("RabbitFeet", rabbitFeet);
        PlayerPrefs.SetInt("HealthPackPurchased", healthPack);
        PlayerPrefs.SetInt("PremiumAmmoPurchased", premiumAmmo);
    }

    public void AddRabbitFeet(int cant)
    {
        if (GameManager.sharedInstance.ActualGameState == GameManager.GameState.IN_GAME) return;
        if (cant <= 0) return;
        rabbitFeet += cant;
        SaveData();
    }

    public void UpdateUI()
    {
        if(GameManager.sharedInstance.ActualGameState == GameManager.GameState.MAIN_MENU)
        {
            GetComponent<MainMenuManager>().UpdateRabbitFeet(rabbitFeet);
        }
    }

    public void BuyProduct(SO_Product product)
    {
        switch(product.category)
        {
            case SO_Category.CategoriesShop.CONSUMABLE:
                AddConsumable(product);
                break;
            case SO_Category.CategoriesShop.SKINS:
                AddSkin(product);
                break;
            case SO_Category.CategoriesShop.WEAPONS:
                AddWeapon(product);
                break;
            default:
                Debug.LogError("Incorrect product category");
                break;
        }
    }

    private void AddConsumable(SO_Product consProduct)
    {
        switch(consProduct.id)
        {
            case healthPackID:
                healthPack++;
                break;
            case premiumAmmoID:
                premiumAmmo++;
                break;
            default:
                Debug.LogError("Incorrect consumable ID");
                break;
        }

        SaveData();
    }

    private void AddSkin(SO_Product skinProduct)
    {
        string id = skinProduct.id;

        foreach (string str in skinsPurchased)
        {
            if (id.Contains(str))
            {
                Debug.LogError($"The skin {id} has already been purchased");
                return;
            }
        }

        skinsPurchased.Add(id);
        SaveData();
    }

    private void AddWeapon(SO_Product weapProduct)
    {
        string id = weapProduct.id;

        foreach (string str in weaponsPurchased)
        {
            if (id.Contains(str))
            {
                Debug.LogError($"The weapon {id} has already been purchased");
                return;
            }
        }

        weaponsPurchased.Add(id);
        SaveData();
    }

}
