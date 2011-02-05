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
        public static Dictionary<String, String> ClassNames { get; private set; }

	    //Following singleton pattern
	    private static PublicationFactory _factory;

	    /**Constructs PublicationFactory object*/
	    private PublicationFactory()
	    {
		    // Hashmap mapping Entry types to class names
		    ClassNames = new Dictionary<String, String>
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
	    public static Publication MakePublication(Entry e, Dictionary<String, String> oneEntry)
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
	    public static Publication MakePublication(Dictionary<String, String> oneEntry)
	    {
            lock (typeof(PublicationFactory))
            {
                Entry e;

                // set e to the entry type, currently stored as a string.
                Enum.TryParse(oneEntry[Field.Entrytype.ToString()], true, out e);

                try
                {
                    // Create a publication object depending upon the entry type
                    return ObjectBuilder.NewPublicationFrom(e, oneEntry);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.StackTrace);
                    return null;
                }
            }
	    }

	    
    }
}