<%@ Page Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    Home Page
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <h2>Welcome to the Bibtex Entry Manager</h2>
    <noscript>The site requires that a javascript-enabled browser is used - please turn on javascript.</noscript>
    <%if (!Request.IsAuthenticated)
      {%><p>
          This is the home page for the bibtex entry manager. To begin using the site, please
          <%:Html.ActionLink("Log in if you have an existing account", "LogOn", "Account")%>
          or
          <%:Html.ActionLink("Register a new account", "Register", "Account")%></p>
    <%
        }
      else
      {
    %><p>You are logged in.</p>
    <%
        }%>
    <p>
        Links to the development information on the project can be found at the repository
        on <a href="http://bibman.sourceforge.net">sourceforge</a>.
    </p>
</asp:Content>
