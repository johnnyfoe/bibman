function DoPageLoad() {
    document.getElementById("entrySelector").value = document.getElementById("EntryType").value;
    document.getElementById("monthSelector").value = document.getElementById("Month").value;
    ChangeEntryType();

    if (ItemId() != -1) {
        timeUpdateContent();
    } 
}

function timeUpdateContent() {
    var t = setTimeout("CheckForModifications()", 500);
}

function CheckForModifications() {
    var createdAt = document.getElementById("PageCreationTime").value;
    var service = new BibtexEntryManager.SearchResults();
    var queryString = createdAt + " " + ItemId();
    service.HasPublicationChanged(queryString, onModificationCheckSuccess, null, null);
}

function onModificationCheckSuccess(result) {
    if (result == -1) {
        return;
    }

    if (result == 0) {
        // continue checking for modifications if there was no change (set the timer for another 3 seconds)
        var t = setTimeout("CheckForModifications()", 3000);
        return;
    }
    if (result == 1) {
        document.getElementById("notificationOfUpdate").innerHTML =
                    "A user has deleted this entry - refresh the page to " +
                    "see their changes or click <a href=\"/Entry/Publication/" +
                    ItemId() + "\" target=\"_blank\">here</a> to see it in a new tab/window.";
        return;
    }
    if (result == 2) {
        document.getElementById("notificationOfUpdate").innerHTML =
                    "A user has updated this entry - refresh the page to " +
                    "see their changes or click <a href=\"/Entry/Publication/" +
                    ItemId() + "\" target=\"_blank\">here</a> to see it in a new tab/window.";
        return;
    }
}

function ChangeMonth() {
    var month = document.getElementById("monthSelector").value;
    document.getElementById("Month").value = month;
}

function ChangeEntryType() {
    var entryType = document.getElementById("entrySelector").value;

    document.getElementById("EntryType").value = entryType;

    var required = document.getElementById('required'); /* required Fieldset */
    var optional = document.getElementById('optional'); /* optional Fieldset */
    var hidden = document.getElementById('hidingPlace'); /* hiding place for unused fields*/

    var abstract = document.getElementById('field_for_abstract');
    var citekey = document.getElementById('field_for_citekey');

    var address = document.getElementById('field_for_address');
    var annote = document.getElementById('field_for_annote');
    var author = document.getElementById('field_for_authors');
    var booktitle = document.getElementById('field_for_booktitle');
    var chapter = document.getElementById('field_for_chapter');
    var crossref = document.getElementById('field_for_crossref');
    var edition = document.getElementById('field_for_edition');
    var editor = document.getElementById('field_for_editors');
    var howpublished = document.getElementById('field_for_howpublished');
    var institution = document.getElementById('field_for_institution');
    var journal = document.getElementById('field_for_journal');
    var key = document.getElementById('field_for_thekey');
    var month = document.getElementById('field_for_month');
    var note = document.getElementById('field_for_note');
    var number = document.getElementById('field_for_number');
    var organization = document.getElementById('field_for_organisation');
    var pages = document.getElementById('field_for_pages');
    var publisher = document.getElementById('field_for_publisher');
    var school = document.getElementById('field_for_school');
    var series = document.getElementById('field_for_series');
    var title = document.getElementById('field_for_title');
    var type = document.getElementById('field_for_type');
    var volume = document.getElementById('field_for_volume');
    var year = document.getElementById('field_for_year');

    required.appendChild(citekey);

    if (entryType == "Article") {
        required.appendChild(author);
        required.appendChild(title);
        required.appendChild(journal);
        required.appendChild(year);

        optional.appendChild(key);
        optional.appendChild(volume);
        optional.appendChild(number);
        optional.appendChild(pages);
        optional.appendChild(month);
        optional.appendChild(note);
        optional.appendChild(annote);


        hidden.appendChild(address);
        hidden.appendChild(booktitle);
        hidden.appendChild(chapter);
        hidden.appendChild(crossref);
        hidden.appendChild(edition);
        hidden.appendChild(editor);
        hidden.appendChild(howpublished);
        hidden.appendChild(institution);
        hidden.appendChild(organization);
        hidden.appendChild(publisher);
        hidden.appendChild(school);
        hidden.appendChild(series);
        hidden.appendChild(type);

    }
    else if (entryType == "Book") {

        required.appendChild(author);
        required.appendChild(editor);
        required.appendChild(title);
        required.appendChild(publisher);
        required.appendChild(year);


        optional.appendChild(key);
        optional.appendChild(volume);
        optional.appendChild(number);
        optional.appendChild(series);
        optional.appendChild(address);
        optional.appendChild(edition);
        optional.appendChild(month);
        optional.appendChild(note);
        optional.appendChild(annote);



        hidden.appendChild(booktitle);
        hidden.appendChild(chapter);
        hidden.appendChild(crossref);
        hidden.appendChild(howpublished);
        hidden.appendChild(institution);
        hidden.appendChild(journal);
        hidden.appendChild(organization);
        hidden.appendChild(pages);
        hidden.appendChild(school);
        hidden.appendChild(type);
    }
    else if (entryType == "Booklet") {
        required.appendChild(title);

        optional.appendChild(key);
        optional.appendChild(author);
        optional.appendChild(howpublished);
        optional.appendChild(address);
        optional.appendChild(month);
        optional.appendChild(year);
        optional.appendChild(note);
        optional.appendChild(annote);


        hidden.appendChild(booktitle);
        hidden.appendChild(chapter);
        hidden.appendChild(crossref);
        hidden.appendChild(edition);
        hidden.appendChild(editor);
        hidden.appendChild(institution);
        hidden.appendChild(journal);
        hidden.appendChild(number);
        hidden.appendChild(organization);
        hidden.appendChild(pages);
        hidden.appendChild(publisher);
        hidden.appendChild(school);
        hidden.appendChild(series);
        hidden.appendChild(type);
        hidden.appendChild(volume);
    }
    else if (entryType == "Conference") {
        required.appendChild(author);
        required.appendChild(title);
        required.appendChild(booktitle);
        required.appendChild(year);

        optional.appendChild(editor);
        optional.appendChild(volume);
        optional.appendChild(series);
        optional.appendChild(pages);
        optional.appendChild(address);
        optional.appendChild(month);
        optional.appendChild(organization);
        optional.appendChild(publisher);
        optional.appendChild(note);

        hidden.appendChild(annote);
        hidden.appendChild(chapter);
        hidden.appendChild(crossref);
        hidden.appendChild(edition);
        hidden.appendChild(howpublished);
        hidden.appendChild(institution);
        hidden.appendChild(journal);
        hidden.appendChild(key);
        hidden.appendChild(number);
        hidden.appendChild(school);
        hidden.appendChild(type);

    }
    else if (entryType == "Inbook") {
        required.appendChild(author);
        required.appendChild(editor);
        required.appendChild(title);
        required.appendChild(chapter);
        required.appendChild(publisher);
        required.appendChild(year);

        optional.appendChild(key);
        optional.appendChild(volume);
        optional.appendChild(number);
        optional.appendChild(series);
        optional.appendChild(type);
        optional.appendChild(address);
        optional.appendChild(edition);
        optional.appendChild(month);
        optional.appendChild(pages);
        optional.appendChild(note);
        optional.appendChild(annote);


        hidden.appendChild(booktitle);
        hidden.appendChild(crossref);
        hidden.appendChild(howpublished);
        hidden.appendChild(institution);
        hidden.appendChild(journal);
        hidden.appendChild(organization);
        hidden.appendChild(school);
    }
    else if (entryType == "Incollection") {
        required.appendChild(author);
        required.appendChild(title);
        required.appendChild(booktitle);

        optional.appendChild(crossref);
        optional.appendChild(key);
        optional.appendChild(pages);
        optional.appendChild(publisher);
        optional.appendChild(year);
        optional.appendChild(editor);
        optional.appendChild(volume);
        optional.appendChild(number);
        optional.appendChild(series);
        optional.appendChild(type);
        optional.appendChild(chapter);
        optional.appendChild(address);
        optional.appendChild(edition);
        optional.appendChild(month);
        optional.appendChild(note);
        optional.appendChild(annote);


        hidden.appendChild(howpublished);
        hidden.appendChild(institution);
        hidden.appendChild(journal);
        hidden.appendChild(organization);
        hidden.appendChild(school);
    }
    else if (entryType == "Inproceedings") {
        required.appendChild(author);
        required.appendChild(title);

        optional.appendChild(crossref);
        optional.appendChild(key);
        optional.appendChild(booktitle);
        optional.appendChild(pages);
        optional.appendChild(year);
        optional.appendChild(editor);
        optional.appendChild(volume);
        optional.appendChild(number);
        optional.appendChild(series);
        optional.appendChild(address);
        optional.appendChild(month);
        optional.appendChild(organization);
        optional.appendChild(publisher);
        optional.appendChild(note);
        optional.appendChild(annote);


        hidden.appendChild(chapter);
        hidden.appendChild(edition);
        hidden.appendChild(howpublished);
        hidden.appendChild(institution);
        hidden.appendChild(journal);
        hidden.appendChild(school);
        hidden.appendChild(type);
    }
    else if (entryType == "Manual") {
        required.appendChild(title);

        optional.appendChild(key);
        optional.appendChild(author);
        optional.appendChild(organization);
        optional.appendChild(address);
        optional.appendChild(edition);
        optional.appendChild(month);
        optional.appendChild(year);
        optional.appendChild(note);
        optional.appendChild(annote);


        hidden.appendChild(booktitle);
        hidden.appendChild(chapter);
        hidden.appendChild(crossref);
        hidden.appendChild(editor);
        hidden.appendChild(howpublished);
        hidden.appendChild(institution);
        hidden.appendChild(journal);
        hidden.appendChild(number);
        hidden.appendChild(pages);
        hidden.appendChild(publisher);
        hidden.appendChild(school);
        hidden.appendChild(series);
        hidden.appendChild(type);
        hidden.appendChild(volume);


    }
    else if (entryType == "Mastersthesis") {
        required.appendChild(author);
        required.appendChild(title);
        required.appendChild(school);
        required.appendChild(year);

        optional.appendChild(key);
        optional.appendChild(type);
        optional.appendChild(address);
        optional.appendChild(month);
        optional.appendChild(note);
        optional.appendChild(annote);


        hidden.appendChild(booktitle);
        hidden.appendChild(chapter);
        hidden.appendChild(crossref);
        hidden.appendChild(edition);
        hidden.appendChild(editor);
        hidden.appendChild(howpublished);
        hidden.appendChild(institution);
        hidden.appendChild(journal);
        hidden.appendChild(number);
        hidden.appendChild(organization);
        hidden.appendChild(pages);
        hidden.appendChild(publisher);
        hidden.appendChild(series);
        hidden.appendChild(volume);
    }
    else if (entryType == "Misc") {

        optional.appendChild(key);
        optional.appendChild(author);
        optional.appendChild(title);
        optional.appendChild(howpublished);
        optional.appendChild(month);
        optional.appendChild(year);
        optional.appendChild(note);
        optional.appendChild(annote);


        hidden.appendChild(address);
        hidden.appendChild(booktitle);
        hidden.appendChild(chapter);
        hidden.appendChild(crossref);
        hidden.appendChild(edition);
        hidden.appendChild(editor);
        hidden.appendChild(institution);
        hidden.appendChild(journal);
        hidden.appendChild(number);
        hidden.appendChild(organization);
        hidden.appendChild(pages);
        hidden.appendChild(publisher);
        hidden.appendChild(school);
        hidden.appendChild(series);
        hidden.appendChild(type);
        hidden.appendChild(volume);
    }
    else if (entryType == "Phdthesis") {

        required.appendChild(author);
        required.appendChild(title);
        required.appendChild(school);
        required.appendChild(year);


        optional.appendChild(key);
        optional.appendChild(type);
        optional.appendChild(address);
        optional.appendChild(month);
        optional.appendChild(note);
        optional.appendChild(annote);



        hidden.appendChild(booktitle);
        hidden.appendChild(chapter);
        hidden.appendChild(crossref);
        hidden.appendChild(edition);
        hidden.appendChild(editor);
        hidden.appendChild(howpublished);
        hidden.appendChild(institution);
        hidden.appendChild(journal);
        hidden.appendChild(number);
        hidden.appendChild(organization);
        hidden.appendChild(pages);
        hidden.appendChild(publisher);
        hidden.appendChild(series);
        hidden.appendChild(volume);
    }
    else if (entryType == "Proceedings") {

        required.appendChild(title);
        required.appendChild(year);



        optional.appendChild(key);
        optional.appendChild(booktitle);
        optional.appendChild(editor);
        optional.appendChild(volume);
        optional.appendChild(number);
        optional.appendChild(series);
        optional.appendChild(address);
        optional.appendChild(month);
        optional.appendChild(organization);
        optional.appendChild(publisher);
        optional.appendChild(note);
        optional.appendChild(annote);


        hidden.appendChild(author);
        hidden.appendChild(chapter);
        hidden.appendChild(crossref);
        hidden.appendChild(edition);
        hidden.appendChild(howpublished);
        hidden.appendChild(institution);
        hidden.appendChild(journal);
        hidden.appendChild(pages);
        hidden.appendChild(school);
        hidden.appendChild(type);
    }
    else if (entryType == "Techreport") {

        required.appendChild(author);
        required.appendChild(title);
        required.appendChild(institution);
        required.appendChild(year);

        optional.appendChild(key);
        optional.appendChild(type);
        optional.appendChild(number);
        optional.appendChild(address);
        optional.appendChild(month);
        optional.appendChild(note);
        optional.appendChild(annote);


        hidden.appendChild(booktitle);
        hidden.appendChild(chapter);
        hidden.appendChild(crossref);
        hidden.appendChild(edition);
        hidden.appendChild(editor);
        hidden.appendChild(howpublished);
        hidden.appendChild(journal);
        hidden.appendChild(organization);
        hidden.appendChild(pages);
        hidden.appendChild(publisher);
        hidden.appendChild(school);
        hidden.appendChild(series);
        hidden.appendChild(volume);
    }
    else if (entryType == "Unpublished") {

        required.appendChild(author);
        required.appendChild(title);
        required.appendChild(note);
        optional.appendChild(key);
        optional.appendChild(month);
        optional.appendChild(year);
        optional.appendChild(annote);


        hidden.appendChild(address);
        hidden.appendChild(booktitle);
        hidden.appendChild(chapter);
        hidden.appendChild(crossref);
        hidden.appendChild(edition);
        hidden.appendChild(editor);
        hidden.appendChild(howpublished);
        hidden.appendChild(institution);
        hidden.appendChild(journal);
        hidden.appendChild(number);
        hidden.appendChild(organization);
        hidden.appendChild(pages);
        hidden.appendChild(publisher);
        hidden.appendChild(school);
        hidden.appendChild(series);
        hidden.appendChild(type);
        hidden.appendChild(volume);
    }

    optional.appendChild(abstract);
}