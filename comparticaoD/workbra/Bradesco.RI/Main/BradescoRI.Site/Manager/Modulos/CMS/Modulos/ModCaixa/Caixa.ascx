<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Caixa.ascx.cs" Inherits="Modulos_CMS_Modulos_ModCaixa_Caixa" %>

<div id="divSemConteudo" runat="server" class="moduloSemConteudo">
    <asp:Label ID="lblTitulo" runat="server"><%= string.Format(Resources.Textos.Modulo_Sem_Conteudo, "Caixas") %></asp:Label>
</div>

<div id="divConteudo" runat="server">

    <asp:Repeater runat="server" ID="rptCaixas" OnItemDataBound="rptCaixas_ItemDataBound">
        <HeaderTemplate>
            <!-- MÓDULO DE 3 CAIXAS COM ÍCONE DE CADEADO (Estilos caixa)  -->
            <div class="container">
                <div class="row horizon-caixas">
                    <div class="col-lg-12 col-lg-offset-0 col-md-12 col-md-offset-0 bloco-caixas">
                    <div class="horizon-swiper horizon-box-swiper">
        </HeaderTemplate>
        <ItemTemplate>
            <div class="col-md-4 horizon-item">
                <div class="box-tab">
                <button type="button" class="btn btn-bri btn-circle btn-xl btn-cad">
                    <asp:Image ID="imgImagem" runat="server" />
                    </button>
                <div class="quad-tit"><%# DataBinder.Eval(Container.DataItem, "Titulo")%></div>
                <div class="quad-txt"><%# DataBinder.Eval(Container.DataItem, "Descricao")%></div>
                </div>
            </div>
        </ItemTemplate>
        <FooterTemplate>
            </div><!-- /.horizon-box-swiper -->
            </div><!-- /.col-lg-12 -->
            </div><!-- /. row horizon-caixas-->
            </div>

        </FooterTemplate>
    </asp:Repeater>




</div>
