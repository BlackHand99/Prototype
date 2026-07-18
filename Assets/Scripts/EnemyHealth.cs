using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    private float health;
    private RoomDirector roomDirector;
    public RoomDirector RoomDirector => roomDirector;

    [SerializeField] private float maxHealth = 3f;

    private void Start()
    {
        health = maxHealth;
    }
    public void SetRoomDirector(RoomDirector director)
    {
        roomDirector = director;
    }

    public void TakeDamage(float damage)
    {
        health -= damage;

        if (health <= 0f)
        {
            Die();
        }
    }

    private void Die()
    {
        roomDirector?.RegisterDeath();
        Destroy(gameObject);
    }
}