<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Timeline.aspx.cs" Inherits="Modulos_CMS_Modulos_ModTimeline_Timeline" %>
<%@ Register Namespace="Controles.FCKeditor" TagPrefix="FCKEditor" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>   
     <script>
        function refreshParent() {
            window.opener.location.href = window.opener.location.href;
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            Idioma:
            <asp:DropDownList ID="ddlIdioma" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlIdioma_SelectedIndexChanged"></asp:DropDownList>
            <asp:HiddenField ID="hdnModTimelineId" runat="server" />
        </div>
        <div>
            Ano:
            <asp:TextBox runat="server" TextMode="SingleLine" ID="txtAno" CssClass="frmTxt" MaxLength="4" title="Informe o Ano"></asp:TextBox>
        </div>
        <div>
            Titulo:
            <asp:TextBox runat="server" TextMode="SingleLine" ID="txtTitulo" CssClass="frmTxt" title="Informe o titulo"></asp:TextBox>
        </div>
        <div>
            <FCKEditor:FCKeditor ID="txtTexto" runat="server" BasePath="~/fckeditor/" DefaultLanguage="pt-BR" EnableSourceXHTML="false"
                EnableXHTML="false" Debug="false">
            </FCKEditor:FCKeditor>
        </div>
        <div>
            <asp:FileUpload ID="fupTimeline" runat="server" />
            Imagem:
            <asp:Button ID="btnUpload" Text="Upload" runat="server" OnClick="UploadFile" />
        </div>

        <div>
            <asp:Button ID="btnSalvarModuloTimeline" runat="server" Text="Salvar" OnClick="btnSalvarModuloTimeline_Click" />
        </div>

                    
    </form>
</body>
</html>
