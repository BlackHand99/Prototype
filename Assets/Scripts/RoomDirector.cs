using UnityEngine;

public class RoomDirector : MonoBehaviour
{
    [SerializeField] private EnemySpawner[] spawners;
    [SerializeField] private int enemiesToSpawn = 25;
    public void ActivateRoom()
    {
        foreach (EnemySpawner spawner in spawners)
        {
            spawner.EnableSpawner();
        }
    }

    public void DeactivateRoom()
    {
        foreach (EnemySpawner spawner in spawners)
        {
            spawner.DisableSpawner();
        }
    }
}
