using System;
using System.Collections.Generic;
using BibtexEntryManager.Models;
using BibtexEntryManager.Models.EntryTypes;
using BibtexEntryManager.Models.Enums;

namespace BibtexEntryManager.Helpers
{
    public class PublicationFactory
    {
	    //Map for mapping entry to their respective classes
        public static Dictionary<string, string> ClassNames { get; private set; }

	    //Following singleton pattern
	    private static PublicationFactory _factory;

	    /**Constructs PublicationFactory object*/
	    private PublicationFactory()
	    {
		    // Hashmap mapping Entry types to class names
		    ClassNames = new Dictionary<string, string>
		                     {
		            {Entry.Article.ToString(), "Article"},
		            {Entry.Book.ToString(), "Book"},
		            {Entry.Booklet.ToString(), "BookLet"},
		            {Entry.Conference.ToString(), "Conference"},
		            {Entry.Inbook.ToString(), "InBook"},
		            {Entry.Incollection.ToString(), "InCollection"},
		            {Entry.Manual.ToString(), "Manual"},
		            {Entry.Mastersthesis.ToString(), "MastersThesis"},
		            {Entry.Misc.ToString(), "Misc"},
		            {Entry.Phdthesis.ToString(), "PhdThesis"},
		            {Entry.Proceedings.ToString(), "Proceedings"},
		            {Entry.Techreport.ToString(), "TechReport"},
		            {Entry.Unpublished.ToString(), "Unpublished"},
		            {Entry.Inproceedings.ToString(), "InProceedings"}
		        };
	    }

	    /**Returns an instance of Publication Factory*/
	    public static PublicationFactory GetInstance()
	    {
	        return _factory ?? (_factory = new PublicationFactory());
	    }

        /**Makes a publication object and populates it with data *
	     * @param e Entry indicating the type of Publication object to be created
	     * @param oneEntry A map containing fields and values*/
	    public static Publication MakePublication(Entry e, Dictionary<string, string> oneEntry)
	    {
            lock (typeof(PublicationFactory))
            {
                oneEntry.Add(Field.Entrytype.ToString(), e.ToString());
                return MakePublication(oneEntry);
            }
	    }
	
	    /*
         * Makes a publication object and populates it with data
	     * Dictionary containing mapping of fields to values
         * 
         * Returns null if an unhandled exception occurs.
         */
	    public static Publication MakePublication(Dictionary<string, string> oneEntry)
	    {
            lock (typeof(PublicationFactory))
            {
                Entry e;

                // set e to the entry type, currently stored as a string.
                Enum.TryParse(oneEntry[Field.Entrytype.ToString()], true, out e);

                
                // Create a publication object depending upon the entry type
                return ObjectBuilder.NewPublicationFrom(e, oneEntry);
            }
	    }


        public static readonly Dictionary<Entry, Dictionary<Field, bool>> EntryFieldUsage
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
                                              {Field.Year, true},
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
                                                      {Field.Booktitle, true},
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
                                                {Field.Title, true},
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
                                                       {Field.School, true},
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
                                                   {Field.School, true},
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
                                                     {Field.Title, true},
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
                                                    {Field.Institution, true},
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
                                                     {Field.Note, true},
                                                     {Field.TheKey, false},
                                                     {Field.Month, false},
                                                     {Field.Year, false},
                                                     {Field.Annote, false}
                                                 }
                          }
                  };
    }
}