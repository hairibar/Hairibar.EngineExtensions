using NaughtyAttributes.Editor;
using UnityEditor;
using UnityEngine;

namespace Hairibar.EngineExtensions.Editor
{
    [CustomPropertyDrawer(typeof(Countdown), true)]
    public class CountdownDrawer : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            TooltipAttribute tooltip = PropertyUtility.GetAttribute<TooltipAttribute>(property);
            label.tooltip = tooltip?.tooltip;

            EditorGUI.PropertyField(position, property.FindPropertyRelative("_length"), label);
        }
    }
}
