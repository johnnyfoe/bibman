<%@ Page Title="" Language="C#" Inherits="System.Web.Mvc.ViewPage<System.Web.HttpPostedFile>"
    MasterPageFile="~/Views/Shared/Site.Master" %>

<asp:Content runat="server" ID="Title" ContentPlaceHolderID="TitleContent">
    File upload</asp:Content>
<asp:Content runat="server" ID="Main" ContentPlaceHolderID="MainContent">
    <h2>Upload a file</h2>
    <form runat="server" action="UploadFile">
    Please choose the file you'd like to upload. Ensure it is a valid .bib file before
    you upload it<br />
    <asp:FileUpload id="FileUploadControl" runat="server"></asp:FileUpload><br />
    <asp:Button ID="UploadButton" runat="server" Text="Click to upload" />
    </form>
</asp:Content>
