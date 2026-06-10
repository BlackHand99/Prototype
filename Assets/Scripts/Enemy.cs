using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] Rigidbody2D myRigidBody2D;

    [SerializeField] private float speed = 5f;

    private void Start()
    {
        myRigidBody2D.linearVelocityX = speed;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        speed = -speed;
        myRigidBody2D.linearVelocityX = speed;
        if (speed * transform.right.x < 0)
        {
            Flip();
        }
    }

    private void Flip()
    {
        transform.right = -transform.right;
    }

}