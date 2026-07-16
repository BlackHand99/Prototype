using UnityEngine;

public class SniperBullet : MonoBehaviour
{
    private Rigidbody2D myRigidBody2D;
    [SerializeField] private float speed = 10f;
    [SerializeField] private LayerMask structureBulletDestroy;
    [SerializeField] private int damage = 1;

    private void Start()
    {
        myRigidBody2D = GetComponent<Rigidbody2D>();
        BulletVelocity();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if ((structureBulletDestroy.value & (1 << collision.gameObject.layer)) > 0)
        {
            Destroy(gameObject);
        }
        else
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