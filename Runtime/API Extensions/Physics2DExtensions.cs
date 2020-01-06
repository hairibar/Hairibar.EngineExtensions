using UnityEngine;

public static class Physics2DExtensions
{
    /// <summary>
    /// Shorthand for setting Physics2D.IgnoreCollision for every combination of the Rigidbodies' Colliders.
    /// </summary>
    public static void IgnoreCollisions(Rigidbody2D a, Rigidbody2D b, bool ignore)
    {
        int aColliderCount = a.attachedColliderCount;
        int bColliderCount = b.attachedColliderCount;

        Collider2D[] aColliders = new Collider2D[aColliderCount];
        Collider2D[] bColliders = new Collider2D[bColliderCount];

        a.GetAttachedColliders(aColliders);
        b.GetAttachedColliders(bColliders);

        for (int i = 0; i < aColliderCount; i++)
        {
            for (int j = 0; j < bColliderCount; j++)
            {
                Physics2D.IgnoreCollision(aColliders[i], bColliders[j], ignore);
            }
        }
    }
}