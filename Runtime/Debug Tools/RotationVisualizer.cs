using UnityEngine;

namespace Hairibar.EngineExtensions.Debugging
{
    /// <summary>
    /// Draws a line that visualizes the current rotation of the Transform.
    /// </summary>
    [AddComponentMenu("Debug/Rotation Visualizer"), RemoveInRelease]
    public class RotationVisualizer : MonoBehaviour
    {
        [Range(0, 1)]
        public float lineLength = 0.2f;
        public Color color = Color.yellow;

        private new Transform transform;

        private void LateUpdate()
        {
            Debug.DrawLine(transform.position, transform.position + transform.forward * lineLength, color);
        }

        private void Awake()
        {
            transform = GetComponent<Transform>();
        }
    }
}
