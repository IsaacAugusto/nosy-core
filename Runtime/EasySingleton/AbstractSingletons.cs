using System;
using UnityEngine;

namespace NosyCore.EasySingleton
{
    // ReSharper disable once ClassWithVirtualMembersNeverInherited.Global

    /// <summary>
    /// Inherit from this class to create a singleton MonoBehaviour that will NOT persist between scenes.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public abstract class MonoBehaviourSingleton<T> : MonoBehaviour where T : MonoBehaviour
    {
        // ReSharper disable once MemberCanBePrivate.Global
        public static T Instance;

        protected virtual void Awake()
        {
            if (Instance == null)
            {
                Instance = this as T;
            }
            else
            {
                Debug.LogWarning($"An instance of {typeof(T)} already exists. Destroying this instance.");
                Destroy(gameObject);
            }
        }
    }
    
    // ReSharper disable once ClassWithVirtualMembersNeverInherited.Global
    
    /// <summary>
    /// Inherit from this class to create a persistent singleton MonoBehaviour that will persist between scenes.
    /// </summary>
    public abstract class PersistentMonoBehaviourSingleton<T> : MonoBehaviour where T : MonoBehaviour
    {
        // ReSharper disable once MemberCanBePrivate.Global
        public static T Instance;

        protected virtual void Awake()
        {
            if (Instance == null)
            {
                Instance = this as T;
                DontDestroyOnLoad(gameObject);
            }
            else
            {
                Debug.LogWarning($"An instance of {typeof(T)} already exists. Destroying this instance.");
                Destroy(gameObject);
            }
        }
    }
}