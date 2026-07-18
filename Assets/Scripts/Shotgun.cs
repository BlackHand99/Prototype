using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class Shotgun : MonoBehaviour, IGun
{
    [SerializeField] private GameObject gun;

    [SerializeField] private GameObject bullet;

    [SerializeField] private Transform bulletOrigin;

    private bool isShooting;
    private bool isMouseDown;

    [SerializeField]private int bulletCount = 5;

    [SerializeField] private float spreadAngle = 45f;

    [SerializeField] private bool automatic = true;

    [SerializeField] private float fireRate = 120f;

    [SerializeField] private float bulletTime = 0.5f;

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
        Vector2[] fireDirections = GetFireDirection();
        for(int i = 0; i < fireDirections.Length; i++)
        {
            GameObject myBullet =  Instantiate(bullet, bulletOrigin.position, bulletOrigin.rotation);
            myBullet.transform.right = fireDirections[i];
            ShotgunPellets pellets = myBullet.GetComponent<ShotgunPellets>();
            pellets.bulletTime = bulletTime;
        }
    }

    private Vector2[] GetFireDirection()
    {
        Vector2[] directions = new Vector2[bulletCount];

        Vector2 baseDir = bulletOrigin.right.normalized;

        if (bulletCount == 1)
        {
            directions[0] = baseDir;
            return directions;
        }

        float step = spreadAngle / (bulletCount - 1);
        float startAngle = -spreadAngle / 2f;

        for (int i = 0; i < bulletCount; i++)
        {
            float currentAngle = startAngle + step * i;

            // Rotate direction
            float rad = currentAngle * Mathf.Deg2Rad;

            Vector2 rotatedDir = new Vector2(
                baseDir.x * Mathf.Cos(rad) - baseDir.y * Mathf.Sin(rad),
                baseDir.x * Mathf.Sin(rad) + baseDir.y * Mathf.Cos(rad)
            );

            directions[i] = rotatedDir.normalized;
        }
        return directions;
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

    public void FunnelShotUpgrade()
    {
        spreadAngle -= 10f;
        bulletCount += 1;
        bulletTime += 0.3f;
    }

    public void BuckshotUpgrade()
    {
        bulletCount += 1;
        spreadAngle += 10f;
    }
}