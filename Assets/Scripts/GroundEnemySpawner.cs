using UnityEngine;

public class GroundEnemySpawner : EnemySpawner
{
    [SerializeField] private Transform[] spawnPoints;

    public bool SpawnerEnabled;
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

#if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        if (spawnPoints == null)
            return;

        Gizmos.color = Color.green;

        foreach (Transform point in spawnPoints)
        {
            if (point == null)
                continue;

            Gizmos.DrawSphere(point.position, 0.2f);
        }
    }
#endif
}