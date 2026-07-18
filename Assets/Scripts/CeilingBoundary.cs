using UnityEngine;

public class CeilingBoundary : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("Boundary hit: " + collision.gameObject.name);
    }
}