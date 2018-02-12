<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Links.aspx.cs" Inherits="Modulos_CMS_Modulos_ModLinks_Links" %>

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
            window.close();
        }
    </script>
</head>
<body class="modulos-cms">
    <div id="wrapper">
         <div id="page-wrapper">
            <div class="row">
                <div class="col-lg-12">
                    <h1 class="page-header">Links</h1>
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
                                            <label>Grupo de Links:</label>
                                            <asp:DropDownList ID="ddlGrupoLink" runat="server" AutoPostBack="true" CssClass="form-control"></asp:DropDownList>
                                        </div>
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
