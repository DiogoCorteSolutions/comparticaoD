<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Glossario.aspx.cs" Inherits="Modulos_CMS_Modulos_ModGlossario_Glossario" %>

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
                    <h1 class="page-header">Glossário</h1>
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
                                            <asp:HiddenField ID="hdnConteudoId" runat="server" />
                                        </div>
                                        <div class="form-group">
                                            <label>Glossario:</label>
                                            <asp:DropDownList ID="ddlGlossario" runat="server" CssClass="form-control"></asp:DropDownList>
                                            <asp:Button ID="btnAdicionar" runat="server" Text="Adicionar" OnClick="btnAdicionar_Click" CssClass="btn btn-default btn-bra" />
                                        </div>
                                        
                                            <asp:GridView ID="grvGlossario" OnRowDataBound="grvGlossario_RowDataBound" OnRowCommand="grvGlossario_RowCommand"  EmptyDataText="Registro não encontrado" AutoGenerateColumns="false" runat="server" BorderWidth="0" BorderColor="Transparent" CssClass="table table-striped table-bra-cms">
                                                <Columns>
                                                    <asp:BoundField DataField="Id" HeaderText="ID" ItemStyle-Width="5%"/>
                                                    <asp:TemplateField HeaderText="Título" ItemStyle-Width="90%">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblTituloGlossario" runat="server"></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField ItemStyle-Width="5%">
                                                        <ItemTemplate>
                                                            <asp:ImageButton ID="imgRemover" CommandArgument='<%# Eval("Id") %>' CommandName="Excluir" runat="server" ImageUrl="~/Imagens/remove-icon.png" Width="20px" Height="20px" />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                </Columns>
                                            </asp:GridView>
                                        
                                        
                                            <asp:Button ID="btnSalvar" runat="server" Text="Salvar" OnClick="btnSalvar_Click" CssClass="btn btn-default btn-bra" />
                                        

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
