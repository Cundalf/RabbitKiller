using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public float timeReload;
    public int chargerCapacity;
    public float rateOfFire;
    public GameObject bullet;
    public ShootController shootController;
    public Animator weaponAnimator;
    public string nombre { get; set; }
    public int ammoInCharger { get; set; }

    private float reloadTime;
    private float rateOfFireTime;

    void Start()
    {
        ammoInCharger = chargerCapacity;
        rateOfFireTime = 0;
    }

    void Update() 
    {
        if (ammoInCharger == 0)
        {
            reload();
        }
        else
        {
            if (rateOfFireTime > 0)
            {
                rateOfFireTime -= Time.deltaTime;
            }
        }
    }

    public void reload() 
    {
        reloadTime += Time.deltaTime;
        if (reloadTime >= timeReload)
        {
            ammoInCharger = chargerCapacity;
            reloadTime = 0;
            rateOfFireTime = 0;
            SFXManager.SharedInstance.PlaySFX(SFXType.SoundType.RELOAD);
        }
    }

    public void shoot() 
    {
        if (ammoInCharger != 0)
        {
            weaponAnimator.SetTrigger("Shooting");

            if (rateOfFireTime <= 0)
            {
                shootController.Shoot(bullet);
                SFXManager.SharedInstance.PlaySFX(SFXType.SoundType.FIRE);
                ammoInCharger--;
                rateOfFireTime = rateOfFire;
            }
        }
    }

    public void makeVisible() 
    {
        gameObject.SetActive(true);
    }

    public void makeInvisible() 
    {
        gameObject.SetActive(false);
    }
}
