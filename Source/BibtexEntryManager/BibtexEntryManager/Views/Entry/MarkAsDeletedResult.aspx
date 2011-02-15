<%@ Page Title="" Language="C#" Inherits="System.Web.Mvc.ViewPage" MasterPageFile="~/Views/Shared/Site.master" %>
<asp:Content runat="server" ID="Title" ContentPlaceHolderID="TitleContent">Deletion <%:ViewData["message"]%></asp:Content>
<asp:Content runat="server" ID="Scripts" ContentPlaceHolderID="ScriptsContent"></asp:Content>
<asp:Content runat="server" ID="Main" ContentPlaceHolderID="MainContent">
The deletion of the entry was a <%:ViewData["message"] %>.
<%
    object exception;
    if (ViewData.TryGetValue("exception", out exception))
        Writer.Write((string)exception);%>
</asp:Content>
