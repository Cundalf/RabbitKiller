using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class ShootController : MonoBehaviour
{
    public GameObject BulletGO;
    public GameObject ShootPS;
    Animator _wepon;

    void Start()
    {
        _wepon = GameObject.Find("Wepon").GetComponent<Animator>();
    }
    public void Shoot()
    {
        _wepon.SetTrigger("Trigger");
        Instantiate(BulletGO, transform.position, transform.rotation);
        Instantiate(ShootPS, transform.position, transform.rotation);
    }
}
