<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Contato.aspx.cs" Inherits="Modulos_CMS_Modulos_ModContato_Contato" %>

<%@ Register Namespace="Controles.FCKeditor" TagPrefix="FCKEditor" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
        
    <meta charset="utf-8">
    <meta http-equiv="X-UA-Compatible" content="IE=edge">
    <meta name="viewport" content="width=device-width, initial-scale=1">
    <link href="<%=ResolveUrl("~/CSS/bootstrap.min.css")%>" rel="stylesheet" />
    <link href="<%=ResolveUrl("~/CSS/style-manager.css")%>" rel="stylesheet" />
     <!--[if lt IE 9]>
        <script src="https://oss.maxcdn.com/libs/html5shiv/3.7.0/html5shiv.js"></script>
        <script src="https://oss.maxcdn.com/libs/respond.js/1.4.2/respond.min.js"></script>
    <![endif]-->
</head>
<body class="modulos-cms">
    <div id="wrapper">
         <div id="page-wrapper">
            <div class="row">
                <div class="col-lg-12">
                    <h1 class="page-header">Contato</h1>
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
                                        </div>
                                        <div class="form-group">
                                            <label>Assuntos:</label>
                                            <asp:TextBox ID="txtAssuntos" runat="server" MaxLength="1000" ToolTip="Digite os assuntos separando-os por ';'" Columns="20" Rows="10" TextMode="MultiLine" CssClass="form-control"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="rfvAssuntos" runat="server" ControlToValidate="txtAssuntos" Text="*" ValidationGroup="vgrModContato"></asp:RequiredFieldValidator>
                                            <label class="cms-label-p">Itens separados por ";"</label>
                                        </div>
                                        <div class="form-group">
                                            <label>Email To:</label>
                                            <asp:TextBox ID="txtEmailTo" runat="server" MaxLength="200" ToolTip="Digite o e-mail de destino. Separado(s) por ';'" CssClass="form-control"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="rfvEmailTo" runat="server" ControlToValidate="txtEmailTo" Text="*" ValidationGroup="vgrModContato"></asp:RequiredFieldValidator>
                                        </div>
                                        <div class="form-group">
                                            <label>Email CC:</label>
                                            <asp:TextBox ID="txtEmailToCc" runat="server" MaxLength="200" ToolTip="Digite o e-mail (Cópia) de destino. Separado(s) por ';'" CssClass="form-control"></asp:TextBox>
                                        </div>
                                        <div class="form-group">
                                            <label>Email CCo:</label>
                                            <asp:TextBox ID="txtEmailToCco" runat="server" MaxLength="200" ToolTip="Digite o e-mail (Cópia oculta) de destino. Separado(s) por ';'" CssClass="form-control"></asp:TextBox>
                                        </div>
                                        <div class="form-group">
                                            <label>Assunto E-mail:</label>
                                            <asp:TextBox ID="txtAssuntoEmail" runat="server" MaxLength="200" ToolTip="Digite o assunto do e-mail a ser enviado." CssClass="form-control"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="rfvAssuntoEmail" runat="server" ControlToValidate="txtAssuntoEmail" Text="*" ValidationGroup="vgrModContato"></asp:RequiredFieldValidator>
                                        </div>
                                        <div class="form-group">
                                            <FCKEditor:FCKeditor ID="fckEditor" runat="server" BasePath="~/fckeditor/" DefaultLanguage="pt-BR" EnableSourceXHTML="false"
                                                EnableXHTML="false" Debug="false" EditorAreaCSS="form-control">
                                            </FCKEditor:FCKeditor>
                                        </div>
                                        <div class="form-group">
                                            <asp:Button ID="btnSalvar" runat="server" Text="Salvar" OnClick="btnSalvar_Click" ValidationGroup="vgrModContato" CssClass="btn btn-default btn-bra" />
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
