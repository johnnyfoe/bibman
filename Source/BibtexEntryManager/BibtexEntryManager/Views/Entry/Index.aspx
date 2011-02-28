<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<IList<BibtexEntryManager.Models.EntryTypes.Publication>>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    List of Entries
</asp:Content>
<asp:Content runat="server" ID="Scripts" ContentPlaceHolderID="ScriptsContent">
    <script src="../../Scripts/IndexAjaxManager.js" type="text/javascript"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <h2>
        List of Entries
    </h2>
    <p>To modify or delete an existing entry, click the cite key. <br />
    Items that have been added or deleted since the page was loaded will appear and be highlighted automatically</p>
    <form id="form1" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server">
            <Services>
                <asp:ServiceReference Path="/SearchResults.svc" />
            </Services>
        </asp:ScriptManager>
    <%: ViewData["DeletionResult"] %>
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
    <input type="hidden" id="PageCreationTime" value="<%: DateTime.Now %>" />
    </form>
</asp:Content>
