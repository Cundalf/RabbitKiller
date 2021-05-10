using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRespawnController : MonoBehaviour
{
    //? Separamos la logica en StageController - EnemyRespawnController?

    public List<GameObject> EnemyPrefab;
    public GameObject EnemyBossPrefab;

    //TODO: Automatizar
    [SerializeField]
    private int initialRabbitCount;
    [SerializeField]
    private float minStopTime;
    [SerializeField]
    private float maxStopTime;
    [SerializeField]
    [Tooltip("En que horda cambia de mapa")]
    private int hordeChangeMap = 0;
    [SerializeField]
    private float newHordeDelay = 1.0f;
    [SerializeField]
    private bool stageWithBoss = false;
    [SerializeField]
    [Tooltip("Si stageWithBoss es verdadero, indicar en que ronda inicia el boss")]
    private int hordeNumberBoss = 0;
    [SerializeField]
    private int cantRabbitsForHorde = 10;

    private float timeStop = 0.0f;
    private int enemiesInHorde;
    private int currentHordeNumber;
    private bool spawnBoss = false;
    private float timeCounter = 0.0f;
    private int enemiesDead = 0;
    private int enemiesSpawn = 0;
    private bool bossStillAlive = false;

    private UIManager currentUI;
    private List<GameObject> respawnPoints;
    public GameObject respawnPointBoss;

    void Start()
    {
        timeStop = Random.Range(minStopTime, maxStopTime);
        respawnPoints = new List<GameObject>();
        currentUI = FindObjectOfType<UIManager>();

        foreach (Transform t in transform)
        {
           respawnPoints.Add(t.gameObject);
        }

        enemiesInHorde = initialRabbitCount;
    }

    void Update()
    {
        if (GameManager.sharedInstance.ActualGameState != GameManager.GameState.IN_GAME) return;
        timeCounter += Time.deltaTime;

        if (timeCounter < timeStop) 
            return;

        timeCounter = 0;
        timeStop = Random.Range(minStopTime, maxStopTime);
        hordeControl();
    }

    public void hordeControl() 
    {
        if (enemiesInHorde <= enemiesDead && !bossStillAlive)
        {
            Invoke("newHorde", newHordeDelay);
        }
        else
        {
            if (enemiesSpawn < enemiesInHorde)
            {
                if (spawnBoss)
                {
                    spawnEnemyBoss();
                }
                else
                {
                    spawnEnemies();
                }
            }
        }
    }

    void spawnEnemies() 
    {
        GameObject respawnPointGO;
        int iRespawnPoint = Random.Range(0, respawnPoints.Count);
        int iRespawnPoint2 = Random.Range(0, respawnPoints.Count);
        respawnPointGO = respawnPoints[iRespawnPoint];

        InstantiateEnemies(respawnPointGO.transform,EnemyPrefab);

        if (iRespawnPoint != iRespawnPoint2 && enemiesSpawn <= enemiesInHorde-1)
        {
            respawnPointGO = respawnPoints[iRespawnPoint2];

            InstantiateEnemies(respawnPointGO.transform, EnemyPrefab);
        }
        
    }
    void spawnEnemyBoss() 
    {
        Debug.Log("Instanciando boss");
  
        Instantiate(EnemyBossPrefab, respawnPointBoss.transform.position, respawnPointBoss.transform.rotation);

        int randomSFX = Random.Range(0, 2);
        if (randomSFX == 0) SFXManager.SharedInstance.PlaySFX(SFXType.SoundType.RABBIT_RESPAWN);
        if (randomSFX == 1) SFXManager.SharedInstance.PlaySFX(SFXType.SoundType.RABBIT_RESPAWN_ALT);
        enemiesSpawn++;
        bossStillAlive = true;

    }

    void InstantiateEnemies(Transform respawnTransform, List<GameObject> EnemyPrefab)
    {
        Instantiate(EnemyPrefab[Random.Range(0, 3)], respawnTransform.position, respawnTransform.rotation);

        int randomSFX = Random.Range(0, 2);
        if (randomSFX == 0) SFXManager.SharedInstance.PlaySFX(SFXType.SoundType.RABBIT_RESPAWN);
        if (randomSFX == 1) SFXManager.SharedInstance.PlaySFX(SFXType.SoundType.RABBIT_RESPAWN_ALT);
        enemiesSpawn++;
    }

    void newHorde()
    {   
        currentHordeNumber++;
        if (hordeChangeMap == currentHordeNumber)
        {
            GameManager.sharedInstance.changeMap();
            return;
        }

        Debug.Log("Nueva horda");

        if (currentHordeNumber == hordeNumberBoss && stageWithBoss)
        {
            enemiesInHorde = 1;
            spawnBoss = true;
        }
        else 
        {
            enemiesInHorde = enemiesInHorde + cantRabbitsForHorde;
            spawnBoss = false;
        }
        
        enemiesDead  = 0;
        enemiesSpawn = 0;
        Debug.Log("Numero de horda actual: " + currentHordeNumber);
        Debug.Log("Horda de boss?: " + (spawnBoss ? "SI" : "NO"));
    }

    public void setCurrentHorde(int numberOrde) 
    {
        currentHordeNumber = numberOrde;
    }

    public void enemyDead() 
    {
        enemiesDead++;
        currentUI.PointsControl();
    }

    public void bossDead()
    {
        bossStillAlive = false;
    }

    public void setOrdeChangMap(int numberOrder)
    {
        hordeChangeMap = numberOrder;
    }
}
