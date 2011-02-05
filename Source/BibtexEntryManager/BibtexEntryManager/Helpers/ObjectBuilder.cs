using System.Collections.Generic;
using BibtexEntryManager.Models.EntryTypes;
namespace BibtexEntryManager.Helpers
{
    public static class ObjectBuilder
    {
        private const int CurrentId = 0;
        private const string StaticJournal = "JournalName";
        private const string DefaultOwner = "Mark White";
        private const string CkPrefix = "JS2010";
        public static Publication BuildDefaultPublication()
        {
            return NewDefaultPublication();
        }
        private static Publication NewDefaultPublication()
        {
            Publication p = new Publication
                    {

                    };
            return p;
        }
        public static Publication NewPublicationFrom(Dictionary<string, string> oneEntry)
        {
            Publication p = new Publication
                    {

                    };
            return p;
        }
    }
}