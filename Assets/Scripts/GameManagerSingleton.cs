using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class GameManagerSingleton : MonoBehaviour
{
    public static GameManagerSingleton Instance { get; private set; }

    public float PerformanceScore;
    public int ConsecutiveNoHitRooms;
    private Transform playerTransform;
    private Vector3 playerPosition;
    private void Awake()
    {
        //prevent duplicate 
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;

        //persist across scenes
        DontDestroyOnLoad(gameObject);
    }

    public event Action<Vector3> OnPlayerPositionChanged;

    public void SetPlayerPosition(Vector3 pos)
    {
        playerPosition = pos;
        OnPlayerPositionChanged?.Invoke(pos);
    }

    public Vector3 GetPlayerPosition() => playerPosition;
    public void SetPlayer(Transform player)
    {
        playerTransform = player;
    }

    public Transform GetPlayer()
    {
        return playerTransform;
    }
}


