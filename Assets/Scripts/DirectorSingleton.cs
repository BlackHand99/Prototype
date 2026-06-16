using UnityEngine;

public class DirectorSingleton : MonoBehaviour
{
    public static DirectorSingleton Instance;

    public float PerformanceScore;
    public int NoHitRooms;

    private void Awake()
    {
        Instance = this;
    }
}
