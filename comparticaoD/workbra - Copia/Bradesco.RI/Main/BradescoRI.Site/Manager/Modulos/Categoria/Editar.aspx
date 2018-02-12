<%@ Page Language="C#" MasterPageFile="~/Modulos/Modulos.master" AutoEventWireup="true" CodeFile="Editar.aspx.cs" Inherits="Modulos_CMS_Paginas_EditarArea" %>

<asp:Content ID="Content1" ContentPlaceHolderID="contentInterna" runat="Server">

    <div id="title">
        <h1>
            <asp:Label ID="lblTitulo" runat="server"><%=Resources.Categoria.Titulo %></asp:Label>
        </h1>
    </div>
    <br />
    <div class="fields">
        <label>
            <span>
                <asp:Label ID="lblDescricao" runat="server" AssociatedControlID="txtDescricao" title="Área"><%=Resources.Categoria.Descricao %>*</asp:Label>
            </span>
            <asp:TextBox runat="server" ID="txtDescricao" CssClass="frmTxt" MaxLength="200" title="Informe a área"></asp:TextBox>
            <asp:RequiredFieldValidator ID="rfvDescricao" runat="server" ControlToValidate="txtDescricao" Display="Dynamic" title="Campo obrigatório"></asp:RequiredFieldValidator>
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
