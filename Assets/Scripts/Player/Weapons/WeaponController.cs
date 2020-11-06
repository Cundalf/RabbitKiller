using System.Collections.Generic;
using UnityEngine;

public class WeaponController : MonoBehaviour
{
    public List<Weapon> weaponInInventori;
    public Weapon currentWeapon;
    public Weapon innerWeapon;
    public void changeWepon(string weaponName)
    {
        foreach (Weapon weapon in this.weaponInInventori)
        {
            if (weapon.nombre == weaponName)
            {
                this.currentWeapon.makeInvisible();
                this.innerWeapon = this.currentWeapon;
                this.currentWeapon = weapon;
                this.currentWeapon.makeVisible();
            }
        }
    }

    public void purpleBushBust() 
    {
        this.currentWeapon.ammoInCharger = 10;
    }

    public void addWepon(Weapon weaponToAdd) 
    {
        this.weaponInInventori.Add(weaponToAdd);
    }

    public void quickChangeOfWeapon()
    {
        if (innerWeapon != null)
        {
            Weapon current = this.currentWeapon;
            this.currentWeapon.makeInvisible();
            this.currentWeapon = this.innerWeapon;
            this.currentWeapon.makeVisible();
            this.innerWeapon = current;
        }
    }

    public void shoot() 
    {
        this.currentWeapon.shoot();
    }

    public void reload() 
    {
        this.currentWeapon.reload();
    }

}

