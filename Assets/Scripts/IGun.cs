using UnityEngine;
using UnityEngine.InputSystem;

public interface IGun
{
    public void TryFire(InputAction.CallbackContext context);
}