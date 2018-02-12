<%@ Page Language="C#" AutoEventWireup="true" CodeFile="EditarAbas.aspx.cs" Inherits="Modulos_CMS_Paginas_EditarAbas" %>

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
        <div>
            <asp:PlaceHolder ID="plhControles" runat="server"></asp:PlaceHolder>  
            <asp:Button ID="btnSalvar" runat="server" OnClick="btnSalvar_Click" />  
        </div>
    </form>
</body>
</html>
