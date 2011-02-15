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
        public ActionResult MarkAsDeletedResult(int id)
        {
            try
            {

                DataPersistence.DeletePublication(id);
                ViewData["message"] = "success";
            }
            catch (PublicationNotFoundException e)
            {
                ViewData["message"] = "failure";
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
            if (!String.IsNullOrEmpty(requiredKey))
            {
                entries = Parser.GetEntriesFrom(Request.Form[requiredKey]);
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
                publications.Add(PublicationFactory.MakePublication(dictionary));
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
                catch (Exception e) // todo probably want to have a look at what exception to catch here...
                {
                    ViewData["data"] += "<br/><br/>" + l.CiteKey + " Failed because of an exception:" + e.Message;
                    failureCounter++;
                }
            }
            ViewData["data"] += "Successfully added " + successCounter
                                   + " items and failed to add " + failureCounter + " items.";

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

            var k = Parser.GetEntriesFrom(fileContent.ToString());
            var publicationCollection = new List<Publication>();
            foreach (var l in k)
            {
                publicationCollection.Add(PublicationFactory.MakePublication(l));
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

        #region Entry Creation Post Methods
        [HttpPost]
        [Authorize]
        public ActionResult Publication(Publication a)
        {
            if (ModelState.IsValid)
            {
                a.Owner = HttpContext.User.Identity.Name;
                a.SaveOrUpdateInDatabase();
                return Redirect("/Entry");
            }
            return View(a);
        }
        #endregion

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
    }
}
