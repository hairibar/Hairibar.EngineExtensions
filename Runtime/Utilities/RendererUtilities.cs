using System.Collections.Generic;
using UnityEngine;

namespace Hairibar.EngineExtensions
{
    public static class RendererUtilities
    {
        public static Bounds GetTotalBounds(IReadOnlyCollection<Renderer> renderers)
        {
            Bounds totalBounds = new Bounds();
            bool isFirstRenderer = true;

            foreach (Renderer renderer in renderers)
            {
                if (isFirstRenderer)
                {
                    totalBounds = renderer.bounds;
                    isFirstRenderer = false;
                }
                else
                {
                    totalBounds.Encapsulate(renderer.bounds);
                }
            }

            return totalBounds;
        }
    }
}
