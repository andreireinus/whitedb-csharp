namespace WhiteDb.Data.Internal
{
    using System;

    public interface IRecord
    {
        IntPtr Database { get; set; }

        IntPtr Record { get; set; }
    }
}