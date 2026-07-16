using System.Runtime.CompilerServices;
using Unity.Mathematics;
using Unity.VisualScripting;
using Unity.VisualScripting.InputSystem;
using UnityEngine;



public class Health : MonoBehaviour
{
    [SerializeField] private float maxHealth = 3;
    public float CurrentHealth { get; private set; }

    private void Start()
    {
        CurrentHealth = maxHealth;
    }

    public void TakeDamage(float damage)
    {
        CurrentHealth -= damage;

        if (CurrentHealth <= 0)
        {
            Destroy(gameObject);
        }
    }

    public void IncreaseMaxHealth(float amount)
    {
        maxHealth += amount;
        CurrentHealth += amount;
    }
}