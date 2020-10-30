using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wepon : MonoBehaviour
{
    public float timeReload;
    public int chargerCapacity;
    public float rateOfFire;
    public GameObject bullet;
    public ShootController shootController;
    public string nombre { get; set; }
    
    private float time;
    private int ammoInCharger;

    void Start()
    {
        rateOfFire = 10;
        this.ammoInCharger = this.chargerCapacity;
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
            if (rateOfFire == 0)
            {
                shootController.Shoot(this.bullet);
                ammoInCharger--;
                SFXManager.SharedInstance.PlaySFX(SFXType.SoundType.FIRE);
                rateOfFire = 10;
            }
            else
            {
                rateOfFire--;
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
