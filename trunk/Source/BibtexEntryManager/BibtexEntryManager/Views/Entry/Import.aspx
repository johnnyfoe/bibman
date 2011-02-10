<%@ Page Title="" Language="C#" Inherits="System.Web.Mvc.ViewPage<System.Web.HttpPostedFile>"
    MasterPageFile="~/Views/Shared/Site.Master" %>

<asp:Content runat="server" ID="Title" ContentPlaceHolderID="TitleContent">
    Import Entries</asp:Content>
<asp:Content runat="server" ID="Main" ContentPlaceHolderID="MainContent">
    <h2>Import Entries</h2>
    <% Html.RenderPartial("ImportFromBibFile"); %>
    <% Html.RenderPartial("ImportFromBibFormat"); %>
</asp:Content>
