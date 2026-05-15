using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInteraction : MonoBehaviour
{
    private IInteractible interactible;
    [SerializeField] private GameObject player;

    public void SetInteractible(IInteractible newInteractible)
    {
        interactible = newInteractible;
    }

    public void OnInteract(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            if (interactible != null)
            {
                interactible.Interact(player);
            }
        }
    }
}