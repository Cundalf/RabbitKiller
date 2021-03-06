﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Bullet : MonoBehaviour
{
    public float velocity;
    Rigidbody _rb;
    
    void Start()
    {
        _rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        _rb.velocity = transform.forward * velocity;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Ground") || other.gameObject.CompareTag("Pasture"))
        {
            Destroy(gameObject);
        }
        else if(other.gameObject.CompareTag("Enemy")) 
        {
            other.gameObject.GetComponent<EnemyController>().Die();
            Destroy(gameObject);
        }
    }
}
