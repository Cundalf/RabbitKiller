using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField]
    private float rateOfFire;
    [SerializeField]
    private int chargerCapacity;
    [SerializeField]
    private float timeReload;
    [SerializeField]
    private string weaponName;
    [SerializeField]
    private GameObject bullet;

    private Animator _animator;
    private float reloadTime;
    private float rateOfFireTime;
    private int ammoInCharger;
    private int ammoBonus;
    
    public GameObject WeaponBullet
    {
        get
        {
            return bullet;
        }
    }

    public int AmmoBonus
    {
        get
        {
            return ammoBonus;
        }
        set
        {
            if (value < 0)
            {
                ammoBonus = 0;
            }
            else
            {
                ammoBonus = value;
            }
        }
    }

    public string WeaponName
    {
        get
        {
            return weaponName;
        }
    }

    void Start()
    {
        ammoInCharger = chargerCapacity;
        rateOfFireTime = 0;
        ammoBonus = 0;
        _animator = GetComponent<Animator>();
    }

    void Update() 
    {
        if (ammoInCharger == 0 && ammoBonus == 0)
        {
            //* Recarga automatica temporal
            if(Reload())
                SFXManager.SharedInstance.PlaySFX(SFXType.SoundType.RELOAD);
        }
        else
        {
            if (rateOfFireTime > 0f)
            {
                rateOfFireTime -= Time.deltaTime;
            }
        }
    }

    public bool Reload() 
    {
        reloadTime += Time.deltaTime;
        if (reloadTime >= timeReload)
        {
            ammoInCharger = chargerCapacity;
            reloadTime = 0f;
            rateOfFireTime = 0f;
            return true;
        }

        return false;
    }

    public bool Shoot() 
    {
        if (ammoInCharger != 0 && rateOfFireTime <= 0)
        {
            if (ammoBonus > 0)
            {
                ammoBonus--;
            }
            else
            {
                ammoInCharger--;
            }

            _animator.SetTrigger("Shooting");
            rateOfFireTime = rateOfFire;
            return true;
        }

        return false;
    }
}
