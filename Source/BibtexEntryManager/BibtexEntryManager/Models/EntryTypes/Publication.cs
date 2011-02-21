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
        
        [Required(ErrorMessage = ErrorMessages.CiteKeyRequired)]
        [DisplayName("Cite Key")]
        public virtual string CiteKey { get; set; }
        public virtual string Owner { get; set; }

        [DisplayName("Entry Type")]
        public virtual Entry EntryType { get; set; }
        public virtual string Abstract { get; set; }
        
        public virtual string Address { get; set; }
        public virtual string Annote { get; set; }
        public virtual string Authors { get; set; }
        public virtual string Booktitle { get; set; }
        public virtual string Chapter { get; set; }
        public virtual string Crossref { get; set; }
        public virtual string Edition { get; set; }
        public virtual string Editors { get; set; }
        public virtual string Howpublished { get; set; }
        public virtual string Institution { get; set; }
        public virtual string Journal { get; set; }

        [DisplayName("Key")]
        public virtual string TheKey { get; set; }
        public virtual string Month { get; set; }
        public virtual string Note { get; set; }
        public virtual string Number { get; set; }
        public virtual string Organization { get; set; }
        public virtual string Pages { get; set; }
        public virtual string Publisher { get; set; }
        public virtual string School { get; set; }
        public virtual string Series { get; set; }
        public virtual string Title { get; set; }
        public virtual string Type { get; set; }
        public virtual string Volume { get; set; }
        public virtual string Year { get; set; }

        //public virtual PublicationGroup PublicationGroup { get; set; }
        // times of modification/creation/
        public virtual DateTime? DeletionTime { get; set; }
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
            const string newLineIndent = "\r\n    ";

            string retVal = "@" + EntryType + "{" + CiteKey + "," + newLineIndent;
            bool auth;
            if (dict.TryGetValue(Field.Author, out auth))
            {
                //if (auth)
                retVal += "Author = {" + Authors + "}," + newLineIndent;
            }
            bool ddress;
            if (dict.TryGetValue(Field.Address, out ddress))
            {
                //if (ddress)
                retVal += "Address = {" + Address + "}," + newLineIndent;
            }
            bool nnote;
            if (dict.TryGetValue(Field.Annote, out nnote))
            {
                //if (nnote)
                retVal += "Annote = {" + Annote + "}," + newLineIndent;
            }
            bool ooktitle;
            if (dict.TryGetValue(Field.Booktitle, out ooktitle))
            {
                //if (ooktitle)
                retVal += "Booktitle = {" + Booktitle + "}," + newLineIndent;
            }
            bool hapter;
            if (dict.TryGetValue(Field.Chapter, out hapter))
            {
                //if (hapter)
                retVal += "Chapter = {" + Chapter + "}," + newLineIndent;
            }
            bool rossref;
            if (dict.TryGetValue(Field.Crossref, out rossref))
            {
                //if (rossref)
                retVal += "Crossref = {" + Crossref + "}," + newLineIndent;
            }
            bool dition;
            if (dict.TryGetValue(Field.Edition, out dition))
            {
                //if (dition)
                retVal += "Edition = {" + Edition + "}," + newLineIndent;
            }
            bool ditor;
            if (dict.TryGetValue(Field.Editor, out ditor))
            {
                //if (ditor)
                retVal += "Editor = {" + Editors + "}," + newLineIndent;
            }
            bool owpublished;
            if (dict.TryGetValue(Field.Howpublished, out owpublished))
            {
                //if (owpublished)
                retVal += "Howpublished = {" + Howpublished + "}," + newLineIndent;
            }
            bool nstitution;
            if (dict.TryGetValue(Field.Institution, out nstitution))
            {
                //if (nstitution)
                retVal += "Institution = {" + Institution + "}," + newLineIndent;
            }
            bool heKey;
            if (dict.TryGetValue(Field.TheKey, out heKey))
            {
                //if (heKey)
                retVal += "Key = {" + TheKey + "}," + newLineIndent;
            }
            bool onth;
            if (dict.TryGetValue(Field.Month, out onth))
            {
                //if (onth)
                retVal += "Month = {" + Month + "}," + newLineIndent;
            }
            bool ote;
            if (dict.TryGetValue(Field.Note, out ote))
            {
                //if (ote)
                retVal += "Note = {" + Note + "}," + newLineIndent;
            }
            bool umber;
            if (dict.TryGetValue(Field.Number, out umber))
            {
                //if (umber)
                retVal += "Number = {" + Number + "}," + newLineIndent;
            }
            bool rganization;
            if (dict.TryGetValue(Field.Organization, out rganization))
            {
                //if (rganization)
                retVal += "Organization = {" + Organization + "}," + newLineIndent;
            }
            bool ages;
            if (dict.TryGetValue(Field.Pages, out ages))
            {
                //if (ages)
                retVal += "Pages = {" + Pages + "}," + newLineIndent;
            }
            bool ublisher;
            if (dict.TryGetValue(Field.Publisher, out ublisher))
            {
                //if (ublisher)
                retVal += "Publisher = {" + Publisher + "}," + newLineIndent;
            }
            bool chool;
            if (dict.TryGetValue(Field.School, out chool))
            {
                //if (chool)
                retVal += "School = {" + School + "}," + newLineIndent;
            }
            bool eries;
            if (dict.TryGetValue(Field.Series, out eries))
            {
                //if (eries)
                retVal += "Series = {" + Series + "}," + newLineIndent;
            }
            bool itle;
            if (dict.TryGetValue(Field.Title, out itle))
            {
                //if (itle)
                retVal += "Title = {" + Title + "}," + newLineIndent;
            }
            bool ype;
            if (dict.TryGetValue(Field.Type, out ype))
            {
                //if (ype)
                retVal += "Type = {" + Type + "}," + newLineIndent;
            }
            bool olume;
            if (dict.TryGetValue(Field.Volume, out olume))
            {
                //if (olume)
                retVal += "Volume = {" + Volume + "}," + newLineIndent;
            }
            bool ear;
            if (dict.TryGetValue(Field.Year, out ear))
            {
                //if (ear)
                retVal += "Year = {" + Year + "}" + "\r\n";
            }
            retVal += "}\r\n\r\n";


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
                    where a.CiteKey.Equals(CiteKey)
                    select a).Count() != 0;
        }

        private void UpdateInDatabase()
        {
            if (!IsValidEntry())
                throw new InvalidEntryException();
            ISession ses = DataPersistence.GetSession();
            ses.Update(this);
            ses.Flush();
            ses.Close();
        }

        public virtual void SaveOrUpdateInDatabase()
        {
            if (!IsValidEntry())
                throw new InvalidEntryException();

            if (ExistsInDatabase())
                UpdateInDatabase();
            else
                SaveToDatabase();
        }

        private void SaveToDatabase()
        {
            if (!IsValidEntry())
                throw new InvalidEntryException();
            this.CreationTime = DateTime.Now;
            DataPersistence.GetSession().Persist(this);
        }

        #endregion

        #region Validity Checks

        public virtual bool IsValidEntry()
        {
            if (String.IsNullOrEmpty(CiteKey))
                return false;
            
            if (EntryType == Entry.Article)
            {
                return IsValidArticle();
            }
            else if (EntryType == Entry.Book)
            {
                return IsValidBook();
            }
            else if (EntryType == Entry.Booklet)
            {
                return IsValidBooklet();
            }
            else if (EntryType == Entry.Conference)
            {
                return IsValidConference();
            }
            else if (EntryType == Entry.Inbook)
            {
                return IsValidInbook();
            }
            else if (EntryType == Entry.Incollection)
            {
                return IsValidIncollection();
            }
            else if (EntryType == Entry.Inproceedings)
            {
                return IsValidInproceedings();
            }
            else if (EntryType == Entry.Manual)
            {
                return IsValidManual();
            }
            else if (EntryType == Entry.Mastersthesis)
            {
                return IsValidMastersthesis();
            }
            else if (EntryType == Entry.Misc)
            {
                return IsValidMisc();
            }
            else if (EntryType == Entry.Phdthesis)
            {
                return IsValidPhdthesis();
            }
            else if (EntryType == Entry.Proceedings)
            {
                return IsValidProceedings();
            }
            else if (EntryType == Entry.Techreport)
            {
                return IsValidTechreport();
            }
            else
            {
                return IsValidUnpublished();
            }
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
            return String.IsNullOrEmpty(Title);
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