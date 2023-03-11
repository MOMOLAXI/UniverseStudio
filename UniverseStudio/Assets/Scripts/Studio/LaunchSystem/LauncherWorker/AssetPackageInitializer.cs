using Universe;

namespace UniverseStudio
{
    public class AssetPackageInitializer : WorkNode
    {
        bool m_IsStart;
        
        public override bool IsDone => true;

        protected override void OnStart()
        {
            if (Engine.GetAssetsPackage("Scene", out AssetsPackage package))
            {
                // package.InitializeAsync()
            }
        }
    }
}