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
    <fieldset>
        <legend>Fields</legend>
        <div class="inputRow">
            <div class="labelColumn">
                <%: Html.LabelFor(model => model.EntryType) %>
            </div>
            <div class="inputColumn">
                <%  DropDownList ddl = new DropDownList();
                    var itemValues = Enum.GetValues(typeof(Entry));
                    string[] itemNames = Enum.GetNames(typeof(Entry));

                    for (int i = 0; i < itemNames.Length; i++)
                    {
                        ddl.Items.Add(new ListItem(itemNames[i], itemValues.GetValue(i).ToString()));
                    }
                    ddl.RenderControl(Writer);
                %>
            </div>
        </div>
        <div class="inputRow">
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
        <div class="inputRow">
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
        <div class="inputRow">
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
        <div class="inputRow">
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
        <div class="inputRow">
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
        <div class="inputRow">
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
        <div class="inputRow">
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
        <div class="inputRow">
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
        <div class="inputRow">
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
        <div class="inputRow">
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
        <div class="inputRow">
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
        <div class="inputRow">
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
        <div class="inputRow">
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
        <div class="inputRow">
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
        <div class="inputRow">
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
        <div class="inputRow">
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
        <div class="inputRow">
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
        <div class="inputRow">
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
        <div class="inputRow">
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
        <div class="inputRow">
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
        <div class="inputRow">
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
        <div class="inputRow">
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
        <div class="inputRow">
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
        <div class="inputRow">
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
        <div class="inputRow">
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
        <div class="inputRow">
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
        <p>
            <input type="submit" value="<%:amendOrCreate%>" />
            <% if (Model != null)
               {%>
            | <a href="/Entry/DeletePublication/<%:Model.Id%>">Delete this item</a>
            <%
                }%>
        </p>
    </fieldset>
    <% } %>
    <div>
        <%: Html.ActionLink("Back to List", "Index") %>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ScriptsContent" runat="server">
</asp:Content>
