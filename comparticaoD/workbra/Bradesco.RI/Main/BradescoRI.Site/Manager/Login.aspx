<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Login.aspx.cs" Inherits="Login" MasterPageFile="~/MasterPage.master" %>

<asp:Content ContentPlaceHolderID="head" runat="Server"></asp:Content>
<asp:Content ContentPlaceHolderID="content" runat="Server">
    <header>
        <img src="Imagens/logo.png" class="logo" width="225" height="60" alt="Logo Bradesco" title="Logo Bradesco">
        <label for="menu-togle" class="navbar-btn" title="Desabilitar/Habilitar Menu">

            <span class="sr-only">Toggle navigation</span>
            <span class="icon-bar"></span>
            <span class="icon-bar"></span>
            <span class="icon-bar"></span>

        </label>

    </header>
    <input id="menu-togle" type="checkbox" />
    <main class="message">
        <section class="login">

            <h2><%=Resources.Login.Titulo %></h2>

            <asp:PlaceHolder runat="server" ID="phlMensagem" Visible="false">
                <p style="width: 15em;">
                    <asp:Label ID="lblMensagem" runat="server" ToolTip="Mensagem" Font-Bold="true"></asp:Label>
                </p>
            </asp:PlaceHolder>

            <asp:Panel ID="pnlLogin" runat="server">
                <label>
                    <asp:Label ID="lblLogin" runat="server" ToolTip="Login"><%=Resources.Login.Campo_Login %></asp:Label>
                    <asp:TextBox runat="server" ID="txtLogin" MaxLength="50" ToolTip="Informe o login"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="rfvLogin" runat="server" ControlToValidate="txtLogin" Text="*" ForeColor="Red" ToolTip="Preenchimento obrigatório"></asp:RequiredFieldValidator>
                </label>
                <label>
                    <asp:Label ID="lblSenha" runat="server" ToolTip="Senha"><%=Resources.Login.Campo_Senha %></asp:Label>
                    <asp:TextBox TextMode="Password" runat="server" ID="txtSenha" MaxLength="50" ToolTip="Informe a senha"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="rfvSenha" runat="server" ControlToValidate="txtSenha" Text="*" ForeColor="Red" ToolTip="Preenchimento obrigatório"></asp:RequiredFieldValidator>
                </label>
                <div class="no-label">
                    <asp:Button runat="server" ID="btSubmit" Text="OK" OnClick="btSubmit_Click" ToolTip="OK" />
                    <asp:Button runat="server" ID="btnEsqueciSenha" Text="Esqueci minha senha" OnClick="btSubmitEsqueciSenha_Click" CausesValidation="false" ToolTip="Esqueci minha senha" />
                </div>
            </asp:Panel>

            <asp:Panel ID="pnlEsqueciSenha" runat="server" Visible="false">
                <label>
                    <asp:Label ID="lblEmailEsqueci" runat="server" ToolTip="Email"><%=Resources.Login.Campo_Esqueci_Senha_Email %></asp:Label>
                    <asp:TextBox runat="server" ID="txtEmailEsqueci" MaxLength="50" ToolTip="Informe Email"></asp:TextBox>
                </label>
                <div class="no-label">
                    <asp:Button runat="server" ID="btnEnviarEsqueci" Text="Enviar" OnClick="btnEnviarEsqueci_Click" ToolTip="Enviar" />
                    <asp:Button runat="server" ID="btnVoltar" Text="Voltar" OnClick="btnVoltar_Click" ToolTip="Voltar" />
                </div>
            </asp:Panel>

            <asp:Panel ID="pnlPrimeiroAcesso" runat="server" Visible="false">

                <p>
                    <%=Resources.Login.Mensagem_Trocar_Senha %>
                </p>

                <label style="text-align: center; display: block;">
                    <asp:Label ID="lblNovaSenha" runat="server" ToolTip="Nova senha" Style="text-align: center;"><%=Resources.Login.Campo_Nova_Senha %></asp:Label>
                    <asp:TextBox TextMode="Password" runat="server" ID="txtSenhaNova" MaxLength="50" ToolTip="Informe nova senha"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="rfvSenhaNova" runat="server" ControlToValidate="txtSenhaNova" Text="*" ForeColor="Red" Display="Dynamic" ToolTip="Preenchimento obrigatório"></asp:RequiredFieldValidator>
                </label>

                <label style="text-align: center; display: block;">
                    <asp:Label ID="lblNovaSenhaConfirma" runat="server" ToolTip="Nova senha" Style="text-align: center;"><%=Resources.Login.Campo_Nova_Senha_Confirma %></asp:Label>
                    <asp:TextBox TextMode="Password" runat="server" ID="txtSenhaNovaConfirma" MaxLength="50" ToolTip="Confirme nova senha"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="rfvSenhaNovaConfirma" runat="server" ControlToValidate="txtSenhaNovaConfirma" Text="*" ForeColor="Red" Display="Dynamic" ToolTip="Preenchimento obrigatório"></asp:RequiredFieldValidator>
                </label>

                <div class="no-label" style="margin-left: 0 !important; width: auto !important; text-align: center !important;">
                    <asp:Button runat="server" ID="btnSalvarNovaSenha" Text="Salvar" OnClick="btnSalvarNovaSenha_Click" ToolTip="Salvar" />
                </div>
            </asp:Panel>

            <div class="border bottom">
                <div class="version">
                    <span><%=Resources.Textos.Versao_Aplicaco %></span>
                </div>
            </div>

        </section>
    </main>


</asp:Content>