using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class ShootController : MonoBehaviour
{
    public GameObject BulletGO;
    public GameObject ShootPS;
    public Animator _playeranim;

    public void Shoot()
    {
        _playeranim.SetTrigger("Shoot");
        Instantiate(BulletGO, transform.position, transform.rotation);
        Instantiate(ShootPS, transform.position, transform.rotation);
    }
}
