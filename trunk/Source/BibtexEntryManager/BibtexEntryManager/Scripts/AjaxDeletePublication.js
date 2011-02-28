function DeletePublication(target) {
    var service = new BibtexEntryManager.SearchResults();
    service.DeletePublication(target, deletionSuccess, null, null);
}