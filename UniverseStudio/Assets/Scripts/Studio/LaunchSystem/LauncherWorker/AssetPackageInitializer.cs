using Universe;

namespace UniverseStudio
{
    public class AssetPackageInitializer : WorkNode
    {
        bool m_IsDone;
        readonly (string packageName, EPlayMode playMode)[] m_TaskList =
        {
            (AssetInitializeParam.UI_PACKAGE, EPlayMode.EditorSimulateMode),
            (AssetInitializeParam.SCENE_PACKAGE, EPlayMode.EditorSimulateMode),
        };

        public override string Name => "Initialize Asset Packages";

        public override bool IsDone => m_IsDone;

        protected override void OnStart()
        {
            Engine.RegisterAssetInitialParam(AssetInitializeParam.EditorParams(AssetInitializeParam.UI_PACKAGE),
                                             AssetInitializeParam.UI_PACKAGE);
            Engine.RegisterAssetInitialParam(AssetInitializeParam.EditorParams(AssetInitializeParam.SCENE_PACKAGE),
                                             AssetInitializeParam.SCENE_PACKAGE);
            Engine.InitializeAssetPackageList(m_TaskList, result => m_IsDone = result)
                  .Forget();
        }

    }
}