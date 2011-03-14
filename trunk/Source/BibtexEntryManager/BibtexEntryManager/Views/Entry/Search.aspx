<%@ Page Title="" Language="C#" Inherits="System.Web.Mvc.ViewPage<System.Collections.Generic.IList<BibtexEntryManager.Models.EntryTypes.Publication>>"
    MasterPageFile="~/Views/Shared/Site.Master" %>

<asp:Content runat="server" ID="Title" ContentPlaceHolderID="TitleContent">
    Search Results</asp:Content>
<asp:Content runat="server" ID="Scripts" ContentPlaceHolderID="ScriptsContent">
    <script type="text/javascript">
        // To be included with ./Search.aspx
        function keysearch() {
            var service = new BibtexEntryManager.SearchResults();
            service.DoSearchRaw(document.getElementById("searchString").value, onSuccess, null, null);
        }

        function onSuccess(result) {
            document.getElementById("searchCount").innerHTML = (result != null) ? result.length + " result" + ((result.length != 1) ? "s" : "") : "";
            if (result.length > 0) {
                var s = "";
                for (var i = 0; i < result.length; i++) {
                    s += result[i];
                }
                document.getElementById("results").innerHTML = s;
                sorttable.makeSortable(document.getElementsByTagName("table")[0]);
            }
            else {
                document.getElementById("results").innerHTML = "<tr><td>There are no results.</td></tr>";
            }
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
        Typing here will perform an instant search across all fields in the database. <br />
        Wildcards '*' and '?' may be used in searches, for example, 'mat*' will return results which have the string 'mat' followed by any series of characters.<br />
        Instant search box:
        <input id="searchString" type="text" onkeyup="return keysearch()" value="<% Writer.Write(ViewData["searchterm"]); %>"/> <span id="searchCount">
        <%
            string res;
            if (Model != null)
            {
                if (Model.Count > 0)
                {
                    res = Model.Count + " result" + ((Model.Count > 1) ? "s" : "") + " for ";
                }
                else
                {
                    res = "No results for ";
                }
                res += ViewData["searchterm"];
            }
            else
            {
                res = "Enter a query";
            }
            Writer.Write(res);
        %></span></p>
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
        <tbody id="results">
            <%  if (Model != null)
                {
                    foreach (var p in Model)
                    {
                        Writer.WriteLine(p.ToHtmlTableRowWithLinks() + "\r\n            ");
                    }
                }
                else
                {
                    Writer.WriteLine("<tr><td>Enter a term to begin a search</td></tr>");
                }
            %>
        </tbody>
        <tfoot>
        </tfoot>
    </table>
</asp:Content>
