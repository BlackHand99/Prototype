using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;

public class PlayerController : MonoBehaviour
{
    private Vector3 lastPosition;

    [SerializeField] private Rigidbody2D myRigidBody2D;

    [SerializeField] private float fallGravity = 4f;

    [SerializeField] private float maxFallSpeed = 15f;

    [SerializeField] private float speed = 5f;

    [SerializeField] private float dashSpeed = 15f;

    [SerializeField] private float dashDuration = 0.5f;

    [SerializeField] private float dashCooldown = 2f;

    private bool canDash = true;

    private bool isDashing;

    private float horizontalinput;

    private float jumpsLeft;

    private float maxJumps = 0f;

    [SerializeField] private float jumpForce = 8f;

    [SerializeField] private float groundCheckVerticalOffset;

    [SerializeField] private LayerMask groundLayerMask;

    [SerializeField] private CapsuleCollider2D myCollider;

    [SerializeField] private float groundCheckHeight;

    private bool isGrounded = false;

    [SerializeField, Range(-1, 1)] private float lerpedHorizontalInput = 0;
    [SerializeField, Range(1f, 10f)] private float lerpSpeed = 4f;

    public void OnMove(InputAction.CallbackContext context)
    {
        if (context.performed && !isDashing)
        {
            horizontalinput = context.ReadValue<Vector2>().x;

            if (horizontalinput * transform.right.x < 0)
            {
                Flip();
            }
        }
        if (context.canceled)
        {
            horizontalinput = 0f;
        }
    }

    public void OnDash(InputAction.CallbackContext context)
    {
        if (context.performed && canDash)
        {
            StartCoroutine(Dash());
        }
    }
    private IEnumerator Dash()
    {
        float initialGravity = myRigidBody2D.gravityScale;
        myRigidBody2D.gravityScale = 0;
        isDashing = true;
        canDash = false;
        float direction = Mathf.Sign(horizontalinput);
        myRigidBody2D.linearVelocity = new Vector2(direction * dashSpeed, 0);
        yield return new WaitForSeconds(dashDuration);
        isDashing = false;
        myRigidBody2D.gravityScale = initialGravity;
        yield return new WaitForSeconds(dashCooldown);
        canDash = true;
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        if (context.performed && !isDashing)
        {
            Jump();
        }
    }

    private void Jump()
    {
        if (maxJumps < 2)
        {
            myRigidBody2D.linearVelocityY = 0f;
            myRigidBody2D.AddForceY(jumpForce, ForceMode2D.Impulse);
            maxJumps++;
            jumpsLeft = maxJumps;
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Ground"))
        {
            maxJumps = 0;
        }
    }

    private void Flip()
    {
        transform.right = -transform.right;
    }

    private void FixedUpdate()
    {
        if (!isDashing) 
        {
            myRigidBody2D.linearVelocity = new Vector2(lerpedHorizontalInput * speed, myRigidBody2D.linearVelocity.y); 
        }
        if (!isDashing)
        {
            if (myRigidBody2D.linearVelocity.y < 0)
            {
                myRigidBody2D.gravityScale = fallGravity;
                myRigidBody2D.linearVelocity = new Vector2(myRigidBody2D.linearVelocity.x, Mathf.Max(myRigidBody2D.linearVelocity.y, -maxFallSpeed));
            }
            else
            {
                myRigidBody2D.gravityScale = 1f;
            }
        }
        isGrounded = IsGrounded();
        bool isStopping = Mathf.Approximately(horizontalinput, 0f);
        if (isStopping)
        {
            lerpedHorizontalInput = Mathf.MoveTowards(lerpedHorizontalInput, 0f, lerpSpeed * Time.fixedDeltaTime);
        }
        else
        {
            lerpedHorizontalInput = Mathf.MoveTowards(lerpedHorizontalInput, horizontalinput, lerpSpeed * 3f * Time.fixedDeltaTime);
        }
    }
    private bool IsGrounded()
    {
        return (Physics2D.OverlapBox(
            new Vector3(
                    transform.position.x + myCollider.offset.x * Mathf.Sign(transform.right.x),
                    transform.position.y + myCollider.offset.y - myCollider.size.y / 2 + groundCheckVerticalOffset,
                    transform.position.z
            ),
            new Vector2(myCollider.size.x, groundCheckHeight),
            0f, groundLayerMask
            ) != null &&

            Mathf.Abs(myRigidBody2D.linearVelocityY) < 0.001f);
    }
    private void OnDrawGizmosSelected()
    {
        if (myCollider != null)
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireCube(
                new Vector3(
                    transform.position.x + myCollider.offset.x * Mathf.Sign(transform.right.x),
                    transform.position.y + myCollider.offset.y - myCollider.size.y / 2 + groundCheckVerticalOffset,
                    transform.position.z
                ),
                new Vector2(myCollider.size.x, groundCheckHeight)
            );
        }
    }
    void Update()
    {
        if (transform.position != lastPosition)
        {
            GameManagerSingleton.Instance.SetPlayerPosition(transform.position);
            lastPosition = transform.position;
        }
    }

    public void BarkShoes()
    {
        speed *= 1.5f;
        jumpForce *= 1.3f;
        dashSpeed *= 1.5f;
    }
}