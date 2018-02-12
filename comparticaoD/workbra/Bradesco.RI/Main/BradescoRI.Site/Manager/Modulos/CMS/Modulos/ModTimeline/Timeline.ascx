<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Timeline.ascx.cs" Inherits="Modulos_CMS_Modulos_ModTimeline_Timeline" %>

<div id="divSemConteudo" runat="server" class="moduloSemConteudo">
    <asp:Label ID="lblTitulo" runat="server"><%= string.Format(Resources.Textos.Modulo_Sem_Conteudo, "Timeline") %></asp:Label>
</div>

<div id="divConteudo" runat="server">
    <!-- INÍCIO MÓDULO TIMELINE  -->
    <div class="container-fluid container">
        <!-- Começo dos filtros de busca -->
        <div class="row down-filtro">
            <form name="filtros">
                <div class="down-filtro-txt">Filtrar</div>
                <div class="down-filtro-ano">
                    <span class="labelsel">Ano</span>
                    <asp:DropDownList ID="ddlAno" runat="server" CssClass="down-select"></asp:DropDownList>
                </div>
            </form>
        </div>
        <!-- Fim dos filtros de busca -->

        <%--<asp:Literal ID="litTimeLine" runat="server"></asp:Literal>--%>


        <div class="row">
            <section class="cd-horizontal-timeline">

                <!-- CONDICIONAL Início da linha do tempo (se escolher linha do tempo) -->
                <div class="timeline">
                    <div class="events-wrapper">
                        <div class="events">
                            <ol>
                                <asp:Literal ID="litEvents" runat="server"></asp:Literal>
                            </ol>

                            <span class="filling-line" aria-hidden="true"></span>
                        </div>
                        <!-- .events -->
                    </div>
                    <!-- .events-wrapper -->

                    <ul class="cd-timeline-navigation">
                        <li><a href="#0" class="prev inactive">Prev</a></li>
                        <li><a href="#0" class="next">Next</a></li>
                    </ul>
                    <!-- .cd-timeline-navigation -->
                </div>
                <!-- CONDICIONAL - Fim da linha do tempo (se escolher linha do tempo) -->

                <div class="events-content">

                    <ol>
                        <asp:Literal ID="litContent" runat="server"></asp:Literal>
                    </ol>
                    <div class="cd-timeline-navigation-box">
                        <a href="#" class="prev">Prev</a>
                        <a href="#" class="next">Next</a>
                    </div>
                </div>
                <!-- .events-content -->
            </section>
        </div>
    </div>
    <!-- FIM DO MÓDULO TIMELINE  -->

</div>


