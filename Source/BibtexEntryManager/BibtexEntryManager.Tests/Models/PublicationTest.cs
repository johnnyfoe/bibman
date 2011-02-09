using BibtexEntryManager.Helpers;
using BibtexEntryManager.Models.Enums;
using NUnit.Framework;
namespace BibtexEntryManager.Tests.Models
{
    [TestFixture]
    public class PublicationTest
    {
        [Test]
        public void NotEqualsTest()
        {
            var target = ObjectBuilder.BuildDefaultPublication();
            var that = ObjectBuilder.BuildDefaultPublication();
            that.Abstract = "Some other abstract";
            Assert.IsFalse(target.Equals(that));
        }

        [Test]
        public void EqualsTest()
        {
            var target = ObjectBuilder.BuildDefaultPublication();
            var that = ObjectBuilder.BuildDefaultPublication();
            Assert.IsTrue(target.Equals(that));
        }
        
        [Test]
        public void EqualsNotTrueDifferentTypeTest()
        {
            var target = ObjectBuilder.BuildDefaultPublication();
            var that = ObjectBuilder.BuildDefaultPublication();
            that.EntryType = Entry.Incollection;
            Assert.IsFalse(target.Equals(that));
        }
        
        [Test]
        public void EqualsNotTrueDifferentDataTest()
        {
            var target = ObjectBuilder.BuildDefaultPublication();
            target.CiteKey = "JT2010";
            var that = ObjectBuilder.BuildDefaultPublication();
            Assert.IsFalse(target.Equals(that));
        }
    }
}
