using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using BibtexEntryManager.Data;
using BibtexEntryManager.Error;
using BibtexEntryManager.Helpers;
using BibtexEntryManager.Models.Enums;
//using BibtexEntryManager.Models.Grouping;
using BibtexEntryManager.Models.Exceptions;
using NHibernate;
using NHibernate.Linq;

namespace BibtexEntryManager.Models.EntryTypes
{

    /* @author Mitesh Furia
     * @version 0.1 2009 Jun 6
     * 
     * Translated to C# by John Thow 2010-10-22
     */

    /* Publication class represents a publication or a BIBTEX entry */
    public class Publication
    {
        #region Field Declaration

        public virtual int Id { get; set; }

        [Required(ErrorMessage = ErrorMessages.CiteKeyIsRequired)]
        [DisplayName("Cite Key")]
        [RegularExpression("[^,]*", ErrorMessage = "Cite Keys cannot contain commas")]
        [StringLength(30)]
        public virtual string CiteKey { get; set; }
        public virtual string Owner { get; set; }

        [DisplayName("Entry Type")]
        public virtual Entry EntryType { get; set; }
        [StringLength(1500)]
        public virtual string Abstract { get; set; }

        [StringLength(255)]
        public virtual string Address { get; set; }
        [StringLength(255)]
        public virtual string Annote { get; set; }
        [StringLength(255)]
        public virtual string Authors { get; set; }

        [DisplayName("Book Title")]
        [StringLength(255)]
        public virtual string Booktitle { get; set; }
        [StringLength(255)]
        public virtual string Chapter { get; set; }
        [DisplayName("Cross Reference")]
        [StringLength(255)]
        public virtual string Crossref { get; set; }
        [StringLength(255)]
        public virtual string Edition { get; set; }
        [StringLength(255)]
        public virtual string Editors { get; set; }
        [DisplayName("How Published")]
        [StringLength(255)]
        public virtual string Howpublished { get; set; }
        [StringLength(255)]
        public virtual string Institution { get; set; }
        [StringLength(255)]
        public virtual string Journal { get; set; }

        [DisplayName("Key")]
        public virtual string TheKey { get; set; }
        [StringLength(255)]
        public virtual string Month { get; set; }
        [StringLength(255)]
        public virtual string Note { get; set; }
        [StringLength(255)]
        public virtual string Number { get; set; }

        [DisplayName("Organisation")]
        [StringLength(255)]
        public virtual string Organization { get; set; }
        [StringLength(255)]
        public virtual string Pages { get; set; }
        [StringLength(255)]
        public virtual string Publisher { get; set; }
        [StringLength(255)]
        public virtual string School { get; set; }
        [StringLength(255)]
        public virtual string Series { get; set; }
        [StringLength(255)]
        public virtual string Title { get; set; }
        [StringLength(255)]
        public virtual string Type { get; set; }
        [StringLength(255)]
        public virtual string Volume { get; set; }
        [RegularExpression("[0-9][0-9][0-9][0-9]", ErrorMessage = "The year must be a four-digit number")]
        public virtual string Year { get; set; }

        // times of modification/creation/
        public virtual DateTime? DeletionTime { get; set; }
        public virtual DateTime? AmendmentTime { get; set; }
        public virtual DateTime? CreationTime { get; set; }
        //public Dictionary<String, String> UnknownFields { get; private set; }

        #endregion

        public Publication()
        {
            //UnknownFields = new Dictionary<String, String>();
        }

        /// <summary>
        /// Returns a html row as a string representing the Publication. Should include only CiteKey, Author, Year and title with no HTML links
        /// </summary>
        /// <returns></returns>
        public virtual string ToHtmlTableRowNoLinks()
        {
            string a1 = "";
            string a2 = "";

            // Separate authors/editors into fields a1 and a2
            if (!String.IsNullOrEmpty(Authors))
                SeparateAuthors(Authors, out a1, out a2);
            else if (!String.IsNullOrEmpty(Editors))
                SeparateAuthors(Editors, out a1, out a2);

            return "<tr><td>" + CiteKey + "</td><td>" + EntryType + "</td><td>" + a1 + "</td><td>" + a2 +
                   "</td><td>" + Title + "</td><td>" + Year + "</td></tr>";
        }

        /// <summary>
        /// Returns a html row as a string representing the Publication. Should include only CiteKey, Author, Year and title with HTML links to the edit page.
        /// </summary>
        /// <returns></returns>
        public virtual string ToHtmlTableRowWithLinks()
        {
            string a1 = "";
            string a2 = "";

            // Separate authors/editors into fields a1 and a2
            if (!String.IsNullOrEmpty(Authors))
                SeparateAuthors(Authors, out a1, out a2);
            else if (!String.IsNullOrEmpty(Editors))
                SeparateAuthors(Editors, out a1, out a2);

            return "<tr id=\"tr_" + Id + "\"><td><a href=\"/Entry/Publication/" + Id + "\">" + CiteKey + "</a></td><td>" + EntryType +
                   "</td><td>" + a1 + "</td><td>" + a2 + "</td><td>" + Title + "</td><td>" + Year + "</td></tr>";
        }

        /// <summary>
        /// Returns a html row as a string representing the Publication. Should include only CiteKey, Author, Year and title with HTML links to the edit page.
        /// </summary>
        /// <returns></returns>
        public virtual string FormatAsNewRow()
        {
            string a1 = "";
            string a2 = "";

            // Separate authors/editors into fields a1 and a2
            if (!String.IsNullOrEmpty(Authors))
                SeparateAuthors(Authors, out a1, out a2);
            else if (!String.IsNullOrEmpty(Editors))
                SeparateAuthors(Editors, out a1, out a2);

            return "<tr id=\"tr_" + Id + "\" class=\"NewPublication\"><td><a href=\"/Entry/Publication/" + Id + "\">" + CiteKey + "</a></td><td>" + EntryType +
                   "</td><td>" + a1 + "</td><td>" + a2 + "</td><td>" + Title + "</td><td>" + Year + "</td></tr>";
        }

        /// <summary>
        /// Returns a html row as a string representing the Publication. Should include only CiteKey, Author, Year and title with HTML links to the edit page.
        /// </summary>
        /// <returns></returns>
        public virtual string ToHtmlTableRowWithCheckBoxes()
        {
            string a1 = "";
            string a2 = "";

            // Separate authors/editors into fields a1 and a2
            if (!String.IsNullOrEmpty(Authors))
                SeparateAuthors(Authors, out a1, out a2);
            else if (!String.IsNullOrEmpty(Editors))
                SeparateAuthors(Editors, out a1, out a2);

            return "<tr><td><input type=\"checkbox\" name=\"" + Id + "\" /></td><td><a href=\"/Entry/Publication/" + Id + "\">" + CiteKey + "</a></td><td>" + EntryType +
                   "</td><td>" + a1 + "</td><td>" + a2 + "</td><td>" + Title + "</td><td>" + Year + "</td></tr>";
        }

        public virtual string ToBibFormat()
        {
            Dictionary<Field, bool> dict = PublicationFactory.EntryFieldUsage[EntryType];
            const string commaNewlineIndent = ",\r\n  ";
            string retVal = "@" + EntryType + "{" + CiteKey;
            bool auth;
            if (dict.TryGetValue(Field.Author, out auth) && !String.IsNullOrEmpty(Authors))
            {
                //if (auth)
                retVal += commaNewlineIndent + "Author = {" + Authors + "}";
            }
            bool ddress;
            if (dict.TryGetValue(Field.Address, out ddress) && !String.IsNullOrEmpty(Address))
            {
                //if (ddress)
                retVal += commaNewlineIndent + "Address = {" + Address + "}";
            }
            bool nnote;
            if (dict.TryGetValue(Field.Annote, out nnote) && !String.IsNullOrEmpty(Annote))
            {
                //if (nnote)
                retVal += commaNewlineIndent + "Annote = {" + Annote + "}";
            }
            bool ooktitle;
            if (dict.TryGetValue(Field.Booktitle, out ooktitle) && !String.IsNullOrEmpty(Booktitle))
            {
                //if (ooktitle)
                retVal += commaNewlineIndent + "Booktitle = {" + Booktitle + "}";
            }
            bool hapter;
            if (dict.TryGetValue(Field.Chapter, out hapter) && !String.IsNullOrEmpty(Chapter))
            {
                //if (hapter)
                retVal += commaNewlineIndent + "Chapter = {" + Chapter + "}";
            }
            bool rossref;
            if (dict.TryGetValue(Field.Crossref, out rossref) && !String.IsNullOrEmpty(Crossref))
            {
                //if (rossref)
                retVal += commaNewlineIndent + "Crossref = {" + Crossref + "}";
            }
            bool dition;
            if (dict.TryGetValue(Field.Edition, out dition) && !String.IsNullOrEmpty(Edition))
            {
                //if (dition)
                retVal += commaNewlineIndent + "Edition = {" + Edition + "}";
            }
            bool ditor;
            if (dict.TryGetValue(Field.Editor, out ditor) && !String.IsNullOrEmpty(Editors))
            {
                //if (ditor)
                retVal += commaNewlineIndent + "Editor = {" + Editors + "}";
            }
            bool owpublished;
            if (dict.TryGetValue(Field.Howpublished, out owpublished) && !String.IsNullOrEmpty(Howpublished))
            {
                //if (owpublished)
                retVal += commaNewlineIndent + "Howpublished = {" + Howpublished + "}";
            }
            bool nstitution;
            if (dict.TryGetValue(Field.Institution, out nstitution) && !String.IsNullOrEmpty(Institution))
            {
                //if (nstitution)
                retVal += commaNewlineIndent + "Institution = {" + Institution + "}";
            }
            bool ournal;
            if (dict.TryGetValue(Field.Journal, out ournal) && !String.IsNullOrEmpty(Journal))
            {
                //if (ournal)
                retVal += commaNewlineIndent + "Journal = {" + Journal + "}";
            }
            bool heKey;
            if (dict.TryGetValue(Field.TheKey, out heKey) && !String.IsNullOrEmpty(TheKey))
            {
                //if (heKey)
                retVal += commaNewlineIndent + "Key = {" + TheKey + "}";
            }
            bool onth;
            if (dict.TryGetValue(Field.Month, out onth) && !String.IsNullOrEmpty(Month))
            {
                //if (onth)
                retVal += commaNewlineIndent + "Month = {" + Month + "}";
            }
            bool ote;
            if (dict.TryGetValue(Field.Note, out ote) && !String.IsNullOrEmpty(Note))
            {
                //if (ote)
                retVal += commaNewlineIndent + "Note = {" + Note + "}";
            }
            bool umber;
            if (dict.TryGetValue(Field.Number, out umber) && !String.IsNullOrEmpty(Number))
            {
                //if (umber)
                retVal += commaNewlineIndent + "Number = {" + Number + "}";
            }
            bool rganization;
            if (dict.TryGetValue(Field.Organization, out rganization) && !String.IsNullOrEmpty(Organization))
            {
                //if (rganization)
                retVal += commaNewlineIndent + "Organization = {" + Organization + "}";
            }
            bool ages;
            if (dict.TryGetValue(Field.Pages, out ages) && !String.IsNullOrEmpty(Pages))
            {
                //if (ages)
                retVal += commaNewlineIndent + "Pages = {" + Pages + "}";
            }
            bool ublisher;
            if (dict.TryGetValue(Field.Publisher, out ublisher) && !String.IsNullOrEmpty(Publisher))
            {
                //if (ublisher)
                retVal += commaNewlineIndent + "Publisher = {" + Publisher + "}";
            }
            bool chool;
            if (dict.TryGetValue(Field.School, out chool) && !String.IsNullOrEmpty(School))
            {
                //if (chool)
                retVal += commaNewlineIndent + "School = {" + School + "}";
            }
            bool eries;
            if (dict.TryGetValue(Field.Series, out eries) && !String.IsNullOrEmpty(Series))
            {
                //if (eries)
                retVal += commaNewlineIndent + "Series = {" + Series + "}";
            }
            bool itle;
            if (dict.TryGetValue(Field.Title, out itle) && !String.IsNullOrEmpty(Title))
            {
                //if (itle)
                retVal += commaNewlineIndent + "Title = {" + Title + "}";
            }
            bool ype;
            if (dict.TryGetValue(Field.Type, out ype) && !String.IsNullOrEmpty(Type))
            {
                //if (ype)
                retVal += commaNewlineIndent + "Type = {" + Type + "}";
            }
            bool olume;
            if (dict.TryGetValue(Field.Volume, out olume) && !String.IsNullOrEmpty(Volume))
            {
                //if (olume)
                retVal += commaNewlineIndent + "Volume = {" + Volume + "}";
            }
            bool ear;
            if (dict.TryGetValue(Field.Year, out ear) && !String.IsNullOrEmpty(Year))
            {
                //if (ear)
                retVal += commaNewlineIndent + "Year = {" + Year + "}";
            }
            retVal += "\r\n}\r\n\r\n";


            return retVal;
        }

        public virtual new bool Equals(Object that)
        {
            if (that == null)
                return false;
            if (GetType() != that.GetType())
                return false;

            Publication t = (Publication)that;

            return (CiteKey == t.CiteKey &&
                    Abstract == t.Abstract &&
                    Owner == t.Owner &&
                    EntryType == t.EntryType &&
                    Address == t.Address &&
                    Annote == t.Annote &&
                    (Authors.Equals(t.Authors)) &&
                    Booktitle == t.Booktitle &&
                    Chapter == t.Chapter &&
                    Crossref == t.Crossref &&
                    Edition == t.Edition &&
                    (Editors.Equals(t.Editors)) &&
                    Howpublished == t.Howpublished &&
                    Institution == t.Institution &&
                    Journal == t.Journal &&
                    TheKey == t.TheKey &&
                    Month == t.Month &&
                    Note == t.Note &&
                    Number == t.Number &&
                    Organization == t.Organization &&
                    Pages == t.Pages &&
                    Publisher == t.Publisher &&
                    School == t.School &&
                    Series == t.Series &&
                    Title == t.Title &&
                    Type == t.Type &&
                    Volume == t.Volume &&
                    Year == t.Year);
        }

        #region Database Interaction

        public virtual bool ExistsInDatabase()
        {
            return (from a in (DataPersistence.GetSession()).Linq<Publication>()
                    where a.Id == Id
                    select a).Count() != 0;
        }

        private void UpdateInDatabase()
        {
            if (DeletionTime == null)
                AmendmentTime = DateTime.Now;

            ISession ses = DataPersistence.GetSession();
            ses.Update(this);
            ses.Flush();
            ses.Close();
        }

        public virtual void SaveOrUpdateInDatabase()
        {
            Dictionary<string, string> dict = IsValidEntry();
            if (dict.Count != 0)
                throw new InvalidEntryException(dict);

            if (ExistsInDatabase())
                UpdateInDatabase();
            else
                SaveToDatabase();
        }

        private void SaveToDatabase()
        {
            CreationTime = DateTime.Now;
            DataPersistence.GetSession().Persist(this);
        }

        #endregion

        #region Validity Checks - Boolean

        public virtual bool IsValidEntryBoolean()
        {
            if (String.IsNullOrEmpty(CiteKey))
                return false;

            if (EntryType == Entry.Article)
            {
                return IsValidArticle();
            }
            if (EntryType == Entry.Book)
            {
                return IsValidBook();
            }
            if (EntryType == Entry.Booklet)
            {
                return IsValidBooklet();
            }
            if (EntryType == Entry.Conference)
            {
                return IsValidConference();
            }
            if (EntryType == Entry.Inbook)
            {
                return IsValidInbook();
            }
            if (EntryType == Entry.Incollection)
            {
                return IsValidIncollection();
            }
            if (EntryType == Entry.Inproceedings)
            {
                return IsValidInproceedings();
            }
            if (EntryType == Entry.Manual)
            {
                return IsValidManual();
            }
            if (EntryType == Entry.Mastersthesis)
            {
                return IsValidMastersthesis();
            }
            if (EntryType == Entry.Misc)
            {
                return IsValidMisc();
            }
            if (EntryType == Entry.Phdthesis)
            {
                return IsValidPhdthesis();
            }
            if (EntryType == Entry.Proceedings)
            {
                return IsValidProceedings();
            }
            if (EntryType == Entry.Techreport)
            {
                return IsValidTechreport();
            }
            // else
            return IsValidUnpublished();
            
        }

        private bool IsValidArticle()
        {
            if (String.IsNullOrEmpty(Authors))
            {
                return false;
            }
            if (String.IsNullOrEmpty(Title))
            {
                return false;
            }
            if (String.IsNullOrEmpty(Journal))
            {
                return false;
            }
            if (String.IsNullOrEmpty(Year))
            {
                return false;
            }
            return true;
        }

        private bool IsValidBook()
        {
            if (String.IsNullOrEmpty(Authors) && String.IsNullOrEmpty(Editors))
            {
                return false;
            }
            if (String.IsNullOrEmpty(Title))
            {
                return false;
            }
            if (String.IsNullOrEmpty(Publisher))
            {
                return false;
            }
            if (String.IsNullOrEmpty(Year))
            {
                return false;
            }
            return true;
        }

        private bool IsValidBooklet()
        {
            return String.IsNullOrEmpty(Title);
        }

        private bool IsValidConference()
        {
            if (String.IsNullOrEmpty(Authors))
            {
                return false;
            }
            if (String.IsNullOrEmpty(Title))
            {
                return false;
            }
            return true;
        }

        private bool IsValidInbook()
        {
            if (String.IsNullOrEmpty(Authors) && String.IsNullOrEmpty(Editors))
            {
                return false;
            }
            if (String.IsNullOrEmpty(Title))
            {
                return false;
            }
            if (String.IsNullOrEmpty(Publisher))
            {
                return false;
            }
            if (String.IsNullOrEmpty(Year))
            {
                return false;
            }
            return true;
        }

        private bool IsValidIncollection()
        {
            if (String.IsNullOrEmpty(Authors))
            {
                return false;
            }
            if (String.IsNullOrEmpty(Title))
            {
                return false;
            }
            if (String.IsNullOrEmpty(Booktitle))
            {
                return false;
            }
            return true;
        }

        private bool IsValidInproceedings()
        {
            if (String.IsNullOrEmpty(Authors))
            {
                return false;
            }
            if (String.IsNullOrEmpty(Title))
            {
                return false;
            }
            return true;
        }

        private bool IsValidManual()
        {
            return !String.IsNullOrEmpty(Title);
        }

        private bool IsValidMastersthesis()
        {
            if (String.IsNullOrEmpty(Authors))
            {
                return false;
            }
            if (String.IsNullOrEmpty(Title))
            {
                return false;
            }
            if (String.IsNullOrEmpty(School))
            {
                return false;
            }
            if (String.IsNullOrEmpty(Year))
            {
                return false;
            }
            return true;
        }

        private bool IsValidMisc()
        {
            return true; // if the item exists, it has at least one non-null/empty field
        }

        private bool IsValidPhdthesis()
        {
            if (String.IsNullOrEmpty(Authors))
            {
                return false;
            }
            if (String.IsNullOrEmpty(Title))
            {
                return false;
            }
            if (String.IsNullOrEmpty(School))
            {
                return false;
            }
            if (String.IsNullOrEmpty(Year))
            {
                return false;
            }
            return true;
        }

        private bool IsValidProceedings()
        {
            if (String.IsNullOrEmpty(Title))
            {
                return false;
            }
            if (String.IsNullOrEmpty(Year))
            {
                return false;
            }
            return true;
        }

        private bool IsValidTechreport()
        {
            if (String.IsNullOrEmpty(Authors))
            {
                return false;
            }
            if (String.IsNullOrEmpty(Title))
            {
                return false;
            }
            if (String.IsNullOrEmpty(Institution))
            {
                return false;
            }
            if (String.IsNullOrEmpty(Year))
            {
                return false;
            }
            return true;
        }

        private bool IsValidUnpublished()
        {
            if (String.IsNullOrEmpty(Authors))
            {
                return false;
            }
            if (String.IsNullOrEmpty(Title))
            {
                return false;
            }
            if (String.IsNullOrEmpty(Note))
            {
                return false;
            }
            return true;
        }

        #endregion

        #region Validity Checks - Dictionaries returned

        public virtual Dictionary<string, string> IsValidEntry()
        {
            Dictionary<string,string> dict = new Dictionary<string, string>();
            if (String.IsNullOrEmpty(CiteKey)) 
                dict.Add("CiteKey", ErrorMessages.CiteKeyIsRequired);

            if (EntryType == Entry.Article)
            {
                return IsValidArticle(dict);
            }
            if (EntryType == Entry.Book)
            {
                return IsValidBook(dict);
            }
            if (EntryType == Entry.Booklet)
            {
                return IsValidBooklet(dict);
            }
            if (EntryType == Entry.Conference)
            {
                return IsValidConference(dict);
            }
            if (EntryType == Entry.Inbook)
            {
                return IsValidInbook(dict);
            }
            if (EntryType == Entry.Incollection)
            {
                return IsValidIncollection(dict);
            }
            if (EntryType == Entry.Inproceedings)
            {
                return IsValidInproceedings(dict);
            }
            if (EntryType == Entry.Manual)
            {
                return IsValidManual(dict);
            }
            if (EntryType == Entry.Mastersthesis)
            {
                return IsValidMastersthesis(dict);
            }
            if (EntryType == Entry.Misc)
            {
                return IsValidMisc(dict);
            }
            if (EntryType == Entry.Phdthesis)
            {
                return IsValidPhdthesis(dict);
            }
            if (EntryType == Entry.Proceedings)
            {
                return IsValidProceedings(dict);
            }
            if (EntryType == Entry.Techreport)
            {
                return IsValidTechreport(dict);
            }
            // else
            return IsValidUnpublished(dict);
        }

        private Dictionary<string, string> IsValidArticle(Dictionary<string, string> dict)
        {
            if (String.IsNullOrEmpty(Authors))
            {
                dict.Add("Authors", ErrorMessages.AuthorsIsRequired);
            }
            if (String.IsNullOrEmpty(Title))
            {
                dict.Add("Title", ErrorMessages.TitleIsRequired);
            }
            if (String.IsNullOrEmpty(Journal))
            {
                dict.Add("Journal", ErrorMessages.JournalIsRequired);
            }
            if (String.IsNullOrEmpty(Year))
            {
                dict.Add("Year", ErrorMessages.YearIsRequired);
            }
            return dict;
        }

        private Dictionary<string, string> IsValidBook(Dictionary<string, string> dict)
        {
            if (String.IsNullOrEmpty(Authors) && String.IsNullOrEmpty(Editors))
            {
                dict.Add("Authors", "Either Authors or Editors must not be empty");
                dict.Add("Editors", "Either Authors or Editors must not be empty");
            }
            if (String.IsNullOrEmpty(Title))
            {
                dict.Add("Title", ErrorMessages.TitleIsRequired);
            }
            if (String.IsNullOrEmpty(Publisher))
            {
                dict.Add("Publisher", ErrorMessages.PublisherIsRequired);
            }
            if (String.IsNullOrEmpty(Year))
            {
                dict.Add("Year", ErrorMessages.YearIsRequired);
            }
            return dict;
        }

        private Dictionary<string, string> IsValidBooklet(Dictionary<string, string> dict)
        {
            dict.Add("Title", ErrorMessages.TitleIsRequired);
            return dict;
        }

        private Dictionary<string, string> IsValidConference(Dictionary<string, string> dict)
        {
            if (String.IsNullOrEmpty(Authors))
            {
                dict.Add("Authors", ErrorMessages.AuthorsIsRequired);
            }
            if (String.IsNullOrEmpty(Title))
            {
                dict.Add("Title", ErrorMessages.TitleIsRequired);
            }
            return dict;
        }

        private Dictionary<string, string> IsValidInbook(Dictionary<string, string> dict)
        {
            if (String.IsNullOrEmpty(Authors) && String.IsNullOrEmpty(Editors))
            {
                dict.Add("Authors", "Either Authors or Editors must not be empty");
                dict.Add("Editors", "Either Authors or Editors must not be empty");
            }
            if (String.IsNullOrEmpty(Title))
            {
                dict.Add("Title", ErrorMessages.TitleIsRequired);
            }
            if (String.IsNullOrEmpty(Chapter))
            {
                dict.Add("Chapter", ErrorMessages.ChapterIsRequired);
            }
            if (String.IsNullOrEmpty(Publisher))
            {
                dict.Add("Publisher", ErrorMessages.PublisherIsRequired);
            }
            if (String.IsNullOrEmpty(Year))
            {
                dict.Add("Year", ErrorMessages.YearIsRequired);
            }
            return dict;
        }

        private Dictionary<string, string> IsValidIncollection(Dictionary<string, string> dict)
        {
            if (String.IsNullOrEmpty(Authors))
            {
                dict.Add("Authors", ErrorMessages.AuthorsIsRequired);
            }
            if (String.IsNullOrEmpty(Title))
            {
                dict.Add("Title", ErrorMessages.TitleIsRequired);
            }
            if (String.IsNullOrEmpty(Booktitle))
            {
                dict.Add("Booktitle", ErrorMessages.BooktitleIsRequired);
            }
            return dict;
        }

        private Dictionary<string, string> IsValidInproceedings(Dictionary<string, string> dict)
        {
            if (String.IsNullOrEmpty(Authors))
            {
                dict.Add("Authors", ErrorMessages.AuthorsIsRequired);
            }
            if (String.IsNullOrEmpty(Title))
            {
                dict.Add("Title", ErrorMessages.TitleIsRequired);
            }
            return dict;
        }

        private Dictionary<string, string> IsValidManual(Dictionary<string, string> dict)
        {
            if (String.IsNullOrEmpty(Title))
                dict.Add("Title", ErrorMessages.TitleIsRequired);
            return dict;
        }

        private Dictionary<string, string> IsValidMastersthesis(Dictionary<string, string> dict)
        {
            if (String.IsNullOrEmpty(Authors))
            {
                dict.Add("Authors", ErrorMessages.AuthorsIsRequired);
            }
            if (String.IsNullOrEmpty(Title))
            {
                dict.Add("Title", ErrorMessages.TitleIsRequired);
            }
            if (String.IsNullOrEmpty(School))
            {
                dict.Add("School", ErrorMessages.SchoolIsRequired);
            }
            if (String.IsNullOrEmpty(Year))
            {
                dict.Add("Year", ErrorMessages.YearIsRequired);
            }
            return dict;
        }

        private Dictionary<string, string> IsValidMisc(Dictionary<string, string> dict) { return dict; } // if the item exists, it has at least one non-null/empty field}

        private Dictionary<string, string> IsValidPhdthesis(Dictionary<string, string> dict)
        {
            if (String.IsNullOrEmpty(Authors))
            {
                dict.Add("Authors", ErrorMessages.AuthorsIsRequired);
            }
            if (String.IsNullOrEmpty(Title))
            {
                dict.Add("Title", ErrorMessages.TitleIsRequired);
            }
            if (String.IsNullOrEmpty(School))
            {
                dict.Add("School", ErrorMessages.SchoolIsRequired);
            }
            if (String.IsNullOrEmpty(Year))
            {
                dict.Add("Year", ErrorMessages.YearIsRequired);
            }
            return dict;
        }

        private Dictionary<string, string> IsValidProceedings(Dictionary<string, string> dict)
        {
            if (String.IsNullOrEmpty(Title))
            {
                dict.Add("Title", ErrorMessages.TitleIsRequired);
            }
            if (String.IsNullOrEmpty(Year))
            {
                dict.Add("Year", ErrorMessages.YearIsRequired);
            }
            return dict;
        }

        private Dictionary<string, string> IsValidTechreport(Dictionary<string, string> dict)
        {
            if (String.IsNullOrEmpty(Authors))
            {
                dict.Add("Authors", ErrorMessages.AuthorsIsRequired);
            }
            if (String.IsNullOrEmpty(Title))
            {
                dict.Add("Title", ErrorMessages.TitleIsRequired);
            }
            if (String.IsNullOrEmpty(Institution))
            {
                dict.Add("Institution", ErrorMessages.InstitutionIsRequired);
            }
            if (String.IsNullOrEmpty(Year))
            {
                dict.Add("Year", ErrorMessages.YearIsRequired);
            }
            return dict;
        }

        private Dictionary<string, string> IsValidUnpublished(Dictionary<string, string> dict)
        {
            if (String.IsNullOrEmpty(Authors))
            {
                dict.Add("Authors", ErrorMessages.AuthorsIsRequired);
            }
            if (String.IsNullOrEmpty(Title))
            {
                dict.Add("Title", ErrorMessages.TitleIsRequired);
            }
            if (String.IsNullOrEmpty(Note))
            {
                dict.Add("Note", ErrorMessages.NoteIsRequired);
            }
            return dict;
        }

        #endregion

        /* This author assumes that all authors/editors names will be separated by the 
         * word 'and', with spaces either side */
        public static void SeparateAuthors(string targ, out string a1, out string a2)
        {
            if (string.IsNullOrEmpty(targ))
            {
                a1 = "";
                a2 = "";
                return;
            }

            string[] split = targ.Split(new string[] { " and " }, StringSplitOptions.RemoveEmptyEntries);
            if (split.Length < 1)
            {
                a1 = targ; // Sole author/editor must be stored in targ
                a2 = "";
                return;
            }
            if (split.Length < 2)
            {
                a1 = split[0];
                a2 = "";
                return;
            }
            a1 = split[0];
            a2 = split[1];
            return;
        }
    }
}