using UnityEngine;

namespace Hairibar.EngineExtensions
{
    /// <summary>
    /// Do not overuse.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public abstract class MonoBehaviourSingleton<T> : MonoBehaviour where T : MonoBehaviourSingleton<T>
    {
        public static T Instance
        {
            get
            {
                CreateIfNecessary();
                return _instance;
            }
        }
        static T _instance;

        static void CreateIfNecessary()
        {
            if (!_instance)
            {
                _instance = FindObjectOfType<T>();
            }

            if (!_instance)
            {
                GameObject go = new GameObject(typeof(T).Name);
                _instance = go.AddComponent<T>();
            }
        }
    }
}
