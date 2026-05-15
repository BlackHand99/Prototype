using UnityEngine;

public class ChaseFlying : MonoBehaviour
{
    [SerializeField] private float moveSpeed;
    [SerializeField] private BoxCollider2D boxCollider;
    [SerializeField] private float yVelocity;
    [SerializeField] private float stiffness = 10f;
    [SerializeField] private float damping = 2f;

    [Header("Constraints")]
    [SerializeField] private float flyHeight = 1.5f;
    [SerializeField] private float attackRange = 2f;


    private void FixedUpdate()
    {
        Vector3 playerPos = GameManagerSingleton.Instance.GetPlayerPosition();

        Vector3 pos = transform.position;

        float targetY = playerPos.y + flyHeight;

        float displacement = targetY - pos.y;

        float force = displacement * stiffness;
        yVelocity += force * Time.fixedDeltaTime;

        yVelocity *= 1f / (1f + damping * Time.fixedDeltaTime);

        pos.y += yVelocity * Time.fixedDeltaTime;

        float xDiff = playerPos.x - pos.x;

        if (Mathf.Abs(xDiff) > attackRange)
        {
            pos.x += Mathf.Sign(xDiff) * moveSpeed * Time.fixedDeltaTime;
        }


        transform.position = pos;

        float dir = playerPos.x - transform.position.x;
        if (dir != 0)
            transform.localScale = new Vector3(Mathf.Sign(dir), 1, 1);
    }
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;

        Vector3 left = new Vector3(transform.position.x - attackRange, transform.position.y, 0);
        Vector3 right = new Vector3(transform.position.x + attackRange, transform.position.y, 0);

        Gizmos.DrawLine(left, right);

        Gizmos.color = Color.green;

        Vector3 playerPos = GameManagerSingleton.Instance != null
            ? GameManagerSingleton.Instance.GetPlayerPosition()
            : transform.position;

        float targetY = playerPos.y + flyHeight;

        Vector3 heightLeft = new Vector3(transform.position.x - 5f, targetY, 0);
        Vector3 heightRight = new Vector3(transform.position.x + 5f, targetY, 0);

        Gizmos.DrawLine(heightLeft, heightRight);

        Gizmos.DrawSphere(new Vector3(transform.position.x, targetY, 0), 0.1f);
    }
}


