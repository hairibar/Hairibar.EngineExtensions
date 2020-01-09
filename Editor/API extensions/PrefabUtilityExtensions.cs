using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace Hairibar.EngineExtensions.Editor
{
    public static class PrefabUtilityExtensions
    {
        public static string[] GetAllPrefabPaths()
        {
            string[] temp = AssetDatabase.GetAllAssetPaths();
            List<string> result = new List<string>();
            foreach (string s in temp)
            {
                if (s.Contains(".prefab")) result.Add(s);
            }
            return result.ToArray();
        }

        public static void ForEachPrefab(System.Action<GameObject> action)
        {
            string[] allPrefabPaths = GetAllPrefabPaths();

            foreach (string prefabPath in allPrefabPaths)
            {
                GameObject myPrefab = AssetDatabase.LoadAssetAtPath<GameObject>(prefabPath);
                action(myPrefab);
            }
        }
    }
}
