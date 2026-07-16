using UnityEngine;

public class PistolBullet : MonoBehaviour
{
    private Rigidbody2D myRigidBody2D;
    public float speed;
    [SerializeField] private LayerMask bulletDestroy;
    public float damage;
    [SerializeField] private float bulletSize = 1f;

    private void Start()
    {
        myRigidBody2D = GetComponent<Rigidbody2D>();
        BulletVelocity();
    }

    /* private void OnTriggerEnter2D(Collider2D collision)
    {
        if((bulletDestroy.value & (1 << collision.gameObject.layer)) > 0)
        {
            Destroy(gameObject);
        }
        if (collision.gameObject.TryGetComponent(out DamageEnemy health))
        {
            health.TakeDamage(damage);
        }
    } */

    private void BulletVelocity()
    {
        myRigidBody2D.linearVelocity = transform.right * speed;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Bullet hit: " + collision.name);

        if ((bulletDestroy.value & (1 << collision.gameObject.layer)) > 0)
        {
            Destroy(gameObject);
        }

        if (collision.gameObject.TryGetComponent(out EnemyHealth health))
        {
            Debug.Log("Damaging enemy");
            health.TakeDamage(damage);
        }
    }
}