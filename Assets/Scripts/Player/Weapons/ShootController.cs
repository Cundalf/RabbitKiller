using System.Collections;
using System.Collections.Generic;
using System.Security.Policy;
using UnityEngine;

public class ShootController : MonoBehaviour
{
    public GameObject ShootPS;

    public void Shoot(GameObject bullet)
    {
        Instantiate(bullet, transform.position, transform.rotation);
        Instantiate(ShootPS, transform.position, transform.rotation);
    }
}
