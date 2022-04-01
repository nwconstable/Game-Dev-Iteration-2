using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject enemyObjectToSpawn;

    public float timeToSpawn;
    private float currentTimeToSpawn;

    public int maxEnemyCount = 1000;
    private int enemyCount;

    void Start()
    {

    }

    void Update()
    {
        if (currentTimeToSpawn > 0)
        {
            currentTimeToSpawn -= Time.deltaTime;
        }
        else
        {
            if(enemyCount < maxEnemyCount)
            {
                Instantiate(enemyObjectToSpawn, transform.position, transform.rotation);
                currentTimeToSpawn = timeToSpawn;
                enemyCount++;
            }
            
        }

    }
}
