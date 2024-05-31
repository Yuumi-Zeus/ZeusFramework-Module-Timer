using System.Collections.Generic;

namespace ImprovedTimer
{
    public static class TimerManager
    {
        static readonly List<AbsTimer> Timers = new List<AbsTimer>();

        public static void RegisterTimer(AbsTimer timer)
        {
            Timers.Add(timer);
        }

        public static void DeRegisterTimer(AbsTimer timer)
        {
            Timers.Remove(timer);
        }

        public static void UpdateTimer()
        {
            foreach (var timer in Timers)
            {
                // Update the timer
                timer.Tick();
            }
        }

        public static void Clear()
        {
            Timers.Clear();
        }
    }
}