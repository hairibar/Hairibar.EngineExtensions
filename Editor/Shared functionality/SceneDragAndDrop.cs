using UnityEditor;
using UnityEngine;

namespace Hairibar.EngineExtensions.Editor
{
    /// <summary>
    /// Shared functionality for dragging assets to the Scene view.
    /// </summary>
    public static class SceneDragAndDrop
    {
        static GameObject[] cameraIgnoreArray = new GameObject[1];

        /// <summary>
        /// Looks for a component T in the object under the cursor in the SceneView. 
        /// Must be called called from OnSceneDrag, and Event.current must be passed. 
        /// Looks in the object and its children first, and down from the root after.
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