using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class WeaponManager : MonoBehaviour
{

    [field:SerializeField] public Pistol pistol { get; private set; }
    [field:SerializeField] public PistolBullet pistolBullet {  get; private set; }
    [field: SerializeField] public Shotgun shotgun { get; private set; }

    [field: SerializeField] public SniperRifle sniper { get; private set; }

    private IGun currentGun;

    private List<GameObject> unlockedWeapons = new();
    private int currentWeaponIndex = 0;
    private GameObject nextWeapon;

    private void Start()
    {
        pistol.gameObject.SetActive(false);
        shotgun.gameObject.SetActive(false);
        sniper.gameObject.SetActive(false);
    }

    private void ActiveWeapon(GameObject weaponInHand)
    {
        pistol.gameObject.SetActive(false);
        shotgun.gameObject.SetActive(false);
        sniper.gameObject.SetActive(false);

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
        if (unlockedWeapons.Contains(pistol.gameObject))
            return;

        unlockedWeapons.Add(pistol.gameObject);

        currentWeaponIndex = unlockedWeapons.Count - 1;
        ActiveWeapon(pistol.gameObject);
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
        if (unlockedWeapons.Contains(sniper.gameObject))
            return;

        unlockedWeapons.Add(sniper.gameObject);

        currentWeaponIndex = unlockedWeapons.Count - 1;
        ActiveWeapon(sniper.gameObject);
    }
}