<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Header.ascx.cs" Inherits="Modulos_CMS_Modulos_Header_Header" %>

<div id="divSemConteudo" runat="server" class="moduloSemConteudo">
    <asp:Label ID="lblTituloSemConteudo" runat="server"><%= string.Format(Resources.Textos.Modulo_Sem_Conteudo, "Header") %></asp:Label>
</div>

<div id="divConteudo" runat="server">

    <!-- Header de Página - cor branca -->
    <div class="div-branco-top">
        <asp:Image ID="imgImagemModulo" runat="server" />
        <div class="container-fluid container">
            <div class="row page-header-branco">
                <div class="col-lg-8 col-lg-offset-2 col-md-10 col-md-offset-1">

                    <div class="site-heading">
                        <ol class="breadcrumb glyphicon glyphicon-chevron-right">
                            <asp:Literal ID="litBreadcrumb" runat ="server"></asp:Literal>
                        </ol>
                        <h1 class="heading-inter">
                            <asp:Label ID="lblTitulo" runat="server"></asp:Label></h1>
                        <p class="subheading-inter">
                            <asp:Label ID="lblSubtitulo" runat="server"></asp:Label>
                        </p>
                    </div>
                </div>
            </div>
        </div>
    </div>

</div>
