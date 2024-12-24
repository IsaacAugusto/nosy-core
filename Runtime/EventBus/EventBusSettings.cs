using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;

namespace NosyCore.EventBus
{
    [CreateAssetMenu(fileName = "EventBusSettings", menuName = "NosyCore/EventBusSettings", order = 0)]
    public class EventBusSettings : ScriptableObject
    {
        public List<string> assembliesWithEvents;
    }

    public static class EventBusSettingsUtility
    {
        private const string ConfigPath = "Assets/Nosy_Core/EventBusSettings.asset";

        public static EventBusSettings GetOrCreateSettings()
        {
            EventBusSettings settings = Resources.Load<EventBusSettings>(ConfigPath);
            if (settings == null)
            {
#if UNITY_EDITOR
                settings = ScriptableObject.CreateInstance<EventBusSettings>();
                Directory.CreateDirectory(Path.GetDirectoryName(ConfigPath));
                settings.assembliesWithEvents = new List<string> { "Assembly-CSharp", "Assembly-CSharp-FirstPass", "Assembly-CSharp-Editor", "Assembly-CSharp-Editor-FirstPass" };
                
                AssetDatabase.CreateAsset(settings, ConfigPath);
                AssetDatabase.SaveAssets();
#endif
            }
            return settings;
        }
    }
}