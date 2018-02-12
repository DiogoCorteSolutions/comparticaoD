<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Caixa.ascx.cs" Inherits="Modulos_CMS_Modulos_ModCaixa_Caixa" %>

<div id="divSemConteudo" runat="server" class="moduloSemConteudo">
    <asp:Label ID="lblTitulo" runat="server"><%= string.Format(Resources.Textos.Modulo_Sem_Conteudo, "Caixas") %></asp:Label>
</div>

<div id="divConteudo" runat="server">

    <asp:Repeater runat="server" ID="rptCaixas" OnItemDataBound="rptCaixas_ItemDataBound">
        <HeaderTemplate>
            <!-- MÓDULO DE 3 CAIXAS COM ÍCONE DE CADEADO (Estilos caixa)  -->
            <div class="container-fluid container">
                <div class="row">
                    <div class="horizon-swiper horizon-box-swiper">
        </HeaderTemplate>
        <ItemTemplate>
            <div class="col-md-3 horizon-item box-tab">
                <button type="button" class="btn btn-bri btn-circle btn-xl btn-cad">
                    <asp:Image ID="imgImagem" runat="server" width="35" height="35" />
                <div class="quad-tit"><%# DataBinder.Eval(Container.DataItem, "Titulo")%></div>
                <div class="quad-txt"><%# DataBinder.Eval(Container.DataItem, "Descricao")%></div>
            </div>
        </ItemTemplate>
        <FooterTemplate>
            </div><!-- /.horizon-box-swiper -->
            </div><!-- /. row -->
            </div>

        </FooterTemplate>
    </asp:Repeater>




</div>
