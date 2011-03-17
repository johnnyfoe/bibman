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
        public void SetupDatabase()
        {
            new SchemaExport(Config)
                .SetOutputFile(@"..\\out.sql")
                .Create(false, true);
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
