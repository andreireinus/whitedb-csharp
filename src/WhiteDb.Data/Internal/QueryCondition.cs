namespace WhiteDb.Data.Internal
{
    using System;
    using System.Runtime.InteropServices;

    [StructLayout(LayoutKind.Sequential)]
    public struct QueryCondition
    {
        public UIntPtr Column { get; set; }

        public UIntPtr Condition { get; set; }

        public UIntPtr Value { get; set; }
    }
}