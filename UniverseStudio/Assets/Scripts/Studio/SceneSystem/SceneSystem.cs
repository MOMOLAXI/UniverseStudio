using UnityEngine.SceneManagement;
using Universe;

namespace UniverseStudio
{
    public class SceneSystem : GameSystem
    {
        public override void Init()
        {
            Engine.RegisterClassEventHook("Scene", EntityEvent.OnCreate, OnCreateScene);
        }

        static void OnCreateScene(EntityID self, EntityID sender, Variables args)
        {
            Engine.LoadSceneAsync("Scene", "StudioMain").Completed += handle =>
            {
                Scene scene = handle.SceneObject;
                self.SetString("Name", scene.name);
                self.SetInt("Handle", scene.handle);
            };
        }
    }
}