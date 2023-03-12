using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Universe;

namespace UniverseStudio
{
    public class AssetDownloader
    {
        public string PackageName;
        public float TotalSizeMb;
        public string TotalSizeMbText;
        public int TotalDownloadCount;
        public bool DownladResult;
        public PatchDownloaderOperation Operation;
    }

    public class PatchSystem : GameSystem
    {
        static readonly Dictionary<string, string> s_AssetVersion = new();
        static readonly Dictionary<string, AssetDownloader> s_AssetDownloader = new();

        public static void RegisterAssetDownloader(string packageName, PatchDownloaderOperation downloader)
        {
            float sizeMb = downloader.TotalDownloadBytes / 1048576f;
            sizeMb = Mathf.Clamp(sizeMb, 0.1f, float.MaxValue);
            string totalSizeMb = sizeMb.ToString("F1");
            s_AssetDownloader[packageName] = new()
            {
                PackageName = packageName,
                TotalSizeMb = sizeMb,
                TotalSizeMbText = totalSizeMb,
                TotalDownloadCount = downloader.TotalDownloadCount,
                DownladResult = false,
                Operation = downloader
            };
        }

        public static AssetDownloader GetDownloader(string packageName)
        {
            return s_AssetDownloader[packageName];
        }

        public static bool DontNeedDownload()
        {
            return s_AssetDownloader.Select(assetDownloader => assetDownloader.Value.TotalDownloadCount)
                                    .All(totalCount => totalCount <= 0);
        }

        public static void RegisterAsset(string packageName, string version)
        {
            s_AssetVersion[packageName] = version;
        }

        public static string GetAssetVersion(string packageName)
        {
            return s_AssetVersion[packageName];
        }

    }
}