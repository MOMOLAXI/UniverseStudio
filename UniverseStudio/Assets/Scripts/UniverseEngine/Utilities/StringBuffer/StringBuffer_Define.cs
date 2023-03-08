namespace Universe
{
    /// <summary>
    /// 字符串容器
    /// </summary>
    internal partial class StringBuffer
    {
        struct StringData
        {
            public int Pos;
            public int MaxCount;
            public bool Used;

            public EncodingType Encoding;
            public string Str;
            public int Count;
        }

        public enum EncodingType
        {
            Default,
            UTF8,
            Unicode,
        }
    }
}