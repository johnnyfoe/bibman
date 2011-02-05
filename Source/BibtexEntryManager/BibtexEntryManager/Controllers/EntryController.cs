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
using NHibernate.Linq;

namespace BibtexEntryManager.Controllers
{
    [HandleError]
    public class EntryController : Controller
    {

        public ActionResult Index()
        {
            var p = DataPersistance.GetAllPublications();
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
                f[i] = (byte) chrArray[i];
            }
            return File(f, "text/plain", p.CiteKey + ".bib");
        }

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
            var res = DataPersistance.GetAllPublicationsMatching(searchVal);

            return View(res);

        }

        public ActionResult Search(string s)
        {
            if (String.IsNullOrEmpty(s))
            {
                return View();
            }

            var res = DataPersistance.GetAllPublicationsMatching(s);

            return View(res);

        }

        [Authorize]
        public FileResult DownloadAll()
        {
            var allPubs = DataPersistance.GetAllPublications();
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
            return File(f, "text/plain","AllBibEntries.bib");
        }

        #region File Import
        [Authorize]
        public ActionResult Import()
        {
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
                    if (l == null)
                        continue;
                    if (l.GetType() == typeof(Publication))
                    {
                        var art = ((Publication)l);
                        art.SaveToDatabase();
                        successCounter++;
                    }
                    else
                    {
                        ViewData["Message"] += "<br/><br/>" + l.CiteKey + " Failed because the type was not recognised";
                        // type unknown!
                        failureCounter++;
                    }
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
                var session = DataPersistance.GetSession();
                var s = from b in session.Linq<Publication>()
                        where b.Id == id
                        select b;
                if (s.Count() < 1)
                {
                    return Redirect("/Entry/Publication");
                }
                var p = s.First(a => a.Id == id);
                ViewData["ButtonText"] = "Amend";
                return View(p);
            }
            // else
            return View();
        }
        #endregion
    }
}
