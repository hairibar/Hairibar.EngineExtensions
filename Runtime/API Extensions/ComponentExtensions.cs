using UnityEngine;

#pragma warning disable UNT0014 // Invalid type for call to GetComponent
namespace Hairibar.EngineExtensions
{
    public static class ComponentExtensions
    {
        public static bool TryGetComponentInParent<T>(this Component thisComponent, out T result)
        {
            result = thisComponent.GetComponentInParent<T>();
            return result != null;
        }

        public static bool TryGetComponentInChildren<T>(this Component thisComponent, out T result, bool includeInactive = false)
        {
            result = thisComponent.GetComponentInChildren<T>(includeInactive);
            return result != null;
        }
    }
}
