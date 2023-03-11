using System.Collections;
using Universe;

namespace UniverseStudio
{
    public class UIAssetInitializer : WorkNode
    {
        bool m_IsDone;
        
        public override string Name => "UI Asset package initialize...";

        public override bool IsDone => m_IsDone;

        protected override void OnStart()
        {
            Engine.CreateAssetsPackage("UI");
            Engine.SetUIPackageName("UI");

            m_IsDone = false;
            if (Engine.GetAssetsPackage("UI", out AssetsPackage package))
            {
               Engine.StartGlobalCoroutine(InitializeUIPackage(package));
            }
        }

        IEnumerator InitializeUIPackage(AssetsPackage package)
        {
            OfflinePlayModeParameters param = AssetInitializeParam.OfflineParams();
            InitializationOperation operation = package.InitializeAsync(param);
            yield return operation;
            m_IsDone = true;
        }
    }
}