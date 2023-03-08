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
            Engine.RegisterGameSystem<TestSystem>();
        }
    }
}