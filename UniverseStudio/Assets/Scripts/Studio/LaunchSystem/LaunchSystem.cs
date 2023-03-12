using UnityEngine;
using Universe;

namespace UniverseStudio
{
    public class LaunchSystem : GameSystem
    {
        public void Start()
        {
            Application.targetFrameRate = 60;
            Application.runInBackground = true;
            Engine.SetOperationSystemComponentMaxTimeSlice(30);
            Engine.Sequencer("GameLauncher")
                  .AppendSingle<UIAssetInitializer>()
                  .AppendSingle<SceneAssetInitializer>()
                  .AppendSingle<GameInitializer>()
                  .Start();
        }
    }
}