using UnityEngine;

namespace Hairibar.EngineExtensions
{
    public static class TransformExtensions
    {
        /// <summary>
        /// Searches for a child with a name identical to name. Searches recursively down the hierarchy. Returns null if no match is found. WARNING: Pretty slow, don't use during gameplay.
        /// </summary>
        public static Transform FindChildRecursively(this Transform parent, string name)
        {
            Transform child;
            Transform match;
            for (int i = 0; i < parent.childCount; i++)
            {
                child = parent.GetChild(i);

                if (child.name == name) return child;
                else
                {
                    match = child.FindChildRecursively(name);
                    if (match) return match;
                }
            }

            return null;
        }
    }
}

