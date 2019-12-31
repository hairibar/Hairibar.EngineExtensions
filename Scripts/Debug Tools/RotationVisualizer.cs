using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Hairibar.EngineExtensions
{
    public class RotationVisualizer : MonoBehaviour
    {
        [Range(0, 1)]
        public float lineLength = 0.2f;
        public Color color = Color.yellow;

        new Transform transform;

        void LateUpdate()
        {
            Debug.DrawLine(transform.position, transform.position + transform.forward * lineLength, color);
        }

        private void Awake()
        {
            transform = GetComponent<Transform>();
        }
    }

}
