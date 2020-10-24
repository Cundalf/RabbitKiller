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
        _weponAnimator.SetTrigger("Shoot");
        Instantiate(BulletGO[0], transform.position, transform.rotation);
        Instantiate(ShootPS, transform.position, transform.rotation);
    }
}
