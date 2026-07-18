using UnityEngine;
using UnityEngine.InputSystem;

public class InteractibleNpc : MonoBehaviour, IInteractible
{
    private WeaponManager _weaponManager;

    private Health playerHealth;

    [SerializeField] private UpgradeMenu upgradeMenu;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent(out PlayerInteraction playerInteraction))
        {
            playerInteraction.SetInteractible(this);
            _weaponManager = playerInteraction.GetComponentInParent<WeaponManager>();
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.TryGetComponent(out PlayerInteraction playerInteraction))
        {
            playerInteraction.SetInteractible(null);
            _weaponManager = null;
        }
    }

    public void Interact(GameObject player)
    {
        upgradeMenu.Open(_weaponManager);

        Health playerHealth = player.GetComponent<Health>();

        if (playerHealth != null)
        {
            playerHealth.InteractHeal(1);
        }
    }
}