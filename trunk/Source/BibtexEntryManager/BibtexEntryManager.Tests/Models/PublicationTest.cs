using BibtexEntryManager.Helpers;
using BibtexEntryManager.Models.EntryTypes;
using NUnit.Framework;
namespace BibtexEntryManager.Tests.Models
{
    [TestFixture]
    public class PublicationTest
    {

        [Test]
        public void ArticleNotEqualsTest()
        {
            var target = ObjectBuilder.BuildDefault<Article>();
            var that = ObjectBuilder.BuildDefault<Article>();
            that.Author = "Some other author";
            Assert.IsFalse(target.Equals(that));
        }
        [Test]
        public void BookNotEqualsTest()
        {
            var target = ObjectBuilder.BuildDefault<Book>();
            var that = ObjectBuilder.BuildDefault<Book>();
            that.Author = "Some other author";
            Assert.IsFalse(target.Equals(that));
        }
        [Test]
        public void BookLetNotEqualsTest()
        {
            var target = ObjectBuilder.BuildDefault<BookLet>();
            var that = ObjectBuilder.BuildDefault<BookLet>();
            target.Title = "Another title";
            Assert.IsFalse(target.Equals(that));
        }
        [Test]
        public void ConferenceNotEqualsTest()
        {
            var target = ObjectBuilder.BuildDefault<Conference>();
            var that = ObjectBuilder.BuildDefault<Conference>();
            that.Author = "Some other author";
            Assert.IsFalse(target.Equals(that));
        }
        [Test]
        public void InBookNotEqualsTest()
        {
            var target = ObjectBuilder.BuildDefault<InBook>();
            var that = ObjectBuilder.BuildDefault<InBook>();
            that.Author = "Some other author";
            Assert.IsFalse(target.Equals(that));
        }
        [Test]
        public void InCollectionNotEqualsTest()
        {
            var target = ObjectBuilder.BuildDefault<InCollection>();
            var that = ObjectBuilder.BuildDefault<InCollection>();
            that.Author = "Some other author";
            Assert.IsFalse(target.Equals(that));
        }
        [Test]
        public void InProceedingsNotEqualsTest()
        {
            var target = ObjectBuilder.BuildDefault<InProceedings>();
            var that = ObjectBuilder.BuildDefault<InProceedings>();
            that.Author = "Some other author";
            Assert.IsFalse(target.Equals(that));
        }
        [Test]
        public void ManualNotEqualsTest()
        {
            var target = ObjectBuilder.BuildDefault<Manual>();
            var that = ObjectBuilder.BuildDefault<Manual>();
            that.Title = "Some other title";
            Assert.IsFalse(target.Equals(that));
        }
        [Test]
        public void MasterThesisNotEqualsTest()
        {
            var target = ObjectBuilder.BuildDefault<MastersThesis>();
            var that = ObjectBuilder.BuildDefault<MastersThesis>();
            that.Author = "Some other author";
            Assert.IsFalse(target.Equals(that));
        }
        [Test]
        public void MiscNotEqualsTest()
        {
            var target = ObjectBuilder.BuildDefault<Misc>();
            var that = ObjectBuilder.BuildDefault<Misc>();
            that.CiteKey = "bla";
            Assert.IsFalse(target.Equals(that));
        }
        [Test]
        public void PhdThesisNotEqualsTest()
        {
            var target = ObjectBuilder.BuildDefault<PhdThesis>();
            var that = ObjectBuilder.BuildDefault<PhdThesis>();
            that.Author = "Some other author";
            Assert.IsFalse(target.Equals(that));
        }
        [Test]
        public void ProceedingsNotEqualsTest()
        {
            var target = ObjectBuilder.BuildDefault<Proceedings>();
            var that = ObjectBuilder.BuildDefault<Proceedings>();
            that.Year = "2011";
            Assert.IsFalse(target.Equals(that));
        }
        [Test]
        public void TechReportNotEqualsTest()
        {
            var target = ObjectBuilder.BuildDefault<TechReport>();
            var that = ObjectBuilder.BuildDefault<TechReport>();
            that.Author = "Some other author";
            Assert.IsFalse(target.Equals(that));
        }
        [Test]
        public void UnpublishedNotEqualsTest()
        {
            var target = ObjectBuilder.BuildDefault<Unpublished>();
            var that = ObjectBuilder.BuildDefault<Unpublished>();
            that.Author = "Some other author";
            Assert.IsFalse(target.Equals(that));
        }


        [Test]
        public void BookEqualsTest()
        {
            var target = ObjectBuilder.BuildDefault<Book>();
            var that = ObjectBuilder.BuildDefault<Book>();
            Assert.IsTrue(target.Equals(that));
        }
        [Test]
        public void BookLetEqualsTest()
        {
            var target = ObjectBuilder.BuildDefault<BookLet>();
            var that = ObjectBuilder.BuildDefault<BookLet>();
            Assert.IsTrue(target.Equals(that));
        }
        [Test]
        public void ConferenceEqualsTest()
        {
            var target = ObjectBuilder.BuildDefault<Conference>();
            var that = ObjectBuilder.BuildDefault<Conference>();
            Assert.IsTrue(target.Equals(that));
        }
        [Test]
        public void InBookEqualsTest()
        {
            var target = ObjectBuilder.BuildDefault<InBook>();
            var that = ObjectBuilder.BuildDefault<InBook>();
            Assert.IsTrue(target.Equals(that));
        }
        [Test]
        public void InCollectionEqualsTest()
        {
            var target = ObjectBuilder.BuildDefault<InCollection>();
            var that = ObjectBuilder.BuildDefault<InCollection>();
            Assert.IsTrue(target.Equals(that));
        }
        [Test]
        public void InProceedingsEqualsTest()
        {
            var target = ObjectBuilder.BuildDefault<InProceedings>();
            var that = ObjectBuilder.BuildDefault<InProceedings>();
            Assert.IsTrue(target.Equals(that));
        }
        [Test]
        public void ManualEqualsTest()
        {
            var target = ObjectBuilder.BuildDefault<Manual>();
            var that = ObjectBuilder.BuildDefault<Manual>();
            Assert.IsTrue(target.Equals(that));
        }
        [Test]
        public void MasterThesisEqualsTest()
        {
            var target = ObjectBuilder.BuildDefault<MastersThesis>();
            var that = ObjectBuilder.BuildDefault<MastersThesis>();
            Assert.IsTrue(target.Equals(that));
        }
        [Test]
        public void MiscEqualsTest()
        {
            var target = ObjectBuilder.BuildDefault<Misc>();
            var that = ObjectBuilder.BuildDefault<Misc>();
            Assert.IsTrue(target.Equals(that));
        }
        [Test]
        public void PhdThesisEqualsTest()
        {
            var target = ObjectBuilder.BuildDefault<PhdThesis>();
            var that = ObjectBuilder.BuildDefault<PhdThesis>();
            Assert.IsTrue(target.Equals(that));
        }
        [Test]
        public void ProceedingsEqualsTest()
        {
            var target = ObjectBuilder.BuildDefault<Proceedings>();
            var that = ObjectBuilder.BuildDefault<Proceedings>();
            Assert.IsTrue(target.Equals(that));
        }
        [Test]
        public void TechReportEqualsTest()
        {
            var target = ObjectBuilder.BuildDefault<TechReport>();
            var that = ObjectBuilder.BuildDefault<TechReport>();
            Assert.IsTrue(target.Equals(that));
        }
        [Test]
        public void UnpublishedEqualsTest()
        {
            var target = ObjectBuilder.BuildDefault<Unpublished>();
            var that = ObjectBuilder.BuildDefault<Unpublished>();
            Assert.IsTrue(target.Equals(that));
        }
        [Test]
        public void EqualsNotTrueDifferentTypeTest()
        {
            var target = ObjectBuilder.BuildDefault<Article>();
            var that = ObjectBuilder.BuildDefault<Book>();
            Assert.IsFalse(target.Equals(that));
        }
        [Test]
        public void EqualsNotTrueDifferentDataTest()
        {
            var target = ObjectBuilder.BuildDefault<Book>();
            target.CiteKey = "JT2010";
            var that = ObjectBuilder.BuildDefault<Book>();
            Assert.IsFalse(target.Equals(that));
        }

    }
}
