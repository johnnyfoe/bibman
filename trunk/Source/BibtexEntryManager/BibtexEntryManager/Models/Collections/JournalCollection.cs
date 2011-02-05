using System;
using System.Collections.Generic;
using BibtexEntryManager.Models.EntryTypes;

namespace BibtexEntryManager.Models.Collections
{
    public class JournalCollection
    {
        private static Dictionary<String, Journal> journals = new Dictionary<String, Journal>();
        private JournalCollection()
        {

        }

        public static void addJournal(String key, Journal j)
        {
            journals.Add(key, j);
        }

        public static Journal getJournal(String key)
        {
            Journal j = journals[key];
            if (j != null)
            {
                return j;
            }
            return new Journal("", "");
            
        }

        public static ICollection<Journal> getAllJournals()
        {
            return journals.Values;
        }

    
    }
}