namespace WhiteDb.Generator.Library
{
    using System;

    public class TypeMapper
    {
        public string GetTypeName(string type)
        {
            type = type.Replace(" ", "");

            switch (type)
            {
                case "void*": return "IntPtr";
                case "void**": return "IntPtr";
                case "wg_query*": return "IntPtr";
                case "wg_int*": return "IntPtr";
                case "int*": return "IntPtr";
                case "wg_query_arg*": return "IntPtr";

                case "char*": return "string";

                case "void": return "void";
                case "int": return "int";
                case "wg_int": return "int";
                case "wg_uint": return "int";
                case "double": return "double";
                case "char": return "char";

                default:
                    throw new Exception("unknown type: " + type);
            }
        }
    }
}