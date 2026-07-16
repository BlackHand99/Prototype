using UnityEngine;

public class DamageEnemy : MonoBehaviour
{
    private float health;
    [SerializeField] private int maxHealth = 3;

    private void Start()
    {
        health = maxHealth;
    }

    public void TakeDamage(float damage)
    {
        health -= damage;
        if (health <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        Destroy(gameObject);
    }
}