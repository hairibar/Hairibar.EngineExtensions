using UnityEditor;
using UnityEngine;

namespace Hairibar.EngineExtensions.Editor
{
    /// <summary>
    /// Shared functionality for draggind assets to the Scene view
    /// </summary>
    public static class SceneDragAndDrop
    {
        static GameObject[] cameraIgnoreArray = new GameObject[1];

        /// <summary>
        /// Looks for a component T in the object under the cursor in the SceneView. Call from OnSceneDrag, and pass Event.current. Looks in the object and its children first, down from the root after.
        /// </summary>
        public static T GetAssignTarget<T>(Event e) where T:Component
        {
            cameraIgnoreArray[0] = SceneView.currentDrawingSceneView.camera.gameObject;

            GameObject go = HandleUtility.PickGameObject(e.mousePosition, true, cameraIgnoreArray);
            T target = null;

            if (go)
            {
                target = go.GetComponentInChildren<T>();
                if (!target)
                {
                    target = go.transform.root.GetComponentInChildren<T>();
                }
            }

            return target;
        }
    }
}


