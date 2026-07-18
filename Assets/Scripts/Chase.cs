using UnityEngine;

public class Chase : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] private float chaseSpeed = 4f;
    [SerializeField] private Rigidbody2D rb;

    private Enemy01Attack attack;
    private Transform player;

    public float FacingDirection { get; private set; } = 1f;


    private void Awake()
    {
        attack = GetComponent<Enemy01Attack>();
    }


    private void Start()
    {
        player = GameManagerSingleton.Instance.GetPlayer();
    }


    private void FixedUpdate()
    {
        if (player == null)
            return;


        // Attack script controls movement during attack
        if (attack.IsAttacking)
        {
            Debug.Log("CHASE BLOCKED");
            return;
        }


        float xDifference =
            player.position.x - transform.position.x;


        // Face player
        if (Mathf.Abs(xDifference) > 0.01f)
        {
            FacingDirection = Mathf.Sign(xDifference);

            Vector3 scale = transform.localScale;

            scale.x =
                Mathf.Abs(scale.x) * FacingDirection;

            transform.localScale = scale;
        }


        // Start attack
        if (attack.PlayerInRange())
        {
            rb.linearVelocity = new Vector2(
                0f,
                rb.linearVelocity.y
            );


            if (attack.CanAttack)
            {
                StartCoroutine(
                    attack.LungeAttack(player.position)
                );
            }

            return;
        }


        // Chase player
        Vector2 velocity = rb.linearVelocity;

        velocity.x =
            FacingDirection * chaseSpeed;

        rb.linearVelocity = velocity;
    }
}