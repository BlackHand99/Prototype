using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
public class SniperRifle : MonoBehaviour, IGun
{
    [SerializeField] private GameObject gun;

    [SerializeField] private GameObject bullet;

    [SerializeField] private Transform bulletOrigin;

    private GameObject bulletInstantiate;

    private bool isShooting;
    private bool isMouseDown;

    [SerializeField] private bool automatic = true;

    [SerializeField] private float fireRate = 120f;

    private Coroutine fireRoutine;

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
        Instantiate(bullet, bulletOrigin.position, bulletOrigin.rotation);
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