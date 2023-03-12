using Universe;

namespace UniverseStudio
{
    public class AssetDownloadStateChecker : ICondition
    {
        public bool OnMatching()
        {
            return PatchSystem.DontNeedDownload();
        }
    }
}