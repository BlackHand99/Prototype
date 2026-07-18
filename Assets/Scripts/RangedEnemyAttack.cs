using UnityEngine;

public class RangedEnemyAttack : MonoBehaviour
{
    [SerializeField] private GameObject beakProjectile;
    [SerializeField] private Transform firePoint;
    [SerializeField] private float cooldown = 2f;

    [SerializeField] private RangedChase chase;

    private Transform player;
    private float nextAttackTime;

    private void Start()
    {
        player = GameManagerSingleton.Instance.GetPlayer();
    }

    public bool CanShoot()
    {
        if (player == null)
            return false;

        float distance = Vector2.Distance(
            transform.position,
            player.position
        );

        return distance <= chase.attackDistance;
    }

    private void Update()
    {
        if (player == null)
            return;

        if (!CanShoot())
            return;

        if (Time.time < nextAttackTime)
            return;

        Shoot();

        nextAttackTime = Time.time + cooldown;
    }

    private void Shoot()
    {
        GameObject beak = Instantiate(
            beakProjectile,
            firePoint.position,
            Quaternion.identity
        );

        Vector2 direction =
            ((Vector2)player.position - (Vector2)firePoint.position).normalized;

        beak.GetComponent<BeakProjectile>()
            .Initialize(direction);
    }
}