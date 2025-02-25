using UnityEngine;
using UnityEngine.Events;

namespace NosyCore.ScriptableEvents
{
    public interface IGameEventListener<T>
    {
        public void OnEventRaised(T data);
    }
    
    public class GameEventListener<T> : MonoBehaviour, IGameEventListener<T>
    {
        [SerializeField] protected GameEvent<T> _gameEvent;
        [SerializeField] protected UnityEvent<T> _response;

        protected virtual void OnEnable() => _gameEvent.RegisterListener(this);
        protected virtual void OnDisable() => _gameEvent.UnregisterListener(this);
        public virtual void OnEventRaised(T data) => _response.Invoke(data);
    }

    public class GameEventListener : GameEventListener<Unit> { }
}