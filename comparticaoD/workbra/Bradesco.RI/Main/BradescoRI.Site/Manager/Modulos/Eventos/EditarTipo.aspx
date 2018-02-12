<%@ Page Language="C#" MasterPageFile="~/Modulos/Modulos.master" AutoEventWireup="true" CodeFile="EditarTipo.aspx.cs" Inherits="Modulos_Eventos_EditarTipo" %>


<asp:Content ID="Content1" ContentPlaceHolderID="contentInterna" runat="Server">

    <div id="title">
        <h1>
            <asp:Label ID="lblTitulo" runat="server"><%=Resources.Eventos.TituloTipoEvento %></asp:Label>
        </h1>
    </div>
    <br />
    <div class="fields">
        <label>
            <span>
                <asp:Label ID="lblIdioma" runat="server" AssociatedControlID="ddlIdioma" title="Idioma"><%=Resources.Eventos.Idioma %>*</asp:Label>
            </span>
            <asp:DropDownList runat="server" ID="ddlIdioma" title="Selecione o Idioma"></asp:DropDownList>
            <asp:RequiredFieldValidator ID="rfvidioma" runat="server" ControlToValidate="ddlIdioma" Display="Dynamic" title="Campo obrigatório"></asp:RequiredFieldValidator>
        </label>
        <label>
            <span>
                <asp:Label ID="lblDescricao" runat="server" AssociatedControlID="txtDescricao" title="Descrição"><%=Resources.Eventos.Descricao %>*</asp:Label>
            </span>
            <asp:TextBox runat="server" ID="txtDescricao" CssClass="frmTxt" MaxLength="200" title="Informe a descrição"></asp:TextBox>
            <asp:RequiredFieldValidator ID="rfvDescricao" runat="server" ControlToValidate="txtDescricao" Display="Dynamic" title="Campo obrigatório"></asp:RequiredFieldValidator>
        </label>
        <label>
            <span>
                <asp:Label ID="lblCor" runat="server" AssociatedControlID="txtDescricao" title="Descrição"><%=Resources.Eventos.Descricao %>*</asp:Label>
            </span>
            <asp:DropDownList runat="server" ID="ddlCor" AutoPostBack="true" OnSelectedIndexChanged="ddlCor_SelectedIndexChanged" title="Selecione a cor"></asp:DropDownList>
            <asp:Image ID="imgCor" Width="30px" Height="30px" ImageUrl="#" BackColor="White" runat="server" />
            <asp:RequiredFieldValidator ID="rfvCor" runat="server" ControlToValidate="ddlCor" Display="Dynamic" title="Campo obrigatório"></asp:RequiredFieldValidator>

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


