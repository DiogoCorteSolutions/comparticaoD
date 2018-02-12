<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Modulos/Modulos.master" CodeFile="Default.aspx.cs" Inherits="Modulos_Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="contentInterna" runat="Server">
    <div class="bem-vindo">
        Bem Vindo a Área Administrativa
    </div>
    <div class="bem-vindo-subtitulo">Bradesco | RI</div>
    <div class="bem-vindo-usuario">
        Usuário:
        <asp:Label ID="lblUsuarioLogado" runat="server"></asp:Label>
    </div>
    <div class="bem-vindo-usuario">
        <asp:Label ID="lblVan" runat="server"></asp:Label>
    </div>
</asp:Content>

