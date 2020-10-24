using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class ShootController : MonoBehaviour
{
    public List<GameObject> BulletGO;
    public GameObject ShootPS;
    public Animator _weponAnimator;
    public string typeOfWepon;

    public void Shoot()
    {
        switch (typeOfWepon) 
        {
            case ("ShootGun"):
                _weponAnimator.SetTrigger("ShootGun");
                Instantiate(BulletGO[0], transform.position, transform.rotation);
                Instantiate(ShootPS, transform.position, transform.rotation);
                break;
            case ("MachineGun"):
                _weponAnimator.SetTrigger("MachineGun");
                Instantiate(BulletGO[1], transform.position, transform.rotation);
                Instantiate(ShootPS, transform.position, transform.rotation);
                break;
            default:
                break;
        }
    }
}
