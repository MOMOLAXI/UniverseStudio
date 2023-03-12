using Universe;

namespace UniverseStudio
{
    public enum EMessage
    {
        OnLaunchSequenceUpdate,

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