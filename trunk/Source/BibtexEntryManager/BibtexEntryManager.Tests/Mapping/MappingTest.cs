using BibtexEntryManager.Helpers;
using BibtexEntryManager.Models.EntryTypes;
using BibtexEntryManager.Tests.Helpers;
using FluentNHibernate.Testing;
using NHibernate.Linq;
using NHibernate.Tool.hbm2ddl;
using NUnit.Framework;
using System.Linq;

namespace BibtexEntryManager.Tests.Mapping
{
    [TestFixture]
    public class MappingFixture : FixtureBase
    {
        [Ignore("Sets up new database - skipping.")]
        [Test]
        public void SetupDatabase()
        {
            new SchemaExport(Config)
                .SetOutputFile(@"..\\out.sql")
                .Create(false, true);
        }
        [Ignore("Issue with NHibernate, not sure what's going on but it can't find the .dll")]
        [Test]
        public void TestSavePublication()
        {
            var art = ObjectBuilder.BuildDefaultPublication();
            Session.Save(art);
            var a = (from b in Session.Linq<Publication>()
                    where art.CiteKey == b.CiteKey
                    select b).First();
            Assert.IsNotNull(a);
            Session.Delete(art);
        }


        [Test]
        public void CanMapArticle()
        {
            new PersistenceSpecification<Publication>(Session)
                .CheckProperty(c => c.CiteKey, "js2010")
                .CheckProperty(c => c.Owner, "Martin Smith")
                .CheckProperty(c => c.Authors, "John Thow")
                .CheckProperty(c => c.Title, "fake title")
                .CheckProperty(c => c.Year, "2010")
                .CheckProperty(c => c.Journal, "some journal")
                .CheckProperty(c => c.Volume, "a volume")
                .CheckProperty(c => c.Pages, null)
                .CheckProperty(c => c.Month, null)
                .CheckProperty(c => c.TheKey, null)
                .CheckProperty(c => c.DeletionTime, null)
                .CheckProperty(c => c.Note, null)
                .VerifyTheMappings();
        }
    }
}
