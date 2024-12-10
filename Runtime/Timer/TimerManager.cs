using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.LowLevel;
using UnityEngine.PlayerLoop;


namespace NosyCore.Timer
{
    public static class TimerManager
    {
        private static LinkedList<Timer> _activeTimers;

        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.SubsystemRegistration)]
        private static void Init()
        {
            _activeTimers = new LinkedList<Timer>();
            
            var timerManagerLoop = new TimerManagerUpdateLoop();
            UnityLoop.UnityLoopUtils.RegisterToUpdate(timerManagerLoop, before: typeof(Update));
        }

#if UNITY_EDITOR
        [UnityEditor.InitializeOnLoadMethod]
        private static void ResetStaticMembers()
        {
            UnityEditor.EditorApplication.playModeStateChanged += state =>
            {
                if (state == UnityEditor.PlayModeStateChange.ExitingPlayMode)
                {
                    _activeTimers.Clear();
                    
                    var defaultLoop = PlayerLoop.GetDefaultPlayerLoop();
                    PlayerLoop.SetPlayerLoop(defaultLoop);
                }
            };
        }
#endif

        private static void TickTimers()
        {
            var deltaTime = UnityEngine.Time.deltaTime;
            var item = _activeTimers.First;
            while (item != null)
            {
                var next = item.Next;
                item.Value.Update(deltaTime);
                if (item.Value.Running == false && item.Value.Ended)
                {
                    UnregisterTimer(item.Value);
                }

                item = next;
            }
        }

        public static void RegisterTimer(Timer timer)
        {
            _activeTimers?.AddLast(timer);
        }

        public static void UnregisterTimer(Timer timer)
        {
            _activeTimers?.Remove(timer);
        }
        
        private struct TimerManagerUpdateLoop : UnityLoop.UnityLoopUtils.ICustomPlayerLoop
        {
            public PlayerLoopSystem.UpdateFunction UpdateFunction => TimerManager.TickTimers;
        }
    }
    
    public class Timer
    {
        public bool Running;

        internal bool Ended;
        
        private float _duration;
        private float _timeElapsed;

        private readonly Action OnTimerStart;
        private readonly Action OnTimerEnd;
        
        public Timer(float duration, Action onTimerStart = default, Action onTimerEnd = default)
        {
            Init(duration);
            OnTimerStart = onTimerStart;
            OnTimerEnd = onTimerEnd;
        }

        private void Init(float duration)
        {
            // Check if duration is valid
            if (duration <= 0)
            {
                throw new ArgumentException("Duration must be greater than 0");
            }
            
            _duration = duration;
            _timeElapsed = 0;
            Running = false;
            Ended = false;
            TimerManager.RegisterTimer(this);
        }
        
        public void Start()
        {
            if (Running) return;
            
            Running = true;
            _timeElapsed = 0;
            OnTimerStart?.Invoke();
        }
        
        public void Stop()
        {
            Running = false;
        }

        public void Restart(float duration = -1)
        {
            TimerManager.UnregisterTimer(this);
            Init(duration > 0 ? duration : _duration);
            Start();
        }
        
        public void Update(float deltaTime)
        {
            if (Running == false) return;
            
            _timeElapsed += deltaTime;
            if (_timeElapsed >= _duration)
            {
                Ended = true;
                Running = false;
                OnTimerEnd?.Invoke();
            }
        }
    }
}