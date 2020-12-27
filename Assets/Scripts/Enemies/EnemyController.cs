using System.Collections;
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
    private EnemyRespawnController enemyRespawnController;
    private bool isMoving = false;
    
    Transform playerT;
    NavMeshAgent agent;
    Animator _Anim;

    public virtual void Start()
    {
        enemyRespawnController = FindObjectOfType<EnemyRespawnController>();
        playerT = FindObjectOfType<PlayerController>().transform;
        agent = GetComponent<NavMeshAgent>();
        _Anim = GetComponent<Animator>();
    }

    void Update()
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

    public void Die()
    {   
        Vector3 bloodPSPoint = new Vector3(gameObject.transform.position.x,gameObject.transform.position.y+3,gameObject.transform.position.z);
        SFXManager.SharedInstance.PlaySFX(SFXType.SoundType.RABBIT_DEATH);
        
        Instantiate(BloodPS,bloodPSPoint, gameObject.transform.rotation);
        Destroy(gameObject);
        enemyRespawnController.enemiDead();
    }

}
