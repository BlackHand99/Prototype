using UnityEngine;
using System.Collections;

public class ShotgunPellets : MonoBehaviour
{
    private Rigidbody2D myRigidBody2D;
    [SerializeField] private float speed = 10f;
    [SerializeField] private LayerMask bulletDestroy;
    [SerializeField] private int damage = 1;
    [SerializeField] public float bulletTime = 0.5f;

    private void Start()
    {
        myRigidBody2D = GetComponent<Rigidbody2D>();
        BulletVelocity();
        StartCoroutine(DestroyPellets());
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if ((bulletDestroy.value & (1 << collision.gameObject.layer)) > 0)
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
    private IEnumerator DestroyPellets()
    {
        yield return new WaitForSeconds(bulletTime);
        Destroy(gameObject);
    }
}