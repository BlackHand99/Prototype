using UnityEngine;
using System.Collections;

public abstract class EnemySpawner : MonoBehaviour
{
    [Header("Spawn Settings")]
    [SerializeField] protected GameObject[] enemyPrefabs;
    [SerializeField] protected float spawnInterval = 3f;

    protected bool spawnActive;

    protected virtual void Awake()
    {
        StartCoroutine(SpawnTimer());
    }

    protected virtual IEnumerator SpawnTimer()
    {
        spawnActive = false;

        yield return new WaitForSeconds(spawnInterval);

        spawnActive = true;
    }

    protected virtual void Update()
    {
        if (!spawnActive)
            return;

        SpawnEnemy();

        StartCoroutine(SpawnTimer());
    }

    protected abstract void SpawnEnemy();
}
