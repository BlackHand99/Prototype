using UnityEngine;
using System.Collections;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private GameObject[] enemyPrefabs;
    [SerializeField] private float spawnInterval = 3f;
    [SerializeField] private Transform[] spawnPoints;

    public bool spawnActive;

    private void Awake()
    {
        StartCoroutine(SpawnTimer());
    }
    public IEnumerator SpawnTimer()
    {
        spawnActive = false;
        yield return new WaitForSeconds(spawnInterval);
        spawnActive = true;
    }
    void Update()
    {

        if (spawnActive == true)
        {
            SpawnEnemy();
            StartCoroutine(SpawnTimer());
        }
    }

    void SpawnEnemy()
    {
        if (enemyPrefabs.Length == 0 || spawnPoints.Length == 0) return;

        int enemyIndex = Random.Range(0, enemyPrefabs.Length);
        int spawnIndex = Random.Range(0, spawnPoints.Length);

        GameObject enemyPrefab = enemyPrefabs[enemyIndex];
        Transform spawnPoint = spawnPoints[spawnIndex];

        Instantiate(enemyPrefab, spawnPoint.position, spawnPoint.rotation);
    }
}

