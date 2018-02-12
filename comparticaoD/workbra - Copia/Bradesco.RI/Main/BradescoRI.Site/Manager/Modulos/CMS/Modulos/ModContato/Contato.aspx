<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Contato.aspx.cs" Inherits="Modulos_CMS_Modulos_ModContato_Contato" %>

<%@ Register Namespace="Controles.FCKeditor" TagPrefix="FCKEditor" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <label>Idioma:</label>
            <asp:DropDownList ID="ddlIdioma" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlIdioma_SelectedIndexChanged"></asp:DropDownList>
        </div>
        <div>
            <label style="vertical-align:top;">Assuntos:</label>
            <asp:TextBox ID="txtAssuntos" runat="server" MaxLength="1000" ToolTip="Digite os assuntos separando-os por ';'" Columns="20" Rows="10" TextMode="MultiLine"></asp:TextBox>
            <asp:RequiredFieldValidator ID="rfvAssuntos" runat="server" ControlToValidate="txtAssuntos" Text="*" ValidationGroup="vgrModContato"></asp:RequiredFieldValidator>
            <label>Itens separados por ";"</label>
        </div>
        <div>
            <label>Email To:</label>
            <asp:TextBox ID="txtEmailTo" runat="server" MaxLength="200" ToolTip="Digite o e-mail de destino. Separado(s) por ';'"></asp:TextBox>
            <asp:RequiredFieldValidator ID="rfvEmailTo" runat="server" ControlToValidate="txtEmailTo" Text="*" ValidationGroup="vgrModContato"></asp:RequiredFieldValidator>
        </div>
        <div>
            <label>Email CC:</label>
            <asp:TextBox ID="txtEmailToCc" runat="server" MaxLength="200" ToolTip="Digite o e-mail (Cópia) de destino. Separado(s) por ';'"></asp:TextBox>
        </div>
        <div>
            <label>Email CCo:</label>
            <asp:TextBox ID="txtEmailToCco" runat="server" MaxLength="200" ToolTip="Digite o e-mail (Cópia oculta) de destino. Separado(s) por ';'"></asp:TextBox>
        </div>
        <div>
            <label>Assunto E-mail:</label>
            <asp:TextBox ID="txtAssuntoEmail" runat="server" MaxLength="200" ToolTip="Digite o assunto do e-mail a ser enviado."></asp:TextBox>
            <asp:RequiredFieldValidator ID="rfvAssuntoEmail" runat="server" ControlToValidate="txtAssuntoEmail" Text="*" ValidationGroup="vgrModContato"></asp:RequiredFieldValidator>
        </div>
        <div>
            <FCKEditor:FCKeditor ID="fckEditor" runat="server" BasePath="~/fckeditor/" DefaultLanguage="pt-BR" EnableSourceXHTML="false"
                EnableXHTML="false" Debug="false" >
            </FCKEditor:FCKeditor>
        </div>
        <div>
            <asp:Button ID="btnSalvar" runat="server" Text="Salvar" OnClick="btnSalvar_Click" ValidationGroup="vgrModContato" />
        </div>
    </form>
</body>
</html>
