using UnityEngine;

public class GameManagerSingleton : MonoBehaviour
{
    public static GameManagerSingleton Instance { get; private set; }

    [SerializeField] private Transform player;

    public bool GlobalAlert { get; private set; }

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
    }

    public Vector2 GetPlayerPosition()
    {
        if (player == null)
            return Vector2.zero;

        return player.position;
    }

    public Transform GetPlayerTransform()
    {
        return player;
    }

    public void TriggerGlobalAlert()
    {
        GlobalAlert = true;
    }

    public void ResetGlobalAlert()
    {
        GlobalAlert = false;
    }
}