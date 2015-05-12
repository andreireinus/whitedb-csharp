namespace WhiteDb.Data
{
    public static class OsHelper
    {
#if __MonoCS__
        private const bool Mono = true;
#else
        private const bool Mono = false;
#endif

        public static bool IsMono
        {
            get
            {
                return Mono;
            }
        }
    }
}