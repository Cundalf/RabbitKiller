using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRespawnController : MonoBehaviour
{
    public List<GameObject> EnemyPrefab;
    public GameObject EnemyBossPrefab;
    public float minStopTime;
    public float maxStopTime;

    [SerializeField]
    private float timeCounter = 0f;
    private float timeStop = 0f;

    //Orde variable
    [SerializeField]
    private int ordeChangeMap;
    [SerializeField]
    private int enemisInOrde;
    [SerializeField]
    private int enemisSpawn;
    [SerializeField]
    private int enemisDead;
    [SerializeField]
    private int currentOrdeNumber;
    [SerializeField]
    private int DELAYNEWORDE = 1;
    [SerializeField]
    private int ordeNumberBoss;
    [SerializeField]
    private bool SPAWNEARBOSS = false;
    private bool BOSSSTILLALIVE = false;

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
    }

    void Update()
    {
        if (GameManager.SharedInstance.ActualGameState != GameManager.GameState.IN_GAME) return;
        timeCounter += Time.deltaTime;

        if (timeCounter < timeStop) return;

        timeCounter = 0;
        timeStop = Random.Range(minStopTime, maxStopTime);
        ordeControl();
    }

    public void ordeControl() 
    {
        if (this.ordeChangeMap == this.currentOrdeNumber)
        {
            GameManager.SharedInstance.changeMap();
        }
        else
        {
            if (enemisInOrde <= enemisDead && !BOSSSTILLALIVE)
            {
                newOrde();
            }
            else
            {
                if (enemisSpawn < enemisInOrde)
                {
                    if (SPAWNEARBOSS)
                    {
                        spawnEnemiBoss();
                    }
                    else
                    {
                        spawnEnemis();
                    }
                }
            }
        }
    }

    void spawnEnemis() 
    {
        GameObject respawnPointGO;
        int iRespawnPoint = Random.Range(0, respawnPoints.Count);
        int iRespawnPoint2 = Random.Range(0, respawnPoints.Count);
        respawnPointGO = respawnPoints[iRespawnPoint];

        InstantiateEnemis(respawnPointGO.transform,EnemyPrefab);

        if (iRespawnPoint != iRespawnPoint2 && enemisSpawn <= enemisInOrde-1)
        {
            respawnPointGO = respawnPoints[iRespawnPoint2];

            InstantiateEnemis(respawnPointGO.transform, EnemyPrefab);
        }
        
    }
    void spawnEnemiBoss() 
    {
        Debug.Log("Instanciando boss");
  
        Instantiate(EnemyBossPrefab, respawnPointBoss.transform.position, respawnPointBoss.transform.rotation);

        int randomSFX = Random.Range(0, 2);
        if (randomSFX == 0) SFXManager.SharedInstance.PlaySFX(SFXType.SoundType.RABBIT_RESPAWN);
        if (randomSFX == 1) SFXManager.SharedInstance.PlaySFX(SFXType.SoundType.RABBIT_RESPAWN_ALT);
        enemisSpawn++;
        BOSSSTILLALIVE = true;

    }
    void InstantiateEnemis(Transform respawnTransform, List<GameObject> EnemyPrefab)
    {
        Instantiate(EnemyPrefab[Random.Range(0, 3)], respawnTransform.position, respawnTransform.rotation);

        int randomSFX = Random.Range(0, 2);
        if (randomSFX == 0) SFXManager.SharedInstance.PlaySFX(SFXType.SoundType.RABBIT_RESPAWN);
        if (randomSFX == 1) SFXManager.SharedInstance.PlaySFX(SFXType.SoundType.RABBIT_RESPAWN_ALT);
        enemisSpawn++;
    }
    void newOrde()
    {
        Debug.Log("Nueva orda");
        StartCoroutine(delayForNewOrde());
        currentOrdeNumber++;
        if (currentOrdeNumber == ordeNumberBoss)
        {
            enemisInOrde = 1;
            SPAWNEARBOSS = true;
        }
        else 
        {
            enemisInOrde = enemisInOrde + 10;
            SPAWNEARBOSS = false;
        }
        enemisDead = 0;
        enemisSpawn = 0;
        Debug.Log("Numero de orda actual: " + currentOrdeNumber);
        Debug.Log("Tiene que spwanear boss?: " + SPAWNEARBOSS);
    }

    public void setCurrentOrde(int numberOrde) 
    {
        currentOrdeNumber = numberOrde;
    }

    public void enemiDead() 
    {
        enemisDead++;
        currentUI.PointsControl();
    }

    public void setBossStillAlive(bool value)
    {
        BOSSSTILLALIVE = value;
    }

    public void setOrdeChangMap(int numberOrder)
    {
        this.ordeChangeMap = numberOrder;
    }

    IEnumerator delayForNewOrde()
    {
        yield return new WaitForSeconds(DELAYNEWORDE);
    }

}
