namespace WhiteDb.Data.Tests.Query
{
    using NUnit.Framework;

    using WhiteDb.Data.Tests.Utils;

    [TestFixture]
    public class QueryTests
    {
        [Test]
        public void Query_EmptyQuery_AllRecords()
        {
            using (var db = new TestDataContext())
            {
                for (var i = 1; i < 1000; i++)
                {
                    var r = db.CreateRecord(1);
                    r.SetFieldValue(0, i * 3);
                }

                var builder = db.CreateQueryBuilder();
                Assert.That(builder, Is.Not.Null);

                var query = builder.Execute();
                var record = query.Fetch();
                var count = 0;
                while (record != null)
                {
                    count++;
                    Assert.That(record.GetFieldValueInteger(0), Is.GreaterThan(1));
                    record = query.Fetch();
                }

                Assert.That(count, Is.EqualTo(999));
            }
        }

        [Test]
        public void EqualOperator_WithStringValue_OneRowReturned()
        {
            using (var db = new TestDataContext())
            {
                db.CreateRecord(1).SetFieldValue(0, "qwerty");
                db.CreateRecord(1).SetFieldValue(0, "asdfg");

                var query = db.CreateQueryBuilder().AddCondition(0, ConditionOperator.Equal, "asdfg").Execute();
                Assert.That(query.GetRowCount(), Is.EqualTo(1));
            }
        }

        [Test]
        public void Query_LessThan_OneRowReturned()
        {
            using (var db = new TestDataContext())
            {
                db.CreateRecord(1).SetFieldValue(0, 10);
                db.CreateRecord(1).SetFieldValue(0, 3);

                var query = db.CreateQueryBuilder().AddCondition(0, ConditionOperator.LessThan, 4).Execute();
                Assert.That(query.GetRowCount(), Is.EqualTo(1));
            }
        }
    }
}