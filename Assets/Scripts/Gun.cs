using UnityEngine;
using UnityEngine.InputSystem;

public class Gun : MonoBehaviour
{
    [SerializeField] private Transform gunPivot;

    private Camera cam;

    private void Awake()
    {
        cam = Camera.main;
    }

    private void Update()
    {
        Vector3 MousePos = cam.ScreenToWorldPoint(Mouse.current.position.ReadValue());
        MousePos.z = 0;
        gunPivot.transform.right = MousePos - gunPivot.position;
    }
}