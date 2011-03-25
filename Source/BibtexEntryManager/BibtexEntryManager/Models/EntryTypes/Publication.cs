using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using BibtexEntryManager.Data;
using BibtexEntryManager.Helpers;
using BibtexEntryManager.Models.Enums;
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
        [StringLength(255)]
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

        /// <summary>
        /// Not settable by users - internal to the system, so need no validation attributes
        /// </summary>
        public virtual DateTime? DeletionTime { get; set; }
        public virtual DateTime? AmendmentTime { get; set; }
        public virtual DateTime? CreationTime { get; set; }

        #endregion

        /// <summary>
        /// Default constructor (required by NHibernate)
        /// </summary>
        public Publication()
        {

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

        /// <summary>
        /// converts the publication to the bibtex format
        /// </summary>
        /// <returns>Bibtex-format string</returns>
        public virtual string ToBibFormat()
        {
            Dictionary<Field, bool> dict = PublicationFactory.EntryFieldUsage[EntryType];
            const string commaNewlineIndent = ",\r\n  ";
            string retVal = "@" + EntryType + "{" + CiteKey;
            bool discardedBoolean;
            // if the author field is in the dictionary, it should be included.  
            // If no Author is recorded, it has been stored without it and is optional.
            if (dict.TryGetValue(Field.Author, out discardedBoolean) && !String.IsNullOrEmpty(Authors))
            {
                retVal += commaNewlineIndent + "Author = {" + Authors + "}";
            }
            if (dict.TryGetValue(Field.Address, out discardedBoolean) && !String.IsNullOrEmpty(Address))
            {
                retVal += commaNewlineIndent + "Address = {" + Address + "}";
            }
            if (dict.TryGetValue(Field.Annote, out discardedBoolean) && !String.IsNullOrEmpty(Annote))
            {
                retVal += commaNewlineIndent + "Annote = {" + Annote + "}";
            }
            if (dict.TryGetValue(Field.Booktitle, out discardedBoolean) && !String.IsNullOrEmpty(Booktitle))
            {
                retVal += commaNewlineIndent + "Booktitle = {" + Booktitle + "}";
            }
            if (dict.TryGetValue(Field.Chapter, out discardedBoolean) && !String.IsNullOrEmpty(Chapter))
            {
                retVal += commaNewlineIndent + "Chapter = {" + Chapter + "}";
            }
            if (dict.TryGetValue(Field.Crossref, out discardedBoolean) && !String.IsNullOrEmpty(Crossref))
            {
                retVal += commaNewlineIndent + "Crossref = {" + Crossref + "}";
            }
            if (dict.TryGetValue(Field.Edition, out discardedBoolean) && !String.IsNullOrEmpty(Edition))
            {
                retVal += commaNewlineIndent + "Edition = {" + Edition + "}";
            }
            if (dict.TryGetValue(Field.Editor, out discardedBoolean) && !String.IsNullOrEmpty(Editors))
            {
                retVal += commaNewlineIndent + "Editor = {" + Editors + "}";
            }
            if (dict.TryGetValue(Field.Howpublished, out discardedBoolean) && !String.IsNullOrEmpty(Howpublished))
            {
                retVal += commaNewlineIndent + "Howpublished = {" + Howpublished + "}";
            }
            if (dict.TryGetValue(Field.Institution, out discardedBoolean) && !String.IsNullOrEmpty(Institution))
            {
                retVal += commaNewlineIndent + "Institution = {" + Institution + "}";
            }
            if (dict.TryGetValue(Field.Journal, out discardedBoolean) && !String.IsNullOrEmpty(Journal))
            {
                retVal += commaNewlineIndent + "Journal = {" + Journal + "}";
            }
            if (dict.TryGetValue(Field.TheKey, out discardedBoolean) && !String.IsNullOrEmpty(TheKey))
            {
                retVal += commaNewlineIndent + "Key = {" + TheKey + "}";
            }
            if (dict.TryGetValue(Field.Month, out discardedBoolean) && !String.IsNullOrEmpty(Month))
            {
                retVal += commaNewlineIndent + "Month = {" + Month + "}";
            }
            if (dict.TryGetValue(Field.Note, out discardedBoolean) && !String.IsNullOrEmpty(Note))
            {
                retVal += commaNewlineIndent + "Note = {" + Note + "}";
            }
            if (dict.TryGetValue(Field.Number, out discardedBoolean) && !String.IsNullOrEmpty(Number))
            {
                retVal += commaNewlineIndent + "Number = {" + Number + "}";
            }
            if (dict.TryGetValue(Field.Organization, out discardedBoolean) && !String.IsNullOrEmpty(Organization))
            {
                retVal += commaNewlineIndent + "Organization = {" + Organization + "}";
            }
            if (dict.TryGetValue(Field.Pages, out discardedBoolean) && !String.IsNullOrEmpty(Pages))
            {
                retVal += commaNewlineIndent + "Pages = {" + Pages + "}";
            }
            if (dict.TryGetValue(Field.Publisher, out discardedBoolean) && !String.IsNullOrEmpty(Publisher))
            {
                retVal += commaNewlineIndent + "Publisher = {" + Publisher + "}";
            }
            if (dict.TryGetValue(Field.School, out discardedBoolean) && !String.IsNullOrEmpty(School))
            {
                retVal += commaNewlineIndent + "School = {" + School + "}";
            }
            if (dict.TryGetValue(Field.Series, out discardedBoolean) && !String.IsNullOrEmpty(Series))
            {
                retVal += commaNewlineIndent + "Series = {" + Series + "}";
            }
            if (dict.TryGetValue(Field.Title, out discardedBoolean) && !String.IsNullOrEmpty(Title))
            {
                retVal += commaNewlineIndent + "Title = {" + Title + "}";
            }
            if (dict.TryGetValue(Field.Type, out discardedBoolean) && !String.IsNullOrEmpty(Type))
            {
                retVal += commaNewlineIndent + "Type = {" + Type + "}";
            }
            if (dict.TryGetValue(Field.Volume, out discardedBoolean) && !String.IsNullOrEmpty(Volume))
            {
                retVal += commaNewlineIndent + "Volume = {" + Volume + "}";
            }
            if (dict.TryGetValue(Field.Year, out discardedBoolean) && !String.IsNullOrEmpty(Year))
            {
                retVal += commaNewlineIndent + "Year = {" + Year + "}";
            }
            retVal += "\r\n}\r\n\r\n";


            return retVal;
        }

        /// <summary>
        /// Establishes whether or not two objects are equal
        /// </summary>
        /// <param name="that">The object to be compared</param>
        /// <returns>true if they match, false otherwise</returns>
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
                    Authors == t.Authors &&
                    Booktitle == t.Booktitle &&
                    Chapter == t.Chapter &&
                    Crossref == t.Crossref &&
                    Edition == t.Edition &&
                    Editors == t.Editors &&
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


        /// <summary>
        /// Examines whether or not an entry already exists, then updates or saves it depending on the result.
        /// Can throw InvalidEntryException if the entry is not valid
        /// </summary>
        public virtual void SaveOrUpdateInDatabase()
        {
            Dictionary<string, string> dict = CheckForValidity();
            if (dict.Count != 0)
                throw new InvalidEntryException(dict);

            if (ExistsInDatabase())
                UpdateInDatabase();
            else
                SaveToDatabase();
        }

        /// <summary>
        /// Finds out if an entry exists in the database already
        /// </summary>
        /// <returns>true if it exists, false otherwise</returns>
        public virtual bool ExistsInDatabase()
        {
            return (from a in (DataPersistence.GetSession()).Linq<Publication>()
                    where a.Id == Id
                    select a).Count() != 0;
        }

        /// <summary>
        /// Persists a new entry to the database and records the creation time.
        /// </summary>
        private void SaveToDatabase()
        {
            CreationTime = DateTime.Now;
            DataPersistence.GetSession().Persist(this);
        }

        /// <summary>
        /// Persists changes made to an entry and records the amendment time.
        /// </summary>
        private void UpdateInDatabase()
        {
            if (DeletionTime == null)
                AmendmentTime = DateTime.Now;

            ISession ses = DataPersistence.GetSession();
            ses.Update(this);
            ses.Flush();
            ses.Close();
        }

        #endregion

        #region Validity Checks - Dictionaries returned
        /// <summary>
        /// Checks that an entry is valid.
        /// </summary>
        /// <returns>A set of key-value pairs which describe any errors with the publication
        /// If there are no errors, the set is empty.</returns>
        public virtual Dictionary<string, string> CheckForValidity()
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
            if (String.IsNullOrEmpty(Title))
                dict.Add("Title", ErrorMessages.TitleIsRequired);
            return dict;
        }

        private Dictionary<string, string> IsValidConference(Dictionary<string, string> dict)
        {
            // Requirements are the same as inproceedings
            return IsValidInproceedings(dict);
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
            if (String.IsNullOrEmpty(Publisher))
            {
                dict.Add("Publisher", ErrorMessages.PublisherIsRequired);
            }
            if (String.IsNullOrEmpty(Booktitle))
            {
                dict.Add("Booktitle", ErrorMessages.BooktitleIsRequired);
            }
            if (String.IsNullOrEmpty(Year))
            {
                dict.Add("Year", ErrorMessages.YearIsRequired);
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

        private Dictionary<string, string> IsValidManual(Dictionary<string, string> dict)
        {
            if (String.IsNullOrEmpty(Title))
                dict.Add("Title", ErrorMessages.TitleIsRequired);
            return dict;
        }

        private Dictionary<string, string> IsValidMastersthesis(Dictionary<string, string> dict)
        {
            // Same as for PhdThesis
            return IsValidPhdthesis(dict);
        }

        private Dictionary<string, string> IsValidMisc(Dictionary<string, string> dict)
        {
            // if the item exists, it has at least one non-null/empty field - no action required.
            return dict;
        }

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

        /// <summary>
        /// This author assumes that all authors/editors names will be separated by the word 'and', 
        /// with spaces either side.  If not, it will be assumed that there is only one author
        /// </summary>
        /// <param name="targ">The string to separate into different authors</param>
        /// <param name="a1">The variable to store author 1 in, passed by reference</param>
        /// <param name="a2">The variable to store author 2 in, passed by reference</param>
        public static void SeparateAuthors(string targ, out string a1, out string a2)
        {
            if (string.IsNullOrEmpty(targ))
            {
                a1 = "";
                a2 = "";
                return;
            }

            string[] split = targ.Split(new [] { " and " }, StringSplitOptions.RemoveEmptyEntries);
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