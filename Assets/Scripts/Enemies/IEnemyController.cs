using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public interface IEnemyController
{
    ParticleSystem BloodPS { get; set; }
    float timeStop { get; set; }
    int healt { get; set; }
    float time { get; set; }
    EnemyRespawnController enemyRespawnController { get; set; }
    bool isMoving { get; set; }

    Transform playerT { get; set; }
    NavMeshAgent agent { get; set; }
    Animator _Anim { get; set; }
    Vector3 bloodPSPoint { get; set; }
    void movePNJ();
    void Start();
    void Update();
}