<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Timeline.aspx.cs" Inherits="Modulos_CMS_Modulos_ModTimeline_Timeline" %>

<%@ Register Namespace="Controles.FCKeditor" TagPrefix="FCKEditor" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="utf-8">
    <meta http-equiv="X-UA-Compatible" content="IE=edge">
    <meta name="viewport" content="width=device-width, initial-scale=1">
    <link href="<%=ResolveUrl("~/CSS/bootstrap.min.css")%>" rel="stylesheet" />
    <link href="<%=ResolveUrl("~/CSS/style-manager.css")%>" rel="stylesheet" />
     <!--[if lt IE 9]>
        <script src="https://oss.maxcdn.com/libs/html5shiv/3.7.0/html5shiv.js"></script>
        <script src="https://oss.maxcdn.com/libs/respond.js/1.4.2/respond.min.js"></script>
    <![endif]-->
    <title></title>
    <script src="<%=ResolveUrl("~/JS/jquery-1.10.2.min.js")%>"></script>
    <script src="<%=ResolveUrl("~/JS/jquery.mask.js")%>"></script>
    <script>
        function refreshParent() {
            window.opener.location.href = window.opener.location.href;
            window.close();
        }

    </script>
</head>
<body class="modulos-cms">
    <div id="wrapper">
         <div id="page-wrapper">
            <div class="row">
                <div class="col-lg-12">
                    <h1 class="page-header">Timeline</h1>
                </div>
                <!-- /.col-lg-12 -->
            </div>
            <!-- /.row -->
            <div class="row">
                <div class="col-lg-12">
                    <div class="panel panel-default">
                        <div class="panel-heading">
                            Configuração do módulo
                        </div>
                        <div class="panel-body">
                            <div class="row">
                                <div class="col-lg-6">
                                    <form id="form1" runat="server" role="form">
                                        <div class="form-group">
                                            <label>Idioma:</label>
                                            <asp:DropDownList ID="ddlIdioma" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlIdioma_SelectedIndexChanged" CssClass="form-control"></asp:DropDownList>
                                        </div>    
                                        <div id="divUpload" runat="server">
                                            <label>Arquivo:</label>
                                            <asp:FileUpload ID="fupArquivo" runat="server" />
                                            <asp:RequiredFieldValidator ID="rfvArquivo" runat="server" ControlToValidate="fupArquivo" ValidationGroup="salvar" Text="*"></asp:RequiredFieldValidator>
                                            <asp:RegularExpressionValidator ID="rev" runat="server" ValidationExpression="^.*\.(jpg|JPG|jpeg|JPEG|png|PNG)$" ControlToValidate="fupArquivo" ErrorMessage="*"></asp:RegularExpressionValidator>

                                        </div>
                                        <div class="form-group">
                                            <label>Título:</label>
                                            <asp:TextBox ID="txtTitulo" runat="server" MaxLength="100" CssClass="form-control"></asp:TextBox><asp:RequiredFieldValidator ID="rfvTitulo" runat="server" ControlToValidate="txtTitulo" ValidationGroup="salvar" Text="*"></asp:RequiredFieldValidator>
                                        </div>
                                        <div class="form-group">
                                            <label>Ano:</label>
                                            <asp:TextBox ID="txtAno" runat="server" MaxLength="400" CssClass="form-control"></asp:TextBox><asp:RequiredFieldValidator ID="rfvAno" runat="server" ControlToValidate="txtAno" ValidationGroup="salvar" Text="*"></asp:RequiredFieldValidator>
                                        </div>
                                        <div class="form-group">
                                            <FCKEditor:FCKeditor ID="txtTexto" runat="server" BasePath="~/fckeditor/" DefaultLanguage="pt-BR" EnableSourceXHTML="false"
                                                EnableXHTML="false" Debug="false" EditorAreaCSS="form-control">
                                            </FCKEditor:FCKeditor>
                                        </div>
                                        <div class="form-group">
                                            <asp:Button ID="btnSalvar" runat="server" Text="Adicionar" OnClick="btnSalvar_Click" ValidationGroup="salvar" CssClass="btn btn-default btn-bra" />
                                            <asp:Button ID="btnFechar" runat="server" Text="Fechar" OnClick="btnFechar_Click" CssClass="btn btn-default btn-bra" />

                                        </div>

                                        <div class="form-group">
                                            <asp:Label ID="lblMensagem" runat="server"></asp:Label>
                                        </div>

                                        
                                        <asp:DataGrid summary="Lista de Timelines" ID="grdDados" runat="server" BorderWidth="0" BorderColor="Transparent" AutoGenerateColumns="false" CssClass="table table-striped table-bra-cms"
                                            AlternatingRowStyle-CssClass="par" Width="100%" OnItemCommand="grdDados_ItemCommand">
                                            <Columns>
                                                <asp:TemplateColumn HeaderText="Imagem" HeaderStyle-CssClass="tabelaHeader" ItemStyle-HorizontalAlign="Center">
                                                    <ItemTemplate>
                                                        <img src="<%# String.Format("{0}/{1}/{2}",ConfigurationManager.AppSettings["BradescoRI.Path.Imagens.ModTimeline"], Request.QueryString["conteudoId"] ,Eval("Imagem"))%>" style="width: 35px; height: 35px" /></a>
                                                    </ItemTemplate>
                                                    <ItemStyle />
                                                </asp:TemplateColumn>
                                                <asp:BoundColumn Visible="false" DataField="Id" HeaderText="Id" HeaderStyle-CssClass="tabelaHeader" />
                                                <asp:BoundColumn DataField="Imagem" Visible="false" />
                                                <asp:BoundColumn DataField="Titulo" HeaderText="Titulo" HeaderStyle-CssClass="tabelaHeader" />
                                                <asp:BoundColumn DataField="Texto" HeaderText="Texto" HeaderStyle-CssClass="tabelaHeader" />
                                                <asp:BoundColumn DataField="Ano" HeaderText="Ano" HeaderStyle-CssClass="tabelaHeader" />
                                                <asp:TemplateColumn HeaderText="Excluir" HeaderStyle-CssClass="tabelaHeader" ItemStyle-Width="5%" ItemStyle-HorizontalAlign="Center">
                                                    <ItemTemplate>
                                                        <asp:ImageButton ID="imgExcluir" runat="server" AlternateText="Excluir" CommandName="excluir" CommandArgument='<%#Eval("Id")%>'
                                                            ImageUrl="/Manager/Imagens/remove-icon.png" Style="width: 1.5em;" />
                                                    </ItemTemplate>
                                                    <ItemStyle />
                                                </asp:TemplateColumn>

                                            </Columns>
                                            <HeaderStyle CssClass="topo-tb"></HeaderStyle>
                                            <ItemStyle CssClass="impar"></ItemStyle>
                                            <AlternatingItemStyle CssClass="par"></AlternatingItemStyle>
                                            <PagerStyle Visible="false" />
                                        </asp:DataGrid>
                                        <asp:Label ID="lblNoRecordsFound" Text="Nenhum resultado encontrado." runat="server" Visible="false" title="Nenhum resultado encontrado"></asp:Label>
                                        
                                    </form>
                                 </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</body>
</html>



































<%--
<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Timeline.aspx.cs" Inherits="Modulos_CMS_Modulos_ModTimeline_Timeline" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <script src="<%=ResolveUrl("~/JS/jquery-1.10.2.min.js")%>"></script>
    <script src="<%=ResolveUrl("~/JS/jquery.mask.js")%>"></script>
    <script>
        function refreshParent() {
            window.opener.location.href = window.opener.location.href;
            window.close();
        }

       </script>
</head>
<body>
    <form id="form1" runat="server">
        <label>
            Idioma:
            <asp:DropDownList ID="ddlIdioma" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlIdioma_SelectedIndexChanged"></asp:DropDownList>
        </label>
        <div id="divUpload" runat="server">
            <label>Arquivo:</label>
            <asp:FileUpload ID="fupArquivo" runat="server" />
            <asp:RequiredFieldValidator ID="rfvArquivo" runat="server" ControlToValidate="fupArquivo"  ValidationGroup="salvar"  Text="*"></asp:RequiredFieldValidator>
            <asp:RegularExpressionValidator ID="rev" runat="server" ValidationExpression="^.*\.(jpg|JPG|jpeg|JPEG|png|PNG)$" ControlToValidate="fupArquivo" ErrorMessage="*"></asp:RegularExpressionValidator>

        </div>
        <div>
            <label>Título:</label>
            <asp:TextBox ID="txtTitulo" runat="server" MaxLength="100"></asp:TextBox><asp:RequiredFieldValidator ID="rfvTitulo" runat="server" ControlToValidate="txtTitulo"  ValidationGroup="salvar"  Text="*"></asp:RequiredFieldValidator>
        </div>
        <div>
            <label>Ano:</label>
            <asp:TextBox ID="txtano" runat="server" MaxLength="400"></asp:TextBox><asp:RequiredFieldValidator ID="rfvano" runat="server" ControlToValidate="txtano"  ValidationGroup="salvar"  Text="*"></asp:RequiredFieldValidator>
        </div>

        <div>
            <asp:Button ID="btnSalvar" runat="server" Text="Adicionar" OnClick="btnSalvar_Click" ValidationGroup="salvar"/>
        </div>

        <div>
            <asp:Label ID="lblMensagem" runat="server"></asp:Label>
        </div>

        <asp:DataGrid summary="Lista de Timeline  " ID="grdDados" runat="server" AutoGenerateColumns="false" CssClass="listagem-Grid" 
            AlternatingRowStyle-CssClass="par" Width="100%" OnItemCommand="grdDados_ItemCommand" >
            <Columns>
                <asp:TemplateColumn HeaderText="Arquivo" HeaderStyle-CssClass="tabelaHeader" ItemStyle-HorizontalAlign="Center">
                    <ItemTemplate>
                        <img src="<%# String.Format("{0}/{1}/{2}",ConfigurationManager.AppSettings["BradescoRI.Path.Imagens.ModTimeline"], Request.QueryString["conteudoId"] ,Eval("Arquivo"))%>" style="width: 35px; height: 35px" /></a>
                    </ItemTemplate>
                    <ItemStyle />
                </asp:TemplateColumn>
                <asp:BoundColumn Visible="false" DataField="timelineId" HeaderText="timelineId" HeaderStyle-CssClass="tabelaHeader" />
                <asp:BoundColumn DataField="Arquivo" Visible="false" />
                <asp:BoundColumn DataField="Titulo" HeaderText="Titulo" HeaderStyle-CssClass="tabelaHeader" />
                <asp:BoundColumn DataField="Texto" HeaderText="Texto" HeaderStyle-CssClass="tabelaHeader" />
                <asp:TemplateColumn HeaderText="Excluir" HeaderStyle-CssClass="tabelaHeader" ItemStyle-Width="5%" ItemStyle-HorizontalAlign="Center">
                    <ItemTemplate> 
                        <asp:ImageButton ID="imgExcluir" runat="server" AlternateText="Excluir" CommandName="excluir" CommandArgument='<%#Eval("timelineId")%>'
                             ImageUrl="/Manager/Imagens/remove-icon.png" style="width: 1.5em;" />
                    </ItemTemplate>
                    <ItemStyle />
                </asp:TemplateColumn>

            </Columns>
            <HeaderStyle CssClass="topo-tb"></HeaderStyle>
            <ItemStyle CssClass="impar"></ItemStyle>
            <AlternatingItemStyle CssClass="par"></AlternatingItemStyle>
            <PagerStyle Visible="false" />
        </asp:DataGrid>
        <asp:Label ID="lblNoRecordsFound" Text="Nenhum resultado encontrado." runat="server" Visible="false" title="Nenhum resultado encontrado"></asp:Label>
        <div>
            <asp:Button ID="btnFechar" runat="server" Text="Salvar" OnClick="btnFechar_Click"/>
        </div>
    </form>
</body>
</html>



--%>

















<%--



<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Timeline.aspx.cs" Inherits="Modulos_CMS_Modulos_ModTimeline_Timeline" %>

<%@ Register Namespace="Controles.FCKeditor" TagPrefix="FCKEditor" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <script>
        function refreshParent() {
            window.opener.location.href = window.opener.location.href;
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            Idioma:
            <asp:DropDownList ID="ddlIdioma" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlIdioma_SelectedIndexChanged"></asp:DropDownList>
            <asp:HiddenField ID="hdnModTimelineId" runat="server" />
        </div>

        <div id="divUpload" runat="server">
            <label>Arquivo:</label>
            <asp:FileUpload ID="fupArquivo" runat="server" />
            <asp:RequiredFieldValidator ID="rfvArquivo" runat="server" ControlToValidate="fupArquivo" ValidationGroup="salvar" Text="*"></asp:RequiredFieldValidator>
            <asp:RegularExpressionValidator ID="rev" runat="server" ValidationExpression="^.*\.(jpg|JPG|jpeg|JPEG|png|PNG)$" ControlToValidate="fupArquivo" ErrorMessage="*"></asp:RegularExpressionValidator>

        </div>
        <div>
            Ano:
            <asp:TextBox runat="server" TextMode="SingleLine" ID="txtAno" CssClass="frmTxt" MaxLength="4" title="Informe o Ano"></asp:TextBox>
        </div>
        <div>
            Titulo:
            <asp:TextBox runat="server" TextMode="SingleLine" ID="txtTitulo" CssClass="frmTxt" title="Informe o titulo"></asp:TextBox>
        </div>
        <div>
            <FCKEditor:FCKeditor ID="txtTexto" runat="server" BasePath="~/fckeditor/" DefaultLanguage="pt-BR" EnableSourceXHTML="false"
                EnableXHTML="false" Debug="false">
            </FCKEditor:FCKeditor>
        </div>
        <div>
            <asp:FileUpload ID="fupTimeline" runat="server" />
            Imagem:
            <asp:Button ID="btnUpload" Text="Upload" runat="server" OnClick="UploadFile" />
        </div>

        <div>
            <asp:Button ID="btnSalvarModuloTimeline" runat="server" Text="Salvar" OnClick="btnSalvarModuloTimeline_Click" />
        </div>


    </form>
</body>
</html>--%>
