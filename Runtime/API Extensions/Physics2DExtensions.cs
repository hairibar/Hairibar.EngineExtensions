#if PHYSICS_2D_MODULE_PRESENT
using UnityEngine;

namespace Hairibar.EngineExtensions
{
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

        public static int ConeCast(Vector2 origin, Vector2 centerDirection, float angle, float distance, int rayCount, LayerMask layerMask, RaycastHit2D[] hits, bool debugRays = false)
        {
            int nextHitIndex = 0;

            float startAngle = -angle / 2 * Mathf.Deg2Rad;
            float endAngle = angle / 2 * Mathf.Deg2Rad;

            for (int rayIndex = 0; rayIndex < rayCount; rayIndex++)
            {
                if (nextHitIndex >= hits.Length) return nextHitIndex - 1;

                Vector2 rayDirection = GetRayDirection(rayIndex);
                RaycastHit2D hit = Physics2D.Raycast(origin, rayDirection, distance, layerMask);

                if (hit)
                {
                    hits[nextHitIndex] = hit;
                    nextHitIndex++;
                }

                if (debugRays)
                {
                    Debug.DrawRay(origin, rayDirection * (hit ? hit.distance : distance),
                        hit ? Color.red : Color.white, 1);
                }
            }

            return nextHitIndex - 1;


            Vector2 GetRayDirection(int rayIndex)
            {
                float t = rayIndex / (float) rayCount;
                float rayAngle = Mathf.Lerp(startAngle, endAngle, t);

                Vector2 rayDirection = Vector2Extensions.Rotate(centerDirection, rayAngle);
                return rayDirection;
            }
        }
    }
}
#endif