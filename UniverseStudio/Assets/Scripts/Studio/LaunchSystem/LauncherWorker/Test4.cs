namespace Universe
{
    public class Test4 : WorkNode
    {
        bool m_IsDone;

        public override bool IsDone => m_IsDone;
        public override string Name => "Test4";
        protected override void OnStart()
        {
            m_IsDone = false;
            Engine.StartGlobalCountBeat("Test4", OnTest, 4f, 1);
        }
        void OnTest(int count)
        {
            m_IsDone = true;
        }

    }
}