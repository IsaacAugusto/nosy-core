using System;
using System.Collections.Generic;
using System.Reflection;
using UnityEditor;
using UnityEngine;

namespace NosyCore.EventBus
{
    public static class EventBusUtil
    {
        public static IReadOnlyList<Type> EventTypes { get; set; }
        public static IReadOnlyList<Type> EventBusTypes { get; set; }

#if UNITY_EDITOR
        [InitializeOnLoadMethod]
        public static void InitializeEditor()
        {
            EditorApplication.playModeStateChanged -= OnPlayModeStateChanged;
            EditorApplication.playModeStateChanged += OnPlayModeStateChanged;
        }

        private static void OnPlayModeStateChanged(PlayModeStateChange state)
        {
            if (state == PlayModeStateChange.ExitingPlayMode)
            {
                ClearAllBuses();
            }
        }
#endif

        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
        public static void Initialize()
        {
            EventTypes = PredefinedAssembliesUtil.GetTypes(typeof(IEvent));
            EventBusTypes = InitializeBuses();
        }

        private static List<Type> InitializeBuses()
        {
            var eventBysTypes = new List<Type>();

            var typedef = typeof(EventBus<>);
            Debug.Log($"Initializing event buses. {EventTypes.Count}");
            foreach (var eventType in EventTypes)
            {
                var busType = typedef.MakeGenericType(eventType);
                eventBysTypes.Add(busType);
                Debug.Log($"Init event bus of type {eventType.Name}");
            }

            return eventBysTypes;
        }

        public static void ClearAllBuses()
        {
            Debug.Log($"Clearing all buses. {EventBusTypes.Count}");
            for (int i = 0; i < EventBusTypes.Count; i++)
            {
                var busType = EventBusTypes[i];
                var clearMethod = busType.GetMethod("Clear", BindingFlags.Static | BindingFlags.NonPublic);
                clearMethod.Invoke(null, null);
            }

            EventBusTypes = null;
        }
    }
}