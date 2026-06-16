using UnityEngine;

public class Chase : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] private float chaseSpeed = 4f;

    [SerializeField] private Rigidbody2D rb;

    private Vector3 initScale;

    private Enemy01Attack attack;
    private bool IsChasing =>
        GameManagerSingleton.Instance.GlobalAlert;

    private void Awake()
    {
        initScale = transform.localScale;
        attack = GetComponent<Enemy01Attack>();
    }

    private void Update()
    {
        if (!IsChasing)
            return;

        if (attack != null && attack.IsAttacking)
            return;

        Vector3 playerPos =
            GameManagerSingleton.Instance.GetPlayerPosition();

        float moveDir = playerPos.x - transform.position.x;

        Vector2 direction =
            (playerPos - transform.position).normalized;


        if (!attack.PlayerInRange())
        {
            Vector2 velocity = rb.linearVelocity;

            velocity.x = direction.x * chaseSpeed;

            rb.linearVelocity = velocity;
        }
        else
        {
            rb.linearVelocity = new Vector2(
                0,
                rb.linearVelocity.y
            );

            if (attack.PlayerInRange() && attack.CanAttack)
            {

                StartCoroutine(attack.LungeAttack(playerPos));
            }
        }

        //sprite flip when chasing
        if (Mathf.Abs(moveDir) > 0.01f)
        {
            Vector3 scale = transform.localScale;

            scale.x = Mathf.Abs(scale.x) * Mathf.Sign(moveDir);

            transform.localScale = scale;
        }
    }
}
