using Hairibar.NaughtyExtensions.Editor;
using NaughtyAttributes.Editor;
using UnityEditor;
using UnityEngine;

namespace Hairibar.EngineExtensions.Editor
{
    [CustomPropertyDrawer(typeof(MinMaxRange))]
    public class MinMaxRangeDrawer : PropertyDrawer
    {
        public static void Draw_Layout(SerializedProperty serializedProperty, float min, float max)
        {
            Draw_Layout(serializedProperty, min, max, serializedProperty.GetLabelContent());
        }

        public static void Draw_Layout(SerializedProperty serializedProperty, float min, float max, GUIContent label)
        {
            Rect rect = EditorGUILayout.GetControlRect();
            Draw(rect, serializedProperty, min, max, label);
        }

        public static void Draw(Rect rect, SerializedProperty serializedProperty, float min, float max)
        {
            Draw(rect, serializedProperty, min, max, serializedProperty.GetLabelContent());
        }

        public static void Draw(Rect rect, SerializedProperty property, float min, float max, GUIContent label)
        {
            EditorGUI.BeginProperty(rect, label, property);

            float indentLength = NaughtyEditorGUI.GetIndentLength(rect);
            float labelWidth = EditorGUIUtility.labelWidth;
            float floatFieldWidth = EditorGUIUtility.fieldWidth;
            float sliderWidth = rect.width - labelWidth - 2f * floatFieldWidth;
            float sliderPadding = 5f;

            Rect labelRect = new Rect(
                rect.x,
                rect.y,
                labelWidth,
                rect.height);

            Rect sliderRect = new Rect(
                rect.x + labelWidth + floatFieldWidth + sliderPadding - indentLength,
                rect.y,
                sliderWidth - 2f * sliderPadding + indentLength,
                rect.height);

            Rect minFloatFieldRect = new Rect(
                rect.x + labelWidth - indentLength,
                rect.y,
                floatFieldWidth + indentLength,
                rect.height);

            Rect maxFloatFieldRect = new Rect(
                rect.x + labelWidth + floatFieldWidth + sliderWidth - indentLength,
                rect.y,
                floatFieldWidth + indentLength,
                rect.height);

            // Draw the label
            EditorGUI.LabelField(labelRect, label.text);

            // Draw the slider
            EditorGUI.BeginChangeCheck();

            SerializedProperty minProperty = property.FindPropertyRelative("min");
            float minSliderValue = minProperty.floatValue;
            SerializedProperty maxProperty = property.FindPropertyRelative("max");
            float maxSliderValue = maxProperty.floatValue;


            EditorGUI.MinMaxSlider(sliderRect, ref minSliderValue, ref maxSliderValue, min, max);

            minSliderValue = EditorGUI.FloatField(minFloatFieldRect, minSliderValue);
            minSliderValue = Mathf.Clamp(minSliderValue, min, Mathf.Min(max, maxSliderValue));

            maxSliderValue = EditorGUI.FloatField(maxFloatFieldRect, maxSliderValue);
            maxSliderValue = Mathf.Clamp(maxSliderValue, Mathf.Max(min, minSliderValue), max);

            if (EditorGUI.EndChangeCheck())
            {
                minProperty.floatValue = minSliderValue;
                maxProperty.floatValue = maxSliderValue;
            }

            EditorGUI.EndProperty();
        }


        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            MinMaxRange.SliderAttribute attribute = PropertyUtility.GetAttribute<MinMaxRange.SliderAttribute>(property);

            if (attribute != null)
            {
                Draw(position, property, attribute.min, attribute.max, label);
            }
            else
            {
                EditorGUI.PropertyField(position, property, label);
                if (property.isExpanded)
                {
                    EditorGUI.indentLevel++;
                    NaughtyEditorGUI.HelpBox_Layout("MinMaxRange needs a [MinMaxRange.Slider(min, max)] attribute for the fancy editor.", MessageType.Info);
                    EditorGUI.indentLevel--;
                }
            }
        }
    }

}
