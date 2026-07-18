using System.Collections;
using UnityEngine;

public class Bomb : MonoBehaviour
{
    [SerializeField] float fuzeTime;
    [SerializeField] float explosionRadius;
    [SerializeField] int damage;
    [SerializeField] private LayerMask playerLayer;
    [SerializeField] private float knockbackForce;
    [Header("Explosion")]
    [SerializeField] private GameObject bombExplosionPrefab;
    private bool hasExploded;

    private Health playerHealth;


    private void Update()
    { 
        fuzeTime -= Time.deltaTime;

        if (fuzeTime <= 0f && !hasExploded)
        {
            Exploded();
        }
    }

    private bool Exploded()
    {
        hasExploded = true;

        if (bombExplosionPrefab != null)
        {
            Instantiate(
                bombExplosionPrefab,
                transform.position,
                Quaternion.identity
            );
        }
        else
        {
            Debug.LogError("Explosion Prefab is not assigned!");
        }

        RaycastHit2D hit = Physics2D.CircleCast(transform.position, 10f * explosionRadius, Vector2.up, 0, playerLayer);

        if (hit.collider != null)
        {
            playerHealth = hit.collider.GetComponent<Health>();

            if (playerHealth != null)
            {
                Vector2 knockbackDir =
                    ((Vector2)hit.collider.transform.position -
                    (Vector2)transform.position).normalized;

                playerHealth.TakeDamage(
                    damage,
                    knockbackDir,
                    knockbackForce
                );
            }
        }
        Destroy(gameObject);

        return hit.collider != null;
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, 10f * explosionRadius);
    }


}

