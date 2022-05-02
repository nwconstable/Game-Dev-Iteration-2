using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Spawner : MonoBehaviour
{
    public static int enemiesAlive = 0;
    public Wave[] waves;
    public TMP_Text waveCountText;
    public float timeToSpawn = 5f;
    private float currentTimeToSpawn = 2f;
    private int waveCounter = 0;
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

        waveCountText.text = "Wave: " + (waveCounter + 1);
    }

    IEnumerator SpawnWave() 
    {
        Wave wave = waves[waveCounter];
        if(waveCounter < numberOfWaves)
        {
            for (int i = 0; i < wave.count; i++)
            {
                SpawnEnemy(wave.enemy);
                yield return new WaitForSeconds(1f / wave.rate);
            }
            yield return new WaitForSeconds(10f);
        }
        waveCounter++;

        if (waveCounter == waves.Length + 1)
        {
            //LoadingScreen.LoadScene("MainMenu"); //i want to put a victory scene here eventually 
            Debug.Log("You Win");
            this.enabled = false;
        }
    }

    void SpawnEnemy(GameObject enemy)
    {
        Instantiate(enemy, transform.position, transform.rotation);
        enemiesAlive++;
    }


}
