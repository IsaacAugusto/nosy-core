using System.Collections.Generic;
using UnityEngine;
using UnityEngine.LowLevel;
using UnityEngine.PlayerLoop;


namespace NosyCore.NosyTimers
{
    public static class TimeManager
    {
        public static bool UseUnscaledTime { get; set; } = false;
        public static bool Running { get; private set; }

        private static LinkedList<NosyTimer> _activeTimers;
        
        public static void Pause() => Running = false;
        public static void Resume() => Running = true;

        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.SubsystemRegistration)]
        private static void Init()
        {
            _activeTimers = new LinkedList<NosyTimer>();
            
            var timerManagerLoop = new TimerManagerUpdateLoop();
            UnityLoop.UnityLoopUtils.RegisterToUpdate(timerManagerLoop, before: typeof(Update));
            Running = true;
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
            if (!Running) return;
            
            int deltaTimeMs = (int)((UseUnscaledTime ? Time.unscaledDeltaTime : Time.deltaTime) * 1000);
            var item = _activeTimers.First;
            while (item != null)
            {
                var next = item.Next;
                var timer = item.Value;
                
                timer.Update(deltaTimeMs);
                
                if (timer.Running == false && timer.Ended)
                {
                    if (timer.Repeat)
                    {
                        timer.Restart();
                    }
                    else
                    {
                        UnregisterTimer(timer);
                    }
                }

                item = next;
            }
        }

        public static void RegisterTimer(NosyTimer nosyTimer)
        {
            _activeTimers?.AddLast(nosyTimer);
        }

        public static void UnregisterTimer(NosyTimer nosyTimer)
        {
            _activeTimers?.Remove(nosyTimer);
        }
        
        private struct TimerManagerUpdateLoop : UnityLoop.UnityLoopUtils.ICustomPlayerLoop
        {
            public PlayerLoopSystem.UpdateFunction UpdateFunction => TickTimers;
        }
    }
}