using System.Collections;
using UnityEngine;

public class Enemy01Attack : MonoBehaviour
{
    [SerializeField] private float attackCooldown;

    [SerializeField] private float range;

    [SerializeField]
    private Vector2 attackBoxSize =
        new Vector2(3f, 1.5f);

    [SerializeField] private int damage;
    [SerializeField] private LayerMask playerLayer;
    [SerializeField] private BoxCollider2D boxCollider;
    [SerializeField] private Rigidbody2D rb;

    //lunging attack stuff
    [SerializeField] private float windupDelay = 1.0f;
    [SerializeField] private float lungeForce = 12f;
    [SerializeField] private float lungeDuration = 0.2f;
    [SerializeField] private float recoveryTime = 1f;
    [SerializeField] private float knockbackForce;
    private bool attacking;
    private bool hasHitPlayer;
    private bool lunging;
    public float attackRange => range;
    public bool IsAttacking => attacking;

    public bool CanAttack => !attacking;
    public bool IsLunging => lunging;

    private Animator anim;

    public bool isCooldownActive;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        isCooldownActive = true;
    }
    public IEnumerator LungeAttack(Vector2 playerPos)
    {
        if (attacking)
            yield break;

        attacking = true;

        // stop current movement
        rb.linearVelocity = Vector2.zero;

        //switch to windup animation
        anim.Play("meleeAttack");

        // optional windup delay
        yield return new WaitForSeconds(windupDelay);

        float dir =
            Mathf.Sign(playerPos.x - transform.position.x);

        lunging = true;
        hasHitPlayer = false;

        rb.linearVelocity = new Vector2(
            dir * lungeForce,
            rb.linearVelocity.y
        );

        float timer = 0f;

        while (timer < lungeDuration)
        {
            Collider2D hit = Physics2D.OverlapBox(
                transform.position,
                attackBoxSize,
                0,
                playerLayer
            );

            if (hit != null && !hasHitPlayer)
            {
                Health playerHealth = hit.GetComponentInParent<Health>();

                if (playerHealth != null)
                {
                    Vector2 knockbackDir =
                        (hit.transform.position - transform.position).normalized;

                    knockbackDir = new Vector2(
                        knockbackDir.x,
                        knockbackForce
                    ).normalized;

                    playerHealth.TakeDamage(
                        damage,
                        knockbackDir,
                        knockbackForce
                    );

                    if (hit != null)
                    {
                        Debug.Log("Lunge hit: " + hit.name);
                    }

                    hasHitPlayer = true;
                }
            }

            timer += Time.deltaTime;
            yield return null;
        }


        // stop lunge
        rb.linearVelocity = new Vector2(
            0,
            rb.linearVelocity.y
        );

        yield return new WaitForSeconds(recoveryTime);

        lunging = false;

        attacking = false;

    }

    public bool PlayerInRange()
    {
        Vector2 direction =
            transform.localScale.x > 0
            ? Vector2.right
            : Vector2.left;

        Vector2 center =
            (Vector2)transform.position +
            direction * range;

        Collider2D hit = Physics2D.OverlapBox(
            center,
            attackBoxSize,
            0,
            playerLayer
        );

        return hit != null;
    }

    private void OnDrawGizmos()
    {
        Vector2 direction =
    transform.localScale.x > 0
    ? Vector2.right
    : Vector2.left;

        Vector2 center =
            (Vector2)transform.position +
            direction * range;

        Gizmos.color = Color.red;

        Gizmos.DrawWireCube(
            center,
            attackBoxSize
        );
    }
    private void Update()
    {
        Collider2D hit =
            Physics2D.OverlapCircle(
                transform.position,
                2f,
                playerLayer
            );

        if (hit != null)
        {
            Debug.Log("Player found");
        }
    }
}
