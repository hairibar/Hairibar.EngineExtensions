using UnityEditor;
using UnityEngine;

namespace Hairibar.EngineExtensions.Serialization.Editor
{
    [CustomPropertyDrawer(typeof(SerializableStringTransformDictionary), true)]
    public class SerializableStringTransformDictionaryDrawer : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            EditorGUILayout.LabelField(label);

            SerializedProperty keys = property.FindPropertyRelative("keys");
            SerializedProperty values = property.FindPropertyRelative("values");

            values.arraySize = keys.arraySize;

            for (int i = 0; i < keys.arraySize; i++)
            {
                EditorGUILayout.BeginHorizontal();
                EditorGUILayout.PropertyField(keys.GetArrayElementAtIndex(i), GUIContent.none);
                EditorGUILayout.PropertyField(values.GetArrayElementAtIndex(i), GUIContent.none);
                EditorGUILayout.EndHorizontal();
            }

            EditorGUILayout.BeginHorizontal();

            if (GUILayout.Button("-"))
            {
                keys.arraySize = Mathf.Max(0, keys.arraySize - 1);
            }

            if (GUILayout.Button("+"))
            {
                int newElementIndex = keys.arraySize;
                keys.arraySize++;
                keys.GetArrayElementAtIndex(newElementIndex).stringValue = "Element " + newElementIndex;
            }

            EditorGUILayout.EndHorizontal();

            values.arraySize = keys.arraySize;
        }
    }
}
