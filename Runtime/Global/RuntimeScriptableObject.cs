﻿using System;
using System.Collections.Generic;
using UnityEngine;

namespace NosyCore
{
    public abstract class RuntimeScriptableObject : ScriptableObject
    {
        private static readonly List<RuntimeScriptableObject> _instances = new();

        private void OnEnable() => _instances.Add(this);
        private void OnDisable() => _instances.Remove(this);

        protected abstract void OnReset();

        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
        public static void ResetAllInstances()
        {
            foreach (var instance in _instances)
            {
                instance.OnReset();
            }
        }
    }
}