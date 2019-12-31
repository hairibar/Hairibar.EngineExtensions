using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace Hairibar.EditorExtensions
{
    /// <summary>
    /// Provides access to the Tag and Layer manager.
    /// </summary>
    public static class TagAndLayerManager
    {
        public static string[] Tags
        {
            get
            {
                SerializedProperty sp = TagManager.FindProperty("tags");

                List<string> tags = new List<string>();
                string tag;
                for (int i = 0; i < sp.arraySize; i++)
                {
                    tag = sp.GetArrayElementAtIndex(i).stringValue;
                    if (!string.IsNullOrEmpty(tag)) tags.Add(tag);
                }

                return tags.ToArray();
            }
        }
        public static string[] Layers
        {
            get
            {
                SerializedProperty sp = TagManager.FindProperty("layers");

                List<string> layers = new List<string>();
                string layer;
                for (int i = 0; i < sp.arraySize; i++)
                {
                    layer = sp.GetArrayElementAtIndex(i).stringValue;
                    if (!string.IsNullOrEmpty(layer)) layers.Add(layer);
                }

                return layers.ToArray();
            }
        }


        private static SerializedObject TagManager
        {
            get
            {
                if (_tagManager == null) LoadTagManager();
                return _tagManager;
            }
        }
        private static SerializedObject _tagManager = null;

        private static void LoadTagManager()
        {
            _tagManager = new SerializedObject(AssetDatabase.LoadAllAssetsAtPath("ProjectSettings/TagManager.asset")[0]);
        }
    }

}
