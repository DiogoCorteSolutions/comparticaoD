<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Modulos/Modulos.master" CodeFile="Editar.aspx.cs" Inherits="Modulos_AlertaRI_Editar" %>

<asp:Content ID="Content1" ContentPlaceHolderID="contentInterna" runat="Server">

    <script type="text/javascript">
        
    </script>

    <div id="title">
        <h1>
            <asp:Label ID="lblTitulo" runat="server"><%=Resources.AlertaRI.Titulo %></asp:Label>
        </h1>
    </div>
    <br />
    <div class="fields">
        <label>
            <span>
                <asp:Label ID="lblNome" runat="server" AssociatedControlID="txtNome" title="Login"><%=Resources.AlertaRI.Nome %>*</asp:Label>
            </span>
            <asp:TextBox runat="server" ID="txtNome" CssClass="frmTxt" MaxLength="128" title="Informe o nome"></asp:TextBox>
            <asp:RequiredFieldValidator ID="rfvNome" runat="server" ControlToValidate="txtNome" Display="Dynamic" title="Campo obrigatório"></asp:RequiredFieldValidator>
        </label>
        <label>
            <span>
                <asp:Label ID="lblEmail" runat="server" AssociatedControlID="txtNome" title="Email"><%=Resources.AlertaRI.Email %>*</asp:Label>
            </span>
            <asp:TextBox runat="server" ID="txtEmail" CssClass="frmTxt" MaxLength="128" title="Informe o e-mail"></asp:TextBox>
            <asp:RequiredFieldValidator ID="rfvEmail" runat="server" ControlToValidate="txtEmail" Display="Dynamic" title="Campo obrigatório"></asp:RequiredFieldValidator>
            <asp:RegularExpressionValidator ID="revEmail" runat="server" ControlToValidate="txtEmail" Display="Dynamic" title="Digite um e-mail válido" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"></asp:RegularExpressionValidator>
        </label>
        <label>
            <span>
                <asp:Label ID="lblTelefone" runat="server" AssociatedControlID="txtTelefone" title="Telefone"><%=Resources.AlertaRI.Telefone %></asp:Label>
            </span>
            <asp:TextBox runat="server" ID="txtTelefoneDDD" CssClass="txtNumeroDdd" MaxLength="2" title="Informe o DDD do telefone"></asp:TextBox>
            <asp:TextBox runat="server" ID="txtTelefone" CssClass="txtNumero"  MaxLength="15" title="Informe o telefone"></asp:TextBox>
        </label>
        <label>
            <span>
                <asp:Label ID="lblEstado" runat="server" AssociatedControlID="txtEstado" title="Empresa"><%=Resources.AlertaRI.Estado %></asp:Label>
            </span>
            <asp:TextBox runat="server" ID="txtEstado" CssClass="frmTxt" MaxLength="128" title="Informe o estado de Residência"></asp:TextBox>
        </label>
        <label>
            <span>
                <asp:Label ID="lblPais" runat="server" AssociatedControlID="ddlPais" title="País"><%=Resources.AlertaRI.Pais %></asp:Label>
            </span>
            <asp:DropDownList runat="server" ID="ddlPais" title="Selecione o País"></asp:DropDownList>
        </label>
        <label>
            <span>
                <asp:CheckBox ID="chkReceberMailing" runat="server" /><%=Resources.AlertaRI.RecebeMailing %>
            </span>
        </label>
        <label>
            <span>
                <asp:Label ID="lblIdioma" runat="server" AssociatedControlID="ddlIdioma" title="Idioma"><%=Resources.AlertaRI.Idioma %></asp:Label>
            </span>
            <asp:DropDownList runat="server" ID="ddlIdioma" title="Selecione o Idioma"></asp:DropDownList>
        </label>
        <label>
            <span>
                <asp:CheckBox ID="chkProfissionalMercado" runat="server" /><%=Resources.AlertaRI.ProfissionalMercado %>
            </span>
        </label>
        <label>
            <span>
                <asp:Label ID="lblEmpresa" runat="server" AssociatedControlID="txtEmpresa" title="Empresa"><%=Resources.AlertaRI.Empresa %></asp:Label>
            </span>
            <asp:TextBox runat="server" ID="txtEmpresa" CssClass="frmTxt" MaxLength="128" title="Informe a empresa"></asp:TextBox>
        </label>
        <label>
            <span>
                <asp:Label ID="lblSegmentoEmpresa" runat="server" AssociatedControlID="ddlSegmentoEmpresa" title="Segmento da Empresa"><%=Resources.AlertaRI.Segmento %></asp:Label>
            </span>
            <asp:DropDownList runat="server" ID="ddlSegmentoEmpresa" title="Selecione o Segmento"></asp:DropDownList>
        </label>
    </div>
    <div class="fields">
        <label>
            <span>
                <asp:Label ID="lblCamposObrigatorios" runat="server" title="Campos obrigatórios"><%=Resources.Textos.Campos_Obrigatorios %></asp:Label>
            </span>
        </label>

    </div>

    <div class="btn-acoes2">
        <asp:PlaceHolder runat="server" ID="phOptions" Visible="true">
            <asp:Button runat="server" ID="btnCancelar" CssClass="cancelar" Text="Voltar" OnClick="btnCancelar_Click" CausesValidation="false" title="Cancelar" alt="Cancelar" />
            <asp:Button runat="server" ID="btnOK" CssClass="submit" Text="Salvar" OnClick="btnOK_Click" title="Salvar" alt="Salvar" />
        </asp:PlaceHolder>
    </div>

</asp:Content>
