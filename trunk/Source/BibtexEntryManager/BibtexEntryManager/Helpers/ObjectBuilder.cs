using System.Collections.Generic;
using BibtexEntryManager.Models.EntryTypes;
using BibtexEntryManager.Models.Enums;

namespace BibtexEntryManager.Helpers
{
    public static class ObjectBuilder
    {
        public static Publication BuildDefaultPublication()
        {
            return NewDefaultPublication();
        }

        private static Publication NewDefaultPublication()
        {
            Publication p = new Publication
            {
                Abstract = "random abstract",
                Owner = "johnny",
                Address = "Address",
                Annote = "Annote",
                Authors = "John Smith",
                Booktitle = "Booktitle",
                Chapter = "Chapter",
                Crossref = "Crossref",
                Edition = "Edition",
                Editors = "Editor",
                Howpublished = "Howpublished",
                Institution = "Institution",
                Journal = "Journal",
                TheKey = "Key",
                Month = "Month",
                Note = "Note",
                Number = "Number",
                Organization = "Organization",
                Pages = "Pages",
                Publisher = "Publisher",
                School = "School",
                Series = "Series",
                Title = "Title",
                Type = "Type",
                Volume = "Volume",
                Year = "Year"
            };
            return p;
        }

        public static Publication NewDefaultBook()
        {
            Publication p = new Publication
            {
                CiteKey = "JS2010",
                Owner = "johnny",
                Authors = "John Smith",
                EntryType = Entry.Book,
                Title = "A thorough investigation book",
                Publisher = "Publisher",
                Year = "2010"
            };
            return p;
        }
        public static Publication NewPublicationFrom(Entry e, Dictionary<string, string> oneEntry)
        {
            // Validates the provided entry. If it fails validity tests, the publication is not created and is rejected.
            //if (!VerifyValidity(e, oneEntry))
            //{
            //    throw new InvalidEntryException();
            //}

            string abs,
                   address,
                   annote,
                   author,
                   booktitle,
                   chapter,
                   citekey,
                   crossref,
                   edition,
                   editor,
                   howpublished,
                   institution,
                   journal,
                   key,
                   m,
                   note,
                   number,
                   organization,
                   pages,
                   publisher,
                   school,
                   series,
                   title,
                   type,
                   volume,
                   year;

            oneEntry.TryGetValue("abstract", out abs);
            oneEntry.TryGetValue("address", out address);
            oneEntry.TryGetValue("annote", out annote);
            oneEntry.TryGetValue("author", out author);
            oneEntry.TryGetValue("booktitle", out booktitle);
            oneEntry.TryGetValue("chapter", out chapter);
            oneEntry.TryGetValue("citekey", out citekey);
            oneEntry.TryGetValue("crossref", out crossref);
            oneEntry.TryGetValue("edition", out edition);
            oneEntry.TryGetValue("editor", out editor);
            oneEntry.TryGetValue("howpublished", out howpublished);
            oneEntry.TryGetValue("institution", out institution);
            oneEntry.TryGetValue("journal", out journal);
            oneEntry.TryGetValue("key", out key);
            oneEntry.TryGetValue("month", out m);
            oneEntry.TryGetValue("note", out note);
            oneEntry.TryGetValue("number", out number);
            oneEntry.TryGetValue("organization", out organization);
            oneEntry.TryGetValue("pages", out pages);
            oneEntry.TryGetValue("publisher", out publisher);
            oneEntry.TryGetValue("school", out school);
            oneEntry.TryGetValue("series", out series);
            oneEntry.TryGetValue("title", out title);
            oneEntry.TryGetValue("type", out type);
            oneEntry.TryGetValue("volume", out volume);
            oneEntry.TryGetValue("year", out year);
            if (!string.IsNullOrEmpty(m))
            {
                m = m.Substring(0, 3).ToLower();

                if (m.Equals("jan"))
                {
                    m = "January";
                }
                else if (m.Equals("feb"))
                {
                    m = "February";
                }
                else if (m.Equals("mar"))
                {
                    m = "March";
                }
                else if (m.Equals("apr"))
                {
                    m = "April";
                }
                else if (m.Equals("may"))
                {
                    m = "May";
                }
                else if (m.Equals("jun"))
                {
                    m = "June";
                }
                else if (m.Equals("jul"))
                {
                    m = "July";
                }
                else if (m.Equals("aug"))
                {
                    m = "August";
                }
                else if (m.Equals("sep"))
                {
                    m = "September";
                }
                else if (m.Equals("oct"))
                {
                    m = "October";
                }
                else if (m.Equals("nov"))
                {
                    m = "November";
                }
                else if (m.Equals("dec"))
                {
                    m = "December";
                }
            }


            Publication p = new Publication
                                {
                                    EntryType = e,
                                    Abstract = abs,
                                    Address = address,
                                    Annote = annote,
                                    Authors = author,
                                    Booktitle = booktitle,
                                    Chapter = chapter,
                                    CiteKey = citekey,
                                    Crossref = crossref,
                                    Edition = edition,
                                    Editors = editor,
                                    Howpublished = howpublished,
                                    Institution = institution,
                                    Journal = journal,
                                    TheKey = key,
                                    Month = m,
                                    Note = note,
                                    Number = number,
                                    Organization = organization,
                                    Pages = pages,
                                    Publisher = publisher,
                                    School = school,
                                    Series = series,
                                    Title = title,
                                    Type = type,
                                    Volume = volume,
                                    Year = year
                                };
            return p;
        }

        public static bool VerifyValidity(Entry e, Dictionary<string,string> check)
        {
            //Dictionary<Field, bool> used = Publication.EntryFieldUsage[e];

            //foreach (Field field in used.Keys)
            //{
            //    if (used[field])
            //    {
            //        string a;
            //        if (!check.TryGetValue(field.ToString().ToLower(),out a))
            //        {
            //            return false;
            //        }
            //    }
            //}
            return true;
        }
    }
}