<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ModImagemUnica.aspx.cs" Inherits="Modulos_CMS_Modulos_ModImagemUnica_ModImagemUnica" %>

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
<body>
    <form id="form1" runat="server">
        <div>
            <label>
                Idioma:
            <asp:DropDownList ID="ddlIdioma" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlIdioma_SelectedIndexChanged"></asp:DropDownList>
            </label>
        </div>
        <div id="divUpload" runat="server">
            <label>Arquivo:</label>
            <asp:FileUpload ID="fupArquivo" runat="server" />
            <asp:RequiredFieldValidator ID="rfvArquivo" runat="server" ControlToValidate="fupArquivo" Text="*"></asp:RequiredFieldValidator>
            <asp:RegularExpressionValidator ID="rev" runat="server" ValidationExpression="^.*\.(jpg|JPG|jpeg|JPEG|png|PNG)$" ControlToValidate="fupArquivo" ErrorMessage="*"></asp:RegularExpressionValidator>

        </div>
        <div id="divImagem" runat="server">
            <label>Arquivo:</label>
            <asp:Image ID="imgImagem" runat="server" Width="200px" />
            <asp:ImageButton ID="btnExcluir" runat="server"
                ImageUrl="~/Imagens/remove-icon.png" OnClick="btnExcluir_Click" Width="20px" Height="20px"
                ToolTip="Excluir Imagem" alt="Excluir imagem" title="Excluir imagem" />
        </div>
        <div>
            <label>Tamanho</label>
            <asp:DropDownList ID="ddlTamanho" CssClass="frmDropdown" runat="server" ToolTip="Informe o tamanho da imagem"></asp:DropDownList>
        </div>
        <div>
            <label>Tooltip</label>
            <asp:TextBox ID="txtTooltip" runat="server" MaxLength="200"></asp:TextBox>
        </div>
        <div>
            <label>Pré-Título</label>
            <asp:TextBox ID="txtTexto1" runat="server" MaxLength="200"></asp:TextBox>
        </div>
        <div>
            <label>Título</label>
            <asp:TextBox ID="txtTexto2" runat="server" MaxLength="200"></asp:TextBox>
        </div>
        <div>
            <label>Subtítulo</label>
            <asp:TextBox ID="txtTexto3" runat="server" MaxLength="200"></asp:TextBox>
        </div>
        <div>
            <label>Texto Url</label>
            <asp:TextBox ID="txtTextoUrl" runat="server" MaxLength="200"></asp:TextBox>
        </div>
        <div>
            <label>URL</label>
            <asp:TextBox ID="txtUrl" runat="server" MaxLength="1000"></asp:TextBox>
        </div>
        <label>
            <asp:DropDownList ID="ddlPaginas" CssClass="frmDropdown ddlPaginas" runat="server"></asp:DropDownList>
        </label>
        <div>
            <label>Target</label>
            <asp:DropDownList ID="ddlTarget" CssClass="frmDropdown" runat="server" ToolTip="Informe a forma de abertura da abertura"></asp:DropDownList>
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
