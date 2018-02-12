<%@ Page Language="C#" AutoEventWireup="true" CodeFile="BannersHeader.aspx.cs" Inherits="Modulos_CMS_Modulos_ModBanners_Banners" %>

<%@ Register Namespace="WebFoundations.ServerControls" TagPrefix="wfc" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
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
            Grupo de Banners:
            <asp:DropDownList ID="ddlGrupoBanner" runat="server" AutoPostBack="true"></asp:DropDownList>
        </label>

        <div>
            <asp:Button ID="btnSalvar" runat="server" Text="Salvar" OnClick="btnSalvar_Click" />
        </div>
    </form>
</body>
</html>
