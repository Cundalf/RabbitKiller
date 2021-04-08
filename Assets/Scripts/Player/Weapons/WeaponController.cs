using System.Collections.Generic;
using UnityEngine;

public class WeaponController : MonoBehaviour
{
    public List<Weapon> weaponInInventori;
    public Weapon currentWeapon;
    public Weapon innerWeapon;

    public void changeWepon(string weaponName)
    {
        foreach (Weapon weapon in weaponInInventori)
        {
            if (weapon.nombre == weaponName)
            {
                currentWeapon.makeInvisible();
                innerWeapon = currentWeapon;
                currentWeapon = weapon;
                currentWeapon.makeVisible();
            }
        }
    }

    public void purpleBushBust() 
    {
        currentWeapon.ammoInCharger = 10;
    }

    public void addWepon(Weapon weaponToAdd) 
    {
        weaponInInventori.Add(weaponToAdd);
    }

    public void quickChangeOfWeapon()
    {
        if (innerWeapon != null)
        {
            Weapon current = currentWeapon;
            currentWeapon.makeInvisible();
            currentWeapon = innerWeapon;
            currentWeapon.makeVisible();
            innerWeapon = current;
        }
    }

    public void shoot() 
    {
        currentWeapon.shoot();
    }

    public void reload() 
    {
        currentWeapon.reload();
    }

}

