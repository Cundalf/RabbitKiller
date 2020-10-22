using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootController : MonoBehaviour
{
    public GameObject BulletGO;
    public GameObject ShootPS;
    public void Shoot()
    {
        Instantiate(BulletGO, transform.position, transform.rotation);
        Instantiate(ShootPS, transform.position, transform.rotation);
    }
}
