<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ModImagemUnica.aspx.cs" Inherits="Modulos_CMS_Modulos_ModImagemUnica_ModImagemUnica" %>

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

        $(document).ready(function () {

            $(".ddlPaginas").change(function () {
                if (this.value != "0") {
                    $("#txtUrl").val(this.value);
                } else {
                    $("#txtUrl").val('');
                }
            });
        });

    </script>
</head>
<body class="modulos-cms">
    <div id="wrapper">
         <div id="page-wrapper">
            <div class="row">
                <div class="col-lg-12">
                    <h1 class="page-header">Imagem</h1>
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
                                        <div id="divUpload" runat="server" class="form-group">
                                            <label>Arquivo:</label>
                                            <asp:FileUpload ID="fupArquivo" runat="server" />
                                            <asp:RequiredFieldValidator ID="rfvArquivo" runat="server" ControlToValidate="fupArquivo" Text="*"></asp:RequiredFieldValidator>
                                            <asp:RegularExpressionValidator ID="rev" runat="server" ValidationExpression="^.*\.(jpg|JPG|jpeg|JPEG|png|PNG)$" ControlToValidate="fupArquivo" ErrorMessage="*"></asp:RegularExpressionValidator>

                                        </div>
                                        <div id="divImagem" runat="server" class="form-group">
                                            <label>Arquivo:</label>
                                            <asp:Image ID="imgImagem" runat="server" Width="200px" CssClass="form-control" />
                                            <asp:ImageButton ID="btnExcluir" runat="server"
                                                ImageUrl="~/Imagens/remove-icon.png" OnClick="btnExcluir_Click" Width="20px" Height="20px"
                                                ToolTip="Excluir Imagem" alt="Excluir imagem" title="Excluir imagem" />
                                        </div>
                                        <div class="form-group">
                                            <label>Tamanho</label>
                                            <asp:DropDownList ID="ddlTamanho" runat="server" ToolTip="Informe o tamanho da imagem" CssClass="form-control"></asp:DropDownList>
                                        </div>
                                        <div class="form-group">
                                            <label>Tooltip</label>
                                            <asp:TextBox ID="txtTooltip" runat="server" MaxLength="200" CssClass="form-control"></asp:TextBox>
                                        </div>
                                        <div class="form-group">
                                            <label>Pré-Título</label>
                                            <asp:TextBox ID="txtTexto1" runat="server" MaxLength="200" CssClass="form-control"></asp:TextBox>
                                        </div>
                                        <div class="form-group">
                                            <label>Título</label>
                                            <asp:TextBox ID="txtTexto2" runat="server" MaxLength="200" CssClass="form-control"></asp:TextBox>
                                        </div>
                                        <div class="form-group">
                                            <label>Subtítulo</label>
                                            <asp:TextBox ID="txtTexto3" runat="server" MaxLength="200" CssClass="form-control"></asp:TextBox>
                                        </div>
                                        <div class="form-group">
                                            <label>Texto Url</label>
                                            <asp:TextBox ID="txtTextoUrl" runat="server" MaxLength="200" CssClass="form-control"></asp:TextBox>
                                        </div>
                                        <div class="form-group">
                                            <label>URL</label>
                                            <asp:TextBox ID="txtUrl" runat="server" MaxLength="1000" CssClass="form-control"></asp:TextBox>
                                        
                                        
                                            <asp:DropDownList ID="ddlPaginas" runat="server" CssClass="form-control"></asp:DropDownList>
                                        </div>
                                        <div class="form-group">
                                            <label>Target</label>
                                            <asp:DropDownList ID="ddlTarget" runat="server" ToolTip="Informe a forma de abertura da abertura" CssClass="form-control"></asp:DropDownList>
                                        </div>

                                        
                                            <asp:Button ID="btnSalvar" runat="server" Text="Salvar" OnClick="btnSalvar_Click" CssClass="btn btn-default btn-bra" />
                                        

                                        <div class="form-group">
                                            <asp:Label ID="lblMensagem" runat="server"></asp:Label>
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
