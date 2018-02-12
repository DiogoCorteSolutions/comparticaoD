<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ListaEvento.ascx.cs" Inherits="Modulos_CMS_Modulos_ModEventos_ListaEventos" %>

<div id="divSemConteudo" runat="server" class="moduloSemConteudo">
    <asp:Literal ID="litSemConteudo" runat="server" Text="Módulo sem conteúdo."></asp:Literal>
</div>

<div id="divConteudo" runat="server">

    <asp:Label ID="lblTitulo" runat="server"></asp:Label>
    <asp:Label ID="lblData" runat="server"></asp:Label>
    <asp:Label ID="lblHora" runat="server"></asp:Label>    
    <asp:Label ID="lblResponsavel" runat="server"></asp:Label>
    <asp:Label ID="lblLocal" runat="server"></asp:Label>

    <asp:Label ID="lblTexto" runat="server"></asp:Label>
</div>