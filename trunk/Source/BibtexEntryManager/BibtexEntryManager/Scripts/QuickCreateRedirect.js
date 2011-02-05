
// When called, user is redirected to the entry type selected.
function Create() {
    var pub = document.getElementById("entrytype").value;
    if (pub != "NoRedirect")
        window.location = "/Entry/" + pub;
}