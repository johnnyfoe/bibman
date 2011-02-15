using System;

namespace BibtexEntryManager.Models.Exceptions
{
    public class PublicationNotFoundException : Exception
    {
        private int _idTried;
        public override string Message
        {
            get
            {
                return "The publication " + _idTried + " was not found";
            }
        }

        public PublicationNotFoundException(int id)
        {
            _idTried = id;
        }

        public override string ToString()
        {
            return Message;
        }
    }

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