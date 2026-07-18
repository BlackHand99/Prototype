using System.Collections;
using UnityEngine;

public class Enemy01Attack : MonoBehaviour
{
    [Header("Detection")]
    [SerializeField] private Vector2 detectionBox = new Vector2(3f, 2f);
    [SerializeField] private float detectionOffset = 1.5f;


    [Header("Attack")]
    [SerializeField] private int damage = 1;

    [SerializeField] private float windupDelay = 0.5f;
    [SerializeField] private float lungeSpeed = 18f;
    [SerializeField] private float lungeDuration = 0.2f;
    [SerializeField] private float recoveryTime = 0.6f;

    [SerializeField] private float knockbackForce = 8f;


    [Header("Hurtbox")]
    [SerializeField] private Vector2 attackBoxSize = new Vector2(1.5f, 1.5f);
    [SerializeField] private LayerMask playerLayer;


    [SerializeField] private Rigidbody2D rb;


    private Animator anim;
    private Chase chase;


    private bool attacking;
    private bool hasHitPlayer;

    private float lungeDirection;


    public bool IsAttacking => attacking;

    public bool CanAttack => !attacking;



    private void Awake()
    {
        anim = GetComponent<Animator>();
        chase = GetComponent<Chase>();
    }



    public bool PlayerInRange()
    {
        Vector2 direction =
            chase.FacingDirection > 0
            ? Vector2.right
            : Vector2.left;


        Vector2 center =
            (Vector2)transform.position +
            direction * detectionOffset;


        Collider2D hit = Physics2D.OverlapBox(
            center,
            detectionBox,
            0f,
            playerLayer
        );


        return hit != null;
    }



    public IEnumerator LungeAttack(Vector2 playerPosition)
    {
        if (attacking)
            yield break;


        attacking = true;
        hasHitPlayer = false;


        rb.linearVelocity = Vector2.zero;


        if (anim != null)
            anim.Play("meleeAttack");


        Debug.Log("Lunge started");


        yield return new WaitForSeconds(windupDelay);


        // Lock direction at the moment of attack
        lungeDirection = chase.FacingDirection;


        Debug.Log(
            "Lunge direction: " + lungeDirection
        );


        // Ensure sprite matches attack direction
        transform.localScale = new Vector3(
            Mathf.Abs(transform.localScale.x) * lungeDirection,
            transform.localScale.y,
            transform.localScale.z
        );


        // Apply dash
        rb.linearVelocity = new Vector2(
            lungeDirection * lungeSpeed,
            rb.linearVelocity.y
        );


        float timer = 0f;


        while (timer < lungeDuration)
        {
            Vector2 hurtboxCenter =
                (Vector2)transform.position +
                Vector2.right * lungeDirection * 0.5f;


            Collider2D hit = Physics2D.OverlapBox(
                hurtboxCenter,
                attackBoxSize,
                0f,
                playerLayer
            );


            if (!hasHitPlayer && hit != null)
            {
                Health health =
                    hit.GetComponent<Health>();


                if (health != null)
                {
                    Vector2 knockback =
                        (
                        (Vector2)hit.transform.position -
                        rb.position
                        ).normalized;


                    health.TakeDamage(
                        damage,
                        knockback,
                        knockbackForce
                    );


                    hasHitPlayer = true;
                }
            }


            timer += Time.deltaTime;

            yield return null;
        }



        // Stop dash
        rb.linearVelocity = Vector2.zero;


        yield return new WaitForSeconds(recoveryTime);


        attacking = false;
    }



    private void OnDrawGizmosSelected()
    {
        float direction =
            transform.localScale.x >= 0
            ? 1f
            : -1f;


        Gizmos.color = Color.yellow;


        Gizmos.DrawWireCube(
            (Vector2)transform.position +
            Vector2.right * direction * detectionOffset,
            detectionBox
        );


        Gizmos.color = Color.red;


        Gizmos.DrawWireCube(
            (Vector2)transform.position +
            Vector2.right * direction * 0.5f,
            attackBoxSize
        );
    }
}