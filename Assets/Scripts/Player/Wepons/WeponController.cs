using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class WeponController : MonoBehaviour
{
    public List<Wepon> weponInInventori;
    public Wepon currentWepon;
    public Wepon innerWepon;

    public void changeWepon(string weponName)
    {
        foreach (Wepon wepon in this.weponInInventori)
        {
            if (wepon.nombre == weponName)
            {
                this.currentWepon.makeInvisible();
                this.innerWepon = this.currentWepon;
                this.currentWepon = wepon;
                this.currentWepon.makeVisible();
            }
        }
    }

    public void addWepon(Wepon weponToAdd) 
    {
        this.weponInInventori.Add(weponToAdd);
    }

    public void quickChangeOfWeapon()
    {
        if (innerWepon != null)
        {
            Wepon current = this.currentWepon;
            this.currentWepon.makeInvisible();
            this.currentWepon = this.innerWepon;
            this.currentWepon.makeVisible();
            this.innerWepon = current;
        }
    }

    public void shoot() 
    {
        this.currentWepon.shoot();
    }

    public void reload() 
    {
        this.currentWepon.reload();
    }

}

