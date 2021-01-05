using System;
using Hairibar.EngineExtensions.Editor;
using Hairibar.NaughtyExtensions.Editor;
using NaughtyAttributes.Editor;
using UnityEditor;
using UnityEngine;

namespace Hairibar.EngineExtensions.Serialization.Editor
{
    [CustomPropertyDrawer(typeof(SerializableDictionary<,>))]
    public class SerializableDictionaryDrawer : PropertyDrawerBase
    {
        SerializedProperty keys;
        SerializedProperty values;

        Type keyType;
        object defaultKey;

        protected override void OnGUI_Internal(Rect rect, SerializedProperty property, GUIContent label)
        {
            EditorGUI.BeginProperty(rect, GUIContent.none, property);

            RefreshCache(property);

            ExtraNaughtyEditorGUI.Header(new Rect(rect.x, rect.y, rect.width, EditorGUIUtility.singleLineHeight), label.text);

            rect.y += EditorGUIUtility.singleLineHeight;

            if (ShouldUseEnumKeys(property))
            {
                OnGUI_EnumKeys(rect);
            }
            else
            {
                OnGUI_ManualKeys(rect);
            }
        }

        bool ShouldUseEnumKeys(SerializedProperty property)
        {
            return keyType.IsEnum && PropertyUtility.GetAttribute<EnumKeysAttribute>(property) != null;
        }

        void RefreshCache(SerializedProperty property)
        {
            keys = property.FindPropertyRelative("keys");
            values = property.FindPropertyRelative("values");

            keyType = GetKeyType(property);

            //Ensure that there's at least one element for this hack.
            keys.arraySize++;
            defaultKey = keys.GetArrayElementAtIndex(0).propertyType.GetDefaultValue();
            keys.arraySize--;
        }

        static Type GetKeyType(SerializedProperty property)
        {
            return property.GetFieldInfo().FieldType.GetGenericArguments()[0];
        }



        void OnGUI_EnumKeys(Rect rect)
        {
            string[] enumNames = keyType.GetEnumNames();

            keys.arraySize = values.arraySize = enumNames.Length;

            float y = rect.y;
            float thirdOfWidth = rect.width / 3;
            float height = EditorGUIUtility.singleLineHeight;

            for (int i = 0; i < keys.arraySize; i++)
            {
                using (new EditorGUI.DisabledScope(true))
                {
                    SerializedProperty keyProperty = keys.GetArrayElementAtIndex(i);
                    keyProperty.enumValueIndex = i;

                    EditorGUI.PropertyField(new Rect(rect.x, y, thirdOfWidth, height), keyProperty, GUIContent.none);
                }

                EditorGUI.PropertyField(new Rect(rect.x + thirdOfWidth, y, thirdOfWidth * 2, height), values.GetArrayElementAtIndex(i), GUIContent.none);

                y += height;
            }
        }

        void OnGUI_ManualKeys(Rect rect)
        {
            values.arraySize = keys.arraySize;

            float height = EditorGUIUtility.singleLineHeight;
            float halfWidth = rect.width / 2;
            float y = rect.y;

            for (int i = 0; i < keys.arraySize; i++)
            {
                Rect labelRect = new Rect(rect.x, y, halfWidth, height);
                Rect valueRect = new Rect(rect.x + halfWidth, y, halfWidth, height);

                EditorGUI.PropertyField(labelRect, keys.GetArrayElementAtIndex(i), GUIContent.none);
                EditorGUI.PropertyField(valueRect, values.GetArrayElementAtIndex(i), GUIContent.none);

                y += EditorGUIUtility.singleLineHeight;
            }

            using (new EditorGUI.DisabledGroupScope(values.arraySize == 0))
            {
                if (GUI.Button(new Rect(rect.x, y, halfWidth, height), "-"))
                {
                    keys.arraySize = Mathf.Max(0, keys.arraySize - 1);
                }
            }

            bool canAddEntry;
            if (keys.arraySize == 0)
            {
                canAddEntry = true;
            }
            else
            {
                object lastKey = keys.GetArrayElementAtIndex(keys.arraySize - 1).GetPropertyValue();
                canAddEntry = lastKey != defaultKey;
            }

            using (new EditorGUI.DisabledGroupScope(!canAddEntry))
            {
                if (GUI.Button(new Rect(rect.x + halfWidth, y, halfWidth, height), "+"))
                {
                    int newElementIndex = keys.arraySize;
                    keys.arraySize++;

                    keys.GetArrayElementAtIndex(newElementIndex).ResetToDefaultValue();
                }
            }

            values.arraySize = keys.arraySize;
        }

        protected override float GetPropertyHeight_Internal(SerializedProperty property, GUIContent label)
        {
            RefreshCache(property);
            return EditorGUIUtility.singleLineHeight *
                (keys.arraySize +  //Entries
                1 +     //Label
                (ShouldUseEnumKeys(property) ? 0 : 1));  //Buttons in case of manual keys
        }
    }

}
