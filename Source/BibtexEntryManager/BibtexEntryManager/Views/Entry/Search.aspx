<%@ Page Title="" Language="C#" Inherits="System.Web.Mvc.ViewPage<System.Collections.Generic.IList<BibtexEntryManager.Models.EntryTypes.Publication>>"
    MasterPageFile="~/Views/Shared/Site.Master" %>

<asp:Content runat="server" ID="Title" ContentPlaceHolderID="TitleContent">
    Search Results</asp:Content>
<asp:Content runat="server" ID="Scripts" ContentPlaceHolderID="ScriptsContent">
    <script type="text/javascript">
        // To be included with /Entry/Search.aspx
        function keysearch() {
            var service = new BibtexEntryManager.SearchResults();
            service.DoSearch(document.getElementById("searchString").value, onSuccess, null, null);
        }

        function onSuccess(result) {
            document.getElementById("results").innerHTML = result;
            sorttable.makeSortable(document.getElementsByTagName("table")[0]);
        }</script>
</asp:Content>
<asp:Content runat="server" ID="Main" ContentPlaceHolderID="MainContent">
    <form id="searchform" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
        <Services>
            <asp:ServiceReference Path="/SearchResults.svc" />
        </Services>
    </asp:ScriptManager>
    </form>
    <p>
        Instant search box:
        <input id="searchString" type="text" onkeypress="return keysearch()" />
    </p>
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
        <tbody id="results">
            <%  if (Model != null)
                {
                    foreach (var p in Model)
                    {
                        Writer.WriteLine(p.ToHtmlTableRowWithLinks() + "\r\n            ");
                    }
                }
            %>
        </tbody>
        <tfoot>
        </tfoot>
    </table>
</asp:Content>
