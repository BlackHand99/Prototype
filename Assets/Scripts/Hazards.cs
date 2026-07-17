using UnityEngine;

public class Hazards : MonoBehaviour
{
    [SerializeField] private float damage = 1f;
    [SerializeField] private float knockbackForce = 8f;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out Health health))
        {
            Vector2 knockbackDir =
                ((Vector2)collision.transform.position -
                (Vector2)transform.position).normalized;

            health.TakeDamage(
                damage,
                knockbackDir,
                knockbackForce
            );
        }
    }
}