namespace WhiteDb.Speed.Runner
{
    using System;
    using System.Diagnostics;

    using WhiteDb.Data;

    public class Program
    {
        private static void Main(string[] args)
        {
            const string Name = "1";
            IntPtr db;

            var sw = Stopwatch.StartNew();
            for (var i = 0; i < 1000; i++)
            {
                db = NativeApiWrapper.wg_attach_database(Name, 1000);
                if (db == IntPtr.Zero)
                {
                    Console.WriteLine("failed at try {0}", i);
                    break;
                }

                NativeApiWrapper.wg_detach_database(db);
                NativeApiWrapper.wg_delete_database(Name);
            }
            sw.Stop();

            Console.WriteLine("Time: {0} ms", sw.ElapsedMilliseconds);
            Console.ReadKey();
        }
    }
}