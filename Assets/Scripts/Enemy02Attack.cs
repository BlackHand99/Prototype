using System.Collections;
using UnityEngine;

public class Enemy02Attack : MonoBehaviour
{
    [SerializeField] private GameObject bombPrefab;
    [SerializeField] private Transform throwPoint;

    [Header("Attack Settings")]
    [SerializeField] private float throwForce = 8f;
    [SerializeField] private float attackCooldown = 2f;
    [SerializeField] private float rangeW = 2f;
    [SerializeField] private float rangeH = 2f;
    [SerializeField] private LayerMask playerLayer;
    [SerializeField] private Vector2 boxOffset = new Vector2(1f, -0.5f);

    private Transform player;
    public bool isCooldownActive;

    private void Awake()
    {
        isCooldownActive = true;
    }

    private void Start()
    {
        player = GameManagerSingleton.Instance.GetPlayer();
    }

    private IEnumerator AttackCooldown()
    {
        isCooldownActive = false;

        yield return new WaitForSeconds(attackCooldown);

        isCooldownActive = true;
    }

    private void Update()
    {
        if (player == null)
            return;

        if (PlayerInRange() && isCooldownActive)
        {
            ThrowBomb();
            StartCoroutine(AttackCooldown());
        }
    }

    private bool PlayerInRange()
    {
        Vector2 boxCenter =
            (Vector2)transform.position +
            Vector2.right * transform.localScale.x * boxOffset.x +
            Vector2.up * boxOffset.y;

        Vector2 boxSize = new Vector2(rangeW, rangeH);

        Collider2D hit = Physics2D.OverlapBox(
            boxCenter,
            boxSize,
            0f,
            playerLayer
        );

        return hit != null;
    }

    private void ThrowBomb()
    {
        GameObject bomb = Instantiate(
            bombPrefab,
            throwPoint.position,
            Quaternion.identity
        );

        Rigidbody2D bombRb = bomb.GetComponent<Rigidbody2D>();

        if (bombRb == null)
            return;

        Vector2 direction =
            ((Vector2)player.position - (Vector2)throwPoint.position).normalized;

        bombRb.linearVelocity = direction * throwForce;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;

        Vector2 boxCenter =
            (Vector2)transform.position +
            Vector2.right * transform.localScale.x * boxOffset.x +
            Vector2.up * boxOffset.y;

        Vector2 boxSize = new Vector2(rangeW, rangeH);

        Gizmos.DrawWireCube(boxCenter, boxSize);
    }
}