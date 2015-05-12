namespace WhiteDb.Data
{
    using System;
    using System.Runtime.InteropServices;

    public static class IndexApi
    {
        public const int WG_INDEX_TYPE_TTREE = 50;
        public const int WG_INDEX_TYPE_TTREE_JSON = 51;
        public const int WG_INDEX_TYPE_HASH = 60;
        public const int WG_INDEX_TYPE_HASH_JSON = 61;

#if __MonoCS__
        [DllImport("libwgdb", EntryPoint = "wg_create_index", CallingConvention = CallingConvention.Cdecl)]
        public static extern int wg_create_index(IntPtr db, int column, int type, IntPtr matchrec, int reclen);
#else

        [DllImport("wgdb.dll", EntryPoint = "wg_create_index", CallingConvention = CallingConvention.Cdecl)]
        public static extern int wg_create_index(IntPtr db, int column, int type, IntPtr matchrec, int reclen);

        //[DllImport("libwgdb", EntryPoint = "wg_create_multi_index", CallingConvention = CallingConvention.Cdecl)]
        //public static extern int wg_create_multi_index(IntPtr db, IntPtr columns, int colCount, int type, IntPtr matchrec, int reclen);

        //[DllImport("wgdb.dll", EntryPoint = "wg_drop_index", CallingConvention = CallingConvention.Cdecl)]
        //public static extern int wg_drop_index(IntPtr db, int indexId);

        //[DllImport("libwgdb", EntryPoint = "wg_column_to_index_id", CallingConvention = CallingConvention.Cdecl)]
        //public static extern int wg_column_to_index_id(IntPtr db, int column, int type, IntPtr matchrec, int reclen);

        //[DllImport("libwgdb", EntryPoint = "wg_multi_column_to_index_id", CallingConvention = CallingConvention.Cdecl)]
        //public static extern int wg_multi_column_to_index_id(IntPtr db, IntPtr columns, int col_count, int type, IntPtr matchrec, int reclen);

        //[DllImport("libwgdb", EntryPoint = "wg_get_index_type", CallingConvention = CallingConvention.Cdecl)]
        //public static extern int wg_get_index_type(IntPtr db, int index_id);

        //[DllImport("libwgdb", EntryPoint = "wg_get_index_template", CallingConvention = CallingConvention.Cdecl)]
        //public static extern IntPtr wg_get_index_template(IntPtr db, int index_id, IntPtr reclen);

#endif
    }
}