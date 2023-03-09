namespace Universe
{
    public class Test2 : WorkNode
    {
        bool m_IsDone;

        public override bool IsDone => m_IsDone;
        public override string Name => "Test2";
        protected override void OnStart()
        {
            m_IsDone = false;
            Engine.StartGlobalCountBeat("Test2", OnTest, 2f, 1);
        }
        void OnTest(int count)
        {
            m_IsDone = true;
        }

    }
}