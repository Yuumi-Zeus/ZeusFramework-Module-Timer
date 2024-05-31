using UnityEngine;
using UnityEngine.LowLevel;
using UnityEngine.PlayerLoop;
#if UNITY_EDITOR
using UnityEditor;
#endif

namespace ImprovedTimer
{
    internal static class TimerBootstrapper
    {
        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterAssembliesLoaded)]
        internal static void Initialize()
        {
            var playerLoop = PlayerLoop.GetCurrentPlayerLoop();

            if (!InsertTimerManager<Update>(ref playerLoop, 0))
            {
                Debug.LogWarning("ImprovedTimer: Failed to insert TimerManager into the player loop system.");
                return;
            }

            // Set the player loop system
            PlayerLoop.SetPlayerLoop(playerLoop);
            // Print the player loop system
            PlayerLoopUtils.PrintPlayerLoop(playerLoop);

#if UNITY_EDITOR
            EditorApplication.playModeStateChanged -= OnPlayModeState;
            EditorApplication.playModeStateChanged += OnPlayModeState;
#endif
            static void OnPlayModeState(PlayModeStateChange state)
            {
                if (state == PlayModeStateChange.ExitingPlayMode)
                {
                    var currentPlayerLoop = PlayerLoop.GetCurrentPlayerLoop();
                    RemoveTimerManager<Update>(ref currentPlayerLoop);
                    PlayerLoop.SetPlayerLoop(currentPlayerLoop);

                    TimerManager.Clear();
                }
            }
        }


        static void RemoveTimerManager<T>(ref PlayerLoopSystem loop)
        {
            var timerSystem = PlayerLoopUtils.GetNewCustomPlayerLoopSystem(typeof(TimerManager), TimerManager.UpdateTimer);
            PlayerLoopUtils.RemoveSystem<T>(ref loop,  timerSystem);
        }

        static bool InsertTimerManager<T>(ref PlayerLoopSystem loop, int index)
        {
            var timerSystem = PlayerLoopUtils.GetNewCustomPlayerLoopSystem(typeof(TimerManager), TimerManager.UpdateTimer);
            // TODO: Insert the timer system into the player loop system at the specified index
            return PlayerLoopUtils.InsertSystem<T>(ref loop, timerSystem, index);
        }
    }
}