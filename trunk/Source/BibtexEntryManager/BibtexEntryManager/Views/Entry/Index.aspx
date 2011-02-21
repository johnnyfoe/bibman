<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<IList<BibtexEntryManager.Models.EntryTypes.Publication>>" %>
<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    List of Entries
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <h2>List of Entries</h2>
        <%: ViewData["DeletionResult"] %>
    <p>
        <%: Html.ActionLink("Download All Entries (.bib)", "DownloadAll", "Entry")%> |
        <%: Html.ActionLink("View Deleted Entries", "DeletedEntries", "Entry") %> | 
        <%: Html.ActionLink("Delete Multiple Entries", "SelectEntriesForDeletion", "Entry") %></p>
    <table class="sortable">
    <thead>
        <tr>
            <td>
                Cite Key
            </td>
            <td>
                Entry Type
            </td>
            <td>
                Author 1
            </td>
            <td>
                Author 2
            </td>
            <td>
                Title
            </td>
            <td>
                Year
            </td>
        </tr>
        </thead>
        <tbody>
        <% foreach (var v in Model)
           {
               Writer.WriteLine(v.ToHtmlTableRowWithLinks());
           }%>
           </tbody>
    </table>
    <% if (Model.Count > 15)
       {%>
    
    <p>
        <%: Html.ActionLink("Download All Entries (.bib)", "DownloadAll", "Entry")%> |
        <%: Html.ActionLink("View Deleted Entries", "DeletedEntries", "Entry") %> | 
        <%: Html.ActionLink("Delete Multiple Entries", "SelectEntriesForDeletion", "Entry") %></p>
        <%
       }%>
</asp:Content>