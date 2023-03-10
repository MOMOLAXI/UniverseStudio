using UnityEngine;
using Universe;

namespace UniverseStudio
{
    public class LaunchSystem : GameSystem
    {
        public void Start()
        {
            // Engine.Sequencer("GameLauncher")
            //       .AppendSingle<Test1>()
            //       .AppendParallel("TestParallel", new Test2(), new Test3())
            //       .AppendSingle<Test4>()
            //       .Start();

            //TODO 各种资源加载
            // Engine.CreateAssetsPackage("Scene");
            // Engine.CreateAssetsPackage("UI");
            // Engine.LoadSceneAsync("Scene", "StudioMain").Completed += OnSceneLoadFinish;
        }
        
        void OnSceneLoadFinish(SceneOperationHandle handle)
        {
            Engine.LoadAssetAsync<GameObject>("UI", "Canvas").Completed += OnCanvasLoaded;
        }
        
        void OnCanvasLoaded(AssetOperationHandle handle)
        {
            Object.Instantiate(handle.AssetObject);
        }
    }
}