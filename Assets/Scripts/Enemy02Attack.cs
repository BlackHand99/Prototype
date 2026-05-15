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

    private float cooldownTimer;
    public bool isCooldownActive;

    private void Awake()
    {
        isCooldownActive = true;
    }

    public IEnumerator CooldownTimer()
    {
        isCooldownActive = false;
        yield return new WaitForSeconds(attackCooldown);
        isCooldownActive = true;
    }

    void Update()
    {
        if (GameManagerSingleton.Instance == null) return;

        if (PlayerInRange() && isCooldownActive)
        {
            ThrowBomb();
            StartCoroutine(CooldownTimer());
        }
    }

    bool PlayerInRange()
    {
        Vector2 boxCenter = (Vector2)transform.position + Vector2.right * transform.localScale.x * boxOffset.x + Vector2.up * boxOffset.y;

        Vector2 boxSize = new Vector2(rangeW, rangeH); // width, height of attack range

        Collider2D hit = Physics2D.OverlapBox(boxCenter, boxSize, 0f, playerLayer);

        return hit != null;
    }

    void ThrowBomb()
    {
        GameObject bomb = Instantiate(bombPrefab, throwPoint.position, Quaternion.identity);

        Rigidbody2D rb = bomb.GetComponent<Rigidbody2D>();

        Vector2 direction = (GameManagerSingleton.Instance.GetPlayerPosition() - throwPoint.position).normalized;

        rb.linearVelocity = direction * throwForce;
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;

        Vector2 boxCenter = (Vector2)transform.position + Vector2.right * transform.localScale.x * boxOffset.x + Vector2.up * boxOffset.y;
        Vector2 boxSize = new Vector2(rangeW, rangeH);

        Gizmos.DrawWireCube(boxCenter, boxSize);
    }

}
