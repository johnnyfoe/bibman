<%@ Page Title="" Language="C#" Inherits="System.Web.Mvc.ViewPage<System.Collections.Generic.IList<BibtexEntryManager.Models.EntryTypes.Publication>>"
    MasterPageFile="~/Views/Shared/Site.Master" %>

<%@ Import Namespace="BibtexEntryManager.Models.EntryTypes" %>
<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    Review Duplicates
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <form id="form1" runat="server">
    <h2>Review Duplicates
    </h2>
    <asp:ScriptManager ID="ScriptManager1" runat="server">
        <Services>
            <asp:ServiceReference Path="/SearchResults.svc" />
        </Services>
    </asp:ScriptManager>
    <%if (Model.Count > 1)
      {
    %>
    <div id="CompareTwoEntries">
        <%
            Publication pubOne = Model[0];
            Publication pubTwo = Model[1];
        %>
        <div id="FieldSetOne">
            <a href="javascript:DeletePublication(<%:pubOne.Id %>)">Delete left</a>
            <fieldset>
                <legend>Fields</legend>
                <div class="inputRow">
                    <div class="labelColumn">
                        CiteKey</div>
                    <div class="inputColumn">
                        <%:pubOne.CiteKey%></div>
                </div>
                <%
                    if (pubOne.Abstract != null)
                    {%>
                <div class="inputRow">
                    <div class="labelColumn">
                        Abstract</div>
                    <div class="inputColumn">
                        <%:pubOne.Abstract%></div>
                </div>
                <%
                    }
      if (pubOne.Address != null)
      {%>
                <div class="inputRow">
                    <div class="labelColumn">
                        Address</div>
                    <div class="inputColumn">
                        <%:pubOne.Address%></div>
                </div>
                <%
                    }
      if (pubOne.Annote != null)
      {%>
                <div class="inputRow">
                    <div class="labelColumn">
                        Annote</div>
                    <div class="inputColumn">
                        <%:pubOne.Annote%></div>
                </div>
                <%
                    }
      if (pubOne.Authors != null)
      {%>
                <div class="inputRow">
                    <div class="labelColumn">
                        Authors</div>
                    <div class="inputColumn">
                        <%:pubOne.Authors%></div>
                </div>
                <%
                    }
      if (pubOne.Booktitle != null)
      {%>
                <div class="inputRow">
                    <div class="labelColumn">
                        Booktitle</div>
                    <div class="inputColumn">
                        <%:pubOne.Booktitle%></div>
                </div>
                <%
                    }
      if (pubOne.Chapter != null)
      {%>
                <div class="inputRow">
                    <div class="labelColumn">
                        Chapter</div>
                    <div class="inputColumn">
                        <%:pubOne.Chapter%></div>
                </div>
                <%
                    }
      if (pubOne.Crossref != null)
      {%>
                <div class="inputRow">
                    <div class="labelColumn">
                        Crossref</div>
                    <div class="inputColumn">
                        <%:pubOne.Crossref%></div>
                </div>
                <%
                    }
      if (pubOne.Edition != null)
      {%>
                <div class="inputRow">
                    <div class="labelColumn">
                        Edition</div>
                    <div class="inputColumn">
                        <%:pubOne.Edition%></div>
                </div>
                <%
                    }
      if (pubOne.Editors != null)
      {%>
                <div class="inputRow">
                    <div class="labelColumn">
                        Editors</div>
                    <div class="inputColumn">
                        <%:pubOne.Editors%></div>
                </div>
                <%
                    }
      if (pubOne.Howpublished != null)
      {%>
                <div class="inputRow">
                    <div class="labelColumn">
                        Howpublished</div>
                    <div class="inputColumn">
                        <%:pubOne.Howpublished%></div>
                </div>
                <%
                    }
      if (pubOne.Institution != null)
      {%>
                <div class="inputRow">
                    <div class="labelColumn">
                        Institution</div>
                    <div class="inputColumn">
                        <%:pubOne.Institution%></div>
                </div>
                <%
                    }
      if (pubOne.Journal != null)
      {%>
                <div class="inputRow">
                    <div class="labelColumn">
                        Journal</div>
                    <div class="inputColumn">
                        <%:pubOne.Journal%></div>
                </div>
                <%
                    }
      if (pubOne.TheKey != null)
      {%>
                <div class="inputRow">
                    <div class="labelColumn">
                        Key</div>
                    <div class="inputColumn">
                        <%:pubOne.TheKey%></div>
                </div>
                <%
                    }
      if (pubOne.Month != null)
      {%>
                <div class="inputRow">
                    <div class="labelColumn">
                        Month</div>
                    <div class="inputColumn">
                        <%:pubOne.Month%></div>
                </div>
                <%
                    }
      if (pubOne.Note != null)
      {%>
                <div class="inputRow">
                    <div class="labelColumn">
                        Note</div>
                    <div class="inputColumn">
                        <%:pubOne.Note%></div>
                </div>
                <%
                    }
      if (pubOne.Number != null)
      {%>
                <div class="inputRow">
                    <div class="labelColumn">
                        Number</div>
                    <div class="inputColumn">
                        <%:pubOne.Number%></div>
                </div>
                <%
                    }
      if (pubOne.Organization != null)
      {%>
                <div class="inputRow">
                    <div class="labelColumn">
                        Organization</div>
                    <div class="inputColumn">
                        <%:pubOne.Organization%></div>
                </div>
                <%
                    }
      if (pubOne.Pages != null)
      {%>
                <div class="inputRow">
                    <div class="labelColumn">
                        Pages</div>
                    <div class="inputColumn">
                        <%:pubOne.Pages%></div>
                </div>
                <%
                    }
      if (pubOne.Publisher != null)
      {%>
                <div class="inputRow">
                    <div class="labelColumn">
                        Publisher</div>
                    <div class="inputColumn">
                        <%:pubOne.Publisher%></div>
                </div>
                <%
                    }
      if (pubOne.School != null)
      {%>
                <div class="inputRow">
                    <div class="labelColumn">
                        School</div>
                    <div class="inputColumn">
                        <%:pubOne.School%></div>
                </div>
                <%
                    }
      if (pubOne.Series != null)
      {%>
                <div class="inputRow">
                    <div class="labelColumn">
                        Series</div>
                    <div class="inputColumn">
                        <%:pubOne.Series%></div>
                </div>
                <%
                    }
      if (pubOne.Title != null)
      {%>
                <div class="inputRow">
                    <div class="labelColumn">
                        Title</div>
                    <div class="inputColumn">
                        <%:pubOne.Title%></div>
                </div>
                <%
                    }
      if (pubOne.Type != null)
      {%>
                <div class="inputRow">
                    <div class="labelColumn">
                        Type</div>
                    <div class="inputColumn">
                        <%:pubOne.Type%></div>
                </div>
                <%
                    }
      if (pubOne.Volume != null)
      {%>
                <div class="inputRow">
                    <div class="labelColumn">
                        Volume</div>
                    <div class="inputColumn">
                        <%:pubOne.Volume%></div>
                </div>
                <%
                    }
      if (pubOne.Year != null)
      {%>
                <div class="inputRow">
                    <div class="labelColumn">
                        Year</div>
                    <div class="inputColumn">
                        <%:pubOne.Year%></div>
                </div>
                <%
                    }%>
            </fieldset>
            <a href="javascript:DeletePublication(<%:pubOne.Id %>)">Delete left</a>
        </div>
        <div id="FieldSetTwo">
            <a href="javascript:DeletePublication(<%:pubTwo.Id %>)">Delete right</a>
            <fieldset>
            <legend>Fields</legend>
            <div class="inputRow">
                <div class="labelColumn">
                    CiteKey</div>
                <div class="inputColumn">
                    <%:pubTwo.CiteKey%></div>
            </div>
            <%
                if (pubTwo.Abstract != null)
                {%>
            <div class="inputRow">
                <div class="labelColumn">
                    Abstract</div>
                <div class="inputColumn">
                    <%:pubTwo.Abstract%></div>
            </div>
            <%
                }
      if (pubTwo.Address != null)
      {%>
            <div class="inputRow">
                <div class="labelColumn">
                    Address</div>
                <div class="inputColumn">
                    <%:pubTwo.Address%></div>
            </div>
            <%
                }
      if (pubTwo.Annote != null)
      {%>
            <div class="inputRow">
                <div class="labelColumn">
                    Annote</div>
                <div class="inputColumn">
                    <%:pubTwo.Annote%></div>
            </div>
            <%
                }
      if (pubTwo.Authors != null)
      {%>
            <div class="inputRow">
                <div class="labelColumn">
                    Authors</div>
                <div class="inputColumn">
                    <%:pubTwo.Authors%></div>
            </div>
            <%
                }
      if (pubTwo.Booktitle != null)
      {%>
            <div class="inputRow">
                <div class="labelColumn">
                    Booktitle</div>
                <div class="inputColumn">
                    <%:pubTwo.Booktitle%></div>
            </div>
            <%
                }
      if (pubTwo.Chapter != null)
      {%>
            <div class="inputRow">
                <div class="labelColumn">
                    Chapter</div>
                <div class="inputColumn">
                    <%:pubTwo.Chapter%></div>
            </div>
            <%
                }
      if (pubTwo.Crossref != null)
      {%>
            <div class="inputRow">
                <div class="labelColumn">
                    Crossref</div>
                <div class="inputColumn">
                    <%:pubTwo.Crossref%></div>
            </div>
            <%
                }
      if (pubTwo.Edition != null)
      {%>
            <div class="inputRow">
                <div class="labelColumn">
                    Edition</div>
                <div class="inputColumn">
                    <%:pubTwo.Edition%></div>
            </div>
            <%
                }
      if (pubTwo.Editors != null)
      {%>
            <div class="inputRow">
                <div class="labelColumn">
                    Editors</div>
                <div class="inputColumn">
                    <%:pubTwo.Editors%></div>
            </div>
            <%
                }
      if (pubTwo.Howpublished != null)
      {%>
            <div class="inputRow">
                <div class="labelColumn">
                    Howpublished</div>
                <div class="inputColumn">
                    <%:pubTwo.Howpublished%></div>
            </div>
            <%
                }
      if (pubTwo.Institution != null)
      {%>
            <div class="inputRow">
                <div class="labelColumn">
                    Institution</div>
                <div class="inputColumn">
                    <%:pubTwo.Institution%></div>
            </div>
            <%
                }
      if (pubTwo.Journal != null)
      {%>
            <div class="inputRow">
                <div class="labelColumn">
                    Journal</div>
                <div class="inputColumn">
                    <%:pubTwo.Journal%></div>
            </div>
            <%
                }
      if (pubTwo.TheKey != null)
      {%>
            <div class="inputRow">
                <div class="labelColumn">
                    Key</div>
                <div class="inputColumn">
                    <%:pubTwo.TheKey%></div>
            </div>
            <%
                }
      if (pubTwo.Month != null)
      {%>
            <div class="inputRow">
                <div class="labelColumn">
                    Month</div>
                <div class="inputColumn">
                    <%:pubTwo.Month%></div>
            </div>
            <%
                }
      if (pubTwo.Note != null)
      {%>
            <div class="inputRow">
                <div class="labelColumn">
                    Note</div>
                <div class="inputColumn">
                    <%:pubTwo.Note%></div>
            </div>
            <%
                }
      if (pubTwo.Number != null)
      {%>
            <div class="inputRow">
                <div class="labelColumn">
                    Number</div>
                <div class="inputColumn">
                    <%:pubTwo.Number%></div>
            </div>
            <%
                }
      if (pubTwo.Organization != null)
      {%>
            <div class="inputRow">
                <div class="labelColumn">
                    Organization</div>
                <div class="inputColumn">
                    <%:pubTwo.Organization%></div>
            </div>
            <%
                }
      if (pubTwo.Pages != null)
      {%>
            <div class="inputRow">
                <div class="labelColumn">
                    Pages</div>
                <div class="inputColumn">
                    <%:pubTwo.Pages%></div>
            </div>
            <%
                }
      if (pubTwo.Publisher != null)
      {%>
            <div class="inputRow">
                <div class="labelColumn">
                    Publisher</div>
                <div class="inputColumn">
                    <%:pubTwo.Publisher%></div>
            </div>
            <%
                }
      if (pubTwo.School != null)
      {%>
            <div class="inputRow">
                <div class="labelColumn">
                    School</div>
                <div class="inputColumn">
                    <%:pubTwo.School%></div>
            </div>
            <%
                }
      if (pubTwo.Series != null)
      {%>
            <div class="inputRow">
                <div class="labelColumn">
                    Series</div>
                <div class="inputColumn">
                    <%:pubTwo.Series%></div>
            </div>
            <%
                }
      if (pubTwo.Title != null)
      {%>
            <div class="inputRow">
                <div class="labelColumn">
                    Title</div>
                <div class="inputColumn">
                    <%:pubTwo.Title%></div>
            </div>
            <%
                }
      if (pubTwo.Type != null)
      {%>
            <div class="inputRow">
                <div class="labelColumn">
                    Type</div>
                <div class="inputColumn">
                    <%:pubTwo.Type%></div>
            </div>
            <%
                }
      if (pubTwo.Volume != null)
      {%>
            <div class="inputRow">
                <div class="labelColumn">
                    Volume</div>
                <div class="inputColumn">
                    <%:pubTwo.Volume%></div>
            </div>
            <%
                }
      if (pubTwo.Year != null)
      {%>
            <div class="inputRow">
                <div class="labelColumn">
                    Year</div>
                <div class="inputColumn">
                    <%:pubTwo.Year%></div>
            </div>
            <%
                }%>
        </fieldset>
            <a href="javascript:DeletePublication(<%:pubTwo.Id %>)">Delete right</a>
        </div>
    </div>
    <%}
      else
      {%>
    <p>There are no longer duplicates for cite key
        <%: Request.Params.Get("ck") %>! <%: Html.ActionLink("Return to view all duplicate cite keys","ViewDuplicates","Entry") %></p>
    <%
        }%>
    </form>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ScriptsContent" runat="server">
    <script src="../../Scripts/AjaxDeletePublication.js" type="text/javascript"></script>
    <script type="text/javascript">
        function deletionSuccess() {
            alert("Deleted item successfully");
            window.location.reload(true);
        }
    </script>
</asp:Content>
