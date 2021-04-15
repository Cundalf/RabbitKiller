using System.Collections.Generic;
using UnityEngine;

public class WeaponController : MonoBehaviour
{
    public Weapon currentWeapon;
    public Weapon innerWeapon;

    [SerializeField]
    private List<Weapon> weaponInInventory;
    [SerializeField]
    private ShootController shootController;

    public Weapon CurrentWeapon
    {
        get 
        {
            return currentWeapon;
        }
    }

    public void changeWepon(string weaponName)
    {
        foreach (Weapon weapon in weaponInInventory)
        {
            if (weapon.WeaponName == weaponName)
            {
                currentWeapon.gameObject.SetActive(false);
                innerWeapon = currentWeapon;
                currentWeapon = weapon;
                currentWeapon.gameObject.SetActive(true);
            }
        }
    }

    public void purpleBushBust() 
    {
        currentWeapon.AmmoBonus = 10;
    }

    public void addWepon(Weapon weaponToAdd) 
    {
        weaponInInventory.Add(weaponToAdd);
    }

    public void quickChangeOfWeapon()
    {
        if (innerWeapon != null)
        {
            Weapon current = currentWeapon;
            currentWeapon.gameObject.SetActive(false);
            currentWeapon = innerWeapon;
            currentWeapon.gameObject.SetActive(true);
            innerWeapon = current;
        }
    }

    public void shoot() 
    {
        bool shootStatus = currentWeapon.Shoot();
        if (shootStatus)
        {
            shootController.MakeShoot(currentWeapon.WeaponBullet);
            SFXManager.SharedInstance.PlaySFX(SFXType.SoundType.FIRE);
        }
    }

    public void reload() 
    {
        bool reloadStatus = currentWeapon.Reload();

        if(reloadStatus)
            SFXManager.SharedInstance.PlaySFX(SFXType.SoundType.RELOAD);
    }

}

