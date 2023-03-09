namespace Universe
{
    public class LaunchSystem : GameSystem
    {
        public void Start()
        {
            //初始化资源Package
            //等等

            Engine.Sequencer("GameLauncher")
                  .AppendSingle<Test1>()
                  .AppendParallel("TestParallel", new Test2(), new Test3())
                  .AppendSingle<Test4>()
                  .Start();
        }
    }
}