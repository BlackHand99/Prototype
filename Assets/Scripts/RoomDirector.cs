using System.Collections;
using UnityEngine;

public class RoomDirector : MonoBehaviour
{
    [Header("Room")]
    [SerializeField] private Collider2D leftWall;
    [SerializeField] private Collider2D rightWall;

    [Header("Spawners")]
    [SerializeField] private EnemySpawner[] spawners;

    [Header("Encounter")]
    [SerializeField] private float spawnDuration = 20f;

    private int enemiesAlive;

    private bool roomActive;
    private bool encounterFinished;
    private bool spawningFinished;

    public float TimeRemaining { get; private set; }

    public bool TimerRunning { get; private set; }

    public static RoomDirector ActiveRoom { get; private set; }

    private void Awake()
    {
        leftWall.enabled = false;
        rightWall.enabled = true;
    }

    public void ActivateRoom()
    {
        if (roomActive)
            return;

        ActiveRoom = this;

        roomActive = true;

        leftWall.enabled = true;

        foreach (EnemySpawner spawner in spawners)
        {
            spawner.EnableSpawner();
        }

        StartCoroutine(SpawnTimer());
    }

    private IEnumerator SpawnTimer()
    {
        TimerRunning = true;
        TimeRemaining = spawnDuration;

        while (TimeRemaining > 0f)
        {
            TimeRemaining -= Time.deltaTime;
            yield return null;
        }

        TimeRemaining = 0f;
        TimerRunning = false;

        spawningFinished = true;

        foreach (EnemySpawner spawner in spawners)
        {
            spawner.DisableSpawner();
        }

        // End the encounter immediately
        EncounterComplete();
    }

    public void RegisterSpawn()
    {
        enemiesAlive++;
    }

    public void RegisterDeath()
    {
        enemiesAlive--;
        Debug.Log($"Enemies Alive: {enemiesAlive}");
    }

    private void EncounterComplete()
    {
        if (encounterFinished)
            return;

        encounterFinished = true;

        foreach (EnemySpawner spawner in spawners)
        {
            spawner.DisableSpawner();
        }

        EnemyHealth[] remainingEnemies = FindObjectsByType<EnemyHealth>(
            FindObjectsSortMode.None
        );

        foreach (EnemyHealth enemy in remainingEnemies)
        {
            if (enemy.RoomDirector == this)
            {
                Destroy(enemy.gameObject);
            }
        }

        enemiesAlive = 0;

        roomActive = false;
        rightWall.enabled = false;

        Debug.Log("Encounter Complete");
    }

    public void ResetEncounter()
    {
        enemiesAlive = 0;

        roomActive = false;
        encounterFinished = false;
        spawningFinished = false;

        TimerRunning = false;
        TimeRemaining = spawnDuration;

        leftWall.enabled = false;
        rightWall.enabled = true;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player"))
            return;

        if (roomActive)
            return;

        ActivateRoom();
    }
}