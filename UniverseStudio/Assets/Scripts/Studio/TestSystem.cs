using UnityEngine;

namespace Universe
{
    public class TestSystem : GameSystem
    {
        public override void Init()
        {
            //Engine.GetSystem<LaunchSystem>().Start();
            
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