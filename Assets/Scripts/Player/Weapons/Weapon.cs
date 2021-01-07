using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public float timeReload;
    public int chargerCapacity;
    public int rateOfFire;
    public GameObject bullet;
    public ShootController shootController;
    public Animator weaponAnimator;
    public string nombre { get; set; }
    
    private float time;
    public int ammoInCharger { get; set; }

    void Start()
    {
        this.ammoInCharger = this.chargerCapacity;
        this.weaponAnimator = GetComponent<Animator>();
    }

    void Update() 
    {
        if (this.ammoInCharger == 0)
        {
            reload();
        }
    }

    public void reload() 
    {
        this.time += Time.deltaTime;
        if (this.time >= this.timeReload)
        {
            this.ammoInCharger = this.chargerCapacity;
            this.time = 0;
            SFXManager.SharedInstance.PlaySFX(SFXType.SoundType.RELOAD);
            rateOfFire = 0;
        }
    }

    public void shoot() 
    {
        if (this.ammoInCharger != 0)
        {
            this.weaponAnimator.SetTrigger("Shooting");
            if (rateOfFire == 0)
            {
                shootController.Shoot(this.bullet);
                ammoInCharger--;
                SFXManager.SharedInstance.PlaySFX(SFXType.SoundType.FIRE);
                rateOfFire = 10;
            }
            else 
            {
                this.rateOfFire--;
            }
            this.weaponAnimator.SetTrigger("Shooting");
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
