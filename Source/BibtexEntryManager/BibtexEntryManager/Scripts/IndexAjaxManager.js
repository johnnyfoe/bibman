function DoPageLoad() {
    timeUpdateContent();
}

function timeUpdateContent() {
    var t = setTimeout("UpdateContent()", 3000);
}

function UpdateContent() {
    var createdAt = document.getElementById("PageCreationTime").value;
    var service = new BibtexEntryManager.SearchResults();
    service.GetAmendedPublications(createdAt, onAmendmentCheckSuccess, null, null);
    service.GetDeletedPublications(createdAt, onDeletedItemsCheckSuccess, null, null);
    service.GetNewPublications(createdAt, onNewPublicationsCheckSuccess, null, null);

    var currentDate = new Date();

    document.getElementById('PageCreationTime').value = currentDate.toGMTString();
    timeUpdateContent(); // poll server every 3 seconds
}

function onAmendmentCheckSuccess(result) {
    if (result != null) {
        for (var i = 0; i < result.length; i++) {
            document.getElementById("tr_" + result[i]).setAttribute("class", "AmendedPublication");
        }
    }
}

function onDeletedItemsCheckSuccess(result) {
    if (result != null) {
        for (var i = 0; i < result.length; i++) {
            document.getElementById("tr_" + result[i]).setAttribute("class", "DeletedPublication");
        }
    }
}

function onNewPublicationsCheckSuccess(result) {
    if (result != null) {
        var tbodies = document.getElementsByTagName("tbody");
        var originalHTML = tbodies[0].innerHTML;
        var finalString = "";
        for (var k = 0; k < result.length; k++) {
            finalString += result[k];
        }
        tbodies[0].innerHTML = finalString + originalHTML;
    }
}