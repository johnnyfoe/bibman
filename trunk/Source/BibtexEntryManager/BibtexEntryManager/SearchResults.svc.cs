using System;
using System.Collections.Generic;
using System.ServiceModel;
using System.ServiceModel.Activation;
using BibtexEntryManager.Data;
using BibtexEntryManager.Models.EntryTypes;
using BibtexEntryManager.Models.Enums;

namespace BibtexEntryManager
{
    [ServiceContract(Namespace = "BibtexEntryManager")]
    [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
    public class SearchResults
    {
        // To use HTTP GET, add [WebGet] attribute. (Default ResponseFormat is WebMessageFormat.Json)
        // To create an operation that returns XML,
        //     add [WebGet(ResponseFormat=WebMessageFormat.Xml)],
        //     and include the following line in the operation body:
        //         WebOperationContext.Current.OutgoingResponse.ContentType = "text/xml";
        [OperationContract]
        public string DoSearch(string searchString)
        {
            return ConvertToResultsTbody(DataPersistence.GetActivePublicationsMatching(searchString));
        }

        [OperationContract]
        public string GetFields(string entryType)
        {
            Entry entry;
            if (!Entry.TryParse(entryType, true, out entry))
                return "Incorrect entry type - error in programming. Provided value: " + entryType + " which was not found.";



            return "Not yet implemented.";
        }
        // Add more operations here and mark them with [OperationContract]

        private static string ConvertToResultsTbody(IEnumerable<Publication> s)
        {
            string retVal = "";
            foreach (var v in s)
            {
                retVal += v.ToHtmlTableRowWithLinks();
            }
            return retVal;
        }
    }
}
