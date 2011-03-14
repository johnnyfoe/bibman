using System;
using System.Collections.Generic;

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
        public Dictionary<string, string> ErrorDictionary { get; set; }
        
        public InvalidEntryException(Dictionary<string,string> dict)
        {
            ErrorDictionary = dict;
        }

        public new string ToString()
        {
            string retVal = "";
            foreach (KeyValuePair<string, string> keyValuePair in ErrorDictionary)
            {
                retVal += keyValuePair.Value + "\r\n";
            }
            return retVal;
        }

    }
}