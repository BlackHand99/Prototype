using UnityEngine;
using UnityEngine.InputSystem;

public class InteractibleNpc : MonoBehaviour, IInteractible
{
    //[SerializeField] PistolBullet otherScript;

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
        //Destroy(gameObject);
        //_weaponManager.pistol.FireRateUpgrade();
        //_weaponManager.pistolBullet.DamageUpgrade();
        _weaponManager.pistolBullet.BulletSpeedUpgrade();
    }
}