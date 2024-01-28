using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private List<GameObject> prefabs;
    [SerializeField] private GameObject bossPrefab;
    //[SerializeField] TextMeshProUGUI waveText;
    private Martillo player;
    private int torretaCounter = 3;
    private int paracaidasCounter = 1;
    private const float spawnInterval = 5f;
    private float timeSinceNoEnemies = 6f;
    private int waveNumber = 0;

    bool waveCleared = true;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Martillo>();
        //waveText.text = $"Wave {waveNumber}/4";
    }
    void Update()
    {
        if (timeSinceNoEnemies > spawnInterval && waveNumber < 3)
        {
            for (int i = 0; i < prefabs.Count; i++)
            {
                GameObject prefabToSpawn = prefabs[i];

                for (int j = 0; j < torretaCounter; j++)
                    SpawnEnemy(prefabToSpawn);

                /*for (int k = 0; k < paracaidasCounter; k++)
                    SpawnEnemy(prefabToSpawn);*/
                /*for (int l = 0; l < skaterCounter; l++)
                    SpawnEnemy(prefabToSpawn);*/
                timeSinceNoEnemies = 0.0f;
                waveCleared = false;
                torretaCounter++;
                paracaidasCounter++;
                waveNumber++;
                //waveText.text = $"Wave {waveNumber}/4";
            }
        }
        /*else if (timeSinceNoEnemies > spawnInterval && waveNumber == 3)
        {
            SpawnEnemy(bossPrefab);
            waveCleared = false;
            timeSinceNoEnemies = 0.0f;
            waveNumber++;
            //waveText.text = $"Wave {waveNumber}/4";
            waveNumber = 0;
        }*/
        if (GameObject.FindGameObjectWithTag("Enemy") == null)
            timeSinceNoEnemies += Time.deltaTime;

    }

    void SpawnEnemy(GameObject enemyPrefab)
    {
        Vector3 playerPos = this.player.transform.position;
        Vector3 spawnPos = playerPos; 

        if (Random.Range(0, 2) == 0)
            spawnPos.x += Camera.main.pixelWidth / 45;
        else
            spawnPos.x -= Camera.main.pixelWidth / 45; 

        Instantiate(enemyPrefab, spawnPos, Quaternion.identity, GameObject.Find("Enemies").transform);
    }


}
