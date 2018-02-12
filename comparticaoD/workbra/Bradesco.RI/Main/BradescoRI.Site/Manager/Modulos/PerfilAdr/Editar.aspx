<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Editar.aspx.cs" Inherits="Modulos_PerfilAdr_Editar" MasterPageFile="~/Modulos/Modulos.master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="contentInterna" runat="Server">
    <div id="title">
        <h1>
            <asp:Label ID="lblTitulo" runat="server"><%=Resources.PerfilAdr.Titulo %></asp:Label>
        </h1>
    </div>
    <br />
    <div class="fields">
        <label>
            <span>
                <asp:Label ID="lblIdioma" runat="server" AssociatedControlID="ddlIdioma" title="Perfil"><%=Resources.PerfilAdr.Idioma%>*</asp:Label>
            </span>
            <asp:DropDownList runat="server" ID="ddlIdioma" title="Selecione o Idioma"></asp:DropDownList>
            <asp:RequiredFieldValidator ID="rfvIdioma" runat="server" ControlToValidate="ddlIdioma" InitialValue="0" Display="Dynamic" title="Campo obrigatório"></asp:RequiredFieldValidator>
        </label>

        <label>
            <span>
                <asp:Label ID="lblTexto" runat="server" AssociatedControlID="txtTexto" title="Título"><%=Resources.PerfilAdr.Texto %>*</asp:Label>
            </span>
            <asp:TextBox runat="server" ID="txtTexto" CssClass="frmTxt" MaxLength="200" title="Informe o título"></asp:TextBox>
            <asp:RequiredFieldValidator ID="rfvTexto" runat="server" ControlToValidate="txtTexto" Display="Dynamic" title="Campo obrigatório"></asp:RequiredFieldValidator>
        </label>

        <label>
            <span>
                <asp:Label ID="lblValor" runat="server" AssociatedControlID="txtValor" title="Valor"><%=Resources.PerfilAdr.Valor %>*</asp:Label>
            </span>
            <asp:TextBox runat="server" ID="txtValor" CssClass="frmTxt" MaxLength="50" title="Informe o valor"></asp:TextBox>
            <asp:RequiredFieldValidator ID="rfvValor" runat="server" ControlToValidate="txtValor" Display="Dynamic" title="Campo obrigatório"></asp:RequiredFieldValidator>
        </label>

        <label>
            <span>
                <asp:Label ID="lblOrdem" runat="server" AssociatedControlID="txtOrdem" title="Ordem"><%=Resources.PerfilAdr.Ordem %>*</asp:Label>
            </span>
            <asp:TextBox runat="server" ID="txtOrdem" CssClass="frmTxt txtNumero3" MaxLength="200" title="Informe a ordem de exibição"></asp:TextBox>
            <asp:RequiredFieldValidator ID="rfvOrdem" runat="server" ControlToValidate="txtOrdem" Display="Dynamic" title="Campo obrigatório"></asp:RequiredFieldValidator>
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
