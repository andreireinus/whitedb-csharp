namespace WhiteDb.Data.Tests.ValueBinders
{
    using System;

    using NUnit.Framework;

    using WhiteDb.Data.ValueBinders;

    [TestFixture]
    public class ValueBinderFactoryTests
    {
        private readonly ValueBinderFactory factory = new ValueBinderFactory();

        [Test]
        public void Get_WhenAskingForIntegerBinder_TypeIsCorrect()
        {
            var binder = this.factory.Get(typeof(int));
            Assert.That(binder.GetType(), Is.EqualTo(typeof(IntegerValueBinder)));
        }

        [Test]
        public void Get_WhenAskingForStringBinder_TypeIsCorrect()
        {
            var binder = this.factory.Get(typeof(string));
            Assert.That(binder.GetType(), Is.EqualTo(typeof(StringValueBinder)));
        }

        [Test]
        public void Get_WhenAskingForUnsupportedType_ExceptionIsThrown()
        {
            Assert.Throws<NotImplementedException>(() => this.factory.Get(typeof(byte)));
        }
    }
}