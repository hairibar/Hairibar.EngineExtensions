using UnityEngine;

namespace Hairibar.EngineExtensions
{
    public class CopyPosition : MonoBehaviour
    {
        public Transform TransformToCopy
        {
            get => _trasformToCopy;
            set
            {
                _trasformToCopy = value;
                enabled = true;
            }
        }
        Transform _trasformToCopy;

        void LateUpdate()
        {
            if (TransformToCopy)
            {
                transform.position = TransformToCopy.position;
            }
            else
            {
                enabled = false;
            }
        }
    }
}
