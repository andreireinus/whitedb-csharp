namespace WhiteDb.Generator.Library.Tests
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Text;

    using NUnit.Framework;

    using WhiteDb.Generator.Library;

    [TestFixture]
    internal class HeaderWriterTests
    {
        private readonly HeaderWriter writer = new HeaderWriter("WhiteDb.Data", "NativeApi");

        [Test]
        public void WriteFunction_TwoParameterFunction_TwoLinesWithImportAndDeclaration()
        {
            var f = new FunctionDefinition("wg_test", "int")
                    {
                        Parameters =
                            new Dictionary<int, Tuple<string, string>>
                            {
                                { 0, new Tuple<string, string>("int", "param1") },
                                { 1, new Tuple<string, string>("int", "param2") },
                            }
                    };

            var sb = new StringBuilder();
            using (var stringWriter = new StringWriter(sb))
            {
                this.writer.Write(stringWriter, f);
            }

            var expected = "\t\t[DllImport(\"wgdb.dll\", EntryPoint = \"wg_test\", CallingConvention = CallingConvention.Cdecl)]" + Environment.NewLine + "\t\tpublic static extern int wg_test(int param1, int param2);" + Environment.NewLine;

            Assert.That(sb.ToString(), Is.EqualTo(expected));
        }

        [Test]
        public void Write_WithOneFunction_FileContentIsGenerated()
        {
            var expected = "namespace WhiteDb.Data" + Environment.NewLine + "{" + Environment.NewLine +
                "\tusing System;" + Environment.NewLine +
                "\tusing System.Runtime.InteropServices;" + Environment.NewLine + Environment.NewLine +
                "\tpublic static class NativeApi" + Environment.NewLine + "\t{" + Environment.NewLine +
                "\t\t[DllImport(\"wgdb.dll\", EntryPoint = \"wg_test\", CallingConvention = CallingConvention.Cdecl)]" + Environment.NewLine +
                "\t\tpublic static extern int wg_test(int param1, int param2);" + Environment.NewLine +
                "\t}" + Environment.NewLine +
                "}" + Environment.NewLine;

            var functions = new List<FunctionDefinition>
                                {
                                    new FunctionDefinition("wg_test", "int")
                                    {
                                        Parameters = new Dictionary<int, Tuple<string, string>>
                                                     {
                                                         {0, new Tuple<string, string>("int", "param1")},
                                                         {1, new Tuple<string, string>("int", "param2")},
                                                     }
                                    }
                                };

            var sb = new StringBuilder();
            using (var stringWriter = new StringWriter(sb))
            {
                this.writer.Write(stringWriter, functions);
            }

            Assert.That(sb.ToString(), Is.EqualTo(expected));
        }
    }
}