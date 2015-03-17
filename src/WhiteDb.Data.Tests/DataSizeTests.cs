namespace WhiteDb.Data.Tests
{
    using NUnit.Framework;

    [TestFixture]
    internal class DataSizeTests
    {
        [Test]
        public void ToInteger_WhenModifierIsNotPassed_ReturnsBytes()
        {
            Assert.That(DataSize.Size(10).ToInteger(), Is.EqualTo(10));
        }

        [Test]
        public void ToInteger_ChekcFor27Bytes_Returns27()
        {
            Assert.That(DataSize.Size(27).Bytes.ToInteger(), Is.EqualTo(27));
        }

        [Test]
        public void ToInteger_CheckForTwoKilobytes_Returns2048()
        {
            Assert.That(DataSize.Size(2).KiloBytes.ToInteger(), Is.EqualTo(2048));
        }

        [Test]
        public void ToInteger_CheckFor3Megabytes_Returns3145728()
        {
            Assert.That(DataSize.Size(3).MegaBytes.ToInteger(), Is.EqualTo(3145728));
        }
    }
}