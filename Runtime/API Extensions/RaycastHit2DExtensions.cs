#if PHYSICS_2D_MODULE_PRESENT
using UnityEngine;

namespace Hairibar.EngineExtensions
{
    public static class RaycastHit2DExtensions
    {
        public static bool StartedAtCollider(this RaycastHit2D hit)
        {
            return hit && hit.fraction == 0;
        }
    }
}
#endif