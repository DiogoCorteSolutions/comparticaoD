<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Arquivos.aspx.cs" Inherits="Modulos_CMS_Modulos_ModArquivos_Arquivos" %>

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
    <script>
        function refreshParent() {
            window.opener.location.href = window.opener.location.href;
        }
    </script>
</head>
<body class="modulos-cms">

    <div id="wrapper">
        <div id="page-wrapper">
            <div class="row">
                <div class="col-lg-12">
                    <h1 class="page-header">Arquivos</h1>
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
                                    <form id="form1" runat="server">
                                        <div class="form-group">
                                            <label>Idioma:</label>
                                            <asp:DropDownList ID="ddlIdioma" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlIdioma_SelectedIndexChanged" CssClass="form-control"></asp:DropDownList>
                                            <asp:HiddenField ID="hdnModArquivo" runat="server" />
                                        </div>
                                        <div class="form-group">
                                            <label>Tipo de Layout:</label>
                                            <asp:DropDownList ID="ddlTipoLayout" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlTipoLayout_SelectedIndexChanged" CssClass="form-control"></asp:DropDownList>
                                        </div>
                                        <div class="form-group">
                                            <label>Filtros:</label>
                                            <asp:DropDownList ID="ddlFiltros" runat="server" CssClass="form-control"></asp:DropDownList>
                                        </div>
                                        <div class="form-group">
                                            <label>Titulo:</label>
                                            <asp:TextBox runat="server" TextMode="SingleLine" ID="txtTitulo" CssClass="frmTxt" MaxLength="4096" title="Informe o titulo" class="form-control"></asp:TextBox>
                                            <%--<asp:CheckBox ID="chkMostraTitulo" runat="server" Text="Mostrar título" Checked="true" />--%>
                                        </div>

                                        <div class="form-group">
                                            <label>Tipo deArquivo:</label>
                                            <asp:DropDownList ID="ddltipoArquivo" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddltipoArquivo_SelectedIndexChanged" CssClass="form-control"></asp:DropDownList>
                                        </div>
                                        <div class="form-group">
                                            <label>Arquivo:</label>
                                            <asp:DropDownList ID="ddlArquivo" runat="server" CssClass="form-control"></asp:DropDownList>
                                        </div>

                                        <div class="form-group">
                                            <asp:Panel ID="pnlImagem" runat="server" Visible="false">
                                                <label>Capa:</label>
                                                <asp:DropDownList ID="ddlCapa" runat="server" CssClass="form-control"></asp:DropDownList>
                                            </asp:Panel>
                                        </div>

                                        <div class="form-group">
                                            <asp:Button ID="btnIncluirArquivo" runat="server" Text="Incluir" OnClick="btnIncluirArquivo_Click" CssClass="btn btn-default btn-bra" />
                                        </div>
                                        <asp:Panel ID="pnlArquivos" runat="server">
                                            <asp:GridView ID="grvArquivos" BorderWidth="0" BorderColor="Transparent" AutoGenerateColumns="false" OnRowDataBound="grvArquivos_RowDataBound" runat="server" EmptyDataText="Nenhum arquivo encontrado" CssClass="table table-striped table-bra-cms">
                                                <Columns>
                                                    <asp:BoundField HeaderText="ID" DataField="Id" ItemStyle-Width="5%" />

                                                    <asp:TemplateField HeaderText="Tipo" HeaderStyle-CssClass="tabelaHeader" ItemStyle-Width="35%" ItemStyle-HorizontalAlign="Center">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblTipoArquivo" runat="server"></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle />
                                                    </asp:TemplateField>
                                                    <asp:BoundField HeaderText="Titulo" ItemStyle-Width="50%" DataField="Titulo" />

                                                    <asp:TemplateField HeaderText="Ações" HeaderStyle-CssClass="tabelaHeader" ItemStyle-Width="10%" ItemStyle-HorizontalAlign="Center">
                                                        <ItemTemplate>

                                                            <asp:ImageButton runat="server" ID="btnExcluir" ToolTip="Excluir" Width="1.5em" ImageUrl="~/Imagens/deny.png"
                                                                OnClientClick="return confirm('Deseja excluir realmente?');" OnCommand="btnExcluir_Command" CommandName="ExcluirArquivo" CommandArgument='<%# ((GridViewRow) Container).RowIndex %>' />

                                                        </ItemTemplate>
                                                        <ItemStyle />
                                                    </asp:TemplateField>
                                                </Columns>
                                                
                                            </asp:GridView>
                                        </asp:Panel>
                                        <div class="form-group">
                                            <asp:Panel ID="pnlDestaque" runat="server" Visible="false">
                                                <label>Destaque:</label>
                                                <asp:DropDownList ID="ddlDestaque" runat="server"></asp:DropDownList>
                                                <asp:DropDownList ID="ddlImageDestaque" AutoPostBack="true" OnSelectedIndexChanged="ddlImageDestaque_SelectedIndexChanged" runat="server"></asp:DropDownList>
                                                <asp:Image ID="imgDestaque" runat="server" Width="100px" Height="50px" Visible="false" />
                                            </asp:Panel>
                                        </div>
                                        <div>
                                            <asp:Button ID="btnSalvar" runat="server" Text="Salvar" OnClick="btnSalvar_Click" CssClass="btn btn-default btn-bra" />
                                        </div>

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
