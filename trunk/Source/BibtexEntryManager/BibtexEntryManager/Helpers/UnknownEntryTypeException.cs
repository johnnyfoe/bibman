using System;
namespace BibtexEntryManager.Helpers
{
    class UnknownEntryTypeException : Exception
    {
        public String UnknownTypeName { private set; get; }
        public UnknownEntryTypeException(String e)
        {
            UnknownTypeName = e;
        }

    }
}
