using UniverseStudio;

namespace Universe
{
    public static class SystemRegister
    {
        public static void Regsiter()
        {
            Engine.RegisterGameSystem<FileSystem>();
            Engine.RegisterGameSystem<ConfigurationSystem>();
            Engine.RegisterGameSystem<UISystem>();
            Engine.RegisterGameSystem<SceneSystem>();
            Engine.RegisterGameSystem<TestSystem>();
            
            Engine.RegisterGameSystem<LaunchSystem>(); //最后注册启动系统
        }
    }
}