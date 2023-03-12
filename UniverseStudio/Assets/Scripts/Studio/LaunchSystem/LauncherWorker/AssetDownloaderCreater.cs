using Universe;

namespace UniverseStudio
{
    public class AssetDownloaderCreater : WorkNode
    {
        public override bool IsDone => true;
        public override string Name => "Create Assets Downloader";

        protected override void OnStart()
        {
            CreateDownloader(AssetInitializeParam.SCENE_PACKAGE);
            CreateDownloader(AssetInitializeParam.UI_PACKAGE);
        }

        static void CreateDownloader(string packageName)
        {
            // 发现新更新文件后，挂起流程系统
            // 需要在下载前检测磁盘空间不足
            const int downloadingMaxCount = 10;
            const int failedTryAgain = 3;
            PatchDownloaderOperation downLoader = Engine.CreatePatchDownloader(AssetInitializeParam.UI_PACKAGE, downloadingMaxCount, failedTryAgain);
            PatchSystem.RegisterAssetDownloader(packageName, downLoader);
        }

    }
}