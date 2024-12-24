using NosyCore.Attributes;
using UnityEditor;
using UnityEngine;

namespace NosyCore.Editor
{
#if UNITY_EDITOR
    [CustomPropertyDrawer(typeof(DependencyAttributeSelf), true)]
    public class DependencyAttributeEditor : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            EditorGUI.BeginDisabledGroup(true);
            EditorGUI.LabelField(position, $"{label.text} (Dependency)");
            EditorGUI.PropertyField(position, property, label);
            EditorGUI.EndDisabledGroup();
        }
    }
#endif
}