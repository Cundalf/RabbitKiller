using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
[RequireComponent(typeof(Animator))]
public class EnemyController : MonoBehaviour
{
    public ParticleSystem BloodPS;

    Transform playerT;
    NavMeshAgent agent;
    Animator _Anim;

    void Start()
    {
        playerT = FindObjectOfType<PlayerController>().transform;
        agent = GetComponent<NavMeshAgent>();
        _Anim = GetComponent<Animator>();
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            agent.destination = playerT.position;
            _Anim.SetBool("Jump", true);

            StartCoroutine(MoveToPlayer());
        }
    }

    IEnumerator MoveToPlayer()
    {
        Debug.Log("1");
        
        yield return new WaitForSeconds(0.5f);
        Debug.Log("2");
        _Anim.SetBool("Jump", false);
        agent.ResetPath();
     
        yield return null;
    }

    public void Die()
    {
        Instantiate(BloodPS, gameObject.transform.position, gameObject.transform.rotation);
        Destroy(gameObject);
        
        //TODO: Notificar al GameMananger o UIManager
    }
}
