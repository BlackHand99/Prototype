using System.Runtime.CompilerServices;
using Unity.Mathematics;
using Unity.VisualScripting;
using Unity.VisualScripting.InputSystem;
using UnityEngine;



public class Health : MonoBehaviour
{
    [SerializeField] private float startingHealth;
    public float CurrentHealth { get; private set; }

    private Animator anim;
    private bool dead;

    private void Start()
    {
        CurrentHealth = startingHealth;

    }

    public void TakeDamage(float damage)
    {
        CurrentHealth -= damage;

            if (CurrentHealth <= 0)
            {
                Destroy(gameObject);
             
            }

        
    }
}
