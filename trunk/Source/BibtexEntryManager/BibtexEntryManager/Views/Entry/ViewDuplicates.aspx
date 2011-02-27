<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<IList<string>>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Find Duplicates
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <h2>Find Duplicates</h2>
    <% 
        if (Model.Count > 0)
        {
            foreach (String ck in Model)
            {
%>
<p><%
                Writer.Write("<a href=\"/Entry/ReviewDuplicates?ck=" + ck + "\">" + ck + "</a>");%></p>
<%
            }
        }
        else
        {%>
            <p>Congratulations - you have no duplicates!</p>
     <% }%>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="ScriptsContent" runat="server">
</asp:Content>
