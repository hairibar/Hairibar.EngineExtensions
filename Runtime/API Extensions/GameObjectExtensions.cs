using UnityEngine;

namespace Hairibar.EngineExtensions
{
    public static class GameObjectExtensions
    {
        public static bool TryGetComponentInParent<T>(this GameObject gameObject, out T result)
        {
            result = gameObject.GetComponentInParent<T>();
            return result != null;
        }

        public static bool TryGetComponentInChildren<T>(this GameObject gameObject, out T component, bool includeInactive = false)
        {
            component = gameObject.GetComponentInChildren<T>(includeInactive);
            return component != null;
        }
    }
}
