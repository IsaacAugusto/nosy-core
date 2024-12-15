using System.Collections.Generic;
using UnityEngine;

namespace NosyCore.EventBus
{
    [CreateAssetMenu(fileName = "EventBusAssembliesAsset", menuName = "NosyCore/EventBus/EventBusAssembliesAsset")]
    public class EventBusAssembliesAsset : ScriptableObject
    {
        public List<string> Assemblies = new List<string>();
    }
}