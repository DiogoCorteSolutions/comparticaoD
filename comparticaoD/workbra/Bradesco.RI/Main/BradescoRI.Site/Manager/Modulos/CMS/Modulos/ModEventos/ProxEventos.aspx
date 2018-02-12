<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ProxEventos.aspx.cs" Inherits="Modulos_CMS_Modulos_ModEventos_ProxEventos" %>

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
                    $("#txtUrlListaEvento").val(this.value);
                } else {
                    $("#txtUrlListaEvento").val('');
                }
            });

            $(".ddlPaginas2").change(function () {
                if (this.value != "0") {
                    $("#txtUrlTodosEventos").val(this.value);
                } else {
                    $("#txtUrlTodosEventos").val('');
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
                    <h1 class="page-header">Próximos Eventos</h1>
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
                                            <label>URL Módulo Lista Evento</label>
                                            <asp:TextBox ID="txtUrlListaEvento" runat="server" MaxLength="1000" Enabled="false" class="form-control"></asp:TextBox>
                                            <asp:DropDownList ID="ddlPaginas" CssClass="form-control" runat="server"></asp:DropDownList>
                                        </div>
                                        <div class="form-group">
                                            <label>URL Módulo Calendário Eventos</label>
                                            <asp:TextBox ID="txtUrlTodosEventos" runat="server" MaxLength="1000" Enabled="false" class="form-control"></asp:TextBox>
                                            <asp:DropDownList ID="ddlPaginas2" CssClass="form-control" runat="server"></asp:DropDownList>
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
