<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Texto.ascx.cs" Inherits="Modulos_CMS_Modulos_ModTexto_Texto" %>
<div id="divSemConteudo" runat="server" class="moduloSemConteudo">
    <asp:Label ID="lblTitulo" runat="server"><%= string.Format(Resources.Textos.Modulo_Sem_Conteudo, "Texto") %></asp:Label>
</div>

<div id="divConteudo" runat="server">
    <asp:Literal ID="litConteudoHtml" runat="server"></asp:Literal>
</div>
