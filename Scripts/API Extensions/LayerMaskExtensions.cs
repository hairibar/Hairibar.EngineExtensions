using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Hairibar.EngineExtensions
{
    public static class LayerMaskExtensions
    {
        public static bool LayerIsEnabled(this LayerMask mask, int layer)
        {
            return mask == (mask | (1 << layer));
        }
    }

}
