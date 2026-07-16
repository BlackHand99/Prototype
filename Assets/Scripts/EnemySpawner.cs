using UnityEngine;
using System.Collections;

public abstract class EnemySpawner : MonoBehaviour
{
    [Header("Enemy Settings")]
    [SerializeField] protected GameObject[] enemyPrefabs;

    [Header("Spawn Settings")]
    [SerializeField] protected float spawnInterval = 3f;

    protected virtual void Start()
    {
        StartCoroutine(SpawnLoop());
    }

    private IEnumerator SpawnLoop()
    {
        while (true)
        {
            yield return new WaitForSeconds(spawnInterval);

            SpawnEnemy();
        }
    }

    protected abstract void SpawnEnemy();
}

