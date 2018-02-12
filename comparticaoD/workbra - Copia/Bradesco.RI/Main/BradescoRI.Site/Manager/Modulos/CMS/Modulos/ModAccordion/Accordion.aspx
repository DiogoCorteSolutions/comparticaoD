<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Accordion.aspx.cs" Inherits="Modulos_CMS_Modulos_ModAccordion_Accordion" %>

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
        <asp:HiddenField ID="hdnModAccordionId" runat="server" />        
        <div>
            <asp:Label ID="lblTitulo" runat="server" alt="Título" title="Título">Título</asp:Label>
            <asp:TextBox ID="txtTitulo" runat="server"></asp:TextBox> <asp:RequiredFieldValidator ID="rfvTitulo" ControlToValidate="txtTitulo" Text="*" ForeColor="Red" runat="server"></asp:RequiredFieldValidator>       
            <br />
            <asp:Label ID="lblPainelAberto" runat="server" alt="Painel Aberto" title="Título">Painel Aberto</asp:Label>
            <asp:CheckBox ID="chkAberto" runat="server" />  
            <br />
            <asp:Label ID="lblModulos" runat="server" alt="Módulos" title="Módulos">Módulos</asp:Label>
            <asp:DropDownList ID="ddlModulos" runat="server" alt="Módulos página" title="Módulos página"></asp:DropDownList>
            <asp:Button ID="btnAdicionar" runat="server" Text="Adcionar" OnClick="btnAdicionarModulo_Click" />
        </div>        
        <br />
        <div>
            <asp:Button ID="btnSalvarModuloAccordion" runat="server" Text="Salvar" OnClick="btnSalvarModuloAccordion_Click" />
        </div>
    </form>
</body>
</html>
