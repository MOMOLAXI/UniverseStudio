using System;
using UnityEngine;

namespace Universe
{
    public class UniverseEngine : MonoBehaviour
    {
        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterAssembliesLoaded)]
        static void InitializeEngine()
        {
            //delete
        }

        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
        static void InitializeGameSystems()
        {
            if (Engine.UnityObject != null)
            {
                return;
            }

            Engine.UnityObject = new(Engine.FormatName(nameof(UniverseEngine)));
            DontDestroyOnLoad(Engine.UnityObject);
            Function.Run(Engine.Initialize, out float s1);
            Log.Info($"Initialize Engine Instance ... using {s1} seconds");
            Function.Run(SystemRegister.Regsiter, out float s2);
            Log.Info($"Initialize Game Systems ... using {s2} seconds");
            Function.Run(Engine.Start, out float s3);
            Log.Info($"Initialize Game Logics ... using {s3} seconds");
        }

        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterSceneLoad)]
        static void Launch()
        {
            Engine.GetSystem<LaunchSystem>().Start();
        }
        
        void Reset()
        {
            Engine.Reset();
        }

        void Update()
        {
            Engine.Update(Time.deltaTime);
        }

        void FixedUpdate()
        {
            Engine.FixedUpdate(Time.fixedDeltaTime);
        }

        void LateUpdate()
        {
            Engine.LateUpdate(Time.deltaTime);
        }

        void OnDestroy()
        {
            Engine.Destroy();
        }

        void OnApplicationFocus(bool hasFocus)
        {
            Engine.ApplicationFocus(hasFocus);
        }

        void OnApplicationPause(bool pauseStatus)
        {
            Engine.ApplicationPause(pauseStatus);
        }

        void OnApplicationQuit()
        {
            Engine.ApplicationQuit();
        }
    }
}