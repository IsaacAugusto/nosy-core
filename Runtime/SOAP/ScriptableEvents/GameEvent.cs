using System.Collections.Generic;
using UnityEngine;

namespace NosyCore.ScriptableEvents
{
    public abstract class GameEvent<T> : ScriptableObject
    {
        readonly List<IGameEventListener<T>> _listeners = new();

        public void Raise(T data)
        {
            for (int i = _listeners.Count - 1; i >= 0; i--)
            {
               _listeners[i].OnEventRaised(data);
            }
        }

        public void RegisterListener(IGameEventListener<T> listener) => _listeners.Add(listener);
        public void UnregisterListener(IGameEventListener<T> listener) => _listeners.Remove(listener);
    }

    public struct Unit
    {
        public static Unit Default => default;
    }
    
    [CreateAssetMenu(menuName = "NosyCore/ScriptableEvents/GameEvent")]
    public class GameEvent : GameEvent<Unit>
    {
        public void Raise() => Raise(Unit.Default);
    }

}