using System.Collections;
using Universe;

namespace UniverseStudio
{
    public class SceneAssetInitializer : WorkNode
    {
        bool m_IsDone;
        public override bool IsDone => m_IsDone;
        public override string Name => "Initialize Scene Asset Package";

        protected override void OnStart()
        {
            Engine.OpenWidget((int)UIID.LoadingScreen);
            Engine.CreateAssetsPackage("Scene");
            Engine.SetScenePackageName("Scene");

            m_IsDone = false;
            if (Engine.GetAssetsPackage("Scene", out AssetsPackage package))
            {
                Engine.StartGlobalCoroutine(InitializeUIPackage(package));
            }
        }

        IEnumerator InitializeUIPackage(AssetsPackage package)
        {
            EditorSimulateModeParameters param = AssetInitializeParam.EditorParams("Scene");
            InitializationOperation operation = package.InitializeAsync(param);
            yield return operation;
            m_IsDone = true;
        }
    }
}