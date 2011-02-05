using BibtexEntryManager.Helpers;
using NUnit.Framework;

namespace BibtexEntryManager.Tests.Helpers
{
    [TestFixture]
    public class CompareTest
    {
        [Test]
        public void EqualsIgnoreCaseTestNulls()
        {
            string a = null;
            string b = null;
            const bool expected = true;
            var actual = Compare.EqualsIgnoreCase(a, b);
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void EqualsIgnoreCaseTestSameDiffCase()
        {
            const string a = "John";
            const string b = "john";
            const bool expected = true;
            var actual = Compare.EqualsIgnoreCase(a, b);
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void EqualsIgnoreCaseTestSameCase()
        {
            const string a = "John";
            const string b = "John";
            const bool expected = true;
            var actual = Compare.EqualsIgnoreCase(a, b);
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void EqualsIgnoreCaseTestDiff()
        {
            const string a = "Mark";
            const string b = "John";
            const bool expected = false;
            var actual = Compare.EqualsIgnoreCase(a, b);
            Assert.AreEqual(expected, actual);
        }
    }
}
