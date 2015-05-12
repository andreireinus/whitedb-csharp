namespace WhiteDb.Data.Tests
{
    using System;

    using NUnit.Framework;

    using WhiteDb.Data.Tests.Utils;

    [TestFixture]
    public class DataContextTests
    {
        [Test]
        public void Dispose_WhenDisposingTwice_ThrowsException()
        {
            var db = new TestDataContext();
            db.Dispose();

            Assert.Throws<ObjectDisposedException>(() => db.Dispose());
        }

        [Test]
        //[Ignore("cmon")]
        public void Ctor_CreateObjectTwice_WillNotThrowException()
        {
            Assert.DoesNotThrow(
                () =>
                {
                    using (var db = new TestDataContext()) { }
                    using (var db = new TestDataContext()) { }
                });
        }

        [Test]
        public void GetFirstRecord_WhenDatabaseIsEmpty_ReturnsNull()
        {
            using (var db = new TestDataContext())
            {
                Assert.That(db.GetFirstRecord(), Is.Null);
            }
        }

        [Test]
        public void GetFirstRecord_WithSingleItemInDatabase_ReturnSameRecord()
        {
            using (var db = new TestDataContext())
            {
                var recordPointer = db.CreateRecord(1).RecordPointer;
                Assert.That(db.GetFirstRecord().RecordPointer, Is.EqualTo(recordPointer));
            }
        }

        [Test]
        public void Delete_WhenDeleteExistingRecord_DatabaseIsEmpty()
        {
            using (var db = new TestDataContext())
            {
                var record = db.CreateRecord(1);
                Assert.DoesNotThrow(() => db.Delete(record));
                Assert.That(db.GetFirstRecord(), Is.Null);
            }
        }

        [Test]
        public void CreateRecord_WhenFieldCountIsZeroOrNegative_ThrowsException()
        {
            using (var db = new TestDataContext())
            {
                Assert.Throws<ArgumentOutOfRangeException>(() => db.CreateRecord(0));
                Assert.Throws<ArgumentOutOfRangeException>(() => db.CreateRecord(-1));
            }
        }
    }
}