<%@ Control Language="C#" AutoEventWireup="true" CodeFile="MenuCircular.ascx.cs" Inherits="Modulos_CMS_Modulos_ModMenuCircular_MenuCircular" %>

<div id="divSemConteudo" runat="server" class="moduloSemConteudo">
    <asp:Label ID="lblTitulo" runat="server"><%= string.Format(Resources.Textos.Modulo_Sem_Conteudo, "Menu Circular") %></asp:Label>
</div>

<div id="divConteudo" runat="server">

    <asp:Repeater runat="server" ID="rptMenuCircular" OnItemDataBound="rptMenuCircular_ItemDataBound">
        <HeaderTemplate>
            <!-- Row #2 - subMenu circular -->
            <div class="row horizon-circ">
                <div class="col-lg-12 col-lg-offset-0 col-md-12 col-md-offset-0 col-sm-12 col-sm-offset-0 top-row-1">
                    <div class="horizon-swiper menu-circ-gray">
        </HeaderTemplate>
        <ItemTemplate>
            <div class="horizon-item box">
                <button id="btnLink" type="button" class="btn btn-bri btn-circle btn-xl ico-rel-int" onclick="<%# string.Format("javascript:window.open('{0}', '{1}');", DataBinder.Eval(Container.DataItem, "Url"), DataBinder.Eval(Container.DataItem, "Target")) %>">
                    <asp:Image ID="imgImagem" runat="server" Width="50px" />
                </button>
                <p class="nom-bot-circ"><%# DataBinder.Eval(Container.DataItem, "Titulo")%></p>
            </div>
        </ItemTemplate>
        <FooterTemplate>
                    </div>
                </div>
            </div>
        </FooterTemplate>
    </asp:Repeater>


</div>

