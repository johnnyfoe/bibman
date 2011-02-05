<%@ Page Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    Home Page
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <h2>
        Welcome to the Bibtex Entry Manager</h2>
    <p>
        This is the home page for the bibtex entry manager. Links to the development information
        on the project can be found at the repository on <a href="http://bibman.sourceforge.net">
            sourceforge</a> and also at the feature tracking software hosted (on a temporary
        server) <a href="http://foe.doesntexist.com/jbugtracker">here</a>.
    </p>
</asp:Content>
