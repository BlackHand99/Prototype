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
        //gunPivot.transform.right = MousePos - gunPivot.position;
        Vector2 direction = MousePos - gunPivot.position;
        gunPivot.right = direction;

        if (direction.x < 0)
        {
            gunPivot.localScale = new Vector3(1, -1, 1);
        }
        else
        {
            gunPivot.localScale = Vector3.one;
        }
    }
}