using UniverseStudio;

namespace Universe
{
    public class GameInitializer : WorkNode
    {
        public override bool IsDone => true;
        public override string Name => "Game Initialize";

        protected override void OnStart()
        {
            base.OnStart();
            Engine.StartGlobalCountBeat("LoadScene", OnCount, 4f, 1);
        }
        
        static void OnCount(int count)
        {
            Engine.CreateScene("StudioMain");
        }
    }
}