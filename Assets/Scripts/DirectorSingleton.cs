using UnityEngine;

public class DirectorSingleton : MonoBehaviour
{
    public static DirectorSingleton Instance;

    //spawner selection
    [SerializeField] private Transform[] spawnPoints;

    [SerializeField] private GameObject meleeEnemy;
    [SerializeField] private GameObject shieldEnemy;
    [SerializeField] private GameObject rangedEnemy;
    [SerializeField] private GameObject flyingEnemy;

    public float PerformanceScore;
    public int NoHitRooms;

    public enum SpawnType
    {
        Ground,
        Flying
    }

    public class EnemySpawnPoint : MonoBehaviour
    {
        public SpawnType spawnType;
    }

    private void Awake()
    {
        Instance = this;
    }
}
