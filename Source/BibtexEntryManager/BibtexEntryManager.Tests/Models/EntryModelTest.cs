using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using BibtexEntryManager.Models.EntryTypes;
using BibtexEntryManager.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BibtexEntryManager.Tests.Models
{
    /// <summary>
    /// Summary description for EntryModelTest
    /// </summary>
    [TestFixture]
    public class EntryModelTest
    {
        [Test]
        public void TestMethod1()
        {
            var testbook = ObjectBuilder.BuildDefault<Book>();
            testbook.setValueForField(BibtexEntryManager.Models.Enums.Field.Author,s
        }
    }
}
