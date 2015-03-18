namespace WhiteDb.Data
{
    using System.Text;

    internal static class StringExtensionMethods
    {
        internal static byte[] ToByteArray(this string s)
        {
            return Encoding.Default.GetBytes(s);
        }
    }

    internal static class ByteArrayExtensionMethods
    {
        internal static string ToStringValue(this byte[] bytes)
        {
            return Encoding.Default.GetString(bytes);
        }
    }
}