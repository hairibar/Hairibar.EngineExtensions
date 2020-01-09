using UnityEngine;

namespace Hairibar.EngineExtensions.Debugging
{
    /// <summary>
    /// Recursively draws lines to visualize a Transform hierarchy.
    /// </summary>
    [AddComponentMenu("Debug/Skeleton Visualizer"), RemoveInRelease]
    public class SkeletonVisualizer : MonoBehaviour
    {
        public Transform rootBone;
        public Color boneColor = Color.red;
        public Color leafDirectionColor = Color.green;
        [Range(0.05f, 1f)] public float leafDirectionLength = 0.2f;

        private void Update()
        {
            if (Application.isPlaying)
            {
                if (!rootBone)
                {
                    Debug.LogError("There is no root bone assigned at SkeletonVisualizer.", this);
                    return;
                }
                DrawBonesRecursively(rootBone);
            }
        }

        private void DrawBonesRecursively(Transform bone)
        {
            int childCount = bone.childCount;

            if (childCount > 0)
            {
                for (int i = 0; i < bone.childCount; i++)
                {
                    Transform child = bone.GetChild(i);

                    Debug.DrawLine(bone.position, child.position, boneColor);

                    DrawBonesRecursively(child);
                }
            }
            else
            {
                Debug.DrawRay(bone.position, bone.up * leafDirectionLength, leafDirectionColor);
            }
        }
    }
}
