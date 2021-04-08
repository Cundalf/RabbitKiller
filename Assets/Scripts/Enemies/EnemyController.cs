using System.Collections;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
[RequireComponent(typeof(Animator))]

public class EnemyController : MonoBehaviour
{
    public float timeStop;
    public float timeInAir;
    public int healt;
    private float time;
    public EnemyRespawnController enemyRespawnController;
    public bool isMoving;

    public Transform playerT;
    public NavMeshAgent agent;
    public Animator _Anim;
    
    public ParticleSystem BloodPS;
    public Vector3 bloodPSPoint;

    public void Awake()
    {
        enemyRespawnController = FindObjectOfType<EnemyRespawnController>();
        playerT = FindObjectOfType<PlayerController>().transform;
        agent = GetComponent<NavMeshAgent>();
    }

    public virtual void Start()
    {
        time = 0;
        isMoving = false;
        enemyRespawnController = FindObjectOfType<EnemyRespawnController>();
        playerT = FindObjectOfType<PlayerController>().transform;
        agent = GetComponent<NavMeshAgent>();
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
        yield return new WaitForSeconds(timeInAir);
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
        bloodPSPoint = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y + 3, gameObject.transform.position.z);
        SFXManager.SharedInstance.PlaySFX(SFXType.SoundType.RABBIT_DEATH);

        Instantiate(BloodPS, bloodPSPoint, gameObject.transform.rotation);
        enemyRespawnController.enemiDead();
        Destroy(gameObject);
    }

}
