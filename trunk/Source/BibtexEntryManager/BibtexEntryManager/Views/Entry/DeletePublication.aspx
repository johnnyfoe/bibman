<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<BibtexEntryManager.Models.EntryTypes.Publication>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	DeletePublication
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h2>Delete Publication</h2>
    <%if (Model != null)
      {%>
    <h3>Are you sure you want to delete this?</h3>
    <fieldset>
        <legend>Fields</legend>
        <div class="display-label">CiteKey</div>
        <div class="display-field"><%:Model.CiteKey%></div>
        
        <div class="display-label">Abstract</div>
        <div class="display-field"><%:Model.Abstract%></div>
        
        <div class="display-label">Address</div>
        <div class="display-field"><%:Model.Address%></div>
        
        <div class="display-label">Annote</div>
        <div class="display-field"><%:Model.Annote%></div>
        
        <div class="display-label">Authors</div>
        <div class="display-field"><%:Model.Authors%></div>
        
        <div class="display-label">Booktitle</div>
        <div class="display-field"><%:Model.Booktitle%></div>
        
        <div class="display-label">Chapter</div>
        <div class="display-field"><%:Model.Chapter%></div>
        
        <div class="display-label">Crossref</div>
        <div class="display-field"><%:Model.Crossref%></div>
        
        <div class="display-label">Edition</div>
        <div class="display-field"><%:Model.Edition%></div>
        
        <div class="display-label">Editors</div>
        <div class="display-field"><%:Model.Editors%></div>
        
        <div class="display-label">Howpublished</div>
        <div class="display-field"><%:Model.Howpublished%></div>
        
        <div class="display-label">Institution</div>
        <div class="display-field"><%:Model.Institution%></div>
        
        <div class="display-label">Journal</div>
        <div class="display-field"><%:Model.Journal%></div>
        
        <div class="display-label">TheKey</div>
        <div class="display-field"><%:Model.TheKey%></div>
        
        <div class="display-label">Month</div>
        <div class="display-field"><%:Model.Month%></div>
        
        <div class="display-label">Note</div>
        <div class="display-field"><%:Model.Note%></div>
        
        <div class="display-label">Number</div>
        <div class="display-field"><%:Model.Number%></div>
        
        <div class="display-label">Organization</div>
        <div class="display-field"><%:Model.Organization%></div>
        
        <div class="display-label">Pages</div>
        <div class="display-field"><%:Model.Pages%></div>
        
        <div class="display-label">Publisher</div>
        <div class="display-field"><%:Model.Publisher%></div>
        
        <div class="display-label">School</div>
        <div class="display-field"><%:Model.School%></div>
        
        <div class="display-label">Series</div>
        <div class="display-field"><%:Model.Series%></div>
        
        <div class="display-label">Title</div>
        <div class="display-field"><%:Model.Title%></div>
        
        <div class="display-label">Type</div>
        <div class="display-field"><%:Model.Type%></div>
        
        <div class="display-label">Volume</div>
        <div class="display-field"><%:Model.Volume%></div>
        
        <div class="display-label">Year</div>
        <div class="display-field"><%:Model.Year%></div>
        
    </fieldset>
        <p>
            <a href="/Entry/MarkAsDeletedResult/<%:Model.Id %>">Delete this item</a> | 
		    <%:Html.ActionLink("Cancel (back to list)", "Index")%>
        </p>
    <%
        
      }%>
      <%else
      {
          Writer.Write("There was a problem with the Publication ID supplied - please refresh the previous page and ensure that it was not out of date");
      }%><%:Html.ActionLink("Return to list of entries","Index","Entry") %>

</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="ScriptsContent" runat="server">
</asp:Content>

