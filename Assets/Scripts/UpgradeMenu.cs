using UnityEngine;

public class UpgradeMenu : MonoBehaviour
{
    private WeaponManager weaponManager;

    private PlayerController player;

    private Health playerHealth;

    public void Open(WeaponManager manager)
    {
        weaponManager = manager;
        player = manager.GetComponent<PlayerController>();
        playerHealth = manager.GetComponent<Health>();

        gameObject.SetActive(true);
        Time.timeScale = 0f;
    }

    public void HeavyBarkUpgrade()
    {
        weaponManager.pistol.HeavyBarkUpgrade();
        weaponManager.UnlockPistol();
        Close();
    }

    public void LightBarkUpgrade()
    {
        weaponManager.pistol.LightBarkUpgrade();
        weaponManager.UnlockPistol();
        Close();
    }

    public void BarkShoes()
    {
        player.BarkShoes();
        weaponManager.UnlockPistol();
        Close();
    }

    public void Cannonball()
    {
        weaponManager.pistol.CannonballUpgrade();
        weaponManager.UnlockShotgun();
        Close();
    }

    public void FunnelShot()
    {
        weaponManager.shotgun.FunnelShotUpgrade();
        weaponManager.UnlockShotgun();
        Close();
    }

    public void WoodHelmet()
    {
        playerHealth.IncreaseMaxHealth(1);
        weaponManager.UnlockShotgun();
        Close();
    }

    public void Railgun()
    {
        weaponManager.sniper.RailgunUpgrade();
        weaponManager.UnlockSniper();
        Close();
    }

    public void ShotgunJump()
    {
        weaponManager.shotgun.ShotgunJumpUpgrade();
        weaponManager.UnlockSniper();
        Close();
    }

    public void Minigun()
    {
        weaponManager.pistol.MinigunUpgrade();
        weaponManager.UnlockSniper();
        Close();
    }

    public void Close()
    {
        gameObject.SetActive(false);
        Time.timeScale = 1f;
    }
}
