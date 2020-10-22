using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(Rigidbody))]
public class EnemyController : MonoBehaviour
{
    public float moveForce = 5f;
    public float jumpForce = 5f;
    Rigidbody _rb;
    public Vector3 jump = new Vector3(0.0f, 2.0f, 0.0f);
    public Vector3 direction = new Vector3(0.0f, 2.0f, 0.0f);

    //public GameObject Ears;
    //public GameObject EarsComplete;

    public ParticleSystem BloodPS;

    public Transform goal;
    NavMeshAgent agent;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        //agent.destination = goal.position;
        _rb = GetComponent<Rigidbody>();
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            /*agent.speed = 100f;
            agent.acceleration = 100f;

            _rb.AddRelativeForce(jump * jumpForce, ForceMode.Impulse);
            _rb.AddRelativeForce(direction * moveForce, ForceMode.Impulse);
            agent.speed = 0;*/
            //agent.speed = 5f;
            //agent.acceleration = 8f;
            agent.destination = goal.position;
            StartCoroutine(MoveToPlayer());
            
            //agent.speed = 0;
        }
    }

    IEnumerator MoveToPlayer()
    {
        Debug.Log("1");
        
        yield return new WaitForSeconds(1);
        Debug.Log("2");
        agent.ResetPath();
        
        _rb.AddRelativeForce(jump * jumpForce, ForceMode.Impulse);
        _rb.AddRelativeForce(direction * moveForce, ForceMode.Impulse);
        yield return null;
    }

    public void Die()
    {
        Instantiate(BloodPS, gameObject.transform.position, gameObject.transform.rotation);
        Destroy(gameObject);
        
        //TODO: Notificar al GameMananger o UIManager
    }
}
