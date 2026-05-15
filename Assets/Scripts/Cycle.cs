using UnityEngine;
using UnityEngine.InputSystem;

public class Cycle : MonoBehaviour
{
    public Pistol gun;

    public void OnInteract(InputAction.CallbackContext context)
    {
        if(context.performed)
        {
            gun.SetActive(true);
        }
    }
}