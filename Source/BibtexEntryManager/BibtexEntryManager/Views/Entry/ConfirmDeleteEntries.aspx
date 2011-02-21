<%@ Page Title="" Language="C#" Inherits="System.Web.Mvc.ViewPage<System.Collections.Generic.IList<BibtexEntryManager.Models.EntryTypes.Publication>>"
    MasterPageFile="~/Views/Shared/Site.master" %>

<asp:Content runat="server" ID="Title" ContentPlaceHolderID="TitleContent">
</asp:Content>
<asp:Content runat="server" ID="Scripts" ContentPlaceHolderID="ScriptsContent">
</asp:Content>
<asp:Content runat="server" ID="Main" ContentPlaceHolderID="MainContent">
    <h2>
        List of Entries</h2>
    <form id="confirmDeleteForm" method="post" runat="server">
    <p>
        Are you sure you want to delete the entries below? <input type="submit" value="Yes" /> |
        <%:Html.ActionLink("No - Cancel", "Index", "Entry")%></p>
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
               }
            %>
        </tbody>
    </table>
    <% if (Model.Count > 5)
       {%>
    <p>Are you sure you want to delete the entries above? <input type="submit" value="Yes" />
        |
        <%:Html.ActionLink("No - Cancel", "Index", "Entry")%></p>
    <%
        }%>
    </form>
</asp:Content>
