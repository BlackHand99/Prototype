using UnityEngine;

public class RangedChase : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] private float moveSpeed = 3f;

    [Header("Ranges")]
    [SerializeField] private float retreatDistance = 4f;
    [SerializeField] public float attackDistance = 8f;

    [SerializeField] private Rigidbody2D rb;

    private RangedEnemyAttack attack;

    private bool IsChasing =>
        GameManagerSingleton.Instance.GlobalAlert;

    private void Awake()
    {
        attack = GetComponent<RangedEnemyAttack>();
    }

    private void Update()
    {
        if (!IsChasing)
            return;

        Vector3 playerPos =
            GameManagerSingleton.Instance.GetPlayerPosition();

        float distance =
            Vector2.Distance(
                transform.position,
                playerPos
            );

        float moveDir =
            playerPos.x - transform.position.x;

        // Face player
        if (Mathf.Abs(moveDir) > 0.01f)
        {
            Vector3 scale = transform.localScale;

            scale.x =
                Mathf.Abs(scale.x) *
                Mathf.Sign(moveDir);

            transform.localScale = scale;
        }

        Vector2 velocity = rb.linearVelocity;

        if (distance > attackDistance)
        {
            // Move closer
            float dir = Mathf.Sign(moveDir);

            velocity.x = dir * moveSpeed;
        }
        else if (distance < retreatDistance)
        {
            // Move away
            float dir = -Mathf.Sign(moveDir);

            velocity.x = dir * moveSpeed;
        }
        else
        {
            // Stay in shooting range
            velocity.x = 0;
        }

        rb.linearVelocity = velocity;
    }
}
