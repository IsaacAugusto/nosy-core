using System;

namespace Nosy.EventBus
{
    internal interface IEventBinding<T>
    {
        public Action<T> OnEvent { get; set; }
        public Action OnEventVoid { get; set; }
    }
    
    public class EventBinding<T> : IEventBinding<T> where T : IEvent
    {
        private Action<T> _onEvent = _ => { };
        private Action _onEventVoid = () => { };

        Action<T> IEventBinding<T>.OnEvent
        {
            get => _onEvent;
            set => _onEvent = value;
        }

        Action IEventBinding<T>.OnEventVoid
        {
            get => _onEventVoid;
            set => _onEventVoid = value;
        }

        public EventBinding(Action<T> onEvent) => _onEvent = onEvent;
        public EventBinding(Action onEventVoid) => _onEventVoid = onEventVoid;


        public void Add(Action<T> onEvent) => _onEvent += onEvent;
        public void Remove(Action<T> onEvent) => _onEvent -= onEvent;
        public void Add(Action onEventVoid) => _onEventVoid += onEventVoid;
        public void Remove(Action onEventVoid) => _onEventVoid -= onEventVoid;
    }
}