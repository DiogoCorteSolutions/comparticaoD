<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Modulos/Modulos.master" CodeFile="Editar.aspx.cs" Inherits="Modulos_Usuarios_Editar" %>

<asp:Content ID="Content1" ContentPlaceHolderID="contentInterna" runat="Server">
    <div id="title">
        <h1>
            <asp:Label ID="lblTitulo" runat="server"><%=Resources.Usuario.Titulo %></asp:Label>
        </h1>
    </div>
    <br />
    <div class="fields">
        <label>
            <span>
                <asp:Label ID="lblPerfil" runat="server" AssociatedControlID="ddlPerfil" title="Perfil"><%=Resources.Usuario.Perfil %>*</asp:Label>
            </span>
            <asp:DropDownList runat="server" ID="ddlPerfil" title="Selecione o Perfil"></asp:DropDownList>
            <asp:RequiredFieldValidator ID="rfvPerfil" runat="server" ControlToValidate="ddlPerfil" InitialValue="0" Display="Dynamic" title="Campo obrigatório"></asp:RequiredFieldValidator>
        </label>

        <label>
            <span>
                <asp:CheckBox ID="chkAtivo" runat="server" /><%=Resources.Usuario.Ativo %>
            </span>
        </label>

        <label>
            <span>
                <asp:Label ID="lblNome" runat="server" AssociatedControlID="txtNome" title="Login"><%=Resources.Usuario.Nome %>*</asp:Label>
            </span>
            <asp:TextBox runat="server" ID="txtNome" CssClass="frmTxt" MaxLength="200" title="Informe o nome"></asp:TextBox>
            <asp:RequiredFieldValidator ID="rfvNome" runat="server" ControlToValidate="txtNome" Display="Dynamic" title="Campo obrigatório"></asp:RequiredFieldValidator>
        </label>

        <label>
            <span>
                <asp:Label ID="lblLogin" runat="server" AssociatedControlID="txtNome" title="Login"><%=Resources.Usuario.Login %>*</asp:Label>
            </span>
            <asp:TextBox runat="server" ID="txtLogin" CssClass="frmTxt" MaxLength="50" title="Informe o login"></asp:TextBox>
            <asp:RequiredFieldValidator ID="rfvLogin" runat="server" ControlToValidate="txtLogin" Display="Dynamic" title="Campo obrigatório"></asp:RequiredFieldValidator>
        </label>

        <label>
            <span>
                <asp:Label ID="lblEmail" runat="server" AssociatedControlID="txtNome" title="Email"><%=Resources.Usuario.Email %>*</asp:Label>
            </span>
            <asp:TextBox runat="server" ID="txtEmail" CssClass="frmTxt" MaxLength="200" title="Informe o e-mail"></asp:TextBox>
            <asp:RequiredFieldValidator ID="rfvEmail" runat="server" ControlToValidate="txtEmail" Display="Dynamic" title="Campo obrigatório"></asp:RequiredFieldValidator>
            <asp:RegularExpressionValidator ID="revEmail" runat="server" ControlToValidate="txtEmail" Display="Dynamic" title="Digite um e-mail válido" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"></asp:RegularExpressionValidator>
        </label>

        <asp:Panel runat="server" ID="pnlUltimoAcesso">
            <label>
                <span>
                    <asp:Label ID="lblDataUltimoAcesso" runat="server" AssociatedControlID="txtDataUltimoAcesso" title="Data do último acesso"><%=Resources.Usuario.DataUltimoAcesso %></asp:Label>
                </span>
                <asp:TextBox runat="server" ID="txtDataUltimoAcesso" CssClass="frmTxt" MaxLength="50" Enabled="false" title="Data do último acesso"></asp:TextBox>
            </label>
        </asp:Panel>
        <input type="text" id="login" style="visibility:hidden;" />
        <asp:Panel runat="server" ID="pnlSenha">
            <label>
                <span>
                    <asp:Label ID="lblSenha" runat="server" AssociatedControlID="txtSenha" title="Senha"><%=Resources.Usuario.Senha %>*</asp:Label>
                </span>
                <asp:TextBox TextMode="Password" runat="server" ID="txtSenha" CssClass="frmTxt" MaxLength="10" title="Informe a Senha"></asp:TextBox>
                <asp:RequiredFieldValidator ID="rfvSenha" runat="server" ControlToValidate="txtSenha" Display="Dynamic" title="Campo obrigatório"></asp:RequiredFieldValidator>
            </label>
        </asp:Panel>
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
            <asp:Button runat="server" ID="btnGerarSenha" CssClass="submit" Text="Gerar Nova Senha" OnClick="btnGerarSenha_Click" CausesValidation="false" title="Gerar Nova Senha" alt="Gerar Nova Senha" />
            <asp:Button runat="server" ID="btnOK" CssClass="submit" Text="Salvar" OnClick="btnOK_Click" title="Salvar" alt="Salvar" />
        </asp:PlaceHolder>
    </div>

</asp:Content>
