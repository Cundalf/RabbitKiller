using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRespawnController : MonoBehaviour
{
    public List<GameObject> EnemyPrefab;
    public List<GameObject> EnemyBossPrefab;
    public float minStopTime;
    public float maxStopTime;

    private float timeCounter = 0f;
    private float timeStop = 0f;

    //Orde variable
    private int enemisInOrde;
    private int enemisSapwn;
    private int enemisDead;
    private int enemisCounter;
    private int currentOrdeNumber;
    private int DELAYNEWORDE = 5;
    private int ordeNumberBoss;
    private bool SPAWNEARBOSS = false;
    private UIManager currentUI;
    private List<GameObject> respawnPoints;

    void Start()
    {
        timeStop            = Random.Range(minStopTime, maxStopTime);
        respawnPoints       = new List<GameObject>();
        currentUI           = FindObjectOfType<UIManager>();
        enemisInOrde        = 20;
        enemisSapwn         = 0;
        enemisDead          = 0;
        enemisCounter       = 20;
        currentOrdeNumber   = 1;
        ordeNumberBoss      = 5;
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
        currentUI.updateOrdeInfo(enemisCounter, currentOrdeNumber);
    }

    void ordeControl() 
    {
        if (enemisSapwn <= enemisInOrde)
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
        if (enemisDead == enemisInOrde)
        {
            newOrde();
        }
    }

    void spawnEnemis() 
    {
        GameObject respawnPointGO;
        int iRespawnPoint = Random.Range(0, respawnPoints.Count);
        int iRespawnPoint2 = Random.Range(0, respawnPoints.Count);
        respawnPointGO = respawnPoints[iRespawnPoint];

        InstantiateEnemis(respawnPointGO.transform,EnemyPrefab);

        if (iRespawnPoint != iRespawnPoint2)
        {
            respawnPointGO = respawnPoints[iRespawnPoint2];

            InstantiateEnemis(respawnPointGO.transform, EnemyPrefab);
        }
        
    }

    void spawnEnemiBoss() 
    {
        GameObject respawnPointGO;
        int iRespawnPoint = Random.Range(0, respawnPoints.Count);
        respawnPointGO = respawnPoints[iRespawnPoint];

        InstantiateEnemis(respawnPointGO.transform, EnemyBossPrefab);
    }

    void InstantiateEnemis(Transform respawnTransform, List<GameObject> EnemyPrefab)
    {
        if (enemisSapwn ==enemisInOrde ) return;
        enemisSapwn++;
        Instantiate(EnemyPrefab[Random.Range(0, 3)], respawnTransform.position, respawnTransform.rotation);

        int randomSFX = Random.Range(0, 2);
        if (randomSFX == 0) SFXManager.SharedInstance.PlaySFX(SFXType.SoundType.RABBIT_RESPAWN);
        if (randomSFX == 1) SFXManager.SharedInstance.PlaySFX(SFXType.SoundType.RABBIT_RESPAWN_ALT);
    }
    void newOrde()
    {
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
        enemisCounter = enemisInOrde;
        enemisDead = 0;
        enemisSapwn = 0;
    }

    public void enemiDead() 
    {
        enemisDead++;
        enemisCounter--;
        currentUI.PointsControl();
    }

    IEnumerator delayForNewOrde()
    {
        yield return new WaitForSeconds(DELAYNEWORDE);
    }

}
