using System;
using System.Reflection;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEditor;
using UnityEditor.Build;
using UnityEditor.Build.Reporting;

namespace Hairibar.EngineExtensions.Editor
{
    /// <summary>
    /// Processes RemoveInBuildsAttribute, Re 
    /// </summary>
    internal class RemoveInBuildsProcesser : IPreprocessBuildWithReport, IProcessSceneWithReport, IPostprocessBuildWithReport
    {
        public int callbackOrder
        {
            get { return -1; }
        }

        private bool isReleaseBuild;
        private int undoGroup;

        #region Events
        public void OnPreprocessBuild(BuildReport report)
        {
            isReleaseBuild = !report.summary.options.HasFlag(BuildOptions.Development);

            undoGroup = Undo.GetCurrentGroup();
            Undo.SetCurrentGroupName("Destroy all prefab components with [RemoveInRelease] and [RemoveInBuilds]");

            PrefabUtilityExtensions.ForEachPrefab((GameObject go) =>
            {
                ProcessGameObject(go, true);
            });
        }

        public void OnProcessScene(Scene scene, BuildReport report)
        {
            ProcessScene(scene);
        }

        public void OnPostprocessBuild(BuildReport report)
        {
            Undo.RevertAllDownToGroup(undoGroup);
        }
        #endregion

        #region Actions
        private void ProcessScene(Scene scene)
        {
            GameObject[] GOs = scene.GetRootGameObjects();

            for (int i = 0; i < GOs.Length; i++)
            {
                ProcessGameObject(GOs[i], false);
            }
        }

        private void ProcessGameObject(GameObject go, bool useUndoActions)
        {
            Component[] components = go.GetComponentsInChildren<Component>();

            for (int i = 0; i < components.Length; i++)
            {
                Component component = components[i];
                if (!component) continue;

                if (component is RemoveInBuilds) ProcessRemoveInBuildsComponent(component as RemoveInBuilds, useUndoActions);
                if (!component) continue;

                if (ComponentHasAttribute<RemoveInBuildsAttribute>(component) || (isReleaseBuild && ComponentHasAttribute<RemoveInReleaseAttribute>(component)))
                {
                    DestroyObject(component, useUndoActions);
                }
            }
        }

        private void ProcessRemoveInBuildsComponent(RemoveInBuilds component, bool asUndoAction)
        {
            bool shouldDestroyGameObject;

            if (!isReleaseBuild && component.isAllowedInDevBuilds) shouldDestroyGameObject = false;
            else shouldDestroyGameObject = true;

            shouldDestroyGameObject = !isReleaseBuild && component.isAllowedInDevBuilds;

            if (shouldDestroyGameObject) DestroyObject(component.gameObject, asUndoAction);
            else DestroyObject(component, asUndoAction);
        }

        private void DestroyObject(UnityEngine.Object myObject, bool asUndoAction)
        {
            if (asUndoAction) Undo.DestroyObjectImmediate(myObject);
            else GameObject.DestroyImmediate(myObject);
        }
        #endregion

        #region Utilities
        private bool ComponentHasAttribute<T>(Component component) where T : Attribute
        {
            Type type = component.GetType();
            T att = type.GetCustomAttribute<T>();

            return att != null;
        }
        #endregion
    }
}
