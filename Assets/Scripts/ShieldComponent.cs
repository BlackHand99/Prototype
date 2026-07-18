using UnityEngine;

public class ShieldComponent : MonoBehaviour
{
    public bool IsFrontHit(Vector2 projectilePosition)
    {
        float facingDirection =
            Mathf.Sign(transform.root.localScale.x);

        float projectileDirection =
            Mathf.Sign(
                projectilePosition.x -
                transform.position.x
            );

        return facingDirection == projectileDirection;
    }
}