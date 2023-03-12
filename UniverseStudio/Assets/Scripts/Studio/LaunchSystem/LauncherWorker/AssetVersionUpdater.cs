using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Universe;

namespace UniverseStudio
{
    public class AssetVersionUpdater : WorkNode
    {
        readonly Dictionary<string, bool> m_VersionState = new();

        bool m_IsDone;
        public override bool IsDone => m_VersionState.Values.Aggregate(true, (current, value) => value && current);

        public override string Name => "Update Assets Versions";

        protected override void OnStart()
        {
            if (Engine.GetAssetsPackage(AssetInitializeParam.UI_PACKAGE, out AssetsPackage uiPackage))
            {
                Engine.StartGlobalCoroutine(UpdateAssetVersion(AssetInitializeParam.UI_PACKAGE, uiPackage));
            }

            if (Engine.GetAssetsPackage(AssetInitializeParam.SCENE_PACKAGE, out AssetsPackage scenePackage))
            {
                Engine.StartGlobalCoroutine(UpdateAssetVersion(AssetInitializeParam.SCENE_PACKAGE, scenePackage));
            }
        }

        IEnumerator UpdateAssetVersion(string packageName, AssetsPackage package)
        {
            UpdatePackageVersionOperation opertaion = package.UpdatePackageVersionAsync();
            yield return opertaion;
            if (opertaion.Status == EOperationStatus.Succeed)
            {
                m_VersionState[packageName] = true;
                PatchSystem.RegisterAsset(packageName, opertaion.PackageVersion);
            }
            else
            {
                Log.Error("update assets package version failed");
            }
        }
    }
}