﻿using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Universe;

namespace UniverseStudio
{
    public class AssetPackageInitializer : WorkNode
    {
        readonly Dictionary<string, bool> m_PackageState = new();

        public override string Name => "Initialize Asset Packages";

        public override bool IsDone => m_PackageState.Values.Aggregate(true, (current, value) => value && current);

        protected override void OnStart()
        {
            m_PackageState[AssetInitializeParam.UI_PACKAGE] = false;
            // Engine.CreateAssetsPackage(AssetInitializeParam.UI_PACKAGE);
            // Engine.SetUIPackageName(AssetInitializeParam.UI_PACKAGE);
            //
            // if (Engine.GetAssetsPackage(AssetInitializeParam.UI_PACKAGE, out AssetsPackage uiPackage))
            // {
            //     Engine.StartGlobalCoroutine(InitializePackage(AssetInitializeParam.UI_PACKAGE, uiPackage));
            // }
            //
            // Engine.CreateAssetsPackage(AssetInitializeParam.SCENE_PACKAGE);
            // Engine.SetScenePackageName(AssetInitializeParam.SCENE_PACKAGE);
            //
            // if (Engine.GetAssetsPackage(AssetInitializeParam.SCENE_PACKAGE, out AssetsPackage scenePackage))
            // {
            //     Engine.StartGlobalCoroutine(InitializePackage(AssetInitializeParam.SCENE_PACKAGE, scenePackage));
            // }
        }

        IEnumerator InitializePackage(string packageName, AssetsPackage package)
        {
            //TODO LOG MASK
            //TODO 重构一遍AssetSystem, 池化一些对象
            //将一些接口内置到Engine中，外部只暴露InitializeAssetPackage即可, 提供所有AssetPackage的进度，方便UI显示
            OfflinePlayModeParameters param = AssetInitializeParam.OfflineParams();
            // InitializationOperation operation = package.InitializeAsync(param);
            // yield return operation;
            m_PackageState[packageName] = package.InitializeStatus == EOperationStatus.Succeed;
            yield break;
        }
    }
}