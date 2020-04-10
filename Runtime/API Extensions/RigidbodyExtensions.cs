#if PHYSICS_MODULE_PRESENT
using UnityEngine;

namespace Hairibar.EngineExtensions
{
    public static class RigidbodyExtensions
    {
        /// <summary>
        /// Performs a rigidbody.SweepTestAll, filters the results according to the given LayerMask and returns whether there is still a hit.
        /// </summary>
        public static bool SweepTestWithLayerMask(this Rigidbody rb, Vector3 direction, float maxDistance, QueryTriggerInteraction queryTriggerInteraction, LayerMask layerMask)
        {
            RaycastHit[] hits = rb.SweepTestAll(direction, maxDistance, queryTriggerInteraction);

            for (int i = 0; i < hits.Length; i++)
            {
                int layer = hits[i].collider.gameObject.layer;

                if (layerMask.LayerIsEnabled(layer)) return true;
            }

            return false;
        }
    }
}
#endif