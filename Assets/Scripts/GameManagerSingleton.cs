using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// To use: access with SingletonSimple.Instance


public class GameManagerSingleton : MonoBehaviour
{
    // This is really the only blurb of code you need to implement a Unity singleton
    private static GameManagerSingleton _Instance;
    public static GameManagerSingleton Instance
    {
        get
        {
            if (!_Instance)
            {
                _Instance = new GameObject().AddComponent<GameManagerSingleton>();
                // name it for easy recognition
                _Instance.name = _Instance.GetType().ToString();
                // mark root as DontDestroyOnLoad();
                DontDestroyOnLoad(_Instance.gameObject);
            }
            return _Instance;
        }
    }

    private Vector3 playerPosition;

    public event Action<Vector3> OnPlayerPositionChanged;

    public void SetPlayerPosition(Vector3 pos)
    {
        playerPosition = pos;
        OnPlayerPositionChanged?.Invoke(pos);
    }

    public Vector3 GetPlayerPosition() => playerPosition;
}


    // implement your Awake, Start, Update, or other methods here...

