using UnityEngine;

public class PistolBullet : MonoBehaviour
{
    private Rigidbody2D myRigidBody2D;

    public float speed;
    public float damage;

    [SerializeField] private LayerMask bulletDestroy;


    private void Start()
    {
        myRigidBody2D = GetComponent<Rigidbody2D>();

        BulletVelocity();
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Shield gets priority
        if (collision.GetComponent<ShieldBlocker>() != null)
        {
            Destroy(gameObject);
            return;
        }

        EnemyHealth enemy =
            collision.GetComponentInParent<EnemyHealth>();

        if (enemy != null)
        {
            enemy.TakeDamage(damage);
            Destroy(gameObject);
            return;
        }

        if ((bulletDestroy.value & (1 << collision.gameObject.layer)) != 0)
        {
            Destroy(gameObject);
        }
    }


    private void BulletVelocity()
    {
        myRigidBody2D.linearVelocity =
            transform.right * speed;
    }
}