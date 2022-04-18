using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Spawner : MonoBehaviour
{
    public GameObject enemyObjectToSpawn;

    public float timeToSpawn = 5f;
    private float currentTimeToSpawn = 2f;

    private float waveCounter = 0;

    public TMP_Text waveCountText;

    void Start()
    {

    }

    void Update()
    {

        if (currentTimeToSpawn <= 0)
        {
            StartCoroutine(SpawnWave());
            currentTimeToSpawn = timeToSpawn;

        }
        currentTimeToSpawn -= Time.deltaTime;

        waveCountText.text = "Wave: " + waveCounter;
    }

    IEnumerator SpawnWave() 
    {
        if(waveCounter < 5)
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
    }


}
