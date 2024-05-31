﻿using ImprovedTimer;
using UnityEngine;

namespace ImprovedTimers
{
    /// <summary>
    /// Timer that counts up from zero to infinity.  Great for measuring durations.
    /// </summary>
    public class StopwatchTimer : AbsTimer
    {
        public StopwatchTimer() : base(0) { }

        public override void Tick()
        {
            if (IsRunning)
            {
                CurrentTime += Time.deltaTime;
            }
        }

        public override bool IsFinished => false;
    }
}