using UnityEngine;

public class TutorialNpc : MonoBehaviour, IInteractible
{
    private WeaponManager _weaponManager;

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
        WeaponManager weaponManager = player.GetComponent<WeaponManager>();
        weaponManager.UnlockPistol();
    }
}