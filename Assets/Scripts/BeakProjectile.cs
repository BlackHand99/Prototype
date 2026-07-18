using UnityEngine;

public class BeakProjectile : MonoBehaviour
{
    [SerializeField] private float speed = 8f;
    [SerializeField] private int damage = 1;
    [SerializeField] private float lifetime = 8f;
    [SerializeField] private LayerMask wallLayer;
    [SerializeField] private float knockbackForce;

    private Vector2 moveDirection;

    public void Initialize(Vector2 direction)
    {
        moveDirection = direction.normalized;
    }

    private void Start()
    {
        Destroy(gameObject, lifetime);
    }

    private void Update()
    {
        transform.position +=
            (Vector3)(moveDirection * speed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Beak hit: " + other.name);

        // Destroy on walls
        if ((wallLayer.value & (1 << other.gameObject.layer)) != 0)
        {
            Destroy(gameObject);
            return;
        }

        // Damage player
        if (other.TryGetComponent(out Health player))
        {
            player.TakeDamage(
                damage,
                moveDirection,
                knockbackForce
            );

            Destroy(gameObject);
        }
    }
}