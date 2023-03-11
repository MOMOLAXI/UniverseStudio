using Universe;

namespace UniverseStudio
{
    public enum EMessage
    {
        OnAssetPackageCreated, //资源包创建(param : string packageName)

        OnSceneLoaded,
        OnSceneUnLoaded,
        OnActiveSceneChanged,
        OnMainCameraReady,
        OnUICameraReady,
        OnMainPlayerCreate,
    }

    public static class GameMessage
    {
        public static void BroadCast(EMessage message, Variables variables = null)
        {
            Engine.BroadCast((ulong)message, variables);
        }

        public static void Subscribe(EMessage message, MessageEventCallback callback)
        {
            Engine.Subscribe((ulong)message, callback);
        }
    }
}