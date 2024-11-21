using UnityEngine;

namespace NosyCore.EasySingleton
{
    public static class EasySingleton
    {
        public static T GetOrCreateSingleton<T>() where T : MonoBehaviour
        {
            return MonoBehaviourSingleton<T>.Instance ?? new GameObject($"{typeof(T).Name}Singleton").AddComponent<T>();
        }
        
        public static T GetOrCreatePersistentSingleton<T>() where T : MonoBehaviour
        {
            return PersistentMonoBehaviourSingleton<T>.Instance ?? new GameObject($"{typeof(T).Name}PersistentSingleton").AddComponent<T>();
        }
    }
}