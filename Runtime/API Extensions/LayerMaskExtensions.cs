using UnityEngine;

namespace Hairibar.EngineExtensions
{
    public static class LayerMaskExtensions
    {
        /// <summary>
        /// Checks whether the given layer is enabled in the given LayerMask.
        /// </summary>
        public static bool LayerIsEnabled(this LayerMask mask, int layer)
        {
            return mask == (mask | (1 << layer));
        }

        public static LayerMask Everything => ~0;
        public static LayerMask Nothing => 0;
    }
}
