using System.Collections;
using UnityEngine;

public class EnemyPatrol : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rb;

    // Patrol corridor
    private float corridorLeft;
    private float corridorRight;

    [SerializeField] private float maxRange = 8f;
    [SerializeField] private float minRange = 2f;

    // Prevent collider getting stuck in wall
    [SerializeField] private float wallPadding = 0.5f;

    private float patrolA;
    private float patrolB;
    private bool movingToB = true;

    [SerializeField] private LayerMask groundMask;

    [Header("Movement")]
    [SerializeField] private float patrolSpeed = 2f;

    private bool canMove;

    private Vector3 initScale;

    private void Awake()
    {
        initScale = transform.localScale;
    }

    private void Start()
    {
        StartCoroutine(StartMoveDelay());

        CalculateCorridor();
        PickPatrolPoints();
    }

    private void FixedUpdate()
    {
        if (!canMove)
            return;

        float target = movingToB ? patrolB : patrolA;

        float moveDir = Mathf.Sign(target - rb.position.x);

        Vector2 velocity = rb.linearVelocity;
        velocity.x = moveDir * patrolSpeed;
        rb.linearVelocity = velocity;

        // Switch patrol target
        if (Mathf.Abs(rb.position.x - target) < 0.05f)
        {
            movingToB = !movingToB;
        }

        // Face movement direction
        if (Mathf.Abs(moveDir) > 0.01f)
        {
            transform.localScale = new Vector3(
                Mathf.Abs(initScale.x) * Mathf.Sign(moveDir),
                initScale.y,
                initScale.z
            );
        }
    }

    private IEnumerator StartMoveDelay()
    {
        yield return new WaitForSeconds(0.2f);
        canMove = true;
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

    private void OnDrawGizmos()
    {
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
}