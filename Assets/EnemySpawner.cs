using System.Collections;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject[] enemyPrefabs; 
    public float spawnIntervalMin = 1f;
    public float spawnIntervalMax = 3f;

    public Transform[] spawnPoints;

    public int maxEnemies = 10; 
    private int currentEnemyCount = 0;


    void Start()
    {
        if (enemyPrefabs.Length == 0)
            Debug.LogError("Enemy Prefabs array is empty!");
        if (spawnPoints.Length == 0)
            Debug.LogError("Spawn Points array is empty!");

        StartCoroutine(SpawnEnemies());
    }

    IEnumerator SpawnEnemies()
    {
        while (true)
        {
            if (currentEnemyCount < maxEnemies)
            {
                SpawnRandomEnemy();
            }
            yield return new WaitForSeconds(Random.Range(spawnIntervalMin, spawnIntervalMax));
        }
    }

    void SpawnRandomEnemy()
    {
        if (enemyPrefabs.Length == 0 || spawnPoints.Length == 0) return;

        GameObject chosenEnemy = enemyPrefabs[Random.Range(0, enemyPrefabs.Length)];
        Transform chosenPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];

        GameObject newEnemy = Instantiate(chosenEnemy, chosenPoint.position, Quaternion.identity);

        currentEnemyCount++;

        CharacterStats stats = newEnemy.GetComponent<CharacterStats>();
        if (stats != null)
        {
            stats.OnEnemyDeath += () => currentEnemyCount--;
        }
    }
}
