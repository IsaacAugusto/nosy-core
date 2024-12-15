using System.Collections.Generic;
using UnityEngine;

namespace NosyCore.EventBus
{
    public static class EventBus<T> where T : IEvent
    {
        private static readonly HashSet<IEventBinding<T>> _bindings = new HashSet<IEventBinding<T>>();

        public static void Register(EventBinding<T> binding) => _bindings.Add(binding);
        public static void Deregister(EventBinding<T> binding) => _bindings.Remove(binding);

        public static void Raise(T @event)
        {
            foreach (var binding in _bindings)
            {
                binding.OnEvent.Invoke(@event);
                binding.OnEventVoid.Invoke();
            }
        }

        private static void Clear()
        {
            _bindings.Clear();
        }
    }
}