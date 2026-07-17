using UnityEngine;

public class EnemyContDamage : MonoBehaviour
{
    [SerializeField] private float damage = 1;
    [SerializeField] private float knockbackForce = 8f;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("Player"))
            return;

        Health player = collision.GetComponent<Health>();

        if (player == null)
            return;

        Vector2 knockbackDir =
            (collision.transform.position - transform.position).normalized;

        knockbackDir = new Vector2(knockbackDir.x, knockbackForce).normalized;

        player.TakeDamage(
            damage,
            knockbackDir,
            knockbackForce
        );
    }
}