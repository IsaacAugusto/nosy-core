using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.LowLevel;

namespace ArkaCore.UnityLoopUtils
{
    public static class UnityLoopUtils
    {
        public interface ICustomPlayerLoop
        {
            public PlayerLoopSystem.UpdateFunction UpdateFunction { get; }
        }

#if UNITY_EDITOR
        [InitializeOnLoadMethod]
        public static void EditorClear()
        {
            EditorApplication.playModeStateChanged += state =>
            {
                if (state == PlayModeStateChange.ExitingPlayMode)
                {
                    PlayerLoop.SetPlayerLoop(PlayerLoop.GetDefaultPlayerLoop());
                }
            };
        }
        
        [MenuItem("Tools/NosyCore/Print Player Loop")]
        public static void PrintPlayerLoop()
        {
            var playerLoop = PlayerLoop.GetCurrentPlayerLoop();
            foreach (var subsystem in playerLoop.subSystemList)
            {
                Debug.Log(subsystem.type.ToString());
            }
        }
#endif
        
        public static void RegisterToUpdate<T>(T customLoop, Type before = default, Type after = default)
            where T : ICustomPlayerLoop
        {
            var playerLoop = PlayerLoop.GetCurrentPlayerLoop();
            var subsystems = playerLoop.subSystemList;
            var newSubsystems = new List<PlayerLoopSystem>(subsystems.Length + 1);
            newSubsystems.AddRange(subsystems);

            var newSystemLoop = new PlayerLoopSystem
            {
                type = typeof(T),
                updateDelegate = customLoop.UpdateFunction,
            };

            int index = 0;
            if (after != default)
            {
                for (int i = 0; i < newSubsystems.Count; i++)
                {
                    if (newSubsystems[i].type == after)
                    {
                        index = i + 1;
                        break;
                    }
                }
            }

            if (before != default)
            {
                for (int i = 0; i < newSubsystems.Count; i++)
                {
                    if (newSubsystems[i].type == before)
                    {
                        // Impossible to insert new system between the 2 provided
                        if (index != 0 && i < index)
                        {
                            throw new Exception(
                                $"Impossible to insert new player loop system [{typeof(T)}] between [{before}] and [{after}]");
                        }

                        index = i;
                        break;
                    }
                }
            }


            newSubsystems.Insert(index, newSystemLoop);
            playerLoop.subSystemList = newSubsystems.ToArray();
            PlayerLoop.SetPlayerLoop(playerLoop);
        }
    }
}