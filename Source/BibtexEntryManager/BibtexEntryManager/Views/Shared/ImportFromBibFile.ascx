<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<dynamic>" %>
<fieldset>
    <legend>Import entries (File Upload)</legend>
    <form id="Form1" runat="server" action="UploadFile">
    Please choose the file you'd like to upload. Ensure it is a valid .bib file before
    you upload it<br /><br />
    <asp:FileUpload ID="FileUploadControl" runat="server"></asp:FileUpload><br />
    <asp:Button ID="UploadButton" runat="server" Text="Click to upload" />
    </form>
</fieldset>
