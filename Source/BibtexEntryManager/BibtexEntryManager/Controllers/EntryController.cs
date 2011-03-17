using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using BibtexEntryManager.Data;
using BibtexEntryManager.Error;
using BibtexEntryManager.Helpers;
using BibtexEntryManager.Models.EntryTypes;
using BibtexEntryManager.Models.Exceptions;
using NHibernate.Linq;

namespace BibtexEntryManager.Controllers
{
    [HandleError]
    public class EntryController : Controller
    {
        public ActionResult Index()
        {
            var p = DataPersistence.GetActivePublications();
            return View(p);
        }

        [Authorize]
        [HttpPost]
        public FileResult Download(Publication p)
        {
            var chrArray = p.ToBibFormat().ToCharArray();

            var f = new byte[chrArray.Length];
            for (int i = 0; i < f.Length; i++)
            {
                f[i] = (byte)chrArray[i];
            }
            return File(f, "text/plain", p.CiteKey + ".bib");
        }

        [Authorize]
        public FileResult DownloadAll()
        {
            var allPubs = DataPersistence.GetActivePublications();
            var allPubsString = "";
            foreach (var pub in allPubs)
            {
                allPubsString += pub.ToBibFormat();
            }

            var f = new byte[allPubsString.Length];
            for (int i = 0; i < f.Length; i++)
            {
                f[i] = (byte)allPubsString[i];
            }
            Response.RedirectLocation = "~/Entry";
            Response.ContentType = "text/plain";
            return File(f, "text/plain", "AllBibEntries.bib");
        }

        [Authorize]
        [HttpPost]
        public ActionResult Search(FormCollection s)
        {
            string searchVal = "";
            foreach (var v in s)
            {
                if (v.ToString().Contains("searchval"))
                {
                    searchVal = s[v.ToString()];
                }
            }
            if (searchVal == "")
            {
                return View();
            }
            var res = DataPersistence.GetActivePublicationsMatching(searchVal);
            return View(res);

        }

        [Authorize]
        public ActionResult Search(string s)
        {
            if (String.IsNullOrEmpty(s))
            {
                return View();
            }

            var res = DataPersistence.GetActivePublicationsMatching(s);

            ViewData["searchterm"] = s;

            return View(res);

        }

        [Authorize]
        public ActionResult DeletedEntries()
        {
            IList<Publication> deletedEntries = DataPersistence.GetDeletedPublications();

            return View(deletedEntries);
        }

        [Authorize]
        public ActionResult CleanupDeletions()
        {
            DataPersistence.CleanupExpiredDeletedPublications();
            return Redirect("/Entry/DeletedEntries");
        }

        [Authorize]
        public ActionResult PurgeAll()
        {
            DataPersistence.CleanupAllDeletedPublications();
            return Redirect("/Entry/DeletedEntries");
        }

        [Authorize]
        public ActionResult DeletePublication(int id)
        {
            var pub = (from p in (DataPersistence.GetSession().Linq<Publication>())
                      where p.Id == id
                      select p);
            if (pub.Count() < 1)
                Redirect("/Entry/Publication");

            return View(pub.First());
        }

        [Authorize]
        public ActionResult ViewDuplicates()
        {
            IList<string> citekeys = DataPersistence.GetDuplicateCiteKeys();

            return View(citekeys);
        }

        [Authorize]
        public ActionResult ReviewDuplicates(string ck)
        {
            IList<Publication> duplicatesForCiteKey = DataPersistence.GetPublicationsWithCiteKey(ck);

            return View(duplicatesForCiteKey);
        }

        [Authorize]
        public ActionResult MarkAsDeletedResult(int id)
        {
            try
            {
                DataPersistence.DeletePublication(id);
                ViewData["message"] = "success";
            }
            catch (PublicationNotFoundException e)
            {
                ViewData["message"] = "failure - the publication was not found";
                ViewData["exception"] = e.Message;
            }
            return View();
        }

        #region Entry Import
        [Authorize]
        public ActionResult Import()
        {
            return View();
        }

        [Authorize]
        [HttpPost]
        public ActionResult ImportEntries()
        {
            var f = Request.Form.AllKeys;

            string requiredKey = "";

            foreach (string s in f)
            {
                if (s.Contains("EntryTextBox"))
                {
                    // found key, get value and begin processing:
                    requiredKey = s;
                    break;
                }
            }
            IEnumerable<Dictionary<string, string>> entries;
            string errorString;
            if (!String.IsNullOrEmpty(requiredKey))
            {
                entries = Parser.GetEntriesFrom(Request.Form[requiredKey], out errorString);
            }
            else
            {
                ViewData["data"] =
                    "Error in form - 'EntryTextBox' field not found. (Will only ever see this if the form's field names change).";
                return View();
            }
            IList<Publication> publications = new List<Publication>();
            foreach (Dictionary<string, string> dictionary in entries)
            {
                var p = PublicationFactory.MakePublication(dictionary);
                if (p != null)
                    publications.Add(p);
            }
            var successCounter = 0;
            var failureCounter = 0;
            foreach (var l in publications)
            {
                try
                {
                    l.Owner = HttpContext.User.Identity.Name;
                    l.SaveOrUpdateInDatabase();
                    successCounter++;
                }
                catch(InvalidEntryException e)
                {
                    ViewData["data"] += "<br/><br/>" + l.CiteKey + " Failed because the entry was invalid: " +
                                        e.ToString();
                }
                catch (Exception e) // todo probably want to have a look at what exception to catch here...
                {
                    ViewData["data"] += "<br/><br/>" + l.CiteKey + " Failed because of an exception: " + e.Message;
                    failureCounter++;
                }
            }
            ViewData["data"] += "Successfully added " + successCounter
                                   + " items and failed to add " + failureCounter + " items."+
                                   ((errorString.Length==0)?"":"The parser encountered some errors: " + errorString);

            return View();
        }

        [Authorize]
        [HttpPost]
        public ActionResult UploadFile()
        {
            var f = Request.Files[0];
            if (f == null)
            {
                ViewData["Message"] = ErrorMessages.FileIsNull;
                return View();
            }
            if (!(f.ContentLength > 0))
            {
                ViewData["Message"] = ErrorMessages.FileContentLengthError;
                return View();
            }

            var stream = f.InputStream;

            if (!stream.CanRead)
            {
                ViewData["Message"] = ErrorMessages.CannotReadStream;
                return View();
            }

            var buffer = new byte[f.ContentLength];
            var ms = new MemoryStream();
            while (true)
            {
                var read = stream.Read(buffer, 0, buffer.Length);
                if (!(read <= 0))
                    ms.Write(buffer, 0, read);
                else
                    break;
            }

            var arr = ms.ToArray();
            var fileContent = new StringBuilder();
            foreach (var b in arr)
            {
                fileContent.Append((char)b);
            }
            string errorString;
            var k = Parser.GetEntriesFrom(fileContent.ToString(), out errorString);
            var publicationCollection = new List<Publication>();
            foreach (var l in k)
            {
                try
                {
                    var p = PublicationFactory.MakePublication(l);
                    if (p != null)
                        publicationCollection.Add(p);
                }
                catch (Exception e)
                {
                    errorString += "<br/><br/>" + e + "<br/><br/>";
                }
            }
            var successCounter = 0;
            var failureCounter = 0;
            foreach (var l in publicationCollection)
            {
                try
                {
                    l.Owner = HttpContext.User.Identity.Name;
                    l.SaveOrUpdateInDatabase();
                    successCounter++;
                }
                catch (InvalidEntryException e)
                {
                    ViewData["data"] += "<br/><br/>" + l.CiteKey + " Failed because the entry was invalid: " +
                                        e.ToString();
                }
                catch (Exception e) // todo probably want to have a look at what exception to catch here...
                {
                    ViewData["Message"] += "<br/><br/>" + l.CiteKey + " Failed because of an exception:" + e.Message;
                    failureCounter++;
                }
            }
            ViewData["Message"] += "<br/><br/><br/>Successfully added " + successCounter
                + " items and failed to add " + failureCounter + " items.";

            return View();
        }

        #endregion

        [HttpPost]
        [Authorize]
        public ActionResult Publication(Publication a)
        {

            Dictionary<string, string> errors = a.IsValidEntry();
            var matchingCiteKeys = (from pubs in DataPersistence.GetSession().Linq<Publication>()
                                    where pubs.CiteKey.Equals(a.CiteKey)
                                    select pubs);
            int ckCount = matchingCiteKeys.Count();
            if (ckCount > 0 && a.Id == 0)
            {
                string linkToOther = "";// "<a href=\"Publication/" + matchingCiteKeys.First().Id + "\" target=\"_blank\">Click here to open the clash in a new window</a>";
                const string ckKey = "CiteKey";
                if (errors.ContainsKey(ckKey))
                {
                    errors[ckKey] += ". " + ErrorMessages.CiteKeyNotUnique + " " + linkToOther;
                }
                errors.Add(ckKey, ErrorMessages.CiteKeyNotUnique + " " + linkToOther);
            }
            // there are no errors if this is the case)
            if (errors.Count == 0)
            {
                a.Owner = HttpContext.User.Identity.Name;
                a.SaveOrUpdateInDatabase();
                return Redirect("/Entry");
            }
            foreach (KeyValuePair<string, string> kvp in errors)
            {
                ModelState.AddModelError(kvp.Key, kvp.Value);
            }
            return View(a);
        }

        #region Entry Creation Pages
        [Authorize]
        public ActionResult Publication(int? id)
        {
            if (id != null)
            {
                var session = DataPersistence.GetSession();
                var s = from b in session.Linq<Publication>()
                        where b.Id == id
                        select b;
                if (s.Count() < 1)
                {
                    return Redirect("/Entry/Publication");
                }
                var p = s.First();
                ViewData["ButtonText"] = "Amend";
                return View(p);
            }
            // else
            return View();
        }
        #endregion

        [Authorize]
        public ActionResult SelectEntriesForDeletion()
        {
            IList<Publication> publications = DataPersistence.GetActivePublications();
            return View(publications);
        }

        [Authorize]
        [HttpPost]
        public ActionResult DeleteList(FormCollection fc)
        {
            string s = fc["deleteList"];
            var d = s.Split(' ');
            IList<int> ints = new List<int>();
            foreach (string s1 in d)
            {
                int val;
                if (Int32.TryParse(s1, out val))
                {
                    ints.Add(val);
                }
            }

            foreach (int i in ints)
            {
                DataPersistence.DeletePublication(i);
            }

            return View();
        }

        [Authorize]
        public ActionResult RestorePublication(int? id)
        {
            if (id == null)
            {
                return Redirect("/Entry/");
            }
            int notNullId = (int) id;
            DataPersistence.RestorePublication(notNullId);
            ViewData["message"] = "Item was restored successfully";
            return Redirect("/Entry/Publication/" + notNullId);
        }

        [Authorize]
        [HttpPost]
        public ActionResult ConfirmDeleteEntries(FormCollection fc)
        {
            var keys = fc.AllKeys;
            int[] ids = new int[keys.Length];
            for (int i = 0; i < keys.Length; i++)
            {
                ids[i] = Int32.Parse(keys[i]);
            }

            IList<Publication> deletionList = (from pubs in DataPersistence.GetSession().Linq<Publication>()
                                              where ids.Contains(pubs.Id)
                                              select pubs).ToList();
            return View(deletionList);
        }

        [Authorize]
        [HttpPost]
        public ActionResult DeleteEntries(IList<Publication> pubs)
        {
            DataPersistence.DeletePublications(pubs);

            ViewData["DeletionResult"] = "Deletion of " + pubs.Count + " entries was a success.";
            return Redirect("/Entry");
        }
    }
}
