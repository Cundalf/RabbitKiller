using System.Collections.Generic;
using UnityEngine;

public class WeaponController : MonoBehaviour
{
    [Tooltip("Indice de la arma activa que se encuentra en weaponInInventory")]
    [SerializeField]
    private int initialWeapon;
    [Tooltip("Indice de la arma secundaria que se encuentra en weaponInInventory")]
    [SerializeField]
    private int initialLastWeapon;

    private Weapon _currentWeapon;
    private Weapon _lastWeapon;

    [SerializeField]
    private List<Weapon> weaponInInventory;
    [SerializeField]
    private ShootController shootController;

    public Weapon currentWeapon
    {
        get 
        {
            return _currentWeapon;
        }
    }

    private void Start()
    {
        if (weaponInInventory.Count == 0)
        {
            Debug.LogWarning("El player no tiene ningun arma en el inventario");
        }
        else
        {
            _currentWeapon = weaponInInventory[initialWeapon];
            _lastWeapon = weaponInInventory[initialLastWeapon];
        }
    }

    public void changeWepon(string weaponName)
    {
        foreach (Weapon weapon in weaponInInventory)
        {
            if (weapon.WeaponName == weaponName)
            {
                _currentWeapon.gameObject.SetActive(false);
                _lastWeapon = _currentWeapon;
                _currentWeapon = weapon;
                _currentWeapon.gameObject.SetActive(true);
            }
        }
    }

    public void purpleBushBust() 
    {
        _currentWeapon.AmmoBonus = 10;
    }

    public void addWepon(Weapon weaponToAdd) 
    {
        weaponInInventory.Add(weaponToAdd);
    }

    public void quickChangeOfWeapon()
    {
        if (_lastWeapon != null)
        {
            Weapon current = _currentWeapon;
            _currentWeapon.gameObject.SetActive(false);
            _currentWeapon = _lastWeapon;
            _currentWeapon.gameObject.SetActive(true);
            _lastWeapon = current;
        }
    }

    public void shoot() 
    {
        bool shootStatus = _currentWeapon.Shoot();
        if (shootStatus)
        {
            shootController.MakeShoot(_currentWeapon.WeaponBullet);
            SFXManager.SharedInstance.PlaySFX(SFXType.SoundType.FIRE);
        }
    }

    public void reload() 
    {
        bool reloadStatus = _currentWeapon.Reload();

        if(reloadStatus)
            SFXManager.SharedInstance.PlaySFX(SFXType.SoundType.RELOAD);
    }

}

