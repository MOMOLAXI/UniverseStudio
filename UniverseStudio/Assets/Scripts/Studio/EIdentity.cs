namespace UniverseStudio
{
    public enum EIdentity
    {
        Scene,
        MainPlayer,
        SimulateDateTimeContainer,
    }

    public static class EntityIdentity
    {
        public static int Get(EIdentity identity)
        {
            return (int)identity;
        }
    }

}