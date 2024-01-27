using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private GameObject torretaPrefab;
    [SerializeField] private GameObject paracaidasPrefab;
    //[SerializeField] private GameObject bossPrefab;
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
            for (int i = 0; i < torretaCounter; i++)
            {
                SpawnEnemy(torretaPrefab);
            }
            /*for (int i = 0; i < paracaidasCounter; i++)
            {
                SpawnEnemy(paracaidasPrefab);
            }*/
            timeSinceNoEnemies = 0.0f;
            waveCleared = false;
            torretaCounter++;
            paracaidasCounter++;
            waveNumber++;
            //waveText.text = $"Wave {waveNumber}/4";
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

        Instantiate(enemyPrefab, spawnPos, Quaternion.identity);
    }


}
