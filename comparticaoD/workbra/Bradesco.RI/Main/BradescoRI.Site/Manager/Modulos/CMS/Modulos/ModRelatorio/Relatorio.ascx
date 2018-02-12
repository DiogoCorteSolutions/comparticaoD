<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Relatorio.ascx.cs" Inherits="Modulos_CMS_Modulos_ModRelatorio_Relatorio" %>

<div id="divSemConteudo" runat="server" class="moduloSemConteudo">
    <asp:Label ID="lblTitulo" runat="server"><%= string.Format(Resources.Textos.Modulo_Sem_Conteudo, "Relatórios e Comunicados") %></asp:Label>
</div>

<div id="divComCOnteudo" runat="server" class="moduloComConteudo">
    <div class="row">
        <div class="col-lg-12 col-lg-offset-0 col-md-12 col-md-offset-0 col-sm-12 col-sm-offset-0 top-row-1">
        <div class="container-relat-com">
            <ul class="nav nav-tabs tabs-relat-com">
                <li class="active"><a data-toggle="tab" href="#home">Principais Relatórios</a></li>
                <li><a data-toggle="tab" href="#menu1">Últimos Comunicados</a></li>
            </ul>
            <div class="tab-content">
                <div id="home" class="tab-pane tab-relat-com fade in active col-md-6 col-sm-6 menu-rel-com box-tab-esq">
                    <h3>Principais Relatórios</h3>
                    <hr class="linha-curta" />
                    <asp:Literal ID="litTipoRelatorio" runat="server"></asp:Literal>
                    
                </div>
                <div id="menu1" class="tab-pane tab-relat-com fade col-md-6 col-sm-6 menu-rel-com box-tab-dir">
                    <h3>Últimos Comunicados</h3>
                    <hr class="linha-curta" />
                    <asp:Literal ID="litComunicado" runat="server"></asp:Literal>
                </div>
            </div>

        </div>
        <span class="col-lg-12 col-lg-offset-0 col-md-12 col-md-offset-0 col-sm-12 col-sm-offset-0 faixa-cinza2"></span>
 </div>
    </div><!-- Fim de Row #1 - Relatórios e Cominicados -->

</div>
