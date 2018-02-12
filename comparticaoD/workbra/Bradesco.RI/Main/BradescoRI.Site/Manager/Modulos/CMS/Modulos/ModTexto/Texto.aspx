<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Texto.aspx.cs" Inherits="Modulos_CMS_Modulos_ModTexto_Texto" %>

<%@ Register Namespace="Controles.FCKeditor" TagPrefix="FCKEditor" %>
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
            Idioma:
            <asp:DropDownList ID="ddlIdioma" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlIdioma_SelectedIndexChanged"></asp:DropDownList>
        </div>
        <div>
            <FCKEditor:FCKeditor ID="fckEditor" runat="server" BasePath="~/fckeditor/" DefaultLanguage="pt-BR" EnableSourceXHTML="false"
                EnableXHTML="false" Debug="false">
            </FCKEditor:FCKeditor>
        </div>
        <div>
            <asp:Button ID="btnSalvarModuloTexto" runat="server" Text="Salvar" OnClick="btnSalvarModuloTexto_Click" />
        </div>
    </form>
</body>
</html>
