<%@ Page Language="C#" AutoEventWireup="true" CodeFile="TraducoesEditar.aspx.cs" Inherits="Modulos_Menus_TraducoesEditar" MasterPageFile="~/Modulos/Modulos.master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="contentInterna" runat="Server">
    <div id="title">
        <h1>
            <asp:Label ID="lblTitulo" runat="server"><%=Resources.Menu.TraducoesTitulo %></asp:Label>
        </h1>
    </div>
    <br />
    <div class="fields">
        <label>
            <span>
                <asp:Label ID="lblCultura" runat="server" AssociatedControlID="ddlIdioma" title="Cultura"><%=Resources.Menu.Cultura %>*</asp:Label>
            </span>
            <asp:DropDownList ID="ddlIdioma" runat="server" ToolTip="Informe o idioma"></asp:DropDownList>
        </label>

        <label>
            <span>
                <asp:Label ID="lblChave" runat="server" AssociatedControlID="txtChave" title="Chave de Tradução do Menu"><%=Resources.Menu.ChaveTraducao %>*</asp:Label>
            </span>
            <asp:TextBox runat="server" ID="txtChave" CssClass="frmTxt" MaxLength="50" title="Informe a chave de tradução"></asp:TextBox>
            <asp:RequiredFieldValidator ID="rfvChave" runat="server" ControlToValidate="txtChave" Display="Dynamic" title="Campo obrigatório"></asp:RequiredFieldValidator>
        </label>

        <label>
            <span>
                <asp:Label ID="lblTexto" runat="server" AssociatedControlID="txtTexto" title="Texto"><%=Resources.Menu.Texto %>*</asp:Label>
            </span>
            <asp:TextBox runat="server" ID="txtTexto" CssClass="frmTxt" MaxLength="200" title="Informe o texto"></asp:TextBox>
            <asp:RequiredFieldValidator ID="rfvTexto" runat="server" ControlToValidate="txtTexto" Display="Dynamic" title="Campo obrigatório"></asp:RequiredFieldValidator>
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
