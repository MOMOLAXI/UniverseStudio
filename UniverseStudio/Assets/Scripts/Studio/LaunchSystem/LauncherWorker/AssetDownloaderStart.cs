using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Universe;

namespace UniverseStudio
{
    public class AssetDownloaderStart : WorkNode
    {
        readonly Dictionary<string, bool> m_DownloadStates = new();

        public override bool IsDone => m_DownloadStates.Values.Aggregate(true, (current, value) => value && current);
        public override string Name => "Start Download Assets";

        protected override void OnStart()
        {
            AssetDownloader sceneDownloader = PatchSystem.GetDownloader(AssetInitializeParam.SCENE_PACKAGE);
            AssetDownloader uiDownloader = PatchSystem.GetDownloader(AssetInitializeParam.UI_PACKAGE);
            Engine.StartGlobalCoroutine(StartDownload(sceneDownloader));
            Engine.StartGlobalCoroutine(StartDownload(uiDownloader));
        }

        IEnumerator StartDownload(AssetDownloader downloader)
        {
            downloader.Operation.OnDownloadErrorCallback = (name, error) =>
            {
                GameMessage.BroadCast(EMessage.OnAssetDownLoadError, Variables.AllocNonHold() > name > error);
            };

            downloader.Operation.OnDownloadProgressCallback = (count, downloadCount, bytes, downloadBytes) =>
            {
                GameMessage.BroadCast(EMessage.OnAssetDownLoadError,
                                      Variables.AllocNonHold() > count > downloadCount > bytes > downloadBytes);
            };

            downloader.Operation.BeginDownload();
            yield return downloader.Operation;
            downloader.DownladResult = downloader.Operation.Status == EOperationStatus.Succeed;
            m_DownloadStates[downloader.PackageName] = true;
        }
    }
}