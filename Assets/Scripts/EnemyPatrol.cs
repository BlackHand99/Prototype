using System.Collections;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyPatrol : MonoBehaviour
{

    [SerializeField] private float detectionRange;
    [SerializeField] private float detectionSize;
    [SerializeField] private float detectInterval;
    [SerializeField] private float colliderDistance;
    [SerializeField] private LayerMask playerLayer;
    [SerializeField] private LayerMask enemyLayer;
    [SerializeField] private BoxCollider2D boxCollider;
    [SerializeField] private float allyAggroRadius;

    private Chase chase;
    private Animator anim;
    private Enemy01Attack eA;

    private Chase eC;

    [Header("Patrol Range Settings")]
    [SerializeField] private float minRange = 2f;
    [SerializeField] private float maxRange = 8f;

    [Header("Raycast Settings")]
    [SerializeField] private LayerMask groundMask;
    private float rayHeightOffset = 0.5f;

    [Header("Results")]
    private float patrolLeft;
    private float patrolRight;

    [Header("Enemy")]
    [SerializeField] private Transform enemy;

    [Header("MovementParameter")]
    [SerializeField] private float patrolSpeed;
    [SerializeField] private float flipCooldownTime = 0.25f;
    private float flipCooldown = 0f;
    private int direction = 1;

    private Vector3 initScale;

    private void Awake()
    {
        initScale = enemy.localScale;

    }

    private void Update()
    {
        flipCooldown -= Time.deltaTime;

        Vector2 origin = (Vector2)transform.position + Vector2.up * 0.2f;

        RaycastHit2D wallHit = Physics2D.Raycast(
            origin,
            Vector2.right * direction,
            0.3f,
            groundMask
        );

        Vector3 pos = transform.position;

        bool shouldFlip = false;

        // Wall check
        if (wallHit.collider != null)
            shouldFlip = true;

        // Bounds check
        if (pos.x >= patrolRight || pos.x <= patrolLeft)
            shouldFlip = true;

        // Apply flip only if cooldown allows it
        if (shouldFlip && flipCooldown <= 0f)
        {
            direction *= -1;
            flipCooldown = flipCooldownTime; // prevents rapid bouncing
        }

        // Move AFTER decision
        pos.x += direction * patrolSpeed * Time.deltaTime;
        transform.position = pos;

        // Flip visuals
        transform.localScale = new Vector3(
            Mathf.Abs(transform.localScale.x) * direction,
            transform.localScale.y,
            transform.localScale.z
        );
    }

    private void Start()
    {
        CalculatePatrolBounds();
        StartCoroutine(DetectEnemy());
    }

    private IEnumerator DetectEnemy()
    {
        {
            yield return new WaitForSeconds(detectInterval);
            if (PlayerInSight())
            {
                RaycastHit2D hit = Physics2D.CircleCast(transform.position, 10f * allyAggroRadius, Vector2.up, 0, enemyLayer);
                //eC.chase=true
            }

        }
    }



    private void CalculatePatrolBounds()
    {
        Vector2 origin = (Vector2)transform.position + Vector2.up * 0.2f;

        // Detect real world limits
        RaycastHit2D hitLeft = Physics2D.Raycast(origin, Vector2.left, maxRange, groundMask);
        RaycastHit2D hitRight = Physics2D.Raycast(origin, Vector2.right, maxRange, groundMask);

        float leftLimit = hitLeft.collider ? hitLeft.distance : maxRange;
        float rightLimit = hitRight.collider ? hitRight.distance : maxRange;

        // Convert to world positions
        float worldLeft = transform.position.x - leftLimit;
        float worldRight = transform.position.x + rightLimit;

        // Safety: ensure valid space
        if (worldRight - worldLeft < minRange)
        {
            patrolLeft = worldLeft;
            patrolRight = worldRight;
            return;
        }

        // Pick two random points inside the valid space
        float a = Random.Range(worldLeft, worldRight);
        float b = Random.Range(worldLeft, worldRight);

        patrolLeft = Mathf.Min(a, b);
        patrolRight = Mathf.Max(a, b);
    }



    private void OnDrawGizmos()
    {
        var boxCollider = GetComponent<BoxCollider2D>();

        Gizmos.color = Color.blue;
        Gizmos.DrawWireCube(boxCollider.bounds.center + transform.right * detectionRange * transform.localScale.x * colliderDistance, new Vector3(detectionSize, boxCollider.bounds.size.y, boxCollider.bounds.size.z));

        //Patrol points gizmo

        Vector2 origin = transform.position + Vector3.up * rayHeightOffset;

        Gizmos.color = Color.red;
        Gizmos.DrawLine(origin, origin + Vector2.left * maxRange);
        Gizmos.DrawLine(origin, origin + Vector2.right * maxRange);

        Gizmos.color = Color.green;
        Gizmos.DrawLine(new Vector2(patrolLeft, transform.position.y - 0.5f),
                        new Vector2(patrolLeft, transform.position.y + 0.5f));

        Gizmos.DrawLine(new Vector2(patrolRight, transform.position.y - 0.5f),
                        new Vector2(patrolRight, transform.position.y + 0.5f));
    }


    private bool PlayerInSight()
    {
        RaycastHit2D hit = Physics2D.BoxCast(boxCollider.bounds.center + transform.right * detectionRange * transform.localScale.x * colliderDistance, new Vector3(boxCollider.bounds.size.x, boxCollider.bounds.size.y, boxCollider.bounds.size.z), 0, Vector2.left, 0, playerLayer);

        return hit.collider != null;

    }
}
