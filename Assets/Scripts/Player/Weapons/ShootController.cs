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
