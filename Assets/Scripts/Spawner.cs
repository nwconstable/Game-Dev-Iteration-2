using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Spawner : MonoBehaviour
{
    public static int enemiesAlive = 0;
    public GameObject enemyObjectToSpawn;
    public TMP_Text waveCountText;
    public float timeToSpawn = 5f;
    private float currentTimeToSpawn = 2f;
    private float waveCounter = 0;
    private int numberOfWaves = 10;
    

    void Update()
    {
        if(enemiesAlive > 0)
        {
            return;
        }

        if (currentTimeToSpawn <= 0)
        {
            StartCoroutine(SpawnWave());
            currentTimeToSpawn = timeToSpawn;
            return;
        }
        currentTimeToSpawn -= Time.deltaTime;

        waveCountText.text = "Wave: " + waveCounter;
    }

    IEnumerator SpawnWave() 
    {
        if(waveCounter < numberOfWaves)
        {
            waveCounter++;
            for (int i = 0; i < waveCounter; i++)
            {
                SpawnEnemy();
                yield return new WaitForSeconds(0.5f);
            }
            yield return new WaitForSeconds(10f);
        }
    }

    void SpawnEnemy()
    {
        Instantiate(enemyObjectToSpawn, transform.position, transform.rotation);
        enemiesAlive++;
    }


}
