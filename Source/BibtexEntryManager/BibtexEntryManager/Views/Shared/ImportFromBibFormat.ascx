<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<dynamic>" %>
<fieldset>
    <legend>Import entries (Direct Input)</legend>
    <form id="ImportEntriesForm" runat="server" action="ImportEntries">
    Please enter the entry (or entries) you'd like to upload into the field below, then click the button at the bottom of the page.<br />
    <br />
    <asp:TextBox TextMode="MultiLine" Width="80%" runat="server" ID="EntryTextBox"></asp:TextBox><br />
    <asp:Button ID="UploadViaTextboxButton" runat="server" Text="Click to upload" />
    </form>
</fieldset>
