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
    public float timeStop = 1f;
    public int healt = 1;
    private float time;
    private UIManager uiManager;
    private bool isMoving = false;
    
    Transform playerT;
    NavMeshAgent agent;
    Animator _Anim;

    void Start()
    {
        uiManager = FindObjectOfType<UIManager>();
        playerT = FindObjectOfType<PlayerController>().transform;
        agent = GetComponent<NavMeshAgent>();
        _Anim = GetComponent<Animator>();
    }
    void Update()
    {
        if (GameManager.SharedInstance.actualGameState != GameManager.GameState.IN_GAME) return;

        time += Time.deltaTime;

        if (time >= timeStop && !isMoving)
        {
            isMoving = true;
            agent.destination = playerT.position;
            _Anim.SetBool("Jump", true);
            StartCoroutine(StopMoving());
        }
    }

    IEnumerator StopMoving()
    {   
        yield return new WaitForSeconds(0.5f);
        agent.ResetPath();
        _Anim.SetBool("Jump", false);
        time = 0;
        isMoving = false;
        yield return null;
    }

    public void Die()
    {
        SFXManager.SharedInstance.PlaySFX(SFXType.SoundType.RABBIT_DEATH);
       
        Instantiate(BloodPS, gameObject.transform.position, gameObject.transform.rotation);
        Destroy(gameObject);
        uiManager.PointsControl();
    }

}
