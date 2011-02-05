using System;

namespace BibtexEntryManager.Models.Exceptions
{

    public class InvalidEntryException : Exception
    {

    }
    public class InvalidPublicationException : Exception
    {
        private string KeyNotFoundMessage { get; set; }
        public InvalidPublicationException(string k)
        {
            KeyNotFoundMessage = k;
        }

    }
}