<%@ Control Language="C#" AutoEventWireup="true" CodeFile="MenuCircular.ascx.cs" Inherits="Modulos_CMS_Modulos_ModMenuCircular_MenuCircular" %>

<div id="divSemConteudo" runat="server" class="moduloSemConteudo">
    <asp:Label ID="lblTitulo" runat="server"><%= string.Format(Resources.Textos.Modulo_Sem_Conteudo, "Menu Circular") %></asp:Label>
</div>

<div id="divConteudo" runat="server">

    <asp:Repeater runat="server" ID="rptMenuCircular" OnItemDataBound="rptMenuCircular_ItemDataBound">
        <HeaderTemplate>
            <!-- INÍCIO MENU SUBHOMES -->
		    <div class="container-fluid menu-subhome">
                <div class="slider center">
        </HeaderTemplate>
        <ItemTemplate>
                    <div class="btn-circlesubhome">
                    <button id="btnLink" type="button" class="btn btn-bri btn-circlesub btn-xl ico-rel-int" onclick="<%# string.Format("javascript:window.open('{0}', '{1}');", DataBinder.Eval(Container.DataItem, "Url"), DataBinder.Eval(Container.DataItem, "Target")) %>">
                    <asp:Image ID="imgImagem" runat="server" Width="50px" />
                    </button>
                    <p class="nom-bot-circ"><%# DataBinder.Eval(Container.DataItem, "Titulo")%></p>
                    </div>
        </ItemTemplate>
        <FooterTemplate>
                </div>
            </div>
<script>
$(document).ready(function () {
    $('.center').slick({
        centerMode: true,
        infinite: true,
        centerPadding: '60px',
        slidesToShow: 3,
        speed: 500,
        variableWidth: true,
        responsive: [{
            breakpoint: 767,
            settings: {
                arrows: false,
                centerMode: true,
                centerPadding: '40px',
                slidesToShow: 1,
                variableWidth: true
            }
        }]
    });
});
</script>
            <!-- INÍCIO MENU SUBHOMES -->
        </FooterTemplate>
    </asp:Repeater>


</div>

