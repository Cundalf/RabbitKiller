using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamagePlayer : MonoBehaviour
{
    public float timeStopDamage = 2f;
    private float timeDamage;

    [SerializeField]
    private bool canHit = true;

    void Update()
    {
        if (!canHit)
        {
            timeDamage += Time.deltaTime;
            if (timeDamage >= timeStopDamage)
            {
                canHit = true;
                timeDamage = 0;
            }
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Player") && canHit)
        {
            other.gameObject.GetComponent<PlayerController>().Hit();
            canHit = false;
        }
    }

    /*private void OnCollisionEnter(Collision collision)
    {
        Debug.Log(collision.gameObject.tag);
        if (collision.gameObject.CompareTag("Player") && canHit)
        {
            collision.gameObject.GetComponent<PlayerController>().Hit();
        }
    }*/
}
