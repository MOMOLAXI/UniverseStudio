using System;
using System.IO;
using Universe;

namespace UniverseStudio
{
    public static class AssetInitializeParam
    {
        public const string UI_PACKAGE = "UI";
        public const string SCENE_PACKAGE = "Scene";
        
        public static EditorSimulateModeParameters EditorParams(string packageName)
        {
            EditorSimulateModeParameters parameters = new()
            {
                SimulatePatchManifestPath = EditorSimulateModeUtility.SimulateBuild(packageName),
                PlayMode = EPlayMode.EditorSimulateMode
            };

            return parameters;
        }

        public static OfflinePlayModeParameters OfflineParams()
        {
            OfflinePlayModeParameters parameters = new()
            {
                DecryptionServices = new GameDecryptionServices(),
                PlayMode = EPlayMode.OfflinePlayMode
            };

            return parameters;
        }

        public static HostPlayModeParameters HostPlay = new()
        {
            DecryptionServices = new GameDecryptionServices(),
            QueryServices = new GameQueryServices(),
            DefaultHostServer = "http://127.0.0.1",
            FallbackHostServer = "http://127.0.0.1",
            PlayMode = EPlayMode.HostPlayMode
        };

        /// <summary>
        /// 资源文件解密服务类
        /// </summary>
        private class GameDecryptionServices : IDecryptionServices
        {
            public ulong LoadFromFileOffset(DecryptFileInfo fileInfo)
            {
                return 32;
            }

            public byte[] LoadFromMemory(DecryptFileInfo fileInfo)
            {
                throw new NotImplementedException();
            }

            public FileStream LoadFromStream(DecryptFileInfo fileInfo)
            {
                BundleStream bundleStream = new(fileInfo.FilePath, FileMode.Open);
                return bundleStream;
            }

            public uint GetManagedReadBufferSize()
            {
                return 1024;
            }
        }

        /// <summary>
        /// 内置文件查询服务类
        /// </summary>
        private class GameQueryServices : IQueryServices
        {
            public bool QueryStreamingAssets(string fileName)
            {
                string buildinFolderName = Engine.GetStreamingAssetBuildinFolderName();
                return StreamingAssetsUtility.FileExists($"{buildinFolderName}/{fileName}");
            }
        }

        public class BundleStream : FileStream
        {
            public const byte KEY = 64;

            public BundleStream(string path, FileMode mode,
                                FileAccess access,
                                FileShare share,
                                int bufferSize, bool useAsync)
                : base(path, mode, access, share, bufferSize, useAsync)
            {
            }

            public BundleStream(string path, FileMode mode) : base(path, mode)
            {
            }

            public override int Read(byte[] array, int offset, int count)
            {
                int index = base.Read(array, offset, count);
                for (int i = 0; i < array.Length; i++)
                {
                    array[i] ^= KEY;
                }
                return index;
            }
        }
    }
}