using System;
using System.Collections.Generic;

namespace NosyCore.NosyTimers
{
    
    public class NosyTimer
    {
        private struct TimerEvent
        {
            public float Time;
            public Action Event;
            public bool Fired;
            public bool Repeat;
        }

        public readonly bool Repeat;
        public bool Running;
        public float Progress => (float)_timeElapsed / _duration;
        public int ElapsedTimeMs => _timeElapsed;
        public float ElapsedTimeSec => _timeElapsed * 0.001f;
        public bool Ended => _timeElapsed >= _duration;

        private List<TimerEvent> _timerEvents;
        private int _duration;
        private int _timeElapsed;

        private readonly Action OnTimerStart;
        private readonly Action OnTimerEnd;
        
        public NosyTimer(int duration, bool repeat = false, Action onTimerStart = default, Action onTimerEnd = default)
        {
            Init(duration);
            OnTimerStart = onTimerStart;
            OnTimerEnd = onTimerEnd;
            _timerEvents = new List<TimerEvent>();
            Repeat = repeat;
        }
        
        private void Init(int duration)
        {
            // Check if duration is valid
            if (duration <= 0)
            {
                throw new ArgumentException("Duration must be greater than 0 ms.");
            }
            
            _duration = duration;
            _timeElapsed = 0;
            Running = false;
            TimeManager.RegisterTimer(this);
        }
        
        public void Start()
        {
            if (Running) return;
            
            Running = true;
            OnTimerStart?.Invoke();
        }
        
        public void Stop()
        {
            Running = false;
        }

        public void Restart(int duration = -1)
        {
            TimeManager.UnregisterTimer(this);
            SetTimerEventsUnfired();
            _timeElapsed = 0;
            Init(duration > 0 ? duration : _duration);
            Start();
        }
        
        public void Update(int deltaTimeMs)
        {
            if (Running == false) return;
            
            _timeElapsed += deltaTimeMs;
            
            UpdateTimerEvents();
            
            if (_timeElapsed >= _duration)
            {
                Running = false;
                OnTimerEnd?.Invoke();
            }
        }
        
        public void AddTimerEvent(float time, Action action, bool repeat = false)
        {
            if (time <= 0 || time >= _duration)
            {
                throw new ArgumentException($"Invalid time value [{time}]. Should be bigger than 0 ms and less than the duration of the timer [{_duration}].");
            }
            
            _timerEvents.Add(new TimerEvent
            {
                Time = time,
                Event = action,
                Fired = false,
                Repeat = repeat
            });
        }
        
        private void UpdateTimerEvents()
        {
            if (Running == false) return;
            
            for (int i = _timerEvents.Count - 1; i >= 0; i--)
            {
                var timerEvent = _timerEvents[i];
                if (timerEvent.Fired) continue;
                
                if (_timeElapsed >= timerEvent.Time)
                {
                    timerEvent.Fired = true;
                    timerEvent.Event?.Invoke();
                    if (timerEvent.Repeat == false)
                    {
                        _timerEvents.RemoveAt(i);
                        continue;
                    }
                }

                _timerEvents[i] = timerEvent;
            }
        }
        
        private void SetTimerEventsUnfired()
        {
            for (var index = 0; index < _timerEvents.Count; index++)
            {
                var timerEvent = _timerEvents[index];
                timerEvent.Fired = false;
                _timerEvents[index] = timerEvent;
            }
        }
    }
}