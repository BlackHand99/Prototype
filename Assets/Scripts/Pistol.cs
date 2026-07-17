using System;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class Pistol : MonoBehaviour, IGun
{
    [SerializeField] private GameObject gun;

    [SerializeField] public GameObject bullet;

    [SerializeField] private Transform bulletOrigin;


    private GameObject bulletInstantiate;

    private bool isShooting;
    private bool isMouseDown;

    [SerializeField] private float bulletSizeMultiplier = 1f;

    [SerializeField] private bool automatic = true;

    [SerializeField] private float bulletSpeed = 25f;

    [SerializeField] private float bulletDamage = 1;

    [SerializeField] public float fireRate = 120f;

    [SerializeField] public float fireRateUpgrade = 60f;

    private Coroutine fireRoutine;

    public void HeavyBarkUpgrade()
    {
        fireRate *= 0.5f;
        bulletDamage *= 2.5f;
    }

    public void LightBarkUpgrade()
    {
        fireRate *= 2.5f;
        bulletDamage *= 0.6f;
    }

    public void CannonballUpgrade()
    {
        fireRate *= 0.3f;
        bulletDamage *= 3.5f;
        bulletSpeed *= 0.6f;
        bulletSizeMultiplier *= 3f;
    }

    public void MinigunUpgrade()
    {
        fireRate *= 5f;
        bulletDamage *= 0.25f;
        bulletSpeed *= 2f;
    }

    private void OnDisable()
    {
        isMouseDown = false;
        isShooting = false;
        fireRoutine = null;
    }

    private void OnEnable()
    {
        isMouseDown = false;
        isShooting = false;
    }

    private IEnumerator ContinuousFire()
    {
        WaitForSeconds delay = new WaitForSeconds(60f / fireRate);
        while (isMouseDown)
        {
            Fire();
            yield return delay;
        }
        yield return delay;
        fireRoutine = null;
    }

    private IEnumerator SemiAutomatic()
    {
        isShooting = true;
        Fire();
        yield return new WaitForSeconds(60f / fireRate);
        isShooting = false;
    }

    private void Fire()
    {
        GameObject pistolBullet = Instantiate(bullet, bulletOrigin.position, bulletOrigin.rotation);
        pistolBullet.transform.localScale *= bulletSizeMultiplier;
        PistolBullet PistolBullet = pistolBullet.GetComponent<PistolBullet>();
        PistolBullet.speed = bulletSpeed;
        PistolBullet.damage = bulletDamage;
    }

    internal void SetActive(bool v)
    {
        throw new NotImplementedException();
    }

    public void TryFire(InputAction.CallbackContext context)
    {
        isMouseDown = context.performed;
        if (automatic)
        {
            if (isMouseDown && fireRoutine == null)
            {
                fireRoutine = StartCoroutine(ContinuousFire());
            }
        }
        else
        {
            if (isMouseDown && !isShooting)
            {
                StartCoroutine(SemiAutomatic());
            }
        }
    }
}