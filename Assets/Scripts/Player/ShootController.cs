using System.Collections;
using System.Collections.Generic;
using System.Security.Policy;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class ShootController : MonoBehaviour
{
    public GameObject ShootPS;
    public Animator _weponAnimator;

    public void Shoot(GameObject bullet, string animation)
    {
        _weponAnimator.SetTrigger(animation);
        Instantiate(bullet, transform.position, transform.rotation);
        Instantiate(ShootPS, transform.position, transform.rotation);
        
    }

}
