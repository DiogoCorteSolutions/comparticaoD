<%@ Page Language="C#" MasterPageFile="~/Modulos/Modulos.master"  AutoEventWireup="true" CodeFile="SalvarGrupo.aspx.cs" Inherits="Modulos_Links_SalvarGrupo" %>

<asp:Content ID="Content1" ContentPlaceHolderID="contentInterna" runat="Server">
    <div id="title">
        <h1>
            <asp:Label ID="lblTitulo" runat="server"><%=Resources.Links.Titulo %></asp:Label>
        </h1>
    </div>
    <br />
    <div class="fields">
        <label>
            <span>
                <asp:Label ID="lblGrupo" runat="server" AssociatedControlID="txtGrupo" title="Grupo"><%=Resources.Links.Grupo %>*</asp:Label>
            </span>
            <asp:TextBox runat="server" ID="txtGrupo" CssClass="frmTxt" MaxLength="200" title="Informe o nome do grupo"></asp:TextBox>
            <asp:RequiredFieldValidator ID="rfvGrupo" runat="server" ControlToValidate="txtGrupo" Display="Dynamic" title="Campo obrigatório"></asp:RequiredFieldValidator>
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

