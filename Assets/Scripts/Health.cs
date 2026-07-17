using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] private float maxHealth = 3;
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private DeathUi deathUI;

    public float CurrentHealth { get; private set; }

    private bool isDead;

    private void Start()
    {
        CurrentHealth = maxHealth;
    }

    public void TakeDamage(float damage)
    {
        TakeDamage(damage, Vector2.zero, 0f);
    }

    public void TakeDamage(
        float damage,
        Vector2 knockbackDirection,
        float knockbackForce)
    {
        if (isDead)
            return;

        CurrentHealth -= damage;

        if (rb != null && knockbackForce > 0f)
        {
            rb.linearVelocity = Vector2.zero;
            rb.AddForce(
                knockbackDirection.normalized * knockbackForce,
                ForceMode2D.Impulse
            );
        }

        if (CurrentHealth <= 0)
        {
            isDead = true;
            deathUI.DeathPanel();
        }
    }

    public void IncreaseMaxHealth(float amount)
    {
        maxHealth += amount;
        CurrentHealth += amount;
    }
}