<%@ Control Language="C#" AutoEventWireup="true" CodeFile="BannersHeader.ascx.cs" Inherits="Modulos_CMS_Modulos_ModBanners_Banners" %>

<div id="divSemConteudo" runat="server" class="moduloSemConteudo-header">
    <asp:Label ID="lblTitulo" runat="server"><%= string.Format(Resources.Textos.Modulo_Sem_Conteudo, "Header - Slider") %></asp:Label>
</div>

<div id="divConteudo" runat="server">

    <!-- <!-- Carrossel/ Slider Superior Grande - com thumbnails -->
    <asp:Literal ID="litdiv" runat="server"></asp:Literal>

    <!-- Indicators -->
    <ol class="carousel-indicators">
        <asp:Literal ID="litIndicators" runat="server"></asp:Literal>
    </ol>

    <!-- Wrapper for slides -->
    <div class="carousel-inner" role="listbox">
        <asp:Literal ID="litSlides" runat="server"></asp:Literal>
    </div>

    <asp:Literal ID="litButtons" runat="server"></asp:Literal>
    <div class="faixa-cinza">
        <div class="container-fluid container">
            <div class="row">
                <div class="col-lg-12 col-lg-offset-0 col-md-12 col-md-offset-0 col-sm-12 col-sm-offset-0 top-row-1">
                    <span class="col-lg-12 col-lg-offset-0 col-md-12 col-md-offset-0 col-sm-12 col-sm-offset-0 faixa-cinza-int"></span>
                </div>
            </div>
        </div>
    </div>

    <asp:Literal ID="litFechaDiv" runat="server"></asp:Literal>
    <!-- /.fim de Carrossel/ Slider Grande -->

</div>
