<%@ Page Language="C#" AutoEventWireup="true" CodeFile="FaleConosco.aspx.cs" Inherits="Modulos_CMS_Modulos_ModFaleConosco_FaleConosco" %>

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
        <div>
            <label>
                Idioma:
            <asp:DropDownList ID="ddlIdioma" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlIdioma_SelectedIndexChanged"></asp:DropDownList>
            </label>
        </div>
        <div>
            <label>Assunto (separado por ;)</label>
            <asp:TextBox ID="txtAssunto" runat="server" MaxLength="400"></asp:TextBox>
        </div>
        <div>
            <label>Email (separado por ;)</label>
            <asp:TextBox ID="txtEmail" runat="server" MaxLength="1000"></asp:TextBox>
        </div>
        <div>
            <asp:Button ID="btnSalvar" runat="server" Text="Salvar" OnClick="btnSalvar_Click" />
        </div>

        <div>
            <asp:Label ID="lblMensagem" runat="server"></asp:Label>
        </div>
    </form>
</body>
</html>

