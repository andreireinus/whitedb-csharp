namespace WhiteDb.Data.Tests
{
    using NUnit.Framework;

    [TestFixture]
    public class OsHelperTests
    {
        [Test]
        public void IsMono()
        {
#if __MonoCS__
            const bool Mono = true;
#else
            const bool Mono = false;
#endif

            Assert.That(OsHelper.IsMono, Is.EqualTo(Mono));
        }
    }
}