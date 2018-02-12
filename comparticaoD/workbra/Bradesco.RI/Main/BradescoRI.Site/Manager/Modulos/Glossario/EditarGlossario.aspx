<%@ Page Title="" Language="C#" MasterPageFile="~/Modulos/Modulos.master" AutoEventWireup="true" CodeFile="EditarGlossario.aspx.cs" Inherits="Modulos_Glossario_EditarGlossario" %>

<asp:Content ID="Content1" ContentPlaceHolderID="contentInterna" runat="Server">
    <div id="title">
        <h1>
            <asp:Label ID="lblTitulo" runat="server"><%=Resources.Glossario.Titulo %></asp:Label>
            <asp:HiddenField ID="hdnGlossarioId" runat="server" />
        </h1>
    </div>
    <br />
    <div>
        <label>
            <span>
                <asp:Label ID="lblIdioma" runat="server" AssociatedControlID="ddlIdioma" title="Idioma"><%=Resources.Glossario.LabelIdioma %>*</asp:Label>
            </span>
            <asp:DropDownList runat="server" ID="ddlIdioma" title="Selecione o Idioma"></asp:DropDownList>
            <asp:RequiredFieldValidator ID="rfvidioma" runat="server" ControlToValidate="ddlIdioma" ValidationGroup="vgrGlossario" Display="Dynamic" title="Campo obrigatório"></asp:RequiredFieldValidator>
        </label>
    </div>
    <div>
        <label>
            <span>
                <asp:Label ID="lblTituloArquivo" runat="server" AssociatedControlID="txtTitulo" title="Resumo"><%=Resources.Glossario.LabelTitulo %>*</asp:Label>
            </span>
            <asp:TextBox runat="server" TextMode="SingleLine" ID="txtTitulo" CssClass="frmTxt" MaxLength="100" title="Informe o Titulo"></asp:TextBox>
            <asp:RequiredFieldValidator ID="rfvtitulo" runat="server" ControlToValidate="txtTitulo" ValidationGroup="vgrGlossario" Display="Dynamic" title="Campo obrigatório"></asp:RequiredFieldValidator>
        </label>
    </div>
    <div>
        <label>
            <span>
                <asp:Label ID="lblDescricaoArquivo" runat="server" AssociatedControlID="txtDescricaoArquivo" title="Idioma"><%=Resources.Glossario.LabelDescricao %>*</asp:Label>
            </span>
            <asp:TextBox  runat="server" TextMode="MultiLine" Rows="5" ID="txtDescricaoArquivo" CssClass="frmTxt" MaxLength="1000" title="Informe a descrição"></asp:TextBox>
            <asp:RequiredFieldValidator ID="rfvDescricao" runat="server" ControlToValidate="txtDescricaoArquivo" ValidationGroup="vgrGlossario" Display="Dynamic" title="Campo obrigatório"></asp:RequiredFieldValidator>
        </label>
    </div>
    <div class="btn-acoes2">
        <asp:PlaceHolder runat="server" ID="phOptions" Visible="true">
            <asp:Button runat="server" ID="btnCancelar" CssClass="cancelar" Text="Voltar" OnClick="btnCancelar_Click" CausesValidation="false" title="Cancelar" alt="Cancelar" />
            <asp:Button runat="server" ID="btnSalvar" CssClass="submit" Text="Salvar" ValidationGroup="vrgArquivos" OnClick="btnSalvar_Click" title="Salvar" alt="Salvar" />
        </asp:PlaceHolder>
    </div>
</asp:Content>


