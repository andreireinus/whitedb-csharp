namespace WhiteDb.Generator.Runner
{
    using System.IO;

    using WhiteDb.Generator.Library;

    public static class Program
    {
        public static void Main(string[] args)
        {
            using (var inputStream = new FileStream(@"..\..\..\..\lib\dbapi.h", FileMode.Open))
            {
                var parser = new HeaderFileParser(new TypeMapper());
                var functions = parser.Parse(inputStream);

                using (var streamWriter = new StreamWriter(@"..\..\..\WhiteDb.Data\NativeApiWrapper.cs"))
                {
                    var writer = new HeaderWriter("WhiteDb.Data", "NativeApiWrapper");
                    writer.Write(streamWriter, functions);
                }
            }
        }
    }
}