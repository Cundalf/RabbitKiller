using System.Collections;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
[RequireComponent(typeof(Animator))]

public class EnemyController : MonoBehaviour
{
    public float timeStop;
    public int healt;
    public float time;
    public EnemyRespawnController enemyRespawnController;
    public bool isMoving;

    public Transform playerT;
    public NavMeshAgent agent;
    public Animator _Anim;
    
    public ParticleSystem BloodPS;
    public Vector3 bloodPSPoint;

    public virtual void Start()
    {
        timeStop = 1f;
        time = 0;
        healt = 1;
        isMoving = false;
        enemyRespawnController = FindObjectOfType<EnemyRespawnController>();
        playerT = FindObjectOfType<PlayerController>().transform;
        agent = GetComponent<NavMeshAgent>();
        _Anim = GetComponent<Animator>();
    }

    public virtual void Update()
    {
        moveControl();
    }

    private void moveControl()
    {
        if (GameManager.SharedInstance.ActualGameState != GameManager.GameState.IN_GAME) return;

        time += Time.deltaTime;

        if (time >= timeStop && !isMoving)
        {
            movePNJ();
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

    public virtual void movePNJ()
    {
        isMoving = true;
        agent.destination = playerT.position;
        _Anim.SetBool("Jump", true);
        StartCoroutine(StopMoving());
    }

    public virtual void Die()
    {
        this.bloodPSPoint = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y + 3, gameObject.transform.position.z);
        SFXManager.SharedInstance.PlaySFX(SFXType.SoundType.RABBIT_DEATH);

        Instantiate(BloodPS, this.bloodPSPoint, gameObject.transform.rotation);
        enemyRespawnController.enemiDead();
        Destroy(gameObject);
    }

}
