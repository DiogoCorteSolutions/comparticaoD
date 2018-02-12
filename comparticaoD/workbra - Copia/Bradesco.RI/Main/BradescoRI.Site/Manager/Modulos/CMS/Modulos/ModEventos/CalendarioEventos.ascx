<%@ Control Language="C#" AutoEventWireup="true" CodeFile="CalendarioEventos.ascx.cs" Inherits="Modulos_CMS_Modulos_ModEventos_CalendarioEventos" %>


<div id="divConteudo" runat="server">

    <div class="row">
        <div class="col-centered box-eve-tot">

            <div class="container-fluid container">
                <!-- BARRA DE FILTRAGEM DE DADOS  -->
                <div class="row eve-filtro">
                    <form name="form_filtro" id="form_filtro">
                        <div class="eve-filtro-ano">
                            <span class="labelsel">Ano</span>
                            <select class="eve-select" name="ano" id="ano">
                                <option role="menuitem" value="2008">2008</option>
                                <option role="menuitem" value="2009">2009</option>
                                <option role="menuitem" value="2010">2010</option>
                                <option role="menuitem" value="2011">2011</option>
                                <option role="menuitem" value="2012">2012</option>
                                <option role="menuitem" value="2013">2013</option>
                                <option role="menuitem" value="2014">2014</option>
                                <option role="menuitem" value="2015">2015</option>
                                <option role="menuitem" value="2016">2016</option>
                                <option role="menuitem" value="2017">2017</option>
                            </select>
                        </div>
                        <div class="eve-filtro-tipo">
                            <asp:DropDownList ID="ddlTipoEvento" runat="server"></asp:DropDownList>
                        </div>
                    </form>
                </div>
                <!-- FIM DA BARRA DE FILTRAGEM DE DADOS  -->
            </div>

            <div class="row">

                <div class="col-xs-6 box-eve-cal">
                    <asp:Calendar ID="Calendario" runat="server" ShowGridLines="false" TitleFormat="Month" FirstDayOfWeek="Sunday" OnDayRender="Calendario_DayRender" OnVisibleMonthChanged="Calendario_VisibleMonthChanged">
                        <NextPrevStyle CssClass="" />
                        <DayStyle CssClass="estilodata" HorizontalAlign="Center" VerticalAlign="Middle"/>
                        <OtherMonthDayStyle ForeColor="Gray" />
                    </asp:Calendar>
                </div>
                <div class="col-xs-6 box-eve-rel">
                    <div class="row">Datas Comemorativas</div>
                    <div class="row">Eventos Institucionais</div>
                    <div class="row">Período de Silêncio</div>
                    <div class="row">Integração de Agenda</div>

                    <asp:Repeater runat="server" ID="rptEventosMes" OnItemDataBound="rptEventosMes_ItemDataBound">
                        <HeaderTemplate>
                        </HeaderTemplate>
                        <ItemTemplate>
                            <div class="row">
                                <asp:Label ID="lblEventoMes" runat="server"></asp:Label>
                            </div>
                        </ItemTemplate>
                        <FooterTemplate>
                        </FooterTemplate>
                    </asp:Repeater>
                </div>
            </div>

            <br />
            <br />
            <br />
            <br />
            <br />
            <br />
            <br />
            <br />
            <br />

            <asp:Repeater runat="server" ID="rptProximosEventos" OnItemDataBound="rptProximosEventos_ItemDataBound">
                <HeaderTemplate>
                    <div class="row">
                        <div class="col-md-12 box-eve-txt">
                            Próximos Eventos
                        </div>
                    </div>
                </HeaderTemplate>
                <ItemTemplate>
                    <!-- INICIO da inserção próximos eventos sendo um por linha -->
                    <div class="col-md-12">
                        <!-- INICIO da caixa de próximos eventos -->
                        <!-- Próximo evento 1 -->
                        <div class="row">
                            <div class="col-md-12 box-eve">
                                <div class="row">
                                    <!-- Lado esquerdo -->
                                    <div class="col-xs-2 box-eve-esq">
                                        <div class="row box-eve-dia">
                                            <asp:Label ID="lblDia" runat="server"></asp:Label>
                                        </div>
                                        <div class="row box-eve-mes">
                                            <asp:Label ID="lblMes" runat="server"></asp:Label>
                                        </div>
                                    </div>
                                    <!-- Fim do lado esquerdo -->
                                    <!-- Lado direito -->
                                    <div class="col-xs-10 box-eve-dir">
                                        <div class="row">
                                            <div class="col-xs-12 box-eve-tit"><%# DataBinder.Eval(Container.DataItem, "Titulo")%></div>
                                        </div>
                                        <div class="row">
                                            <div class="col-xs-12 box-eve-dat">
                                                <asp:Label ID="lblData" runat="server"></asp:Label>
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="col-xs-12 box-eve-cid"><%# DataBinder.Eval(Container.DataItem, "Cidade")%></div>
                                        </div>
                                    </div>
                                    <!-- Fim do lado direito -->
                                </div>
                            </div>
                        </div>
                        <!-- INICIO do link Compartilhe -->
                        <div class="row">
                            <div class="col-md-12 box-eve-lnk">
                                <div class="pull-right">
                                    <img src="img/icone-corrente.png" width="15" height="15" /><a class="compartilhe-lnk" href="#">Compartilhar link</a>
                                </div>
                            </div>
                        </div>
                        <!-- FIM do link Compartilhe -->
                    </div>
                    <!-- FIM da inserção próximos eventos sendo um por linha -->
                </ItemTemplate>
                <FooterTemplate>
                </FooterTemplate>
            </asp:Repeater>

        </div>

        <!-- INÍCIO DO CONTEÙDO COM LARGURA MENOR -->
        <div class="row">
            <div class="col-centered box-eve-tot">
                <!-- INÍCIO DO TEXTO PERÌODO DE SILÊNCIO -->
                <div class="eve-siltxt-tit">Perído de Silêncio</div>
                <br />
                <div class="eve-siltxt-con">
                    Formalizado o Período de Silêncio no documento Procedimentos de Divulgação de Resultados, que antecede em 15 dias corridos a divulgação de resultados, no qual o Bradesco não poderá prestar esclarecimentos ou discutir com o Mercado qualquer tipo de informação relacionada aos referidos Demonstrações Financeiras.
                </div>
                <!-- FIM DO TEXTO PERÌODO DE SILÊNCIO -->
            </div>
        </div>
        <!-- FIM DO CONTEÙDO COM LARGURA MENOR -->
        <div class="row eve-filtro">
            <form name="form_periodo" id="form_periodo">
                <div class="eve-filtro-ano">
                    <span class="labelsel">Período</span>
                    <select class="eve-select" name="periodo" id="periodo">
                        <option role="menuitem" value="anual">Anual</option>
                        <option role="menuitem" value="semestral">Semestral</option>
                        <option role="menuitem" value="trimestral">Trimestral</option>
                    </select>
                </div>
            </form>
        </div>
        <!-- FIM DA BARRA DE FILTRAGEM DE DADOS  -->
        <!-- INÍCIO DO CONTEÙDO COM LARGURA MENOR -->
        <div class="row">
            <div class="col-centered box-eve-tot">
                <!-- INÍCIO DO QUADRO DE DIVULGAÇÂO DO PERÌODO DE SILÊNCIO -->
                <div class="row box-eve-calleg">
                    <div class="col-xs-6 box-eve-silesq">
                        <div class="row eve-sil-div">
                            <span class="eve-sil-divtit">Data de divulgação</span><br />
                            <span class="eve-sil-divcon">29/01/2015</span>
                        </div>
                        <div class="row eve-sil-per">
                            <span class="eve-sil-divtit">Período de Silêncio</span><br />
                            <span class="eve-sil-divcon">14/01/2015 a 28/01/2015</span>
                        </div>
                    </div>
                    <div class="col-xs-6 box-eve-sildir">
                    </div>
                </div>
                <!-- INÍCIO DO QUADRO DE DIVULGAÇÂO DO PERÌODO DE SILÊNCIO  -->
            </div>
        </div>
        <!-- FIM DO CONTEÙDO COM LARGURA MENOR -->
    </div>

</div>
