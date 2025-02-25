using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;

namespace NosyCore.EventBus
{
    [CreateAssetMenu(fileName = "EventBusSettings", menuName = "NosyCore/EventBus/EventBusSettings", order = 0)]
    public class EventBusSettings : ScriptableObject
    {
        public List<string> assembliesWithEvents;
    }

    public static class EventBusSettingsUtility
    {
        private const string ConfigAssetPathAtResources = "Assets/NosyCore/Resources";
        private const string ConfigAssetName = "EventBusSettings";

        public static EventBusSettings GetOrCreateSettings()
        {
            EventBusSettings settings = Resources.Load<EventBusSettings>(ConfigAssetName);
            if (settings == null)
            {
#if UNITY_EDITOR
                settings = ScriptableObject.CreateInstance<EventBusSettings>();
                Directory.CreateDirectory(ConfigAssetPathAtResources);
                settings.assembliesWithEvents = new List<string> { "Assembly-CSharp", "Assembly-CSharp-FirstPass", "Assembly-CSharp-Editor", "Assembly-CSharp-Editor-FirstPass" };
                
                AssetDatabase.CreateAsset(settings,  Path.Combine(ConfigAssetPathAtResources, $"{ConfigAssetName}.asset"));
                AssetDatabase.SaveAssets();
#endif
            }
            return settings;
        }
    }
}