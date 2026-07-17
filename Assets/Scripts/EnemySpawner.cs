using UnityEngine;
using System.Collections;

public abstract class EnemySpawner : MonoBehaviour
{
    [SerializeField] protected GameObject[] enemyPrefabs;
    [SerializeField] protected float spawnInterval = 3f;

    private bool spawnActive;
    private bool spawnerEnabled;

    public void EnableSpawner()
    {
        if (spawnerEnabled)
            return;

        spawnerEnabled = true;
        StartCoroutine(SpawnTimer());
    }

    public void DisableSpawner()
    {
        spawnerEnabled = false;
        StopAllCoroutines();
    }

    private IEnumerator SpawnTimer()
    {
        while (spawnerEnabled)
        {
            spawnActive = false;

            yield return new WaitForSeconds(spawnInterval);

            spawnActive = true;

            SpawnEnemy();
        }
    }

    protected abstract void SpawnEnemy();
}

