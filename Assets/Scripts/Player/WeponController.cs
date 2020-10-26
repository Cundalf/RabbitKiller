using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Security.Principal;
using UnityEngine;
using UnityEngine.UIElements;

public class WeponController : MonoBehaviour
{
    [SerializeField]
    
    public ShootController shootController;
    public List<GameObject> weponGO;
    public List<GameObject> weponAmmo;
    public List<string> weponInUse;
    public GameObject bullet;
    public int numberWepon;


    private Animator _anim;
    private float time;
    private float timeReload;
    private int ammo;
    private int charger;
 
    public UIManager uiManager;

    // Start is called before the first frame update
    void Start()
    {
        //uiManager = FindObjectOfType<UIManager>();
        _anim = GetComponent<Animator>();
        weponInUse = new List<string>() { "ShootGun", "MachineGun" };
        numberWepon = 0;
        changeWepon(weponInUse[numberWepon]);
    }

    // Update is called once per frame
    void Update()
    {
        if (ammo == 0)
        {
            reloadWepon(charger, timeReload);
        }
        else
        {
            if (!Input.GetMouseButtonUp((int)MouseButton.LeftMouse)) return;
            shoot(bullet, weponInUse[numberWepon]);
        }

        if (Input.GetKeyDown("q"))
        {
            if (numberWepon == 0) { numberWepon++; }
            else { numberWepon--; }

            UnityEngine.Debug.Log("Disparo");
            changeWepon(weponInUse[numberWepon]);
           
        }
    }
    public void changeWepon(string wepon) 
    {
        switch (wepon)
        {
            case ("ShootGun"):
                charger = 2;
                timeReload = 3;
                bullet = weponAmmo[0];
                weponGO[0].SetActive(true);
                weponGO[1].SetActive(false);
                break;

            case ("MachineGun"):
                charger = 10;
                timeReload = 2;
                bullet = weponAmmo[1];
                weponGO[0].SetActive(false);
                weponGO[1].SetActive(true);
                break;

            default:
                return;
        }
        ammo = charger;
    }

    public void reloadWepon(int ammoInCharge, float timeReload)
    {
        time += Time.deltaTime;
        if (time >= timeReload)
        {
            ammo = ammoInCharge;
            uiManager.BulletsControl(ammo);
            time = 0;
            SFXManager.SharedInstance.PlaySFX(SFXType.SoundType.RELOAD);
        }
    }

    public void shoot(GameObject bullet, string animation)
    {
        //if (!_anim.GetCurrentAnimatorStateInfo(0).IsName("PlayerIdle")) return;
        
        shootController.Shoot(bullet, animation);
        ammo--;
        uiManager.BulletsControl(ammo);
        SFXManager.SharedInstance.PlaySFX(SFXType.SoundType.FIRE);
    }


}
