using UnityEngine;

public class FlyingEnemySpawner : EnemySpawner
{
    [Header("Target Line")]
    [SerializeField] private float lineWidth = 10f;
    [SerializeField] private float lineYOffset = -6f;
    [SerializeField] private float lineXOffset = 2f;

    public bool SpawnerEnabled;
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

    }

    Vector3 GetRandomLinePoint()
    {
        Vector3 lineCenter =
            transform.position +
            Vector3.up * lineYOffset +
            Vector3.right * lineXOffset;

        float randomX =
            Random.Range(-lineWidth / 2f, lineWidth / 2f);

        return lineCenter + Vector3.right * randomX;
    }

    // Draw line in Scene view
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.cyan;

        Vector3 lineCenter =
            transform.position + Vector3.up * lineYOffset + Vector3.right * lineXOffset;

        Vector3 left =
            lineCenter + Vector3.left * (lineWidth / 2f);

        Vector3 right =
            lineCenter + Vector3.right * (lineWidth / 2f);

        Gizmos.DrawLine(left, right);
    }

}
