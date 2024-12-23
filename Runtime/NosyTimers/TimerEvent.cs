using System;

namespace NosyCore.NosyTimers
{
    internal interface ITimerEvent
    {
        public bool ShouldRemoveAfterFired { get; }
        public bool ShouldFire(int timerElapsed, int deltaElapsed);
        public void Fire();
        void OnTimerReset();
    }
    
    internal struct TimerEvent : ITimerEvent
    {
        private readonly float _time;
        private readonly Action _event;
        private readonly bool _repeat;
        
        private bool _fired;

        public TimerEvent(float time, Action @event, bool repeat) : this()
        {
            _time = time;
            _event = @event;
            _repeat = repeat;

            OnTimerReset();
        }

        public bool ShouldRemoveAfterFired => _repeat == false;

        public bool ShouldFire(int timerElapsed, int deltaElapsed)
        {
            if (_fired) return false;
            return timerElapsed >= _time;
        }

        public void Fire()
        {
            _fired = true;
            _event?.Invoke();
        }

        public void OnTimerReset()
        {
            _fired = false;
        }
    }
    
    internal struct RecurringTimerEvent : ITimerEvent
    {
        private float _repeatTime;
        private float _repeatCount;
        private Action _event;
        
        private int _firedCount;
        private int _timeElapsed;

        public bool ShouldRemoveAfterFired => _repeatCount > 0 && _firedCount >= _repeatCount;
        
        public RecurringTimerEvent(float repeatTime, Action @event, int repeatCount = -1) : this()
        {
            _repeatTime = repeatTime;
            _event = @event;
            _repeatCount = repeatCount;
            
            OnTimerReset();
        }

        public bool ShouldFire(int timerElapsed, int deltaElapsed)
        {
            _timeElapsed += deltaElapsed;
            if (_timeElapsed < _repeatTime) return false;
            _timeElapsed = 0;
            return true;
        }

        public void Fire()
        {
            _firedCount++;
            _event?.Invoke();
        }

        public void OnTimerReset()
        {
            _firedCount = 0;
            _timeElapsed = 0;
        }
    }
}