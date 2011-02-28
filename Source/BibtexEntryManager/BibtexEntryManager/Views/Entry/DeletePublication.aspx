<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<BibtexEntryManager.Models.EntryTypes.Publication>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    Delete Publication
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <h2>
        Delete Publication</h2>
    <%if (Model != null)
      {%>
    <h3>Are you sure you want to delete this entry?</h3>
    <form runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
        <Services>
            <asp:ServiceReference Path="/SearchResults.svc" />
        </Services>
    </asp:ScriptManager>
    </form>
        <a href="javascript:DeletePublication(<%:Model.Id %>)">Yes (Delete)</a> |
        <%:Html.ActionLink("No (back to list)", "Index")%>
    <fieldset>
        <legend>Fields</legend>
        <div class="inputRow">
            <div class="labelColumn">
                CiteKey</div>
            <div class="inputColumn">
                <%:Model.CiteKey%></div>
        </div>
        <%
            if (Model.Abstract != null)
            {%>
        <div class="inputRow">
            <div class="labelColumn">
                Abstract</div>
            <div class="inputColumn">
                <%:Model.Abstract%></div>
        </div>
        <%
            }
          if (Model.Address != null)
          {%>
        <div class="inputRow">
            <div class="labelColumn">
                Address</div>
            <div class="inputColumn">
                <%:Model.Address%></div>
        </div>
        <%
            }
            if (Model.Annote != null)
            {%>
        <div class="inputRow">
            <div class="labelColumn">
                Annote</div>
            <div class="inputColumn">
                <%:Model.Annote%></div>
        </div>
        <%
            }
            if (Model.Authors != null)
            {%>
        <div class="inputRow">
            <div class="labelColumn">
                Authors</div>
            <div class="inputColumn">
                <%:Model.Authors%></div>
        </div>
        <%
            }
            if (Model.Booktitle != null)
            {%>
        <div class="inputRow">
            <div class="labelColumn">
                Booktitle</div>
            <div class="inputColumn">
                <%:Model.Booktitle%></div>
        </div>
        <%
            }
            if (Model.Chapter != null)
            {%>
        <div class="inputRow">
            <div class="labelColumn">
                Chapter</div>
            <div class="inputColumn">
                <%:Model.Chapter%></div>
        </div>
        <%
            }
            if (Model.Crossref != null)
            {%>
        <div class="inputRow">
            <div class="labelColumn">
                Crossref</div>
            <div class="inputColumn">
                <%:Model.Crossref%></div>
        </div>
        <%
            }
            if (Model.Edition != null)
            {%>
        <div class="inputRow">
            <div class="labelColumn">
                Edition</div>
            <div class="inputColumn">
                <%:Model.Edition%></div>
        </div>
        <%
            }
            if (Model.Editors != null)
            {%>
        <div class="inputRow">
            <div class="labelColumn">
                Editors</div>
            <div class="inputColumn">
                <%:Model.Editors%></div>
        </div>
        <%
            }
            if (Model.Howpublished != null)
            {%>
        <div class="inputRow">
            <div class="labelColumn">
                Howpublished</div>
            <div class="inputColumn">
                <%:Model.Howpublished%></div>
        </div>
        <%
            }
            if (Model.Institution != null)
            {%>
        <div class="inputRow">
            <div class="labelColumn">
                Institution</div>
            <div class="inputColumn">
                <%:Model.Institution%></div>
        </div>
        <%
            }
            if (Model.Journal != null)
            {%>
        <div class="inputRow">
            <div class="labelColumn">
                Journal</div>
            <div class="inputColumn">
                <%:Model.Journal%></div>
        </div>
        <%
            }
            if (Model.TheKey != null)
            {%>
        <div class="inputRow">
            <div class="labelColumn">
                Key</div>
            <div class="inputColumn">
                <%:Model.TheKey%></div>
        </div>
        <%
            }
            if (Model.Month != null)
            {%>
        <div class="inputRow">
            <div class="labelColumn">
                Month</div>
            <div class="inputColumn">
                <%:Model.Month%></div>
        </div>
        <%
            }
            if (Model.Note != null)
            {%>
        <div class="inputRow">
            <div class="labelColumn">
                Note</div>
            <div class="inputColumn">
                <%:Model.Note%></div>
        </div>
        <%
            }
            if (Model.Number != null)
            {%>
        <div class="inputRow">
            <div class="labelColumn">
                Number</div>
            <div class="inputColumn">
                <%:Model.Number%></div>
        </div>
        <%
            }
            if (Model.Organization != null)
            {%>
        <div class="inputRow">
            <div class="labelColumn">
                Organization</div>
            <div class="inputColumn">
                <%:Model.Organization%></div>
        </div>
        <%
            }
            if (Model.Pages != null)
            {%>
        <div class="inputRow">
            <div class="labelColumn">
                Pages</div>
            <div class="inputColumn">
                <%:Model.Pages%></div>
        </div>
        <%
            }
            if (Model.Publisher != null)
            {%>
        <div class="inputRow">
            <div class="labelColumn">
                Publisher</div>
            <div class="inputColumn">
                <%:Model.Publisher%></div>
        </div>
        <%
            }
            if (Model.School != null)
            {%>
        <div class="inputRow">
            <div class="labelColumn">
                School</div>
            <div class="inputColumn">
                <%:Model.School%></div>
        </div>
        <%
            }
            if (Model.Series != null)
            {%>
        <div class="inputRow">
            <div class="labelColumn">
                Series</div>
            <div class="inputColumn">
                <%:Model.Series%></div>
        </div>
        <%
            }
            if (Model.Title != null)
            {%>
        <div class="inputRow">
            <div class="labelColumn">
                Title</div>
            <div class="inputColumn">
                <%:Model.Title%></div>
        </div>
        <%
            }
            if (Model.Type != null)
            {%>
        <div class="inputRow">
            <div class="labelColumn">
                Type</div>
            <div class="inputColumn">
                <%:Model.Type%></div>
        </div>
        <%
            }
            if (Model.Volume != null)
            {%>
        <div class="inputRow">
            <div class="labelColumn">
                Volume</div>
            <div class="inputColumn">
                <%:Model.Volume%></div>
        </div>
        <%
            }
            if (Model.Year != null)
            {%>
        <div class="inputRow">
            <div class="labelColumn">
                Year</div>
            <div class="inputColumn">
                <%:Model.Year%></div>
        </div>
        <% } %>
    </fieldset>
    <p>
        <a href="javascript:DeletePublication(<%:Model.Id %>)">Yes (Delete)</a> |
        <%:Html.ActionLink("No (back to list)", "Index")%>
    </p>
    <%
        
        }%>
    <%else
        {
            Writer.Write("There was a problem with the Publication ID supplied - please refresh the previous page and ensure that it was not out of date");
        }%>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ScriptsContent" runat="server">
    <script src="../../Scripts/AjaxDeletePublication.js" type="text/javascript"></script>
    <script type="text/javascript">
        function deletionSuccess() {
            alert("Deleted item successfully");
            window.location = "/Entry";
        }
    </script>
</asp:Content>
