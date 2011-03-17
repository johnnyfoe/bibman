using System.Collections.Generic;
using System.IO;
using BibtexEntryManager.Helpers;
using BibtexEntryManager.Models.EntryTypes;
using NUnit.Framework;

namespace BibtexEntryManager.Tests.Helpers
{
    [TestFixture]
    public class ParserTest
    {
        private const string TestFilePath = "..\\..\\TestFiles\\";
        [Test]
        public void TestGetEntriesFrom()
        {
            var data = File.OpenText(TestFilePath + "DefaultBook.bib").ReadToEnd();
            string s = "";
            var coll = Parser.GetEntriesFrom(data, out s);

            var publicationCollection = new List<Publication>();

            foreach (var v in coll)
            {
                Publication p = PublicationFactory.MakePublication(v);
                p.Owner = "johnny";
                publicationCollection.Add(p);
            }

            var defaultBookInstance = ObjectBuilder.NewDefaultBook();

            Assert.IsTrue(publicationCollection[0].Equals(defaultBookInstance));
        }
    }
}
