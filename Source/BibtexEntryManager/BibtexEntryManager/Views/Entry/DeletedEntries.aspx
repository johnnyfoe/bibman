<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<IList<BibtexEntryManager.Models.EntryTypes.Publication>>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    List of Deleted Entries
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ScriptsContent" runat="server">
    <script language="javascript" type="text/javascript">
        function purgeAll() {
            var answer = confirm("This will purge ALL deleted entries from the database. Press OK to continue or Cancel to abort purging");
            if (answer) {
                window.location = "/Entry/PurgeAll";
            }
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <h2>List of Deleted Entries</h2>
    <p>Old entries are entries which have been deleted for longer than 60 seconds (evaluation time - full system would allow adjustment of this time)<br />
    To restore an entry (undelete it) click on its cite key<br /><br />
        <%: Html.ActionLink("Permenantly remove old entries", "CleanupDeletions", "Entry")%> |
        <a href="javascript:purgeAll()">Permenantly remove ALL deleted entries</a>
    </p>
    <table class="sortable">
        <thead>
            <tr title="Click to sort">
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
    <% if (Model.Count > 20)
       {
    %>
    <p>
        <%: Html.ActionLink("Permenantly remove old entries", "CleanupDeletions", "Entry")%> |
        <a href="javascript:purgeAll()">Permenantly remove ALL deleted entries</a>
    </p>
    <%
        } %>
</asp:Content>
