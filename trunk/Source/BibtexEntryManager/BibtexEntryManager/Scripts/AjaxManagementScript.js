// To be included with /Entry/Search.aspx
function keysearch() {
    var service = new SearchResults();
    service.DoSearch(document.getElementById("searchString").value, onSuccess, null, null);
}

function onSuccess(result) {
    document.getElementById("Results").innerHTML = result;
}