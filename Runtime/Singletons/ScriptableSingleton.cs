using System.Linq;
using UnityEngine;

namespace Hairibar.EngineExtensions
{
    /// <summary>
    /// Provides a globally available instance of a ScriptableObject. 
    /// The instance must have been manually added to the preloaded assets list in PlayerSettings.
    /// </summary>
    public abstract class ScriptableSingleton<T> : ScriptableObject where T : ScriptableSingleton<T>
    {
        public static T Instance
        {
            get
            {
                if (!_instance)
                {
                    LoadInstance();
                }
                return _instance;
            }
        }

        static void LoadInstance()
        {
#if UNITY_EDITOR
            var preloadedAssets = UnityEditor.PlayerSettings.GetPreloadedAssets();
            _instance = preloadedAssets.First(asset => asset.GetType() == typeof(T)) as T;
            if (!_instance)
            {
                Debug.LogError($"Requested a ScriptableSingleton (type: {typeof(T).Name}) isn't in Preloaded Assets. Add the ScriptableObject in PlayerSettings -> Other -> PreloadedAssets.");
            }
#else

            var objects = Resources.FindObjectsOfTypeAll<T>();
            _instance = objects.FirstOrDefault();

            if (!_instance)
            {
                Debug.LogError("Could not find an instance for a ScriptableSingleton.");
            }
#endif
        }

        static T _instance;
    }
}
