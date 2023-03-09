namespace Universe
{
    public class Test3 : WorkNode
    {
        bool m_IsDone;

        public override bool IsDone => m_IsDone;
        public override string Name => "Test3";
        protected override void OnStart()
        {
            m_IsDone = false;
            Engine.StartGlobalCountBeat("Test3", OnTest, 3f, 1);
        }
        void OnTest(int count)
        {
            m_IsDone = true;
        }

    }
}