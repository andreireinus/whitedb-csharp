namespace WhiteDb.Data
{
    using System;
    using System.Runtime.InteropServices;

    public static class NativeApiWrapper
    {
        [DllImport("wgdb.dll", EntryPoint = "wg_attach_database", CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr wg_attach_database(string dbasename, int size);

        [DllImport("wgdb.dll", EntryPoint = "wg_attach_existing_database", CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr wg_attach_existing_database(string dbasename);

        [DllImport("wgdb.dll", EntryPoint = "wg_attach_logged_database", CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr wg_attach_logged_database(string dbasename, int size);

        [DllImport("wgdb.dll", EntryPoint = "wg_attach_database_mode", CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr wg_attach_database_mode(string dbasename, int size, int mode);

        [DllImport("wgdb.dll", EntryPoint = "wg_attach_logged_database_mode", CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr wg_attach_logged_database_mode(string dbasename, int size, int mode);

        [DllImport("wgdb.dll", EntryPoint = "wg_detach_database", CallingConvention = CallingConvention.Cdecl)]
        public static extern int wg_detach_database(IntPtr dbase);

        [DllImport("wgdb.dll", EntryPoint = "wg_delete_database", CallingConvention = CallingConvention.Cdecl)]
        public static extern int wg_delete_database(string dbasename);

        [DllImport("wgdb.dll", EntryPoint = "wg_attach_local_database", CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr wg_attach_local_database(int size);

        [DllImport("wgdb.dll", EntryPoint = "wg_delete_local_database", CallingConvention = CallingConvention.Cdecl)]
        public static extern void wg_delete_local_database(IntPtr dbase);

        [DllImport("wgdb.dll", EntryPoint = "wg_database_freesize", CallingConvention = CallingConvention.Cdecl)]
        public static extern int wg_database_freesize(IntPtr db);

        [DllImport("wgdb.dll", EntryPoint = "wg_database_size", CallingConvention = CallingConvention.Cdecl)]
        public static extern int wg_database_size(IntPtr db);

        [DllImport("wgdb.dll", EntryPoint = "wg_create_record", CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr wg_create_record(IntPtr db, int length);

        [DllImport("wgdb.dll", EntryPoint = "wg_create_raw_record", CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr wg_create_raw_record(IntPtr db, int length);

        [DllImport("wgdb.dll", EntryPoint = "wg_delete_record", CallingConvention = CallingConvention.Cdecl)]
        public static extern int wg_delete_record(IntPtr db, IntPtr rec);

        [DllImport("wgdb.dll", EntryPoint = "wg_get_first_record", CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr wg_get_first_record(IntPtr db);

        [DllImport("wgdb.dll", EntryPoint = "wg_get_next_record", CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr wg_get_next_record(IntPtr db, IntPtr record);

        [DllImport("wgdb.dll", EntryPoint = "wg_get_record_len", CallingConvention = CallingConvention.Cdecl)]
        public static extern int wg_get_record_len(IntPtr db, IntPtr record);

        [DllImport("wgdb.dll", EntryPoint = "wg_get_record_dataarray", CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr wg_get_record_dataarray(IntPtr db, IntPtr record);

        [DllImport("wgdb.dll", EntryPoint = "wg_set_field", CallingConvention = CallingConvention.Cdecl)]
        public static extern int wg_set_field(IntPtr db, IntPtr record, int fieldnr, int data);

        [DllImport("wgdb.dll", EntryPoint = "wg_set_new_field", CallingConvention = CallingConvention.Cdecl)]
        public static extern int wg_set_new_field(IntPtr db, IntPtr record, int fieldnr, int data);

        [DllImport("wgdb.dll", EntryPoint = "wg_set_int_field", CallingConvention = CallingConvention.Cdecl)]
        public static extern int wg_set_int_field(IntPtr db, IntPtr record, int fieldnr, int data);

        [DllImport("wgdb.dll", EntryPoint = "wg_set_double_field", CallingConvention = CallingConvention.Cdecl)]
        public static extern int wg_set_double_field(IntPtr db, IntPtr record, int fieldnr, double data);

        [DllImport("wgdb.dll", EntryPoint = "wg_set_str_field", CallingConvention = CallingConvention.Cdecl)]
        public static extern int wg_set_str_field(IntPtr db, IntPtr record, int fieldnr, string data);

        [DllImport("wgdb.dll", EntryPoint = "wg_update_atomic_field", CallingConvention = CallingConvention.Cdecl)]
        public static extern int wg_update_atomic_field(IntPtr db, IntPtr record, int fieldnr, int data, int old_data);

        [DllImport("wgdb.dll", EntryPoint = "wg_set_atomic_field", CallingConvention = CallingConvention.Cdecl)]
        public static extern int wg_set_atomic_field(IntPtr db, IntPtr record, int fieldnr, int data);

        [DllImport("wgdb.dll", EntryPoint = "wg_add_int_atomic_field", CallingConvention = CallingConvention.Cdecl)]
        public static extern int wg_add_int_atomic_field(IntPtr db, IntPtr record, int fieldnr, int data);

        [DllImport("wgdb.dll", EntryPoint = "wg_get_field", CallingConvention = CallingConvention.Cdecl)]
        public static extern int wg_get_field(IntPtr db, IntPtr record, int fieldnr);

        [DllImport("wgdb.dll", EntryPoint = "wg_get_field_type", CallingConvention = CallingConvention.Cdecl)]
        public static extern int wg_get_field_type(IntPtr db, IntPtr record, int fieldnr);

        [DllImport("wgdb.dll", EntryPoint = "wg_get_encoded_type", CallingConvention = CallingConvention.Cdecl)]
        public static extern int wg_get_encoded_type(IntPtr db, int data);

        [DllImport("wgdb.dll", EntryPoint = "wg_free_encoded", CallingConvention = CallingConvention.Cdecl)]
        public static extern int wg_free_encoded(IntPtr db, int data);

        [DllImport("wgdb.dll", EntryPoint = "wg_encode_null", CallingConvention = CallingConvention.Cdecl)]
        public static extern int wg_encode_null(IntPtr db, int data);

        [DllImport("wgdb.dll", EntryPoint = "wg_decode_null", CallingConvention = CallingConvention.Cdecl)]
        public static extern int wg_decode_null(IntPtr db, int data);

        [DllImport("wgdb.dll", EntryPoint = "wg_encode_int", CallingConvention = CallingConvention.Cdecl)]
        public static extern int wg_encode_int(IntPtr db, int data);

        [DllImport("wgdb.dll", EntryPoint = "wg_decode_int", CallingConvention = CallingConvention.Cdecl)]
        public static extern int wg_decode_int(IntPtr db, int data);

        [DllImport("wgdb.dll", EntryPoint = "wg_encode_double", CallingConvention = CallingConvention.Cdecl)]
        public static extern int wg_encode_double(IntPtr db, double data);

        [DllImport("wgdb.dll", EntryPoint = "wg_decode_double", CallingConvention = CallingConvention.Cdecl)]
        public static extern double wg_decode_double(IntPtr db, int data);

        [DllImport("wgdb.dll", EntryPoint = "wg_encode_fixpoint", CallingConvention = CallingConvention.Cdecl)]
        public static extern int wg_encode_fixpoint(IntPtr db, double data);

        [DllImport("wgdb.dll", EntryPoint = "wg_decode_fixpoint", CallingConvention = CallingConvention.Cdecl)]
        public static extern double wg_decode_fixpoint(IntPtr db, int data);

        [DllImport("wgdb.dll", EntryPoint = "wg_encode_date", CallingConvention = CallingConvention.Cdecl)]
        public static extern int wg_encode_date(IntPtr db, int data);

        [DllImport("wgdb.dll", EntryPoint = "wg_decode_date", CallingConvention = CallingConvention.Cdecl)]
        public static extern int wg_decode_date(IntPtr db, int data);

        [DllImport("wgdb.dll", EntryPoint = "wg_encode_time", CallingConvention = CallingConvention.Cdecl)]
        public static extern int wg_encode_time(IntPtr db, int data);

        [DllImport("wgdb.dll", EntryPoint = "wg_decode_time", CallingConvention = CallingConvention.Cdecl)]
        public static extern int wg_decode_time(IntPtr db, int data);

        [DllImport("wgdb.dll", EntryPoint = "wg_current_utcdate", CallingConvention = CallingConvention.Cdecl)]
        public static extern int wg_current_utcdate(IntPtr db);

        [DllImport("wgdb.dll", EntryPoint = "wg_current_localdate", CallingConvention = CallingConvention.Cdecl)]
        public static extern int wg_current_localdate(IntPtr db);

        [DllImport("wgdb.dll", EntryPoint = "wg_current_utctime", CallingConvention = CallingConvention.Cdecl)]
        public static extern int wg_current_utctime(IntPtr db);

        [DllImport("wgdb.dll", EntryPoint = "wg_current_localtime", CallingConvention = CallingConvention.Cdecl)]
        public static extern int wg_current_localtime(IntPtr db);

        [DllImport("wgdb.dll", EntryPoint = "wg_strf_iso_datetime", CallingConvention = CallingConvention.Cdecl)]
        public static extern int wg_strf_iso_datetime(IntPtr db, int date, int time, string buf);

        [DllImport("wgdb.dll", EntryPoint = "wg_strp_iso_date", CallingConvention = CallingConvention.Cdecl)]
        public static extern int wg_strp_iso_date(IntPtr db, string buf);

        [DllImport("wgdb.dll", EntryPoint = "wg_strp_iso_time", CallingConvention = CallingConvention.Cdecl)]
        public static extern int wg_strp_iso_time(IntPtr db, string inbuf);

        [DllImport("wgdb.dll", EntryPoint = "wg_ymd_to_date", CallingConvention = CallingConvention.Cdecl)]
        public static extern int wg_ymd_to_date(IntPtr db, int yr, int mo, int day);

        [DllImport("wgdb.dll", EntryPoint = "wg_hms_to_time", CallingConvention = CallingConvention.Cdecl)]
        public static extern int wg_hms_to_time(IntPtr db, int hr, int min, int sec, int prt);

        [DllImport("wgdb.dll", EntryPoint = "wg_date_to_ymd", CallingConvention = CallingConvention.Cdecl)]
        public static extern void wg_date_to_ymd(IntPtr db, int date, int yr, int mo, int day);

        [DllImport("wgdb.dll", EntryPoint = "wg_time_to_hms", CallingConvention = CallingConvention.Cdecl)]
        public static extern void wg_time_to_hms(IntPtr db, int time, IntPtr hr, IntPtr min, IntPtr sec, IntPtr prt);

        [DllImport("wgdb.dll", EntryPoint = "wg_encode_str", CallingConvention = CallingConvention.Cdecl)]
        public static extern int wg_encode_str(IntPtr db, byte[] str, byte[] lang);

        [DllImport("wgdb.dll", EntryPoint = "wg_decode_str", CallingConvention = CallingConvention.Cdecl)]
        public static extern byte[] wg_decode_str(IntPtr db, int data);

        [DllImport("wgdb.dll", EntryPoint = "wg_decode_str_lang", CallingConvention = CallingConvention.Cdecl)]
        public static extern string wg_decode_str_lang(IntPtr db, int data);

        [DllImport("wgdb.dll", EntryPoint = "wg_decode_str_len", CallingConvention = CallingConvention.Cdecl)]
        public static extern int wg_decode_str_len(IntPtr db, int data);

        [DllImport("wgdb.dll", EntryPoint = "wg_decode_str_lang_len", CallingConvention = CallingConvention.Cdecl)]
        public static extern int wg_decode_str_lang_len(IntPtr db, int data);

        [DllImport("wgdb.dll", EntryPoint = "wg_decode_str_copy", CallingConvention = CallingConvention.Cdecl)]
        public static extern int wg_decode_str_copy(IntPtr db, int data, byte[] strbuf, int buflen);

        [DllImport("wgdb.dll", EntryPoint = "wg_decode_str_lang_copy", CallingConvention = CallingConvention.Cdecl)]
        public static extern int wg_decode_str_lang_copy(IntPtr db, int data, string langbuf, int buflen);

        [DllImport("wgdb.dll", EntryPoint = "wg_encode_xmlliteral", CallingConvention = CallingConvention.Cdecl)]
        public static extern int wg_encode_xmlliteral(IntPtr db, string str, string xsdtype);

        [DllImport("wgdb.dll", EntryPoint = "wg_decode_xmlliteral", CallingConvention = CallingConvention.Cdecl)]
        public static extern string wg_decode_xmlliteral(IntPtr db, int data);

        [DllImport("wgdb.dll", EntryPoint = "wg_decode_xmlliteral_xsdtype", CallingConvention = CallingConvention.Cdecl)]
        public static extern string wg_decode_xmlliteral_xsdtype(IntPtr db, int data);

        [DllImport("wgdb.dll", EntryPoint = "wg_decode_xmlliteral_len", CallingConvention = CallingConvention.Cdecl)]
        public static extern int wg_decode_xmlliteral_len(IntPtr db, int data);

        [DllImport("wgdb.dll", EntryPoint = "wg_decode_xmlliteral_xsdtype_len", CallingConvention = CallingConvention.Cdecl)]
        public static extern int wg_decode_xmlliteral_xsdtype_len(IntPtr db, int data);

        [DllImport("wgdb.dll", EntryPoint = "wg_decode_xmlliteral_copy", CallingConvention = CallingConvention.Cdecl)]
        public static extern int wg_decode_xmlliteral_copy(IntPtr db, int data, string strbuf, int buflen);

        [DllImport("wgdb.dll", EntryPoint = "wg_decode_xmlliteral_xsdtype_copy", CallingConvention = CallingConvention.Cdecl)]
        public static extern int wg_decode_xmlliteral_xsdtype_copy(IntPtr db, int data, string strbuf, int buflen);

        [DllImport("wgdb.dll", EntryPoint = "wg_encode_uri", CallingConvention = CallingConvention.Cdecl)]
        public static extern int wg_encode_uri(IntPtr db, string str, string nspace);

        [DllImport("wgdb.dll", EntryPoint = "wg_decode_uri", CallingConvention = CallingConvention.Cdecl)]
        public static extern string wg_decode_uri(IntPtr db, int data);

        [DllImport("wgdb.dll", EntryPoint = "wg_decode_uri_prefix", CallingConvention = CallingConvention.Cdecl)]
        public static extern string wg_decode_uri_prefix(IntPtr db, int data);

        [DllImport("wgdb.dll", EntryPoint = "wg_decode_uri_len", CallingConvention = CallingConvention.Cdecl)]
        public static extern int wg_decode_uri_len(IntPtr db, int data);

        [DllImport("wgdb.dll", EntryPoint = "wg_decode_uri_prefix_len", CallingConvention = CallingConvention.Cdecl)]
        public static extern int wg_decode_uri_prefix_len(IntPtr db, int data);

        [DllImport("wgdb.dll", EntryPoint = "wg_decode_uri_copy", CallingConvention = CallingConvention.Cdecl)]
        public static extern int wg_decode_uri_copy(IntPtr db, int data, string strbuf, int buflen);

        [DllImport("wgdb.dll", EntryPoint = "wg_decode_uri_prefix_copy", CallingConvention = CallingConvention.Cdecl)]
        public static extern int wg_decode_uri_prefix_copy(IntPtr db, int data, string strbuf, int buflen);

        [DllImport("wgdb.dll", EntryPoint = "wg_encode_blob", CallingConvention = CallingConvention.Cdecl)]
        public static extern int wg_encode_blob(IntPtr db, string str, string type, int len);

        [DllImport("wgdb.dll", EntryPoint = "wg_decode_blob", CallingConvention = CallingConvention.Cdecl)]
        public static extern string wg_decode_blob(IntPtr db, int data);

        [DllImport("wgdb.dll", EntryPoint = "wg_decode_blob_type", CallingConvention = CallingConvention.Cdecl)]
        public static extern string wg_decode_blob_type(IntPtr db, int data);

        [DllImport("wgdb.dll", EntryPoint = "wg_decode_blob_len", CallingConvention = CallingConvention.Cdecl)]
        public static extern int wg_decode_blob_len(IntPtr db, int data);

        [DllImport("wgdb.dll", EntryPoint = "wg_decode_blob_copy", CallingConvention = CallingConvention.Cdecl)]
        public static extern int wg_decode_blob_copy(IntPtr db, int data, string strbuf, int buflen);

        [DllImport("wgdb.dll", EntryPoint = "wg_decode_blob_type_len", CallingConvention = CallingConvention.Cdecl)]
        public static extern int wg_decode_blob_type_len(IntPtr db, int data);

        [DllImport("wgdb.dll", EntryPoint = "wg_decode_blob_type_copy", CallingConvention = CallingConvention.Cdecl)]
        public static extern int wg_decode_blob_type_copy(IntPtr db, int data, string langbuf, int buflen);

        [DllImport("wgdb.dll", EntryPoint = "wg_encode_record", CallingConvention = CallingConvention.Cdecl)]
        public static extern int wg_encode_record(IntPtr db, IntPtr data);

        [DllImport("wgdb.dll", EntryPoint = "wg_decode_record", CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr wg_decode_record(IntPtr db, int data);

        [DllImport("wgdb.dll", EntryPoint = "wg_encode_char", CallingConvention = CallingConvention.Cdecl)]
        public static extern int wg_encode_char(IntPtr db, char data);

        [DllImport("wgdb.dll", EntryPoint = "wg_decode_char", CallingConvention = CallingConvention.Cdecl)]
        public static extern char wg_decode_char(IntPtr db, int data);

        [DllImport("wgdb.dll", EntryPoint = "wg_encode_anonconst", CallingConvention = CallingConvention.Cdecl)]
        public static extern int wg_encode_anonconst(IntPtr db, string str);

        [DllImport("wgdb.dll", EntryPoint = "wg_decode_anonconst", CallingConvention = CallingConvention.Cdecl)]
        public static extern string wg_decode_anonconst(IntPtr db, int data);

        [DllImport("wgdb.dll", EntryPoint = "wg_encode_var", CallingConvention = CallingConvention.Cdecl)]
        public static extern int wg_encode_var(IntPtr db, int varnr);

        [DllImport("wgdb.dll", EntryPoint = "wg_decode_var", CallingConvention = CallingConvention.Cdecl)]
        public static extern int wg_decode_var(IntPtr db, int data);

        [DllImport("wgdb.dll", EntryPoint = "wg_dump", CallingConvention = CallingConvention.Cdecl)]
        public static extern int wg_dump(IntPtr db, string fileName);

        [DllImport("wgdb.dll", EntryPoint = "wg_import_dump", CallingConvention = CallingConvention.Cdecl)]
        public static extern int wg_import_dump(IntPtr db, string fileName);

        [DllImport("wgdb.dll", EntryPoint = "wg_start_logging", CallingConvention = CallingConvention.Cdecl)]
        public static extern int wg_start_logging(IntPtr db);

        [DllImport("wgdb.dll", EntryPoint = "wg_stop_logging", CallingConvention = CallingConvention.Cdecl)]
        public static extern int wg_stop_logging(IntPtr db);

        [DllImport("wgdb.dll", EntryPoint = "wg_replay_log", CallingConvention = CallingConvention.Cdecl)]
        public static extern int wg_replay_log(IntPtr db, string filename);

        [DllImport("wgdb.dll", EntryPoint = "wg_start_write", CallingConvention = CallingConvention.Cdecl)]
        public static extern int wg_start_write(IntPtr dbase);

        [DllImport("wgdb.dll", EntryPoint = "wg_end_write", CallingConvention = CallingConvention.Cdecl)]
        public static extern int wg_end_write(IntPtr dbase, int @lock);

        [DllImport("wgdb.dll", EntryPoint = "wg_start_read", CallingConvention = CallingConvention.Cdecl)]
        public static extern int wg_start_read(IntPtr dbase);

        [DllImport("wgdb.dll", EntryPoint = "wg_end_read", CallingConvention = CallingConvention.Cdecl)]
        public static extern int wg_end_read(IntPtr dbase, int @lock);

        [DllImport("wgdb.dll", EntryPoint = "wg_print_db", CallingConvention = CallingConvention.Cdecl)]
        public static extern void wg_print_db(IntPtr db);

        [DllImport("wgdb.dll", EntryPoint = "wg_print_record", CallingConvention = CallingConvention.Cdecl)]
        public static extern void wg_print_record(IntPtr db, IntPtr rec);

        [DllImport("wgdb.dll", EntryPoint = "wg_snprint_value", CallingConvention = CallingConvention.Cdecl)]
        public static extern void wg_snprint_value(IntPtr db, int enc, string buf, int buflen);

        [DllImport("wgdb.dll", EntryPoint = "wg_parse_and_encode", CallingConvention = CallingConvention.Cdecl)]
        public static extern int wg_parse_and_encode(IntPtr db, string buf);

        [DllImport("wgdb.dll", EntryPoint = "wg_parse_and_encode_param", CallingConvention = CallingConvention.Cdecl)]
        public static extern int wg_parse_and_encode_param(IntPtr db, string buf);

        [DllImport("wgdb.dll", EntryPoint = "wg_export_db_csv", CallingConvention = CallingConvention.Cdecl)]
        public static extern void wg_export_db_csv(IntPtr db, string filename);

        [DllImport("wgdb.dll", EntryPoint = "wg_import_db_csv", CallingConvention = CallingConvention.Cdecl)]
        public static extern int wg_import_db_csv(IntPtr db, string filename);

        [DllImport("wgdb.dll", EntryPoint = "wg_free_query", CallingConvention = CallingConvention.Cdecl)]
        public static extern void wg_free_query(IntPtr db, IntPtr query);

        [DllImport("wgdb.dll", EntryPoint = "wg_encode_query_param_null", CallingConvention = CallingConvention.Cdecl)]
        public static extern int wg_encode_query_param_null(IntPtr db, string data);

        [DllImport("wgdb.dll", EntryPoint = "wg_encode_query_param_record", CallingConvention = CallingConvention.Cdecl)]
        public static extern int wg_encode_query_param_record(IntPtr db, IntPtr data);

        [DllImport("wgdb.dll", EntryPoint = "wg_encode_query_param_char", CallingConvention = CallingConvention.Cdecl)]
        public static extern int wg_encode_query_param_char(IntPtr db, char data);

        [DllImport("wgdb.dll", EntryPoint = "wg_encode_query_param_fixpoint", CallingConvention = CallingConvention.Cdecl)]
        public static extern int wg_encode_query_param_fixpoint(IntPtr db, double data);

        [DllImport("wgdb.dll", EntryPoint = "wg_encode_query_param_date", CallingConvention = CallingConvention.Cdecl)]
        public static extern int wg_encode_query_param_date(IntPtr db, int data);

        [DllImport("wgdb.dll", EntryPoint = "wg_encode_query_param_time", CallingConvention = CallingConvention.Cdecl)]
        public static extern int wg_encode_query_param_time(IntPtr db, int data);

        [DllImport("wgdb.dll", EntryPoint = "wg_encode_query_param_var", CallingConvention = CallingConvention.Cdecl)]
        public static extern int wg_encode_query_param_var(IntPtr db, int data);

        [DllImport("wgdb.dll", EntryPoint = "wg_encode_query_param_int", CallingConvention = CallingConvention.Cdecl)]
        public static extern int wg_encode_query_param_int(IntPtr db, int data);

        [DllImport("wgdb.dll", EntryPoint = "wg_encode_query_param_double", CallingConvention = CallingConvention.Cdecl)]
        public static extern int wg_encode_query_param_double(IntPtr db, double data);

        [DllImport("wgdb.dll", EntryPoint = "wg_encode_query_param_str", CallingConvention = CallingConvention.Cdecl)]
        public static extern int wg_encode_query_param_str(IntPtr db, string data, string lang);

        [DllImport("wgdb.dll", EntryPoint = "wg_encode_query_param_xmlliteral", CallingConvention = CallingConvention.Cdecl)]
        public static extern int wg_encode_query_param_xmlliteral(IntPtr db, string data, string xsdtype);

        [DllImport("wgdb.dll", EntryPoint = "wg_encode_query_param_uri", CallingConvention = CallingConvention.Cdecl)]
        public static extern int wg_encode_query_param_uri(IntPtr db, string data, string prefix);

        [DllImport("wgdb.dll", EntryPoint = "wg_free_query_param", CallingConvention = CallingConvention.Cdecl)]
        public static extern int wg_free_query_param(IntPtr db, int data);

        [DllImport("wgdb.dll", EntryPoint = "wg_register_external_db", CallingConvention = CallingConvention.Cdecl)]
        public static extern int wg_register_external_db(IntPtr db, IntPtr extdb);

        [DllImport("wgdb.dll", EntryPoint = "wg_encode_external_data", CallingConvention = CallingConvention.Cdecl)]
        public static extern int wg_encode_external_data(IntPtr db, IntPtr extdb, int encoded);

        [DllImport("wgdb.dll", EntryPoint = "wg_parse_json_file", CallingConvention = CallingConvention.Cdecl)]
        public static extern int wg_parse_json_file(IntPtr db, string filename);

        [DllImport("wgdb.dll", EntryPoint = "wg_check_json", CallingConvention = CallingConvention.Cdecl)]
        public static extern int wg_check_json(IntPtr db, string buf);

        [DllImport("wgdb.dll", EntryPoint = "wg_parse_json_document", CallingConvention = CallingConvention.Cdecl)]
        public static extern int wg_parse_json_document(IntPtr db, string buf, IntPtr document);

        [DllImport("wgdb.dll", EntryPoint = "wg_parse_json_fragment", CallingConvention = CallingConvention.Cdecl)]
        public static extern int wg_parse_json_fragment(IntPtr db, string buf, IntPtr document);
    }
}