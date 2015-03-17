namespace WhiteDb.Data
{
    using System;

    public class WhiteDbException : Exception
    {
        public WhiteDbException(string message)
            : base(message)
        {
        }

        public WhiteDbException(int code, string message)
            : this(string.Format("[code: {0}] {1}", code, message))
        {
        }
    }
}