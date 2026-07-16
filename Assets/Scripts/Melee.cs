using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;

public class Melee : MonoBehaviour
{
    public Transform meleePoint;
    public float attackRange = 1f;
    public LayerMask enemy;
    [SerializeField] private int damage = 2;
    private bool canMelee = true;
    public void OnMelee(InputAction.CallbackContext context)
    {
        if(context.performed && canMelee)
        {
            MeleeAttack();
            StartCoroutine(Cooldown());
        }
    }

    private void MeleeAttack()
    {
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(meleePoint.position, attackRange, enemy);

        foreach (Collider2D enemy in hitEnemies)
        {
            if (enemy.TryGetComponent<EnemyHealth>(out var damageEnemy))
            {
                damageEnemy.TakeDamage(damage);
            }
        }
    }
    private IEnumerator Cooldown()
    {
        canMelee = false;
        yield return new WaitForSeconds(1f);
        canMelee = true;
    }

    private void OnDrawGizmos()
    {
        Draw();
    }

    private void OnDrawGizmosSelected()
    {
        Draw();
    }

    private void Draw()
    {
        if (meleePoint == null) return;

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(meleePoint.position, attackRange);
    }
}