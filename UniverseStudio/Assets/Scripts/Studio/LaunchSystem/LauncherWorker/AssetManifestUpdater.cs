using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Universe;

namespace UniverseStudio
{
    public class AssetManifestUpdater : WorkNode
    {
        readonly Dictionary<string, bool> m_ManifestState = new();

        bool m_IsDone;
        public override bool IsDone => m_ManifestState.Values.Aggregate(true, (current, value) => value && current);

        public override string Name => "Update AssetPackage Manifests";

        protected override void OnStart()
        {
            if (Engine.GetAssetsPackage(AssetInitializeParam.UI_PACKAGE, out AssetsPackage uiPackage))
            {
                Engine.StartGlobalCoroutine(UpdateAssetManifest(AssetInitializeParam.UI_PACKAGE, uiPackage));
            }

            if (Engine.GetAssetsPackage(AssetInitializeParam.SCENE_PACKAGE, out AssetsPackage scenePackage))
            {
                Engine.StartGlobalCoroutine(UpdateAssetManifest(AssetInitializeParam.SCENE_PACKAGE, scenePackage));
            }
        }

        IEnumerator UpdateAssetManifest(string packageName, AssetsPackage package)
        {
            string version = PatchSystem.GetAssetVersion(packageName);
            if (string.IsNullOrEmpty(version))
            {
                yield break;
            }

            UpdatePackageManifestOperation opertaion = package.UpdatePackageManifestAsync(version);
            yield return opertaion;
            m_ManifestState[packageName] = opertaion.Status == EOperationStatus.Succeed;
        }
    }
}