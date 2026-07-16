using UnityEngine;

public class RangedEnemyAttack : MonoBehaviour
{
    [SerializeField] private GameObject beakProjectile;
    [SerializeField] private Transform firePoint;
    [SerializeField] private float cooldown = 2f;

    public RangedChase chase;
    private float nextAttackTime;

    public bool CanShoot()
    {
        float distance =
            Vector2.Distance(
                transform.position,
                GameManagerSingleton.Instance.GetPlayerPosition()
            );

        return distance <= chase.attackDistance;
    }

    private void Update()
    {
        if (!GameManagerSingleton.Instance.GlobalAlert)
            return;

        if (!CanShoot())
            return;

        if (Time.time < nextAttackTime)
            return;

        Shoot();

        nextAttackTime =
            Time.time + cooldown;
    }

    private void Shoot()
    {
        GameObject beak =
            Instantiate(
                beakProjectile,
                firePoint.position,
                Quaternion.identity
            );

        Vector2 direction =
        (
            GameManagerSingleton.Instance.GetPlayerPosition()
            - (Vector2)firePoint.position
        ).normalized;

        beak.GetComponent<BeakProjectile>()
            .Initialize(direction);

        beak.GetComponent<BeakProjectile>()
            .Initialize(direction);
    }
}
