<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<IList<string>>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Find Duplicates
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <h2>Find Duplicates</h2>
    <% 
        if (Model.Count > 0)
        {
%>
            <p>The following cite keys have duplicate entries assigned to them - please click on one of them to review them in turn.</p>
            <p>
            <%
            foreach (String ck in Model)
            {
                Writer.Write("<a href=\"/Entry/ReviewDuplicates?ck=" + ck + "\">" + ck + "</a><br/>");%>
            <%
            }%>
            </p><%
        }
        else
        {%>
            <p>There are no duplicates in the system. <%: Html.ActionLink("Return to list of all entries", "Index", "Entry") %></p>
     <% }%>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="ScriptsContent" runat="server">
</asp:Content>
