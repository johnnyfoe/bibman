<%@ Page Title="" Language="C#" Inherits="System.Web.Mvc.ViewPage<System.Collections.Generic.IList<BibtexEntryManager.Models.EntryTypes.Publication>>"
    MasterPageFile="~/Views/Shared/Site.master" %>

<asp:Content runat="server" ID="Title" ContentPlaceHolderID="TitleContent">
</asp:Content>
<asp:Content runat="server" ID="Scripts" ContentPlaceHolderID="ScriptsContent">
</asp:Content>
<asp:Content runat="server" ID="Main" ContentPlaceHolderID="MainContent">
<h2>
        List of Entries</h2>
    <p>
        <%:Html.ActionLink("Confirm Deletion", "DeleteEntries", "Entry", Model)%></p>
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
        <%:Html.ActionLink("Confirm Deletion", "DeleteEntries", "Entry") %>
        <%
       }%>
</asp:Content>
