using UnityEngine;

public class ShieldBlocker : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Destroying: " + other.name);

        Destroy(other.gameObject);
    }
}
