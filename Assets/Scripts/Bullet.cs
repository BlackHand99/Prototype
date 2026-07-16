using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private Rigidbody2D myRigidbody2d;
    [SerializeField] private float speed = 4f;
    [SerializeField] private float bulletDamage;

    private void Start()
    {
        myRigidbody2d.linearVelocity = transform.right * speed; 
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Destroy(gameObject);
    }
}
