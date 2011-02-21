<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<IEnumerable<BibtexEntryManager.Models.EntryTypes.Publication>>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    Select Entries For Deletion
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <h2>Select Entries For Deletion</h2>
    
    <form action="/Entry/ConfirmDeleteEntries" method="post">
    <table class="sortable">
        <thead>
            <tr>
                <td>
                <%--<input name="selectAllCheckBox" type="checkbox" onchange="return ToggleSelectAll()" />--%>
                </td>
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
                   Writer.WriteLine(v.ToHtmlTableRowWithCheckBoxes());
               }%>
        </tbody>
    </table>
    <input type="submit" value="Submit" />
    </form>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ScriptsContent" runat="server">
<script type="text/javascript">
    function ToggleSelectAll() {
//        var selected = document.getElementById('selectAllCheckBox').value;
//        
//        var checkboxes = document.getElementsByTagName('input');
//        for (i = 0; i < checkboxes.length; i++) {
//            checkboxes[i].checked = selected;
//        }
        
    }
</script>
</asp:Content>
