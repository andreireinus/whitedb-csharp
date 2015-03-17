namespace WhiteDb.Generator.Library.Tests
{
    using System;

    using NUnit.Framework;

    using WhiteDb.Generator.Library;

    [TestFixture]
    internal class TypeMapperTests
    {
        private readonly TypeMapper mapper = new TypeMapper();

        [Test]
        [TestCase("void*", "IntPtr")]
        [TestCase("int*", "IntPtr")]
        [TestCase("char*", "string")]
        [TestCase("int", "int")]
        public void GetTypeName(string type, string expected)
        {
            Assert.That(this.mapper.GetTypeName(type), Is.EqualTo(expected));
        }

        [Test]
        public void GetTypeName_WhenUnknownType_ThrowsException()
        {
            Assert.Throws<Exception>(() => this.mapper.GetTypeName("unknown"));
        }
    }
}