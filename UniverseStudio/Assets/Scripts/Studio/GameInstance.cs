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

        void Awake()
        {
            Engine.RegisterGameSystem<FileSystem>();
            Engine.RegisterGameSystem<ConfigurationSystem>();
            Engine.RegisterGameSystem<UISystem>();
            Engine.RegisterGameSystem<SceneSystem>();
            Engine.RegisterGameSystem<PatchSystem>();


            Engine.RegisterGameSystem<LaunchSystem>();
            Engine.GetSystem<LaunchSystem>()
                  .Start();
        }

    }
}