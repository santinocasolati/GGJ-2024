
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private List<GameObject> prefabs;
    [SerializeField] private GameObject bossPrefab;
    private Martillo player;
    private int torretaCounter = 3;
    private const float spawnInterval = 5f;
    private float parachuteInterval = 6f;
    private float timeSinceNoEnemies = 6f;
    private int waveNumber = 0;

    private bool waveCleared = true;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Martillo>();
        StartCoroutine(SpawnCarpinchoParacaidistaRoutine());
    }

    void Update()
    {
        if (player == null)
        {
            StopCoroutine(SpawnCarpinchoParacaidistaRoutine());
            Destroy(this.gameObject);
            return;
        }
        if (timeSinceNoEnemies > spawnInterval && waveNumber <= 3)
        {
            parachuteInterval--;

            for (int i = 0; i < prefabs.Count - 1; i++)
            {
                GameObject prefabToSpawn = prefabs[i];

                for (int j = 0; j < torretaCounter; j++)
                    SpawnEnemy(prefabToSpawn);

                timeSinceNoEnemies = 0.0f;
                torretaCounter += 3;
                waveCleared = false;
            }
        }

        if (GameObject.FindGameObjectWithTag("Enemy") == null)
        {
            if (!waveCleared)
            {
                waveCleared = true;
                GameManager gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();

                waveNumber++;
                if (waveNumber >= 4)
                {
                    gameManager.Boss();
                    parachuteInterval = 5f;
                    
                }
                else
                {
                    gameManager.ChangeArea(waveNumber);
                }
            }
            timeSinceNoEnemies += Time.deltaTime;
        }
    }

    IEnumerator SpawnCarpinchoParacaidistaRoutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(parachuteInterval);
            SpawnEnemy(prefabs.Find(prefab => prefab.name == "CarpinchoParachute"));
        }
    }

    void SpawnEnemy(GameObject enemyPrefab)
    {
        Vector3 playerPos = player.transform.position;
        Vector3 spawnPos = playerPos;

        if (!(enemyPrefab.name == "CarpinchoParachute"))
        {
            if (Random.Range(0, 2) == 0)
                spawnPos.x += Camera.main.pixelWidth / 45;
            else
                spawnPos.x -= Camera.main.pixelWidth / 45;
        }
        else
        {
            float radius = 3f;
            float angle = Random.Range(0f, 360f);

            spawnPos.x = playerPos.x + radius * Mathf.Cos(angle * Mathf.Deg2Rad);
            spawnPos.z = playerPos.z + radius * Mathf.Sin(angle * Mathf.Deg2Rad);
            spawnPos.y = 5f;
        }

        Instantiate(enemyPrefab, spawnPos, Quaternion.Euler(new Vector3(0, 180, 0)), GameObject.Find("Enemies").transform);
    }
}
