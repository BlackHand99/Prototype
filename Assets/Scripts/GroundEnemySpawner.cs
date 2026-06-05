using UnityEngine;

public class GroundEnemySpawner : EnemySpawner
{
    [SerializeField] private Transform[] spawnPoints;

    protected override void SpawnEnemy()
    {
        if (enemyPrefabs.Length == 0 || spawnPoints.Length == 0)
            return;

        int enemyIndex = Random.Range(0, enemyPrefabs.Length);
        int spawnIndex = Random.Range(0, spawnPoints.Length);

        Instantiate(
            enemyPrefabs[enemyIndex],
            spawnPoints[spawnIndex].position,
            spawnPoints[spawnIndex].rotation
        );
    }
}