using System.Collections;
using UnityEngine;

public class Enemy01Attack : MonoBehaviour
{
    [SerializeField] private float attackCooldown;
    [SerializeField] private float range;
    [SerializeField] private float colliderDistance;
    [SerializeField] private int damage;
    [SerializeField] private LayerMask playerLayer;
    [SerializeField] private BoxCollider2D boxCollider;

    private Health playerHealth;
    private Animator anim;
    private Chase chase;

    public bool isCooldownActive;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        isCooldownActive = true;
    }

    public IEnumerator CooldownTimer()
    {
        isCooldownActive = false;
        yield return new WaitForSeconds(attackCooldown);
        isCooldownActive = true;
    }

    public bool PlayerInRange()
    {
        RaycastHit2D hit = Physics2D.BoxCast(boxCollider.bounds.center + transform.right * range * transform.localScale.x * colliderDistance,
            new Vector3(boxCollider.bounds.size.x, boxCollider.bounds.size.y, boxCollider.bounds.size.z), 0, Vector2.left, 0, playerLayer);

        if (hit.collider != null)
            playerHealth = hit.collider.GetComponent<Health>();


        return hit.collider != null;
    }

    void Update()
    {
        DamagePlayer();
    }

    private void OnDrawGizmos()
    {
        var boxCollider = GetComponent<BoxCollider2D>();

        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(boxCollider.bounds.center + transform.right * range * transform.localScale.x * colliderDistance, new Vector3(boxCollider.bounds.size.x, boxCollider.bounds.size.y, boxCollider.bounds.size.z));
    }

    public void DamagePlayer()
    {
        if (PlayerInRange() && isCooldownActive)
        {
            anim.Play("meleeAttack");
            playerHealth.TakeDamage(damage);
            StartCoroutine(CooldownTimer());
        }
    }
}
