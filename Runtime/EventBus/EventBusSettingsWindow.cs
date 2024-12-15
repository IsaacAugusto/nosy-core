using UnityEditor;
using UnityEngine;

namespace NosyCore.EventBus
{
    public class EventBusSettingsWindow : EditorWindow
    {
        private EventBusSettings settings;

        [MenuItem("Tools/NosyCore/EventBus/Settings")]
        public static void ShowWindow()
        {
            GetWindow<EventBusSettingsWindow>("EventBus Settings");
        }

        private void OnEnable()
        {
            settings = EventBusSettingsUtility.GetOrCreateSettings();
        }

        private void OnGUI()
        {
            if (settings == null)
            {
                EditorGUILayout.LabelField("Settings not found!");
                return;
            }

            EditorGUILayout.LabelField("EventBus Configuration", EditorStyles.boldLabel);

            SerializedObject serializedObject = new SerializedObject(settings);
            SerializedProperty stringListProperty = serializedObject.FindProperty("assembliesWithEvents");

            
            EditorGUILayout.PropertyField(stringListProperty, new GUIContent("Assemblies With Events"), true);
        }
    }
}