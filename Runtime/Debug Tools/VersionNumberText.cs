using UnityEngine;
using UnityEngine.UI;

namespace Hairibar.EngineExtensions
{
    [RequireComponent(typeof(Text))]
    public class VersionNumberText : MonoBehaviour
    {
        void Start()
        {
            if (!Application.isEditor && Debug.isDebugBuild)
            {
                GetComponent<Text>().text = Application.version;
            }
            else
            {
                Destroy(gameObject);
            }
        }
    }
}
