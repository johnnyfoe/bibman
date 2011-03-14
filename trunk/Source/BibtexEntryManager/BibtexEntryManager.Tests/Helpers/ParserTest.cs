using System.Collections.Generic;
using System.IO;
using BibtexEntryManager.Helpers;
using BibtexEntryManager.Models.EntryTypes;
using NUnit.Framework;

namespace BibtexEntryManager.Tests.Helpers
{
    [Ignore("The test fails at the moment because of the inability to fake a http context")]
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
                publicationCollection.Add(PublicationFactory.MakePublication(v));
            }

            var defaultBookInstance = ObjectBuilder.BuildDefaultPublication();
            defaultBookInstance.CiteKey = "JS2010";
            Assert.IsTrue(publicationCollection[0].Equals(defaultBookInstance));
        }
    }
}
