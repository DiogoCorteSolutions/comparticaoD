<%@ Control Language="C#" AutoEventWireup="true" CodeFile="JCP.ascx.cs" Inherits="Modulos_CMS_Modulos_ModJCP_JCP" %>


<div id="divSemConteudo" runat="server" class="moduloSemConteudo-header">
    <asp:Label ID="lblTitulo" runat="server"><%= string.Format(Resources.Textos.Modulo_Sem_Conteudo, "JCP") %></asp:Label>
</div>

<div id="divConteudo" runat="server">
    <!-- INÍCIO DO MÓDULO JCP  -->
    <div class="row jcp-tot">
        <div class="jcp-tit"><%= Resources.JCP.Titulo %></div>
        <!-- Nav tabs -->
        <ul class="nav nav-tabs" role="tablist">
            <asp:Literal ID="litPresentation" runat="server"></asp:Literal>
        </ul>
        <!-- Tab panes -->
        <div class="tab-content">
            <asp:Literal ID="litTable" runat="server"></asp:Literal>
        </div>
    </div>
    <!-- FIM DO MÓDULO JCP  -->
</div>
