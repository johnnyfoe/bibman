<%@ Page Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage" %>

<asp:Content ID="aboutTitle" ContentPlaceHolderID="TitleContent" runat="server">
    About
</asp:Content>

<asp:Content ID="aboutContent" ContentPlaceHolderID="MainContent" runat="server">
    <h2>About</h2>
    <p>
        Content can be put here about the project, links to more information about TeX, LaTeX &amp; Bibtex.
    </p>
    <br /><br /><br /><br /><br />
    Clicking this link will delete all entries from the system with no warning.<br />
    It is included to assist clearing down of the database when 
    marking and would not be included typically<br />
    <%: Html.ActionLink("Delete ALL entries","DeleteAllEntries","Entry") %>
</asp:Content>
