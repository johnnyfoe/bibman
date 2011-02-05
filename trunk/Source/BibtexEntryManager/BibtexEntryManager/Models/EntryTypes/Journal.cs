using System;

namespace BibtexEntryManager.Models.EntryTypes
{

    public class Journal
    {
        private String key, name;

        public Journal(String key, String name)
        {
            this.key = key;
            this.name = name;
        }

        public Journal(Journal ori)
        {
            key = ori.key;
            name = ori.name;
        }

        public String getKey()
        {
            return this.key;
        }

        public String getName()
        {
            return this.name;
        }
    }
}