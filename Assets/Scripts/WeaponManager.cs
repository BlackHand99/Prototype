using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class WeaponManager : MonoBehaviour
{

    [field:SerializeField] public Pistol pistol { get; private set; }
    [field:SerializeField] public PistolBullet pistolBullet {  get; private set; }
    [SerializeField] private Shotgun shotgun;
    [SerializeField] private SniperRifle sniper;

    private IGun currentGun;

    private List<GameObject> unlockedWeapons = new();
    private int currentWeaponIndex = 0;
    private GameObject nextWeapon;

    private void Start()
    {
        UnlockPistol();
        UnlockShotgun();
        UnlockSniper();

        currentWeaponIndex = 0;
        ActiveWeapon(unlockedWeapons[currentWeaponIndex]);
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
        unlockedWeapons.Add(pistol.gameObject);
        if (pistol.TryGetComponent(out IGun gun))
        {
            pistol.gameObject.SetActive(true);
            shotgun.gameObject.SetActive(false);
            sniper.gameObject.SetActive(false);
            currentGun = gun;
        }
    }

    public void UnlockShotgun()
    {
        unlockedWeapons.Add(shotgun.gameObject);
        if (shotgun.TryGetComponent(out IGun gun))
        {
            pistol.gameObject.SetActive(false);
            shotgun.gameObject.SetActive(true);
            sniper.gameObject.SetActive(false);
            currentGun = gun;
        }
    }

    public void UnlockSniper()
    {
        unlockedWeapons.Add(sniper.gameObject);
        if (sniper.TryGetComponent(out IGun gun))
        {
            pistol.gameObject.SetActive(false);
            shotgun.gameObject.SetActive(false);
            sniper.gameObject.SetActive(true);
            currentGun = gun;
        }
    }
}