namespace Universe
{
    public class Test1 : WorkNode
    {
        bool m_IsDone;

        public override bool IsDone => m_IsDone;

        public override string Name => "Test1";

        protected override void OnStart()
        {
            m_IsDone = false;
            Engine.StartGlobalCountBeat("Test1", OnTest, 2f, 1);
        }
        void OnTest(int count)
        {
            m_IsDone = true;
        }

    }
}