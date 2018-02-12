<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ListaEvento.ascx.cs" Inherits="Modulos_CMS_Modulos_ModEventos_ListaEventos" %>

<div id="divConteudo" runat="server">

    <!-- INÍCIO DA PÁGINA INTERNA DO MÓDULO DE EVENTOS -->
    <div class="container-fluid container">
        <div class="row">
            <div class="col-centered box-eve-tot">
                <div class="eve-int-tit"><asp:Label ID="lblTitulo" runat="server"></asp:Label></div>
                <div class="eve-int-inf">
                    <span><img class="eve-ico" src="img/ico-aut.png" /><asp:Label ID="lblResponsavel" runat="server"></asp:Label></span>
                    <span><img class="eve-ico" src="img/ico-dat.png" /><asp:Label ID="lblData" runat="server"></asp:Label></span>
                    <span><img class="eve-ico" src="img/ico-loc.png" /><asp:Label ID="lblLocal" runat="server"></asp:Label></span>
                </div>
                <div class="eve-int-txt"><asp:Label ID="lblTexto" runat="server"></asp:Label></div>
            </div>
        </div>
    </div>
<!-- FIM DA PÁGINA INTERNA DO MÓDULO DE EVENTOS -->
    
</div>