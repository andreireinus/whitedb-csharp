namespace WhiteDb.Data.Tests
{
    using System;

    using NUnit.Framework;

    using WhiteDb.Data.Utils;

    [TestFixture]
    public class DataContextTests
    {
        [Test]
        public void Dispose_WhenDisposingTwice_ThrowsException()
        {
            var db = new DataContext("name", 1000000);
            db.Dispose();

            Assert.Throws<ObjectDisposedException>(() => db.Dispose());
        }

        [Test]
        public void GetFirstRecord_WhenDatabaseIsEmpty_ReturnsNull()
        {
            DatabaseUtilites.EmptyDatabase("testdb");
            using (var db = new DataContext("testdb"))
            {
                Assert.That(db.GetFirstRecord(), Is.Null);
            }
        }

        [Test]
        public void GetFirstRecord_WithSingleItemInDatabase_ReturnSameRecord()
        {
            DatabaseUtilites.EmptyDatabase("testdb");

            using (var db = new DataContext("testdb"))
            {
                var recordPointer = db.CreateRecord(1).RecordPointer;
                Assert.That(db.GetFirstRecord().RecordPointer, Is.EqualTo(recordPointer));
            }
        }

        [Test]
        public void Delete_WhenDeleteExistingRecord_DatabaseIsEmpty()
        {
            DatabaseUtilites.EmptyDatabase("testdb");

            using (var db = new DataContext("testdb"))
            {
                var record = db.CreateRecord(1);
                Assert.DoesNotThrow(() => db.Delete(record));
                Assert.That(db.GetFirstRecord(), Is.Null);
            }
        }
    }
}