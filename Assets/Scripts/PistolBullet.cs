using UnityEngine;

public class PistolBullet : MonoBehaviour
{
    private Rigidbody2D myRigidBody2D;
    [field:SerializeField] public float speed { get; private set; } = 25f;
    [SerializeField] private LayerMask bulletDestroy;
    [SerializeField] public int damage = 1;
    [SerializeField] private int speedUpgradeAmount = 15;
    [SerializeField] private int damageUpgradeAmount = 1;

    public void BulletSpeedUpgrade()
    {
        speed = speed + speedUpgradeAmount;
    }

    public void DamageUpgrade()
    {
        damage = damage + damageUpgradeAmount;
    }

    private void Start()
    {
        myRigidBody2D = GetComponent<Rigidbody2D>();
        BulletVelocity();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if((bulletDestroy.value & (1 << collision.gameObject.layer)) > 0)
        {
            Destroy(gameObject);
        }
        if (collision.gameObject.TryGetComponent(out DamageEnemy health))
        {
            health.TakeDamage(damage);
        }
    }

    private void BulletVelocity()
    {
        myRigidBody2D.linearVelocity = transform.right * speed;
    }
}