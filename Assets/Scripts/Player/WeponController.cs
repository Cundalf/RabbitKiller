using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.UIElements;

public class WeponController : MonoBehaviour
{
    [SerializeField]
    public string weponInUse;
    public ShootController shootController;

    public UIManager uiManager;
    private Animator _anim;
    private float time;
    private int ammo;
    private int charger;
    private int timeReload;

    // Start is called before the first frame update
    void Start()
    {
        //uiManager = FindObjectOfType<UIManager>();
        _anim = GetComponent<Animator>();
        changeWepon(weponInUse);
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
            shoot();
        }
    }
    public void changeWepon(string wepon) 
    {
        switch (wepon)
        {
            case ("ShootGun"):
                charger = 2;
                timeReload = 3;
                break;
            case ("MachineGun"):
                charger = 10;
                timeReload = 2;
                break;
            default:
                return;
        }
        ammo = charger;
    }

    public void reloadWepon(int ammoInCharge, int timeReload)
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

    public void shoot()
    {
        //if (!_anim.GetCurrentAnimatorStateInfo(0).IsName("PlayerIdle")) return;
        
        shootController.Shoot();
        ammo--;
        uiManager.BulletsControl(ammo);
        SFXManager.SharedInstance.PlaySFX(SFXType.SoundType.FIRE);
    }


}
