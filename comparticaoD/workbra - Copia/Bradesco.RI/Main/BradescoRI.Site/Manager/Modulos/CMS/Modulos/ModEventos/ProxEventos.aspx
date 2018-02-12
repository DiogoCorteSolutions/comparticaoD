<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ProxEventos.aspx.cs" Inherits="Modulos_CMS_Modulos_ModEventos_ProxEventos" %>

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
<body>
    <form id="form1" runat="server">
        <div>
            <label>URL Módulo Lista Evento</label>
            <asp:TextBox ID="txtUrlListaEvento" runat="server" MaxLength="1000" Enabled="false"></asp:TextBox>
        </div>
        <label>
            <asp:DropDownList ID="ddlPaginas" CssClass="frmDropdown ddlPaginas" runat="server"></asp:DropDownList>
        </label>
        <div>
            <label>URL Módulo Calendário Eventos</label>
            <asp:TextBox ID="txtUrlTodosEventos" runat="server" MaxLength="1000" Enabled="false"></asp:TextBox>
        </div>
        <label>
            <asp:DropDownList ID="ddlPaginas2" CssClass="frmDropdown ddlPaginas2" runat="server"></asp:DropDownList>
        </label>

        <div>
            <asp:Button ID="btnSalvar" runat="server" Text="Salvar" OnClick="btnSalvar_Click" />
        </div>

        <div>
            <asp:Label ID="lblMensagem" runat="server"></asp:Label>
        </div>
    </form>
</body>
</html>
