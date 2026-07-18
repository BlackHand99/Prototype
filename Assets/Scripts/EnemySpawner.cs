using UnityEngine;
using System.Collections;

public abstract class EnemySpawner : MonoBehaviour
{
    [SerializeField] protected GameObject[] enemyPrefabs;
    [SerializeField] protected float spawnInterval = 3f;
    [SerializeField] protected RoomDirector roomDirector;

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
            yield return new WaitForSeconds(spawnInterval);

            if (!spawnerEnabled)
                yield break;

            SpawnEnemy();
        }
    }

    protected abstract void SpawnEnemy();
}