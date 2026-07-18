using UnityEngine;

public class ChaseFlying : MonoBehaviour
{
    [SerializeField] private float moveSpeed;
    [SerializeField] private float enemyScale = 0.01f;

    [SerializeField] private float yVelocity;
    [SerializeField] private float stiffness = 10f;
    [SerializeField] private float damping = 2f;

    [Header("Constraints")]
    [SerializeField] private float flyHeight = 1.5f;
    [SerializeField] private float attackRange = 2f;

    private Transform player;

    private void Start()
    {
        player = GameManagerSingleton.Instance.GetPlayer();
    }

    private void FixedUpdate()
    {
        if (player == null)
            return;

        Vector2 playerPos = player.position;

        Vector3 pos = transform.position;

        // Maintain fixed world-space flight height
        float targetY = flyHeight;

        float displacement = targetY - pos.y;

        float force = displacement * stiffness;

        yVelocity += force * Time.fixedDeltaTime;

        yVelocity *=
            1f / (1f + damping * Time.fixedDeltaTime);

        pos.y += yVelocity * Time.fixedDeltaTime;

        // Follow player horizontally
        float xDiff = playerPos.x - pos.x;

        if (Mathf.Abs(xDiff) > attackRange)
        {
            pos.x +=
                Mathf.Sign(xDiff) *
                moveSpeed *
                Time.fixedDeltaTime;
        }

        transform.position = pos;

        // Face player
        if (Mathf.Abs(xDiff) > 0.01f)
        {
            transform.localScale = new Vector3(
            Mathf.Sign(xDiff) * enemyScale,
            enemyScale,
            enemyScale
            );
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;

        Vector3 left =
            new Vector3(transform.position.x - attackRange, transform.position.y, 0);

        Vector3 right =
            new Vector3(transform.position.x + attackRange, transform.position.y, 0);

        Gizmos.DrawLine(left, right);

        Gizmos.color = Color.green;

        Vector3 heightLeft =
            new Vector3(transform.position.x - 5f, flyHeight, 0);

        Vector3 heightRight =
            new Vector3(transform.position.x + 5f, flyHeight, 0);

        Gizmos.DrawLine(heightLeft, heightRight);

        Gizmos.DrawSphere(
            new Vector3(transform.position.x, flyHeight, 0),
            0.1f
        );
    }
}