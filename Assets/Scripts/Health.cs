using System.Runtime.CompilerServices;
using Unity.Mathematics;
using Unity.VisualScripting;
using Unity.VisualScripting.InputSystem;
using UnityEngine;



public class Health : MonoBehaviour
{
    [SerializeField] private float startingHealth;
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private DeathUi deathUI;
    [SerializeField] private float maxHealth = 3;
    public float CurrentHealth { get; private set; }

    private Animator anim;
    private bool isDead;

    private void Start()
    {
        CurrentHealth = startingHealth;

    }

    public void ApplyKnockback(
    Vector2 direction,
    float force
)
    {
        rb.linearVelocity = Vector2.zero;

        rb.AddForce(
            direction.normalized * force,
            ForceMode2D.Impulse
        );
    }

    public void TakeDamage(
        float damage,
        Vector2 knockbackDirection,
        float knockbackForce
    )
    {
        if (isDead)
        {
            return;
        }
        CurrentHealth -= damage;

        rb.linearVelocity = Vector2.zero;

        rb.AddForce(
            knockbackDirection.normalized * knockbackForce,
            ForceMode2D.Impulse
        );

        if (CurrentHealth <= 0)
        {

            isDead = true;
            deathUI.DeathPanel();
            Destroy(gameObject);

        }
    }

    public void IncreaseMaxHealth(float amount)
    {
        maxHealth += amount;
        CurrentHealth += amount;
    }
}
