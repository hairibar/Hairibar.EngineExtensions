using System.Collections.Generic;
using UnityEditor;

namespace Hairibar.EngineExtensions.Editor
{
    /// <summary>
    /// Provides access to the Tag and Layer manager.
    /// </summary>
    public static class TagAndLayerManager
    {
        /// <summary>
        /// An array of all the tags in the project.
        /// </summary>
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

        /// <summary>
        /// An array of all the layers in the project.
        /// </summary>
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

        //The tag manager needs to be loaded as a SerializedObject. 
        //We lazily initialize it.
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
