using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Activation;
using System.ServiceModel.Web;
using System.Text;
using BibtexEntryManager.Data;
using BibtexEntryManager.Models.EntryTypes;

namespace BibtexEntryManager
{
    [ServiceContract(Namespace = "")]
    [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
    public class AjaxMethods
    {
        // To use HTTP GET, add [WebGet] attribute. (Default ResponseFormat is WebMessageFormat.Json)
        // To create an operation that returns XML,
        //     add [WebGet(ResponseFormat=WebMessageFormat.Xml)],
        //     and include the following line in the operation body:
        //         WebOperationContext.Current.OutgoingResponse.ContentType = "text/xml";
        [OperationContract]
        public string DoSearch(string searchString)
        {
            return ConvertToResultsTbody(DataPersistance.GetAllPublicationsMatching(searchString));
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
