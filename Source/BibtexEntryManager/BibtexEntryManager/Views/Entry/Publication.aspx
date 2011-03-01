<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<BibtexEntryManager.Models.EntryTypes.Publication>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    <% string amendOrCreate = (Model == null) ? "Create Entry" : "Amend Entry";%>
    <%: amendOrCreate %>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <% string amendOrCreate = (Model == null) ? "Create Entry" : "Amend Entry";%>
    <h2><%: amendOrCreate %></h2>
    <p id="notificationOfUpdate"></p>
    <form runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server">
            <Services>
                <asp:ServiceReference Path="/SearchResults.svc" />
            </Services>
        </asp:ScriptManager>
    </form>
    <% using (Html.BeginForm())
       {%>
       
    <%:Html.ValidationSummary(true)%>
    <p><%: ViewData["message"] %>
    <%if (Model != null && Model.DeletionTime != null){%>This entry is currently marked as deleted. To restore it, press 'Restore this item' below.<br /><br /><%} %>
        <input type="submit" value="<%:amendOrCreate%>" />
        <%
           if (Model != null)
           {
               if (Model.DeletionTime == null)
               {%>
        | <a href="/Entry/DeletePublication/<%:Model.Id%>">Delete this item</a>
        <%
               }
               else
               {%>
        | <a href="/Entry/RestorePublication/<%:Model.Id%>">Restore this item</a>
        <%
}
           }%>
    </p>
    <div id="selectType" class="inputRow">
        <div class="labelColumn">
            <%:Html.LabelFor(model => model.EntryType)%>
        </div>
        <div class="inputColumn">
            <%:Html.HiddenFor(model => model.EntryType)%>
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
                <%:Html.LabelFor(model => model.CiteKey)%>
            </div>
            <div class="inputColumn">
                <%:Html.TextBoxFor(model => model.CiteKey)%>
            </div>
            <div class="errorColumn">
                <%:Html.ValidationMessageFor(model => model.CiteKey)%>
            </div>
        </div>
        <div id="field_for_authors" class="inputRow">
            <div class="labelColumn">
                <%:Html.LabelFor(model => model.Authors)%>
            </div>
            <div class="inputColumn">
                <%:Html.TextBoxFor(model => model.Authors)%>
            </div>
            <div class="errorColumn">
                <%:Html.ValidationMessageFor(model => model.Authors)%>
            </div>
        </div>
        <div id="field_for_title" class="inputRow">
            <div class="labelColumn">
                <%:Html.LabelFor(model => model.Title)%>
            </div>
            <div class="inputColumn">
                <%:Html.TextBoxFor(model => model.Title)%>
            </div>
            <div class="errorColumn">
                <%:Html.ValidationMessageFor(model => model.Title)%>
            </div>
        </div>
        <div id="field_for_journal" class="inputRow">
            <div class="labelColumn">
                <%:Html.LabelFor(model => model.Journal)%>
            </div>
            <div class="inputColumn">
                <%:Html.TextBoxFor(model => model.Journal)%>
            </div>
            <div class="errorColumn">
                <%:Html.ValidationMessageFor(model => model.Journal)%>
            </div>
        </div>
        <div id="field_for_year" class="inputRow">
            <div class="labelColumn">
                <%:Html.LabelFor(model => model.Year)%>
            </div>
            <div class="inputColumn">
                <%:Html.TextBoxFor(model => model.Year)%>
            </div>
            <div class="errorColumn">
                <%:Html.ValidationMessageFor(model => model.Year)%>
            </div>
        </div>
    </fieldset>
    <fieldset id="optional">
        <legend>Optional Fields</legend>
        <div id="field_for_thekey" class="inputRow">
            <div class="labelColumn">
                <%:Html.LabelFor(model => model.TheKey)%>
            </div>
            <div class="inputColumn">
                <%:Html.TextBoxFor(model => model.TheKey)%>
            </div>
            <div class="errorColumn">
                <%:Html.ValidationMessageFor(model => model.TheKey)%>
            </div>
        </div>
        <div id="field_for_volume" class="inputRow">
            <div class="labelColumn">
                <%:Html.LabelFor(model => model.Volume)%>
            </div>
            <div class="inputColumn">
                <%:Html.TextBoxFor(model => model.Volume)%>
            </div>
            <div class="errorColumn">
                <%:Html.ValidationMessageFor(model => model.Volume)%>
            </div>
        </div>
        <div id="field_for_number" class="inputRow">
            <div class="labelColumn">
                <%:Html.LabelFor(model => model.Number)%>
            </div>
            <div class="inputColumn">
                <%:Html.TextBoxFor(model => model.Number)%>
            </div>
            <div class="errorColumn">
                <%:Html.ValidationMessageFor(model => model.Number)%>
            </div>
        </div>
        <div id="field_for_pages" class="inputRow">
            <div class="labelColumn">
                <%:Html.LabelFor(model => model.Pages)%>
            </div>
            <div class="inputColumn">
                <%:Html.TextBoxFor(model => model.Pages)%>
            </div>
            <div class="errorColumn">
                <%:Html.ValidationMessageFor(model => model.Pages)%>
            </div>
        </div>
        <div id="field_for_month" class="inputRow">
            <div class="labelColumn">
                <%:Html.LabelFor(model => model.Month)%>
            </div>
            <div class="inputColumn">
                <%:Html.TextBoxFor(model => model.Month)%>
            </div>
            <div class="errorColumn">
                <%:Html.ValidationMessageFor(model => model.Month)%>
            </div>
        </div>
        <div id="field_for_note" class="inputRow">
            <div class="labelColumn">
                <%:Html.LabelFor(model => model.Note)%>
            </div>
            <div class="inputColumn">
                <%:Html.TextBoxFor(model => model.Note)%>
            </div>
            <div class="errorColumn">
                <%:Html.ValidationMessageFor(model => model.Note)%>
            </div>
        </div>
        <div id="field_for_annote" class="inputRow">
            <div class="labelColumn">
                <%:Html.LabelFor(model => model.Annote)%>
            </div>
            <div class="inputColumn">
                <%:Html.TextBoxFor(model => model.Annote)%>
            </div>
            <div class="errorColumn">
                <%:Html.ValidationMessageFor(model => model.Annote)%>
            </div>
        </div>
        <div id="field_for_abstract" class="inputRow">
            <div class="labelColumn">
                <%:Html.LabelFor(model => model.Abstract)%>
            </div>
            <div class="inputColumn">
                <%:Html.TextBoxFor(model => model.Abstract)%>
            </div>
            <div class="errorColumn">
                <%:Html.ValidationMessageFor(model => model.Abstract)%>
            </div>
        </div>
    </fieldset>
    <div id="hidingPlace" style="visibility: hidden; height: 0px;">
        <div id="field_for_address" class="inputRow">
            <div class="labelColumn">
                <%:Html.LabelFor(model => model.Address)%>
            </div>
            <div class="inputColumn">
                <%:Html.TextBoxFor(model => model.Address)%>
            </div>
            <div class="errorColumn">
                <%:Html.ValidationMessageFor(model => model.Address)%>
            </div>
        </div>
        <div id="field_for_booktitle" class="inputRow">
            <div class="labelColumn">
                <%:Html.LabelFor(model => model.Booktitle)%>
            </div>
            <div class="inputColumn">
                <%:Html.TextBoxFor(model => model.Booktitle)%>
            </div>
            <div class="errorColumn">
                <%:Html.ValidationMessageFor(model => model.Booktitle)%>
            </div>
        </div>
        <div id="field_for_chapter" class="inputRow">
            <div class="labelColumn">
                <%:Html.LabelFor(model => model.Chapter)%>
            </div>
            <div class="inputColumn">
                <%:Html.TextBoxFor(model => model.Chapter)%>
            </div>
            <div class="errorColumn">
                <%:Html.ValidationMessageFor(model => model.Chapter)%>
            </div>
        </div>
        <div id="field_for_crossref" class="inputRow">
            <div class="labelColumn">
                <%:Html.LabelFor(model => model.Crossref)%>
            </div>
            <div class="inputColumn">
                <%:Html.TextBoxFor(model => model.Crossref)%>
            </div>
            <div class="errorColumn">
                <%:Html.ValidationMessageFor(model => model.Crossref)%>
            </div>
        </div>
        <div id="field_for_edition" class="inputRow">
            <div class="labelColumn">
                <%:Html.LabelFor(model => model.Edition)%>
            </div>
            <div class="inputColumn">
                <%:Html.TextBoxFor(model => model.Edition)%>
            </div>
            <div class="errorColumn">
                <%:Html.ValidationMessageFor(model => model.Edition)%>
            </div>
        </div>
        <div id="field_for_editors" class="inputRow">
            <div class="labelColumn">
                <%:Html.LabelFor(model => model.Editors)%>
            </div>
            <div class="inputColumn">
                <%:Html.TextBoxFor(model => model.Editors)%>
            </div>
            <div class="errorColumn">
                <%:Html.ValidationMessageFor(model => model.Editors)%>
            </div>
        </div>
        <div id="field_for_howpublished" class="inputRow">
            <div class="labelColumn">
                <%:Html.LabelFor(model => model.Howpublished)%>
            </div>
            <div class="inputColumn">
                <%:Html.TextBoxFor(model => model.Howpublished)%>
            </div>
            <div class="errorColumn">
                <%:Html.ValidationMessageFor(model => model.Howpublished)%>
            </div>
        </div>
        <div id="field_for_institution" class="inputRow">
            <div class="labelColumn">
                <%:Html.LabelFor(model => model.Institution)%>
            </div>
            <div class="inputColumn">
                <%:Html.TextBoxFor(model => model.Institution)%>
            </div>
            <div class="errorColumn">
                <%:Html.ValidationMessageFor(model => model.Institution)%>
            </div>
        </div>
        <div id="field_for_organisation" class="inputRow">
            <div class="labelColumn">
                <%:Html.LabelFor(model => model.Organization)%>
            </div>
            <div class="inputColumn">
                <%:Html.TextBoxFor(model => model.Organization)%>
            </div>
            <div class="errorColumn">
                <%:Html.ValidationMessageFor(model => model.Organization)%>
            </div>
        </div>
        <div id="field_for_publisher" class="inputRow">
            <div class="labelColumn">
                <%:Html.LabelFor(model => model.Publisher)%>
            </div>
            <div class="inputColumn">
                <%:Html.TextBoxFor(model => model.Publisher)%>
            </div>
            <div class="errorColumn">
                <%:Html.ValidationMessageFor(model => model.Publisher)%>
            </div>
        </div>
        <div id="field_for_school" class="inputRow">
            <div class="labelColumn">
                <%:Html.LabelFor(model => model.School)%>
            </div>
            <div class="inputColumn">
                <%:Html.TextBoxFor(model => model.School)%>
            </div>
            <div class="errorColumn">
                <%:Html.ValidationMessageFor(model => model.School)%>
            </div>
        </div>
        <div id="field_for_series" class="inputRow">
            <div class="labelColumn">
                <%:Html.LabelFor(model => model.Series)%>
            </div>
            <div class="inputColumn">
                <%:Html.TextBoxFor(model => model.Series)%>
            </div>
            <div class="errorColumn">
                <%:Html.ValidationMessageFor(model => model.Series)%>
            </div>
        </div>
        <div id="field_for_type" class="inputRow">
            <div class="labelColumn">
                <%:Html.LabelFor(model => model.Type)%>
            </div>
            <div class="inputColumn">
                <%:Html.TextBoxFor(model => model.Type)%>
            </div>
            <div class="errorColumn">
                <%:Html.ValidationMessageFor(model => model.Type)%>
            </div>
        </div>
    </div>
    <p>
        <input type="submit" value="<%:amendOrCreate%>" />
        <%
           if (Model != null)
           {
               if (Model.DeletionTime == null)
               {%>
        | <a href="/Entry/DeletePublication/<%:Model.Id%>">Delete this item</a>
        <%
               }
               else
               {%>
        | <a href="/Entry/RestorePublication/<%:Model.Id%>">Restore this item</a>
        <%
}
           }%>
    </p>
    <input type="hidden" id="PageCreationTime" value="<%: DateTime.Now %>" />
    <%
       }%>
    <div>
        <%: Html.ActionLink("Back to List", "Index") %>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ScriptsContent" runat="server">
    <script src="../../Scripts/ChangeEntryType.js" type="text/javascript"></script>
    <script type="text/javascript">
        function ItemId() { 
            return <%:(Model != null) ? Model.Id : -1 %>;
        }
    </script>
</asp:Content>
