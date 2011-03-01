using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Activation;
using BibtexEntryManager.Data;
using BibtexEntryManager.Models.EntryTypes;
using NHibernate;
using NHibernate.Linq;

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
        public IList<int> GetDeletedPublications(string pageCreationTime)
        {
            try
            {
                DateTime d = DateTime.Parse(pageCreationTime);

                var pubs = (from publications in DataPersistence.GetSession().Linq<Publication>()
                            select publications).ToList();
                pubs = pubs.Where(p => (p.DeletionTime.HasValue)).ToList();

                IList<int> result = new List<int>();
                foreach (Publication publication in pubs)
                {
                    if (publication.DeletionTime != null)
                        if (publication.DeletionTime.Value.CompareTo(d) > 0)
                            result.Add(publication.Id);
                }

                return result;
            }
            catch (ArgumentNullException)
            {
                return null;
            }
            catch (FormatException)
            {
                return null;
            }
            catch (InvalidOperationException)
            {
                return null;
            }
        }

        [OperationContract]
        public IList<int> GetAmendedPublications(string pageCreationTime)
        {
            IList<int> result = new List<int>();
            try
            {
                DateTime d = DateTime.Parse(pageCreationTime);

                var pubs = (from publications in DataPersistence.GetSession().Linq<Publication>()
                            select publications).ToList();
                pubs = pubs.Where(p => (p.AmendmentTime.HasValue)).ToList();

                foreach (Publication publication in pubs)
                {
                    if (publication.AmendmentTime != null)
                        if (publication.AmendmentTime.Value.CompareTo(d) > 0)
                            result.Add(publication.Id);
                }

                return result;
            }
            catch (ArgumentNullException)
            {
                return null;
            }
            catch (FormatException)
            {
                return null;
            }
            catch (InvalidOperationException)
            {
                return null;
            }
        }

        [OperationContract]
        public bool DeletePublication(int deletionId)
        {
            try
            {
                DataPersistence.DeletePublication(deletionId);
            }
            catch
            {
                return false;
            }
            return true;
        }

        [OperationContract]
        public int HasPublicationChanged(string queryString)
        {
            var split = queryString.Split(' ');
            int id = 0;
            string pageCreationTime = "";
            int count = 0;
            foreach (string s in split)
            {
                if (!String.IsNullOrEmpty(s))
                {
                    if (count == 0)
                    {
                        pageCreationTime = s;
                    }
                    if (count == 1)
                    {
                        pageCreationTime += " " + s;
                    }
                    if (count == 2)
                    {
                        id = Int32.Parse(s);
                    }
                    count++;
                }
            }
            if (id == -1)
            {
                return -1; // page is at creation stage, so does not exist in the db and cannot have changed.
            }

            ISession ses = DataPersistence.GetSession();
            
            DateTime d = DateTime.Parse(pageCreationTime);

            var pub = (from p in ses.Linq<Publication>()
                      where p.Id == id
                      select p).First();

            if (pub == null)
            {
                return -1; // means that the publication does not exist in the db and therefore cannot have changed
            }
            if (pub.DeletionTime > d)
            {
                return 1; // 1 signifies deletion since page load
            }
            if (pub.AmendmentTime > d)
            {
                return 2; // 2 signifies amendment since page load
            }

            return 0; // 0 signifies no change since page load
        }

        [OperationContract]
        public IList<string> GetNewPublications(string pageCreationTime)
        {
            IList<string> retVal = new List<string>();
            try
            {
                DateTime d = DateTime.Parse(pageCreationTime);

                var pubs = (from publications in DataPersistence.GetSession().Linq<Publication>()
                           select publications).ToList();

                foreach (Publication publication in pubs)
                {
                    if (publication.CreationTime != null && publication.CreationTime.Value.CompareTo(d) > 0)
                        retVal.Add(publication.FormatAsNewRow());
                }
                return retVal;
            }
            catch (ArgumentNullException)
            {
                return null;
            }
            catch (FormatException)
            {
                return null;
            }
            catch (InvalidOperationException)
            {
                return null;
            }
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
