using BibtexEntryManager.Helpers;
using BibtexEntryManager.Models.EntryTypes;
using BibtexEntryManager.Tests.Helpers;
using FluentNHibernate.Testing;
using NHibernate.Tool.hbm2ddl;
using NUnit.Framework;

namespace BibtexEntryManager.Tests.Mapping
{
    [TestFixture]
    public class MappingFixture : FixtureBase
    {
        [Ignore("Sets up new database - skipping.")]
        [Test]
        public void BuildSchema()
        {
            var v = new SchemaExport(Config);
            v.Create(true, true);
        }

        [Test]
        public void TestSaveArticle()
        {
            var art = ObjectBuilder.BuildDefault<Article>();
            Session.Save(art);
            var a = (Article)Session.Get(typeof(Article), 1);
            Assert.IsNotNull(a);
        }
        [Test]
        public void TestSaveArticleEqual()
        {
            var art = ObjectBuilder.BuildDefault<Article>();
            Session.Save(art);
            var a = (Article)Session.Get(typeof(Article), 1);
            Assert.IsTrue(a.Equals(art));
        }
        [Test]
        public void TestSaveBook()
        {

            var book = ObjectBuilder.BuildDefault<Book>();
            Session.Save(book);
            var bk = (Book)Session.Get(typeof(Book), 1);
            Assert.IsNotNull(bk);
        }

        [Test]
        public void TestSaveBookLet()
        {
            var booklet = ObjectBuilder.BuildDefault<BookLet>();
            Session.Save(booklet);
            var bl = (BookLet)Session.Get(typeof(BookLet), 1);
            Assert.IsNotNull(bl);
        }

        [Test]
        public void TestSaveConference()
        {
            var conference = ObjectBuilder.BuildDefault<Conference>();
            Session.Save(conference);
            var c = (Conference)Session.Get(typeof(Conference), 1);
            Assert.IsNotNull(c);
        }

        [Test]
        public void TestSaveInBook()
        {
            var inbook = ObjectBuilder.BuildDefault<InBook>();
            Session.Save(inbook);
            var inb = (InBook)Session.Get(typeof(InBook), 1);
            Assert.IsNotNull(inb);
        }

        [Test]
        public void TestSaveInCollection()
        {
            var inCollection = ObjectBuilder.BuildDefault<InCollection>();
            Session.Save(inCollection);
            var inc = (InCollection)Session.Get(typeof(InCollection), 1);
            Assert.IsNotNull(inc);
        }

        [Test]
        public void TestSaveInProceedings()
        {
            var inproceedings = ObjectBuilder.BuildDefault<InProceedings>();
            Session.Save(inproceedings);
            var inp = (InProceedings)Session.Get(typeof(InProceedings), 1);
            Assert.IsNotNull(inp);
        }

        [Test]
        public void TestSaveManual()
        {
            var manual = ObjectBuilder.BuildDefault<Manual>();
            Session.Save(manual);
            var man = (Manual)Session.Get(typeof(Manual), 1);
            Assert.IsNotNull(man);
        }

        [Test]
        public void TestSaveMasterThesis()
        {
            var masterThesis = ObjectBuilder.BuildDefault<MastersThesis>();
            Session.Save(masterThesis);
            var mast = (MastersThesis)Session.Get(typeof(MastersThesis), 1);
            Assert.IsNotNull(mast);
        }

        [Test]
        public void TestSaveMisc()
        {
            var misc = ObjectBuilder.BuildDefault<Misc>();
            Session.Save(misc);
            var mis = (Misc)Session.Get(typeof(Misc), 1);
            Assert.IsNotNull(mis);
        }

        [Test]
        public void TestSavePhdThesis()
        {
            var phdThesis = ObjectBuilder.BuildDefault<PhdThesis>();
            Session.Save(phdThesis);
            var phd = (PhdThesis)Session.Get(typeof(PhdThesis), 1);
            Assert.IsNotNull(phd);
        }

        [Test]
        public void TestSaveUnpublished()
        {
            var unpublished = ObjectBuilder.BuildDefault<Unpublished>();
            Session.Save(unpublished);
            var un = (Unpublished)Session.Get(typeof(Unpublished), 1);
            Assert.IsNotNull(un);
        }

        [Test]
        public void TestSaveTechReport()
        {
            var techReport = ObjectBuilder.BuildDefault<TechReport>();
            Session.Save(techReport);
            var tec = (TechReport)Session.Get(typeof(TechReport), 1);
            Assert.IsNotNull(tec);
        }

        [Test]
        public void TestSaveProceedings()
        {
            var proceedings = ObjectBuilder.BuildDefault<Proceedings>();
            Session.Save(proceedings);
            var prc = (Proceedings)Session.Get(typeof(Proceedings), 1);
            Assert.IsNotNull(prc);
        }


        [Test]
        public void CanMapArticle()
        {
            new PersistenceSpecification<Article>(Session)
                .CheckProperty(c => c.CiteKey, "js2010")
                .CheckProperty(c => c.Owner, "Martin Smith")
                .CheckProperty(c => c.Author, "John Thow")
                .CheckProperty(c => c.Title, "fake title")
                .CheckProperty(c => c.Year, "2010")
                .CheckProperty(c => c.Journal, "some journal")
                .CheckProperty(c => c.Volume, "a volume")
                .CheckProperty(c => c.Pages, null)
                .CheckProperty(c => c.Month, null)
                .CheckProperty(c => c.Doi, null)
                .CheckProperty(c => c.Annotate, null)
                .CheckProperty(c => c.TheKey, null)
                .CheckProperty(c => c.Note, null)
                .VerifyTheMappings();
        }

        [Test]
        public void CanMapBook()
        {
            new PersistenceSpecification<Book>(Session)
                .CheckProperty(c => c.CiteKey, "js2010")
                .CheckProperty(c => c.Owner, "Martin Smith")
                .CheckProperty(c => c.Author, "John Thow")
                .CheckProperty(c => c.Title, "fake title")
                .CheckProperty(c => c.Publisher, "arpercollins")
                .CheckProperty(c => c.Year, "2010")
                .CheckProperty(c => c.Volume, null)
                .CheckProperty(c => c.Series, null)
                .CheckProperty(c => c.Address, null)
                .CheckProperty(c => c.Edition, null)
                .CheckProperty(c => c.Month, null)
                .CheckProperty(c => c.Isbn, null)
                .CheckProperty(c => c.Note, null)
                .VerifyTheMappings();
        }

        [Test]
        public void CanMapBookLet()
        {
            new PersistenceSpecification<BookLet>(Session)
                .CheckProperty(c => c.CiteKey, "js2010")
                .CheckProperty(c => c.Owner, "Martin Smith")
                .CheckProperty(c => c.Title, "fake title")
                .CheckProperty(c => c.Author, null)
                .CheckProperty(c => c.Address, null)
                .CheckProperty(c => c.Howpublished, null)
                .CheckProperty(c => c.Month, null)
                .CheckProperty(c => c.Year, null)
                .CheckProperty(c => c.Note, null)
                .VerifyTheMappings();
        }

        [Test]
        public void CanMapConference()
        {
            new PersistenceSpecification<Conference>(Session)
                .CheckProperty(c => c.CiteKey, "js2010")
                .CheckProperty(c => c.Owner, "Martin Smith")
                .CheckProperty(c => c.Author, "John Thow")
                .CheckProperty(c => c.Title, "fake title")
                .CheckProperty(c => c.Booktitle, "fake title")
                .CheckProperty(c => c.Year, "2010")
                .CheckProperty(c => c.Editor, null)
                .CheckProperty(c => c.Volume, null)
                .CheckProperty(c => c.Series, null)
                .CheckProperty(c => c.Pages, null)
                .CheckProperty(c => c.Month, null)
                .CheckProperty(c => c.Organisation, null)
                .CheckProperty(c => c.Publisher, null)
                .CheckProperty(c => c.Note, null)
                .VerifyTheMappings();
        }

        [Test]
        public void CanMapInBook()
        {
            new PersistenceSpecification<InBook>(Session)
                .CheckProperty(c => c.CiteKey, "js2010")
                .CheckProperty(c => c.Owner, "Martin Smith")
                .CheckProperty(c => c.Author, "john thow")
                .CheckProperty(c => c.Title, "fake title")
                .CheckProperty(c => c.Chapter, "fake title")
                .CheckProperty(c => c.Publisher, "fake title")
                .CheckProperty(c => c.Year, "2010")
                .CheckProperty(c => c.Volume, null)
                .CheckProperty(c => c.Series, null)
                .CheckProperty(c => c.Type, null)
                .CheckProperty(c => c.Address, null)
                .CheckProperty(c => c.Edition, null)
                .CheckProperty(c => c.Month, null)
                .CheckProperty(c => c.Note, null)
                .VerifyTheMappings();
        }

        [Test]
        public void CanMapInCollection()
        {
            new PersistenceSpecification<InCollection>(Session)
                .CheckProperty(c => c.CiteKey, "js2010")
                .CheckProperty(c => c.Owner, "Martin Smith")
                .CheckProperty(c => c.Author, "john thow")
                .CheckProperty(c => c.Title, "fake title")
                .CheckProperty(c => c.Chapter, "fake title")
                .CheckProperty(c => c.Publisher, "fake title")
                .CheckProperty(c => c.Year, "2010")
                .CheckProperty(c => c.Volume, null)
                .CheckProperty(c => c.Series, null)
                .CheckProperty(c => c.Type, null)
                .CheckProperty(c => c.Address, null)
                .CheckProperty(c => c.Edition, null)
                .CheckProperty(c => c.Month, null)
                .CheckProperty(c => c.Note, null)
                .VerifyTheMappings();
        }

        [Test]
        public void CanMapInProceedings()
        {
            new PersistenceSpecification<InProceedings>(Session)
                .CheckProperty(c => c.CiteKey, "js2010")
                .CheckProperty(c => c.Owner, "Martin Smith")
                .CheckProperty(c => c.Author, "john thow")
                .CheckProperty(c => c.Title, "fake title")
                .CheckProperty(c => c.Booktitle, "fake title")
                .CheckProperty(c => c.Year, "2010")
                .CheckProperty(c => c.Editor, null)
                .CheckProperty(c => c.Volume, null)
                .CheckProperty(c => c.Series, null)
                .CheckProperty(c => c.Pages, null)
                .CheckProperty(c => c.Address, null)
                .CheckProperty(c => c.Month, null)
                .CheckProperty(c => c.Organisation, null)
                .CheckProperty(c => c.Publisher, null)
                .CheckProperty(c => c.Note, null)
                .VerifyTheMappings();
        }

        [Test]
        public void CanMapManual()
        {
            new PersistenceSpecification<Manual>(Session)
                .CheckProperty(c => c.CiteKey, "js2010")
                .CheckProperty(c => c.Owner, "Martin Smith")
                .CheckProperty(c => c.Title, "fake title")
                .CheckProperty(c => c.Author, "john thow")
                .CheckProperty(c => c.Organisation, null)
                .CheckProperty(c => c.Address, null)
                .CheckProperty(c => c.Edition, null)
                .CheckProperty(c => c.Month, null)
                .CheckProperty(c => c.Year, null)
                .CheckProperty(c => c.Note, null)
                .VerifyTheMappings();
        }


        //todo finish of writing tests for rest of Publication objects.
    }
}
