using System.Collections;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyPatrol : MonoBehaviour
{
    //chase stuff
    [SerializeField] private float detectionSizeOffset;
    [SerializeField] private float detectionSize;
    [SerializeField] private float detectInterval;
    [SerializeField] private float colliderDistance;
    [SerializeField] private LayerMask playerLayer;

    [SerializeField] private LayerMask enemyLayer;
    [SerializeField] private BoxCollider2D boxCollider;
    [SerializeField] private Rigidbody2D rb;


    private Animator anim;
    private Enemy01Attack eA;

    private bool canMove;
    private bool IsChasing =>
    GameManagerSingleton.Instance.GlobalAlert;

    //patrol stuff
    private float corridorLeft;
    private float corridorRight;
    [SerializeField] private float maxRange;
    [SerializeField] private float minRange;

    //prevents collider getting stuck in wall
    [SerializeField] private float wallPadding = 0.5f;

    private float patrolA;
    private float patrolB;
    private bool movingToB = true;

    [SerializeField] private LayerMask groundMask;

    [Header("MovementParameter")]
    [SerializeField] private float patrolSpeed;

    private Vector3 initScale;

    private void Awake()
    {
        initScale = transform.localScale;
    }

    private void FixedUpdate()
    {
        if (!canMove)
            return;

        if (IsChasing)
            return;

        float target = movingToB ? patrolB : patrolA;

        float moveDir = Mathf.Sign(target - rb.position.x);

        Vector2 velocity = rb.linearVelocity;

        velocity.x = moveDir * patrolSpeed;

        rb.linearVelocity = velocity;

        // switch target when reached
        if (Mathf.Abs(rb.position.x - target) < 0.05f)
        {
            movingToB = !movingToB;
        }

        //visual facing
        if (Mathf.Abs(moveDir) > 0.01f)
        {
            transform.localScale = new Vector3(
                Mathf.Abs(initScale.x) * Mathf.Sign(moveDir),
                initScale.y,
                initScale.z
            );
        }
    }
    private void Start()
    {
        StartCoroutine(DetectEnemy());
        StartCoroutine(StartMoveDelay());

        CalculateCorridor();
        PickPatrolPoints();

    }

    private IEnumerator DetectEnemy()
    {
        while (true)
        {
            yield return new WaitForSeconds(detectInterval);

            if (GameManagerSingleton.Instance.GlobalAlert)
                continue;

            if (PlayerInSight())
            {
                rb.linearVelocity = Vector2.zero;

                GameManagerSingleton.Instance.TriggerGlobalAlert();
            }
        }
    }
    private IEnumerator StartMoveDelay()
    {
        yield return new WaitForSeconds(0.2f);
        canMove = true;
    }
    private void OnDrawGizmos()
    {
        var boxCollider = GetComponent<BoxCollider2D>();

        Vector2 direction =
            transform.localScale.x > 0
            ? Vector2.right
            : Vector2.left;

        Vector2 center =
            (Vector2)boxCollider.bounds.center +
            direction * detectionSizeOffset * colliderDistance;

        Gizmos.color = Color.blue;

        Gizmos.DrawWireCube(
            center,
            new Vector3(
                detectionSize,
                boxCollider.bounds.size.y,
                1
            )
        );

#if UNITY_EDITOR

        // Patrol corridor
        Gizmos.color = Color.yellow;

        Gizmos.DrawLine(
            new Vector3(corridorLeft, transform.position.y, 0),
            new Vector3(corridorRight, transform.position.y, 0)
        );

        // Patrol point A
        Gizmos.color = Color.green;

        Gizmos.DrawSphere(
            new Vector3(patrolA, transform.position.y, 0),
            0.15f
        );

        // Patrol point B
        Gizmos.color = Color.red;

        Gizmos.DrawSphere(
            new Vector3(patrolB, transform.position.y, 0),
            0.15f
        );

#endif
    }


    private bool PlayerInSight()
    {
        Vector2 direction =
            transform.localScale.x > 0
            ? Vector2.right
            : Vector2.left;

        Vector2 center =
            (Vector2)boxCollider.bounds.center +
            direction * detectionSizeOffset * colliderDistance;

        Collider2D hit = Physics2D.OverlapBox(
            center,
            new Vector2(
                detectionSize,
                boxCollider.bounds.size.y
            ),
            0,
            playerLayer
        );

        return hit != null;
    }

    private void CalculateCorridor()
    {
        Vector2 origin = (Vector2)transform.position + Vector2.up * 0.2f;

        RaycastHit2D hitLeft =
            Physics2D.Raycast(origin, Vector2.left, maxRange, groundMask);

        RaycastHit2D hitRight =
            Physics2D.Raycast(origin, Vector2.right, maxRange, groundMask);

        float leftDist = hitLeft.collider ? hitLeft.distance : maxRange;
        float rightDist = hitRight.collider ? hitRight.distance : maxRange;

        corridorLeft = transform.position.x - leftDist + wallPadding;
        corridorRight = transform.position.x + rightDist - wallPadding;
    }

    private void PickPatrolPoints()
    {
        patrolA = Random.Range(corridorLeft, corridorRight);
        patrolB = Random.Range(corridorLeft, corridorRight);

        if (Mathf.Abs(patrolA - patrolB) < minRange)
        {
            patrolB = patrolA +
                Mathf.Sign(Random.value - 0.5f) * minRange;

            patrolB = Mathf.Clamp(
                patrolB,
                corridorLeft,
                corridorRight
            );
        }
    }
}
