using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStartRespawn : MonoBehaviour
{
    public GameObject EnemyPrefab;
    public int cantRespawn;

    private List<GameObject> respawnPoints;
    private List<bool> userdRespawn;

    void Start()
    {
        if (cantRespawn == 0) return;

        userdRespawn = new List<bool>();
        respawnPoints = new List<GameObject>();
        foreach (Transform t in transform)
        {
            respawnPoints.Add(t.gameObject);
        }

        if (cantRespawn > respawnPoints.Count)
            cantRespawn = respawnPoints.Count;

        for (int i = 0; i < respawnPoints.Count; i++)
        {
            userdRespawn.Add(false);
        }

        int iRespawnPoint = 0;
        for (int i=0; i<cantRespawn; i++)
        {
            iRespawnPoint = Random.Range(0, respawnPoints.Count);

            if (userdRespawn[iRespawnPoint])
            {
                i--;
            }
            else
            {
                GameObject respawnPointGO = respawnPoints[iRespawnPoint];

                Instantiate(EnemyPrefab, respawnPointGO.transform.position, respawnPointGO.transform.rotation);

                userdRespawn[iRespawnPoint] = true;
            }
        }
    }
}
