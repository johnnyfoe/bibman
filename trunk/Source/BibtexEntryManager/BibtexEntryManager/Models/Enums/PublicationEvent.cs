namespace BibtexEntryManager.Models.Enums
{ 

    /**PublicationEvent enum contains enumerations for Publication adding, deleting, updating event*/
    public enum PublicationEvent
    {
	    BULK_ADD_STARTED, BULK_ADD_PROGRESS, BULK_ADD_FINISHED, PUBLICATION_ADDED, PUBLICATION_UPDATED, PUBLICATION_DELETED
    }
}