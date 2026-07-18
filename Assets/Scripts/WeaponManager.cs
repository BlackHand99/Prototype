using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class WeaponManager : MonoBehaviour
{

    [field:SerializeField] public Pistol flicker { get; private set; }
    [field: SerializeField] public Shotgun shotgun { get; private set; }
    [field: SerializeField] public SniperRifle railgun { get; private set; }
    private IGun currentGun;
    private List<GameObject> unlockedWeapons = new();
    private int currentWeaponIndex = 0;
    private GameObject nextWeapon;

    private void Start()
    {
        flicker.gameObject.SetActive(false);
        shotgun.gameObject.SetActive(false);
        railgun.gameObject.SetActive(false);
    }

    private void ActiveWeapon(GameObject weaponInHand)
    {
        flicker.gameObject.SetActive(false);
        shotgun.gameObject.SetActive(false);
        railgun.gameObject.SetActive(false);
        weaponInHand.SetActive(true);
        currentGun = weaponInHand.GetComponent<IGun>();
    }

    public void OnSwap(InputAction.CallbackContext context)
    {
        if (!context.performed || unlockedWeapons.Count == 0)
        {
            return;
        }

        currentWeaponIndex++;
        if (currentWeaponIndex >= unlockedWeapons.Count)
        {
            currentWeaponIndex = 0;
        }
        nextWeapon = unlockedWeapons[currentWeaponIndex];
        ActiveWeapon(nextWeapon);
    }

    public void OnShoot(InputAction.CallbackContext context)
    {
        if (currentGun != null)
        {
            currentGun.TryFire(context);
        }
    }

    public void UnlockPistol()
    {
        if (unlockedWeapons.Contains(flicker.gameObject))
            return;

        unlockedWeapons.Add(flicker.gameObject);
        currentWeaponIndex = unlockedWeapons.Count - 1;
        ActiveWeapon(flicker.gameObject);
    }

    public void UnlockShotgun()
    {
        if (unlockedWeapons.Contains(shotgun.gameObject))
            return;

        unlockedWeapons.Add(shotgun.gameObject);
        currentWeaponIndex = unlockedWeapons.Count - 1;
        ActiveWeapon(shotgun.gameObject);
    }

    public void UnlockSniper()
    {
        if (unlockedWeapons.Contains(railgun.gameObject))
            return;

        unlockedWeapons.Add(railgun.gameObject);
        currentWeaponIndex = unlockedWeapons.Count - 1;
        ActiveWeapon(railgun.gameObject);
    }
}