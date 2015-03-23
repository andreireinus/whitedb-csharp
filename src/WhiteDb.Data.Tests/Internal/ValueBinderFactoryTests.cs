namespace WhiteDb.Data.Tests.Internal
{
    using NUnit.Framework;

    using WhiteDb.Data.ValueBinders;

    [TestFixture]
    public class ValueBinderFactoryTests
    {
        [Test]
        public void Get_WhenAskingForIntegerBinder_TypeIsCorrect()
        {
            var factory = new ValueBinderFactory();
            var binder = factory.Get<int>();
            Assert.That(binder is ValueBinder<int>, Is.True);
        }
    }
}