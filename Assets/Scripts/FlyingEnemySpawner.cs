using UnityEngine;
using System.Collections;

public class FlyingEnemySpawner : EnemySpawner
{
    [Header("Target Line")]
    [SerializeField] private float lineWidth = 10f;
    [SerializeField] private float lineYOffset = -6f;
    [SerializeField] private float lineXOffset = 2f;

    protected override void SpawnEnemy()
    {
        if (enemyPrefabs.Length == 0)
            return;

        int enemyIndex = Random.Range(0, enemyPrefabs.Length);

        Vector3 spawnPos = GetRandomLinePoint();

        GameObject enemy = Instantiate(
            enemyPrefabs[enemyIndex],
            spawnPos,
            Quaternion.identity
        );

        EnemyHealth enemyHealth = enemy.GetComponent<EnemyHealth>();

        if (enemyHealth != null)
        {
            enemyHealth.SetRoomDirector(roomDirector);
        }

        roomDirector.RegisterSpawn();
    }

    private Vector3 GetRandomLinePoint()
    {
        float x = transform.position.x + lineXOffset + Random.Range(-lineWidth * 0.5f, lineWidth * 0.5f);
        float y = transform.position.y + lineYOffset;

        return new Vector3(x, y, 0f);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.cyan;

        Vector3 left = new Vector3(
            transform.position.x + lineXOffset - lineWidth * 0.5f,
            transform.position.y + lineYOffset,
            0f);

        Vector3 right = new Vector3(
            transform.position.x + lineXOffset + lineWidth * 0.5f,
            transform.position.y + lineYOffset,
            0f);

        Gizmos.DrawLine(left, right);
    }
}