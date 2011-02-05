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
using NHibernate.Linq;
using Wintellect.PowerCollections;

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
        public virtual int Id { get; set; }
        [Required(ErrorMessage = ErrorMessages.CiteKeyRequired)]
        [DisplayName("Cite Key")]
        public virtual string CiteKey { get; set; }
        public virtual string Owner { get; set; }
        //public virtual PublicationGroup PublicationGroup { get; set; }
        public virtual Entry EntryType { get; set; }
        //optional
        public virtual string Abstract { get; set; }
        // times of modification/creation/

        // possible core fields
        public virtual string Address { get; set; }
        public virtual string Annote { get; set; }
        public virtual List<string> Authors { get; set; }
        public virtual string Booktitle { get; set; }
        public virtual string Chapter { get; set; }
        public virtual string Crossref { get; set; }
        public virtual string Edition { get; set; }
        public virtual List<string> Editors { get; set; }
        public virtual string Howpublished { get; set; }
        public virtual string Institution { get; set; }
        public virtual string Journal { get; set; }
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

        public Dictionary<String, String> UnknownFields { get; private set; }

        #region Entry Field Usage
        private static Dictionary<Entry, Dictionary<Field, bool>> EntryFieldUsage
            = new Dictionary<Entry, Dictionary<Field, bool>>
                  {
                      {
                          Entry.Article, new Dictionary<Field, bool>
                                             {
                                                 {Field.Author, true},
                                                 {Field.Title, true},
                                                 {Field.Journal, true},
                                                 {Field.Year, true},
                                                 {Field.TheKey, false},
                                                 {Field.Volume, false},
                                                 {Field.Number, false},
                                                 {Field.Pages, false},
                                                 {Field.Month, false},
                                                 {Field.Note, false},
                                                 {Field.Annote, false}
                                             }
                          },
                      {
                          Entry.Book, new Dictionary<Field, bool>
                                          {
                                              {Field.Author, true},
                                              {Field.Title, true},
                                              {Field.Publisher, true},
                                              {Field.Key, false},
                                              {Field.Volume, false},
                                              {Field.Number, false},
                                              {Field.Series, false},
                                              {Field.Address, false},
                                              {Field.Edition, false},
                                              {Field.Month, false},
                                              {Field.Note, false},
                                              {Field.Annote, false}

                                          }
                          },
                      {
                          Entry.Booklet, new Dictionary<Field, bool>
                                             {
                                                 {Field.Title, true},
                                                 {Field.Key, false},
                                                 {Field.Author, false},
                                                 {Field.Howpublished, false},
                                                 {Field.Address, false},
                                                 {Field.Month, false},
                                                 {Field.Year, false},
                                                 {Field.Note, false},
                                                 {Field.Annote, false}
                                             }
                          },
                      {
                          Entry.Conference, new Dictionary<Field, bool>
                                                {
                                                    {Field.Author, true},
                                                    {Field.Title, true},
                                                    {Field.Crossref, false},
                                                    {Field.Key, false},
                                                    {Field.Booktitle, false},
                                                    {Field.Pages, false},
                                                    {Field.Year, false},
                                                    {Field.Editor, false},
                                                    {Field.Volume, false},
                                                    {Field.Number, false},
                                                    {Field.Series, false},
                                                    {Field.Address, false},
                                                    {Field.Month, false},
                                                    {Field.Organization, false},
                                                    {Field.Publisher, false},
                                                    {Field.Note, false},
                                                    {Field.Annote, false}
                                                }
                          },
                      {
                          Entry.Inbook, new Dictionary<Field, bool>
                                            {
                                                {Field.Author, true},
                                                {Field.Title, true},
                                                {Field.Chapter, true},
                                                {Field.Publisher, true},
                                                {Field.Year, true},
                                                {Field.Key, false},
                                                {Field.Volume, false},
                                                {Field.Number, false},
                                                {Field.Series, false},
                                                {Field.Type, false},
                                                {Field.Address, false},
                                                {Field.Edition, false},
                                                {Field.Month, false},
                                                {Field.Pages, false},
                                                {Field.Note, false},
                                                {Field.Annote, false}
                                            }
                          },
                      {
                          Entry.Incollection, new Dictionary<Field, bool>
                                                  {
                                                      {Field.Author, true},
                                                      {Field.Title, true},
                                                      {Field.Chapter, true},
                                                      {Field.Publisher, true},
                                                      {Field.Year, true},
                                                      {Field.Crossref, false},
                                                      {Field.Key, false},
                                                      {Field.Pages, false},
                                                      {Field.Publisher, false},
                                                      {Field.Year, false},
                                                      {Field.Editor, false},
                                                      {Field.Volume, false},
                                                      {Field.Number, false},
                                                      {Field.Series, false},
                                                      {Field.Type, false},
                                                      {Field.Chapter, false},
                                                      {Field.Address, false},
                                                      {Field.Edition, false},
                                                      {Field.Month, false},
                                                      {Field.Note, false},
                                                      {Field.Annote, false}
                                                  }
                          },
                      {
                          Entry.Inproceedings, new Dictionary<Field, bool>
                                                   {
                                                       {Field.Author, true},
                                                       {Field.Title, true},
                                                       {Field.Chapter, true},
                                                       {Field.Publisher, true},
                                                       {Field.Year, true},
                                                       {Field.Crossref, false},
                                                       {Field.Key, false},
                                                       {Field.Booktitle, false},
                                                       {Field.Pages, false},
                                                       {Field.Year, false},
                                                       {Field.Editor, false},
                                                       {Field.Volume, false},
                                                       {Field.Number, false},
                                                       {Field.Series, false},
                                                       {Field.Address, false},
                                                       {Field.Month, false},
                                                       {Field.Organization, false},
                                                       {Field.Publisher, false},
                                                       {Field.Note, false},
                                                       {Field.Annote, false}
                                                   }
                          },
                      {
                          Entry.Manual, new Dictionary<Field, bool>
                                            {
                                                {Field.Author, true},
                                                {Field.Title, true},
                                                {Field.Chapter, true},
                                                {Field.Publisher, true},
                                                {Field.Year, true},
                                                {Field.Key, false},
                                                {Field.Author, false},
                                                {Field.Organization, false},
                                                {Field.Address, false},
                                                {Field.Edition, false},
                                                {Field.Month, false},
                                                {Field.Year, false},
                                                {Field.Note, false},
                                                {Field.Annote, false}
                                            }
                          },
                      {
                          Entry.Mastersthesis, new Dictionary<Field, bool>
                                                   {
                                                       {Field.Author, true},
                                                       {Field.Title, true},
                                                       {Field.Chapter, true},
                                                       {Field.Publisher, true},
                                                       {Field.Year, true},
                                                       {Field.Key, false},
                                                       {Field.Type, false},
                                                       {Field.Address, false},
                                                       {Field.Month, false},
                                                       {Field.Note, false},
                                                       {Field.Annote, false}
                                                   }
                          },
                      {
                          Entry.Misc, new Dictionary<Field, bool>
                                          {
                                              {Field.Author, true},
                                              {Field.Title, true},
                                              {Field.Chapter, true},
                                              {Field.Publisher, true},
                                              {Field.Year, true},
                                              {Field.Key, false},
                                              {Field.Author, false},
                                              {Field.Title, false},
                                              {Field.Howpublished, false},
                                              {Field.Month, false},
                                              {Field.Year, false},
                                              {Field.Note, false},
                                              {Field.Annote, false}
                                          }
                          },
                      {
                          Entry.Phdthesis, new Dictionary<Field, bool>
                                               {
                                                   {Field.Author, true},
                                                   {Field.Title, true},
                                                   {Field.Chapter, true},
                                                   {Field.Publisher, true},
                                                   {Field.Year, true},
                                                   {Field.TheKey, false},
                                                   {Field.Type, false},
                                                   {Field.Address, false},
                                                   {Field.Month, false},
                                                   {Field.Note, false},
                                                   {Field.Annote, false}
                                               }
                          },
                      {
                          Entry.Proceedings, new Dictionary<Field, bool>
                                                 {
                                                     {Field.Author, true},
                                                     {Field.Title, true},
                                                     {Field.Chapter, true},
                                                     {Field.Publisher, true},
                                                     {Field.Year, true},
                                                     {Field.TheKey, false},
                                                     {Field.Booktitle, false},
                                                     {Field.Editor, false},
                                                     {Field.Volume, false},
                                                     {Field.Number, false},
                                                     {Field.Series, false},
                                                     {Field.Address, false},
                                                     {Field.Month, false},
                                                     {Field.Organization, false},
                                                     {Field.Publisher, false},
                                                     {Field.Note, false},
                                                     {Field.Annote, false}
                                                 }
                          },
                      {
                          Entry.Techreport, new Dictionary<Field, bool>
                                                {
                                                    {Field.Author, true},
                                                    {Field.Title, true},
                                                    {Field.Chapter, true},
                                                    {Field.Publisher, true},
                                                    {Field.Year, true},
                                                    {Field.Key, false},
                                                    {Field.Type, false},
                                                    {Field.Number, false},
                                                    {Field.Address, false},
                                                    {Field.Month, false},
                                                    {Field.Note, false},
                                                    {Field.Annote, false}
                                                }
                          },
                      {
                          Entry.Unpublished, new Dictionary<Field, bool>
                                                 {
                                                     {Field.Author, true},
                                                     {Field.Title, true},
                                                     {Field.Chapter, true},
                                                     {Field.Publisher, true},
                                                     {Field.Year, true},
                                                     {Field.TheKey, false},
                                                     {Field.Month, false},
                                                     {Field.Year, false},
                                                     {Field.Annote, false}
                                                 }
                          }
                  };
        #endregion

        public Publication()
        {
            Authors = new List<string>();
            Editors = new List<string>();
            UnknownFields = new Dictionary<String, String>();
        }

        /// <summary>
        /// Returns a html row as a string representing the Publication. Should include only CiteKey, Author, Year and title with no HTML links
        /// </summary>
        /// <returns></returns>
        public virtual string ToHtmlTableRowNoLinks()
        {
            return "<tr><td>" + CiteKey + "</td><td>" + EntryType + "</td><td>" + Authors[0] + "</td><td>" + Authors[1] +
                   "</td><td>" + Title + "</td><td>" + Year + "</td></tr>";
        }

        /// <summary>
        /// Returns a html row as a string representing the Publication. Should include only CiteKey, Author, Year and title with HTML links to the edit page.
        /// </summary>
        /// <returns></returns>
        public virtual string ToHtmlTableRowWithLinks()
        {
            return "<tr><td><a href=/Entry/Publication/" + Id + "\">" + CiteKey + "</a></td><td>TechReport</td><td>" + Authors[0] + "</td><td>" + Authors[1] + "</td><td>" + Title + "</td><td>" + Year + "</td></tr>";
        }

        /// <summary>
        /// Returns a html table as a string representing the Publication. Should include ALL details
        /// </summary>
        /// <returns></returns>
        public virtual string ToHtmlTable()
        {// todo - might want back in.
            return "";
        }

        public virtual string ToBibFormat()
        {
            Dictionary<Field, bool> dict = EntryFieldUsage[EntryType];
            string newLineIndent = "\r\n    ";

            string retVal = "@" + EntryType + "{" + CiteKey + "," + newLineIndent;
            if (dict[Field.Author])
            {
                retVal += "Author = {" + BibFormatAuthorsOrEditors(Authors) + "}," + newLineIndent;
            }
            if (dict[Field.Address])
            {
                retVal += "Address = {" + Address + "}," + newLineIndent;
            }
            if (dict[Field.Annote])
            {
                retVal += "Annote = {" + Annote + "}," + newLineIndent;
            }
            if (dict[Field.Booktitle])
            {
                retVal += "Booktitle = {" + Booktitle + "}," + newLineIndent;
            }
            if (dict[Field.Chapter])
            {
                retVal += "Chapter = {" + Chapter + "}," + newLineIndent;
            }
            if (dict[Field.Crossref])
            {
                retVal += "Crossref = {" + Crossref + "}," + newLineIndent;
            }
            if (dict[Field.Edition])
            {
                retVal += "Edition = {" + Edition + "}," + newLineIndent;
            }
            if (dict[Field.Editor])
            {
                retVal += "Editor = {" + BibFormatAuthorsOrEditors(Editors) + "}," + newLineIndent;
            }
            if (dict[Field.Howpublished])
            {
                retVal += "Howpublished = {" + Howpublished + "}," + newLineIndent;
            }
            if (dict[Field.Institution])
            {
                retVal += "Institution = {" + Institution + "}," + newLineIndent;
            }
            if (dict[Field.TheKey])
            {
                retVal += "Key = {" + TheKey + "}," + newLineIndent;
            }
            if (dict[Field.Month])
            {
                retVal += "Month = {" + Month + "}," + newLineIndent;
            }
            if (dict[Field.Note])
            {
                retVal += "Note = {" + Note + "}," + newLineIndent;
            }
            if (dict[Field.Number])
            {
                retVal += "Number = {" + Number + "}," + newLineIndent;
            }
            if (dict[Field.Organization])
            {
                retVal += "Organization = {" + Organization + "}," + newLineIndent;
            }
            if (dict[Field.Pages])
            {
                retVal += "Pages = {" + Pages + "}," + newLineIndent;
            }
            if (dict[Field.Publisher])
            {
                retVal += "Publisher = {" + Publisher + "}," + newLineIndent;
            }
            if (dict[Field.School])
            {
                retVal += "School = {" + School + "}," + newLineIndent;
            }
            if (dict[Field.Series])
            {
                retVal += "Series = {" + Series + "}," + newLineIndent;
            }
            if (dict[Field.Title])
            {
                retVal += "Title = {" + Title + "}," + newLineIndent;
            }
            if (dict[Field.Type])
            {
                retVal += "Type = {" + Type + "}," + newLineIndent;
            }
            if (dict[Field.Volume])
            {
                retVal += "Volume = {" + Volume + "}," + newLineIndent;
            }
            if (dict[Field.Year])
            {
                retVal += "Year = {" + Year + "}," + newLineIndent;
            }


            return retVal;
        }

        public static string BibFormatAuthorsOrEditors(List<string> set)
        {
            string s = set[0];
            int c = 1;
            while (c < set.Count)
            {
                s += " and " + set[c];
                c++;
            }
            return s;
        }

        public static string HumanFormatAuthorsOrEditors(List<string> set)
        {
            if (set.Count == 0)
                return "";

            string s = set[0];
            int c = 1;
            int setCount = set.Count;
            if (setCount == 1)
                return s;
            // else

            while (c < setCount)
            {
                if (c != (setCount - 2))
                {
                    s += ", " + set[c];
                }
                else
                {
                    s += " and " + set[c];
                }
                c++;
            }
            return s;
        }


        /**Return true if and only if the id of that publication is same as the id of this publication
	     * @param that Publication object
	     * @return boolean*/
        public new bool Equals(Object that)
        {
            if (that == null)
                return false;
            if (GetType() != that.GetType())
                return false;

            Publication t = (Publication)that;

            return (CiteKey == t.CiteKey &&
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

        public bool ExistsInDatabase()
        {
            return (from a in (DataPersistance.GetSession()).Linq<Publication>()
                    where a.CiteKey.Equals(CiteKey)
                    select a).Count() != 0;
        }

        public void UpdateInDatabase()
        {
            if (!IsValidEntry())
                throw new InvalidEntryException();

            DataPersistance.GetSession().Update(this);
        }

        public void SaveOrUpdateInDatabase()
        {
            if (!IsValidEntry())
                throw new InvalidEntryException();

            if (ExistsInDatabase())
                UpdateInDatabase();
            SaveToDatabase();
        }

        public void SaveToDatabase()
        {
            if (!IsValidEntry())
                throw new InvalidEntryException();

            DataPersistance.GetSession().Save(this);
        }
        #endregion

        #region Validity Checks
        public bool IsValidEntry()
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
            if (Authors.Count == 0)
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
            if (Authors.Count == 0 || Editors.Count == 0)
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
            if (Authors.Count == 0)
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
            if (Authors.Count == 0 || Editors.Count == 0)
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
            if (Authors.Count == 0)
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
            if (Authors.Count == 0)
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
            if (Authors.Count == 0)
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
            return (!String.IsNullOrEmpty(TheKey) || !(Authors.Count == 0) ||
                    !String.IsNullOrEmpty(Title) || !String.IsNullOrEmpty(Howpublished) ||
                    !String.IsNullOrEmpty(Month) || !String.IsNullOrEmpty(Year) ||
                    !String.IsNullOrEmpty(Note) || !String.IsNullOrEmpty(Annote));
        }

        private bool IsValidPhdthesis()
        {
            if (Authors.Count == 0)
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
            if (Authors.Count == 0)
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
            if (Authors.Count == 0)
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
    }
}