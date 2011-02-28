<%@ Page Title="" Language="C#" Inherits="System.Web.Mvc.ViewPage" MasterPageFile="~/Views/Shared/Site.Master" %>
<asp:Content runat="server" ID="Title" ContentPlaceHolderID="TitleContent">Deletion Successful</asp:Content>
<asp:Content runat="server" ID="Scripts" ContentPlaceHolderID="ScriptsContent">
<script type="text/javascript">
    function DoPageLoad() {
        var t = setTimeout("RedirectToList()", 10000);
    }

    function RedirectToList() {
        window.location = "/Entry";
    }
</script>
</asp:Content>
<asp:Content runat="server" ID="Main" ContentPlaceHolderID="MainContent">
    <h2>Deletion successful</h2>
    <p>Continuing after 10 seconds or click <%: Html.ActionLink("here","Index","Entry") %> to continue immediately</p>
</asp:Content>
