<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<BibtexEntryManager.Models.EntryTypes.Publication>" %>
<%@ Import Namespace="BibtexEntryManager.Models.Enums" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    <% string amendOrCreate = (Model == null) ? "Create Entry" : "Amend Entry";%>
    <%: amendOrCreate %>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <% string amendOrCreate = (Model == null) ? "Create Entry" : "Amend Entry";%>
    <h2>
        <% Writer.Write(amendOrCreate); %></h2>
    <% using (Html.BeginForm())
       {%>
    <%: Html.ValidationSummary(true) %>

    <div id="selectType" class="inputRow">
        <div class="labelColumn">
            <%: Html.LabelFor(model => model.EntryType) %>
        </div>
        <div class="inputColumn">
        <%: Html.HiddenFor(model => model.EntryType) %>
            <select id="entrySelector" onchange="return ChangeEntryType()">
                <option value="Article">Article</option>
                <option value="Book">Book</option>
                <option value="Booklet">Booklet</option>
                <option value="Conference">Conference</option>
                <option value="Inbook">Inbook</option>
                <option value="Incollection">Incollection</option>
                <option value="Inproceedings">Inproceedings</option>
                <option value="Manual">Manual</option>
                <option value="Mastersthesis">Mastersthesis</option>
                <option value="Misc">Misc</option>
                <option value="Phdthesis">Phdthesis</option>
                <option value="Proceedings">Proceedings</option>
                <option value="Techreport">Techreport</option>
                <option value="Unpublished">Unpublished</option>
            </select>
        </div>
    </div>
    <fieldset id="required">
        <legend>Required Fields</legend>
        <div id="field_for_citekey" class="inputRow">
            <div class="labelColumn">
                <%: Html.LabelFor(model => model.CiteKey) %>
            </div>
            <div class="inputColumn">
                <%: Html.TextBoxFor(model => model.CiteKey) %>
            </div>
            <div class="errorColumn">
                <%: Html.ValidationMessageFor(model => model.CiteKey) %>
            </div>
        </div>
        <div id="field_for_authors" class="inputRow">
            <div class="labelColumn">
                <%: Html.LabelFor(model => model.Authors) %>
            </div>
            <div class="inputColumn">
                <%: Html.TextBoxFor(model => model.Authors) %>
            </div>
            <div class="errorColumn">
                <%: Html.ValidationMessageFor(model => model.Authors) %>
            </div>
        </div>
        <div id="field_for_title" class="inputRow">
            <div class="labelColumn">
                <%: Html.LabelFor(model => model.Title) %>
            </div>
            <div class="inputColumn">
                <%: Html.TextBoxFor(model => model.Title) %>
            </div>
            <div class="errorColumn">
                <%: Html.ValidationMessageFor(model => model.Title) %>
            </div>
        </div>
        <div id="field_for_journal" class="inputRow">
            <div class="labelColumn">
                <%: Html.LabelFor(model => model.Journal) %>
            </div>
            <div class="inputColumn">
                <%: Html.TextBoxFor(model => model.Journal) %>
            </div>
            <div class="errorColumn">
                <%: Html.ValidationMessageFor(model => model.Journal) %>
            </div>
        </div>
        <div id="field_for_year" class="inputRow">
            <div class="labelColumn">
                <%: Html.LabelFor(model => model.Year) %>
            </div>
            <div class="inputColumn">
                <%: Html.TextBoxFor(model => model.Year) %>
            </div>
            <div class="errorColumn">
                <%: Html.ValidationMessageFor(model => model.Year) %>
            </div>
        </div>
    </fieldset>
    <fieldset id="optional">
        <legend>Optional Fields</legend>
        <div id="field_for_thekey" class="inputRow">
            <div class="labelColumn">
                <%: Html.LabelFor(model => model.TheKey) %>
            </div>
            <div class="inputColumn">
                <%: Html.TextBoxFor(model => model.TheKey) %>
            </div>
            <div class="errorColumn">
                <%: Html.ValidationMessageFor(model => model.TheKey) %>
            </div>
        </div>
        <div id="field_for_volume" class="inputRow">
            <div class="labelColumn">
                <%: Html.LabelFor(model => model.Volume) %>
            </div>
            <div class="inputColumn">
                <%: Html.TextBoxFor(model => model.Volume) %>
            </div>
            <div class="errorColumn">
                <%: Html.ValidationMessageFor(model => model.Volume) %>
            </div>
        </div>
        <div id="field_for_number" class="inputRow">
            <div class="labelColumn">
                <%: Html.LabelFor(model => model.Number) %>
            </div>
            <div class="inputColumn">
                <%: Html.TextBoxFor(model => model.Number) %>
            </div>
            <div class="errorColumn">
                <%: Html.ValidationMessageFor(model => model.Number) %>
            </div>
        </div>
        <div id="field_for_pages" class="inputRow">
            <div class="labelColumn">
                <%: Html.LabelFor(model => model.Pages) %>
            </div>
            <div class="inputColumn">
                <%: Html.TextBoxFor(model => model.Pages) %>
            </div>
            <div class="errorColumn">
                <%: Html.ValidationMessageFor(model => model.Pages) %>
            </div>
        </div>
        <div id="field_for_month" class="inputRow">
            <div class="labelColumn">
                <%: Html.LabelFor(model => model.Month) %>
            </div>
            <div class="inputColumn">
                <%: Html.TextBoxFor(model => model.Month) %>
            </div>
            <div class="errorColumn">
                <%: Html.ValidationMessageFor(model => model.Month) %>
            </div>
        </div>
        <div id="field_for_note" class="inputRow">
            <div class="labelColumn">
                <%: Html.LabelFor(model => model.Note) %>
            </div>
            <div class="inputColumn">
                <%: Html.TextBoxFor(model => model.Note) %>
            </div>
            <div class="errorColumn">
                <%: Html.ValidationMessageFor(model => model.Note) %>
            </div>
        </div>
        <div id="field_for_annote" class="inputRow">
            <div class="labelColumn">
                <%: Html.LabelFor(model => model.Annote) %>
            </div>
            <div class="inputColumn">
                <%: Html.TextBoxFor(model => model.Annote) %>
            </div>
            <div class="errorColumn">
                <%: Html.ValidationMessageFor(model => model.Annote) %>
            </div>
        </div>
        <div id="field_for_abstract" class="inputRow">
            <div class="labelColumn">
                <%: Html.LabelFor(model => model.Abstract) %>
            </div>
            <div class="inputColumn">
                <%: Html.TextBoxFor(model => model.Abstract) %>
            </div>
            <div class="errorColumn">
                <%: Html.ValidationMessageFor(model => model.Abstract) %>
            </div>
        </div>
    </fieldset>
    <div id="hidingPlace" style="visibility: hidden; height: 0px;">
        <div id="field_for_address" class="inputRow">
            <div class="labelColumn">
                <%: Html.LabelFor(model => model.Address) %>
            </div>
            <div class="inputColumn">
                <%: Html.TextBoxFor(model => model.Address) %>
            </div>
            <div class="errorColumn">
                <%: Html.ValidationMessageFor(model => model.Address) %>
            </div>
        </div>
        <div id="field_for_booktitle" class="inputRow">
            <div class="labelColumn">
                <%: Html.LabelFor(model => model.Booktitle) %>
            </div>
            <div class="inputColumn">
                <%: Html.TextBoxFor(model => model.Booktitle) %>
            </div>
            <div class="errorColumn">
                <%: Html.ValidationMessageFor(model => model.Booktitle) %>
            </div>
        </div>
        <div id="field_for_chapter" class="inputRow">
            <div class="labelColumn">
                <%: Html.LabelFor(model => model.Chapter) %>
            </div>
            <div class="inputColumn">
                <%: Html.TextBoxFor(model => model.Chapter) %>
            </div>
            <div class="errorColumn">
                <%: Html.ValidationMessageFor(model => model.Chapter) %>
            </div>
        </div>
        <div id="field_for_crossref" class="inputRow">
            <div class="labelColumn">
                <%: Html.LabelFor(model => model.Crossref) %>
            </div>
            <div class="inputColumn">
                <%: Html.TextBoxFor(model => model.Crossref) %>
            </div>
            <div class="errorColumn">
                <%: Html.ValidationMessageFor(model => model.Crossref) %>
            </div>
        </div>
        <div id="field_for_edition" class="inputRow">
            <div class="labelColumn">
                <%: Html.LabelFor(model => model.Edition) %>
            </div>
            <div class="inputColumn">
                <%: Html.TextBoxFor(model => model.Edition) %>
            </div>
            <div class="errorColumn">
                <%: Html.ValidationMessageFor(model => model.Edition) %>
            </div>
        </div>
        <div id="field_for_editors" class="inputRow">
            <div class="labelColumn">
                <%: Html.LabelFor(model => model.Editors) %>
            </div>
            <div class="inputColumn">
                <%: Html.TextBoxFor(model => model.Editors) %>
            </div>
            <div class="errorColumn">
                <%: Html.ValidationMessageFor(model => model.Editors) %>
            </div>
        </div>
        <div id="field_for_howpublished" class="inputRow">
            <div class="labelColumn">
                <%: Html.LabelFor(model => model.Howpublished) %>
            </div>
            <div class="inputColumn">
                <%: Html.TextBoxFor(model => model.Howpublished) %>
            </div>
            <div class="errorColumn">
                <%: Html.ValidationMessageFor(model => model.Howpublished) %>
            </div>
        </div>
        <div id="field_for_institution" class="inputRow">
            <div class="labelColumn">
                <%: Html.LabelFor(model => model.Institution) %>
            </div>
            <div class="inputColumn">
                <%: Html.TextBoxFor(model => model.Institution) %>
            </div>
            <div class="errorColumn">
                <%: Html.ValidationMessageFor(model => model.Institution) %>
            </div>
        </div>
        <div id="field_for_organisation" class="inputRow">
            <div class="labelColumn">
                <%: Html.LabelFor(model => model.Organization) %>
            </div>
            <div class="inputColumn">
                <%: Html.TextBoxFor(model => model.Organization) %>
            </div>
            <div class="errorColumn">
                <%: Html.ValidationMessageFor(model => model.Organization) %>
            </div>
        </div>
        <div id="field_for_publisher" class="inputRow">
            <div class="labelColumn">
                <%: Html.LabelFor(model => model.Publisher) %>
            </div>
            <div class="inputColumn">
                <%: Html.TextBoxFor(model => model.Publisher) %>
            </div>
            <div class="errorColumn">
                <%: Html.ValidationMessageFor(model => model.Publisher) %>
            </div>
        </div>
        <div id="field_for_school" class="inputRow">
            <div class="labelColumn">
                <%: Html.LabelFor(model => model.School) %>
            </div>
            <div class="inputColumn">
                <%: Html.TextBoxFor(model => model.School) %>
            </div>
            <div class="errorColumn">
                <%: Html.ValidationMessageFor(model => model.School) %>
            </div>
        </div>
        <div id="field_for_series" class="inputRow">
            <div class="labelColumn">
                <%: Html.LabelFor(model => model.Series) %>
            </div>
            <div class="inputColumn">
                <%: Html.TextBoxFor(model => model.Series) %>
            </div>
            <div class="errorColumn">
                <%: Html.ValidationMessageFor(model => model.Series) %>
            </div>
        </div>
        <div id="field_for_type" class="inputRow">
            <div class="labelColumn">
                <%: Html.LabelFor(model => model.Type) %>
            </div>
            <div class="inputColumn">
                <%: Html.TextBoxFor(model => model.Type) %>
            </div>
            <div class="errorColumn">
                <%: Html.ValidationMessageFor(model => model.Type) %>
            </div>
        </div>
    </div>
    <p>
        <input type="submit" value="<%:amendOrCreate%>" />
        <% if (Model != null)
           {%>
        | <a href="/Entry/DeletePublication/<%:Model.Id%>">Delete this item</a>
        <%
            }%>
    </p>
    <% } %>
    <div>
        <%: Html.ActionLink("Back to List", "Index") %>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ScriptsContent" runat="server">
    <script src="../../Scripts/jquery-1.4.1-vsdoc.js" type="text/javascript"></script>
    <script language="javascript" type="text/javascript">
        function DoPageLoad() {
            ChangeEntryType();
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
    </script>
</asp:Content>
