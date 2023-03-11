using Universe;

namespace UniverseStudio
{
    public class SceneAssetInitializer : WorkNode
    {
        public override bool IsDone => true;

        protected override void OnStart()
        {
            Engine.CreateAssetsPackage("Scene");
            Engine.CreateAssetsPackage("UI");
            Engine.SetUIPackageName("UI");
        }
    }
}