namespace WhiteDb.Generator.Library
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;

    public class HeaderWriter
    {
        private readonly string namespaceName;

        private readonly string className;

        public HeaderWriter(string namespaceName, string className)
        {
            this.namespaceName = namespaceName;
            this.className = className;
        }

        public void Write(TextWriter writer, IEnumerable<FunctionDefinition> functions)
        {
            writer.WriteLine("namespace {0}", this.namespaceName);
            writer.WriteLine("{"); // namespace
            writer.WriteLine("\tusing System;");
            writer.WriteLine("\tusing System.Runtime.InteropServices;");
            writer.WriteLine();
            writer.WriteLine("\tpublic static class {0}", this.className);
            writer.WriteLine("\t{"); // class

            foreach (var function in functions)
            {
                this.Write(writer, function);
            }
            writer.WriteLine("\t}"); // class

            writer.WriteLine("}");
        }

        public void Write(TextWriter writer, FunctionDefinition function)
        {
            writer.WriteLine("\t\t[DllImport(\"wgdb.dll\", EntryPoint = \"{0}\", CallingConvention = CallingConvention.Cdecl)]", function.Name);
            writer.WriteLine("\t\tpublic static extern {0} {1}({2});", function.ReturnType, function.Name, GetParametersAsString(function.Parameters));
        }

        private static string GetParametersAsString(IDictionary<int, Tuple<string, string>> parameters)
        {
            return string.Join(", ", parameters.OrderBy(a => a.Key).Select(p => string.Format("{0} {1}", p.Value.Item1, p.Value.Item2)));
        }
    }
}