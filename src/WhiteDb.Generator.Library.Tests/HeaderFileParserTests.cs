namespace WhiteDb.Generator.Library.Tests
{
    using System;
    using System.Linq;

    using NUnit.Framework;

    [TestFixture]
    public class HeaderFileParserTests
    {
        private readonly HeaderFileParser parser = new HeaderFileParser(new TypeMapper());

        [Test]
        public void IsFunctionLine_WhenEmptyString_IsFalse()
        {
            Assert.That(HeaderFileParser.IsFunctionLine(""), Is.False);
        }

        [Test]
        public void IsFunctionLine_WhenFunctionLine_IsTrue()
        {
            Assert.That(HeaderFileParser.IsFunctionLine("int wg_detach_database(void* dbase);"), Is.True);
        }

        [Test]
        public void IsFunctionLine_WhenStartsWithAsterix_IsFalse()
        {
            Assert.That(HeaderFileParser.IsFunctionLine("*"), Is.False);
        }

        [Test]
        public void ParseFunctionLine_WhenEmptyString_ThrowsException()
        {
            Assert.Throws<ArgumentException>(() => { this.parser.ParseFunctionLine(""); });
        }

        [Test]
        public void ParseFunctionLine_WhenMultipleParameters_HasMultipleParameters()
        {
            var definition = this.parser.ParseFunctionLine("void* wg_attach_logged_database_mode(char* dbasename, wg_int size, int mode);");

            Assert.That(definition.Name, Is.EqualTo("wg_attach_logged_database_mode"));
            Assert.That(definition.ReturnType, Is.EqualTo("IntPtr"));
            Assert.That(definition.Parameters, Is.Not.Empty);

            var p1 = definition.Parameters[0];
            Assert.That(p1.Item1, Is.EqualTo("string"));
            Assert.That(p1.Item2, Is.EqualTo("dbasename"));

            var p2 = definition.Parameters[1];
            Assert.That(p2.Item1, Is.EqualTo("int"));
            Assert.That(p2.Item2, Is.EqualTo("size"));

            var p3 = definition.Parameters[2];
            Assert.That(p3.Item1, Is.EqualTo("int"));
            Assert.That(p3.Item2, Is.EqualTo("mode"));
        }

        [Test]
        public void ParseFunctionLine_WhenSingleParameter_CorrectName()
        {
            var definition = this.parser.ParseFunctionLine("int wg_detach_database(void* dbase);");

            Assert.That(definition.Name, Is.EqualTo("wg_detach_database"));
            Assert.That(definition.ReturnType, Is.EqualTo("int"));
            Assert.That(definition.Parameters, Is.Not.Empty);

            var parameters = definition.Parameters;
            Assert.That(parameters.First().Value.Item1, Is.EqualTo("IntPtr"));
            Assert.That(parameters.First().Value.Item2, Is.EqualTo("dbase"));
        }

        [Test]
        public void ParseParameters_EmptyString_ZeroParameters()
        {
            var result = this.parser.ParseParameters("");
            Assert.That(result, Is.Empty);
        }

        [Test]
        public void ParseParameters_SingleParameter_OneParameters()
        {
            var result = this.parser.ParseParameters("char* dbasename");
            Assert.That(result, Is.Not.Empty);

            var p = result.First();
            Assert.That(p.Key, Is.EqualTo(0));
            Assert.That(p.Value.Item1, Is.EqualTo("string"));
            Assert.That(p.Value.Item2, Is.EqualTo("dbasename"));
        }

        [Test]
        public void ParseParameters_ReservedKeywordInParameter_NameIsReplacedWithAt()
        {
            var result = this.parser.ParseParameters("int lock");
            Assert.That(result, Is.Not.Empty);

            var p = result.First();
            Assert.That(p.Key, Is.EqualTo(0));
            Assert.That(p.Value.Item1, Is.EqualTo("int"));
            Assert.That(p.Value.Item2, Is.EqualTo("@lock"));
        }

        [Test]
        public void Read_WhenFileProvided_ResultsGenerated()
        {
            using (var stream = typeof(HeaderWriterTests).Assembly.GetManifestResourceStream("WhiteDb.Generator.Library.Tests.Resources.dbapi.h"))
            {
                var result = this.parser.Parse(stream).ToArray();

                Assert.That(result, Is.Not.Empty);
            }
        }
    }
}