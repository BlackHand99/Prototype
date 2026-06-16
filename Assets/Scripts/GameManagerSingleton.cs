using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class GameManagerSingleton : MonoBehaviour
{
    public static GameManagerSingleton Instance { get; private set; }

    public float PerformanceScore;
    public int ConsecutiveNoHitRooms;
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

    private Vector3 playerPosition;

    public event Action<Vector3> OnPlayerPositionChanged;

    public void SetPlayerPosition(Vector3 pos)
    {
        playerPosition = pos;
        OnPlayerPositionChanged?.Invoke(pos);
    }

    public Vector3 GetPlayerPosition() => playerPosition;


    //enemy global aggro
    public bool GlobalAlert { get; private set; }

    public void TriggerGlobalAlert()
    {
        GlobalAlert = true;
    }

    public void ResetGlobalAlert()
    {
        GlobalAlert = false;
    }
}


