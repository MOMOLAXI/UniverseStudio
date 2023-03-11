using UnityEngine;
using Universe;

namespace UniverseStudio
{
    public class GameInstance : MonoBehaviour
    {
        static GameInstance s_Instnace;

        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
        static void Initialize()
        {
            Engine.GetOrAddGlobalComponent(nameof(GameInstance), out s_Instnace);
        }

        static void RegistUIWidgets()
        {
            Engine.RegisterUIWidget((int)UIID.LoadingScreen, string.Empty, ECanvasLayer.TopLayer);
        }

        static void RegistGameSystems()
        {
            Engine.RegisterGameSystem<FileSystem>();
            Engine.RegisterGameSystem<ConfigurationSystem>();
            Engine.RegisterGameSystem<SceneSystem>();
            Engine.RegisterGameSystem<PatchSystem>();
            Engine.RegisterGameSystem<LaunchSystem>();
        }

        void Awake()
        {
            RegistUIWidgets();
            RegistGameSystems();
            Engine.GetSystem<LaunchSystem>()
                  .Start();
        }

    }
}