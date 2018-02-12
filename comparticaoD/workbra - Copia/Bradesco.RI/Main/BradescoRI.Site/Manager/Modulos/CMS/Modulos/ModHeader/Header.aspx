<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Header.aspx.cs" Inherits="Modulos_CMS_Modulos_ModHeader_Header" %>
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

        <div id="divUpload" runat="server">
            <label>Arquivo:</label>
            <asp:FileUpload ID="fupArquivo" runat="server" />
            <asp:RequiredFieldValidator ID="rfvArquivo" runat="server" ControlToValidate="fupArquivo" Text="*"></asp:RequiredFieldValidator>
            <asp:RegularExpressionValidator ID="rev" runat="server"   ValidationExpression="^.*\.(jpg|JPG|jpeg|JPEG|png|PNG)$" ControlToValidate="fupArquivo" ErrorMessage="*"></asp:RegularExpressionValidator>

        </div>
        <div id="divImagem" runat="server">
            <label>Arquivo:</label>
            <asp:Image ID="imgImagem" runat="server" Width="200px" />
            <asp:ImageButton ID="btnExcluir" runat="server"
                ImageUrl="~/Imagens/remove-icon.png" OnClick="btnExcluir_Click" Width="20px" Height="20px"
                ToolTip="Excluir Imagem" alt="Excluir imagem" title="Excluir imagem" />
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
