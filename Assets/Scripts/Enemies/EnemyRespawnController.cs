using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Timeline;

public class EnemyRespawnController : MonoBehaviour
{
    public GameObject EnemyPrefab;
    public float minStopTime = 2f;
    public float maxStopTime = 5f;

    private float timeCounter = 0f;
    private float timeStop = 0f;

    private List<GameObject> respawnPoints;

    void Start()
    {
        timeStop = Random.Range(minStopTime, maxStopTime);
        respawnPoints = new List<GameObject>();
        foreach (Transform t in transform)
        {
            respawnPoints.Add(t.gameObject);
        }
    }

    void Update()
    {
        timeCounter += Time.deltaTime;

        if (timeCounter < timeStop) return;

        timeCounter = 0;
        timeStop = Random.Range(minStopTime, maxStopTime);
        int iRespawnPoint = Random.Range(0, respawnPoints.Count);
        GameObject respawnPointGO = respawnPoints[iRespawnPoint];

        Instantiate(EnemyPrefab, respawnPointGO.transform.position, respawnPointGO.transform.rotation);
    }
}
