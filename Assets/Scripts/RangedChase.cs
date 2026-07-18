using UnityEngine;

public class RangedChase : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] private float moveSpeed = 3f;

    [Header("Ranges")]
    [SerializeField] private float retreatDistance = 4f;
    [SerializeField] private float approachDistance = 9f;
    [SerializeField] public float attackDistance = 8f;

    [SerializeField] private Rigidbody2D rb;

    private RangedEnemyAttack attack;
    private Transform player;

    private void Awake()
    {
        attack = GetComponent<RangedEnemyAttack>();
    }

    private void Start()
    {
        player = GameManagerSingleton.Instance.GetPlayer();
    }

    private void FixedUpdate()
    {
        if (player == null)
            return;

        Vector2 playerPos = player.position;

        float distance = Vector2.Distance(
            rb.position,
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

        if (distance > approachDistance)
        {
            // Move closer
            velocity.x = Mathf.Sign(moveDir) * moveSpeed;
        }
        else if (distance < retreatDistance)
        {
            // Move away
            velocity.x = -Mathf.Sign(moveDir) * moveSpeed;
        }
        else
        {
            // Stay in ideal range
            velocity.x = 0;
        }

        rb.linearVelocity = velocity;
    }
}