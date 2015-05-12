namespace WhiteDb.Data.Tests.Query
{
    using System;
    using System.Linq;
    using System.Linq.Expressions;

    using NUnit.Framework;

    using WhiteDb.Data.Tests.Models;

    [TestFixture]
    public class QueryableTests
    {
        [Test]
        public void WithOneRecord_QueryToList_OneResultReturned()
        {
            using (var data = new DataContext<Person>("1233"))
            {
                data.Create(new Person { Age = 1, FirstName = "Test", LastName = "Case" });
                var list = data.Query().ToList();
                Assert.That(list.Count, Is.EqualTo(1));
            }
        }

        [Test]
        public void WithTwoRecord_WhereEqualsToList_OneResultReturned()
        {
            RunTest(a => a.Age == 2, 1, 2);
        }

        [Test]
        public void WithTwoRecord_WhereNotEqualsToList_OneResultReturned()
        {
            RunTest(a => a.Age != 2, 1, 1);
        }

        [Test]
        public void WithTwoRecord_WhereGreaterThanEqualsToList_OneResultReturned()
        {
            RunTest(a => a.Age >= 2, 1, 2);
        }

        [Test]
        public void WithTwoRecord_WhereGreaterThanToList_OneResultReturned()
        {
            RunTest(a => a.Age > 1, 1, 2);
        }

        [Test]
        public void WithTwoRecord_WhereLessThanToList_OneResultReturned()
        {
            RunTest(a => a.Age < 2, 1, 1);
        }

        [Test]
        public void WithTwoRecord_WhereLessThanEqualToList_OneResultReturned()
        {
            RunTest(a => a.Age <= 1, 1, 1);
        }

        [Test]
        public void WithTwoRecord_TwoArgumentsToList_OneResultReturned()
        {
            RunTest(a => a.Age <= 1 && a.LastName == "Karu", 1, 1);
        }

        [Test]
        public void WithTwoRecords_QueryAllAndGetFirst_OneResultIsReturned()
        {
            using (var data = new DataContext<Person>("1233"))
            {
                data.Create(new Person { Age = 1, FirstName = "Kati", LastName = "Karu" });
                data.Create(new Person { Age = 2, FirstName = "Mati", LastName = "Mesi" });
                var person = data.Query().First(a => a.Age == 1);
                Assert.That(person.Age, Is.GreaterThan(0));
                Assert.That(person.FirstName, Is.Not.Null);
            }
        }

        private static void RunTest(Expression<Func<Person, bool>> predicate, int expectedCount, int expectedAge)
        {
            using (var data = new DataContext<Person>("1233"))
            {
                data.Create(new Person { Age = 1, FirstName = "Kati", LastName = "Karu" });
                data.Create(new Person { Age = 2, FirstName = "Mati", LastName = "Mesi" });
                var list = data.Query().Where(predicate).ToList();
                Assert.That(list.Count, Is.EqualTo(expectedCount), "Expected count is wrong");
                Assert.That(list.First().Age, Is.EqualTo(expectedAge), "Expected age is wrong");
            }
        }
    }
}