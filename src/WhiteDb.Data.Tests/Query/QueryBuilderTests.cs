namespace WhiteDb.Data.Tests.Query
{
    using System;

    using NUnit.Framework;

    using WhiteDb.Data.Tests.Utils;

    [TestFixture]
    public class QueryBuilderTests
    {
        [Test]
        public void Execute_WHEN_EXPECTED_RESULT()
        {
            using (var db = new TestDataContext())
            {
                var builder = db.CreateQueryBuilder();
                builder.AddCondition(0, 0, 0);
                var query = builder.Execute();

                Assert.That(query.QueryPointer, Is.Not.EqualTo(IntPtr.Zero));
            }
        }
    }
}