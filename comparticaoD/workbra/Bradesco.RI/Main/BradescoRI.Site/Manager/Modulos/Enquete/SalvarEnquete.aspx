<%@ Page Language="C#" MasterPageFile="~/Modulos/Modulos.master"  AutoEventWireup="true" CodeFile="SalvarEnquete.aspx.cs" Inherits="Modulos_Enquete_SalvarEnquete" %>

<asp:Content ID="Content1" ContentPlaceHolderID="contentInterna" runat="Server">
    <div id="title">
        <h1>
            <asp:Label ID="lblTitulo" runat="server"><%=Resources.Enquetes.Titulo %></asp:Label>
        </h1>
    </div>
    <br />
    <div class="fields">
        <label>
            <span>
                <asp:Label ID="lblEnquete" runat="server" AssociatedControlID="txtEnquete" title="Grupo"><%=Resources.Enquetes.Enquete %>*</asp:Label>
            </span>
            <asp:TextBox runat="server" ID="txtEnquete" CssClass="txtEnquete" MaxLength="100" title="Informe o nome da enquete"></asp:TextBox>
            <asp:RequiredFieldValidator ID="rfvEnquete" runat="server" ControlToValidate="txtEnquete" Display="Dynamic" title="Campo obrigatório"></asp:RequiredFieldValidator>
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


