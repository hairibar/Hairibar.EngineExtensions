using UnityEngine;

namespace Hairibar.EngineExtensions
{
    /// <summary>
    /// Flags the GameObject for destruction at build time. Can optionally be allowed in development builds.
    /// </summary>
    [ExecuteAlways, DisallowMultipleComponent, AddComponentMenu("Build System/Remove In Builds")]
    public class RemoveInBuilds : MonoBehaviour
    {
#if UNITY_EDITOR
        #region Constants
        private const string REMOVED_IN_BUILD_PREFIX = "[REMOVED IN BUILDS] ";
        private const string REMOVED_IN_RELEASE_PREFIX = "[REMOVED IN RELEASE] ";
        #endregion
#endif
        //This flag must be included when compiling for a build, as Unity has already serialized it and expects it to be there.
        //That's why it's not UNITY_EDITOR only.
        public bool isAllowedInDevBuilds = false;
      
#if UNITY_EDITOR
        private string OriginalName {
            get
            {
                string currentName = gameObject.name;
                string currentPrefix = "";

                if (currentName.StartsWith(REMOVED_IN_BUILD_PREFIX)) currentPrefix = REMOVED_IN_BUILD_PREFIX;
                else if (currentName.StartsWith(REMOVED_IN_RELEASE_PREFIX)) currentPrefix = REMOVED_IN_RELEASE_PREFIX;

                return currentName.Substring(currentPrefix.Length, currentName.Length - currentPrefix.Length);
            }
        }

        private void OnValidate()
        {
            if (isAllowedInDevBuilds)
            {
                gameObject.name = REMOVED_IN_RELEASE_PREFIX + OriginalName;
            }
            else
            {
                gameObject.name = REMOVED_IN_BUILD_PREFIX + OriginalName;
            }
        }
#endif
    }
}

