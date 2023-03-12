using System.Collections.Generic;
using System.Linq;
using Universe;

namespace UniverseStudio
{
    public class AssetDownloaderOver : WorkNode
    {
        readonly Dictionary<string, bool> m_ClearState = new();

        public override bool IsDone => m_ClearState.Values.Aggregate(true, (current, value) => value && current);
        public override string Name => "Assets Download Over";

        protected override void OnStart()
        {
            ClearCache(AssetInitializeParam.SCENE_PACKAGE);
            ClearCache(AssetInitializeParam.UI_PACKAGE);
        }

        void ClearCache(string packageName)
        {
            if (Engine.GetAssetsPackage(packageName, out AssetsPackage package))
            {
                ClearUnusedCacheFilesOperation operation = package.ClearUnusedCacheFilesAsync();
                operation.Completed += _ =>
                {
                    m_ClearState[packageName] = true;
                };
            }
        }

    }
}