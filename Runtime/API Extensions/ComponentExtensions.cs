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
    }
}
