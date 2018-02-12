<%@ Control Language="C#" AutoEventWireup="true" CodeFile="CalendarioEventos.ascx.cs" Inherits="Modulos_CMS_Modulos_ModEventos_CalendarioEventos" %>


<div id="divConteudo" runat="server">

    <div class="container-fluid">
        <!-- BARRA DE FILTRAGEM DE DADOS  -->
        <div class="row">
            <div class="eve-filtro">
                <form name="form_filtro" id="form_filtro">
                    <div class="eve-filtro-ano">
                        <span class="labelsel"></span>
                        <asp:DropDownList ID="ddlAno" class="eve-select" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlAno_SelectedIndexChanged"></asp:DropDownList>
                    </div>
                    <div class="eve-filtro-tipo">
                        <asp:DropDownList ID="ddlTipoEvento" class="eve-select" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlTipoEvento_SelectedIndexChanged"></asp:DropDownList>
                    </div>
                </form>
            </div>
        </div>
        <!-- FIM DA BARRA DE FILTRAGEM DE DADOS  -->
        <div class="box-eve-tot">
            <div class="container-fluid">
            <div class="row">
                <div class="box-eve-calleg">
                <div class="box-eve-cal">
                    <asp:Calendar ID="Calendario" runat="server" CssClass="eve-cal-tab" style="border-width:0px;border-style:none;border-collapse:collapse;" ShowGridLines="false" TitleFormat="Month" FirstDayOfWeek="Sunday" OnDayRender="Calendario_DayRender" OnVisibleMonthChanged="Calendario_VisibleMonthChanged">
                        <DayHeaderStyle CssClass="eve-cal-week" />
                        <TitleStyle CssClass="eve-cal-month" />
                        <NextPrevStyle CssClass="eve-cal-prevnext" ForeColor="#e5173f" />
                        <DayStyle CssClass="eve-cal-day" HorizontalAlign="Center" VerticalAlign="Middle" />
                        <OtherMonthDayStyle ForeColor="#C5C5C5" />
                    </asp:Calendar>
                </div>
                <div class="box-eve-leg">

                     <asp:Repeater runat="server" ID="rptCategorias" OnItemDataBound="rptCategorias_ItemDataBound">
                        <HeaderTemplate>
                        </HeaderTemplate>
                        <ItemTemplate>
                            <div class="box-eve-cat"><asp:Literal runat="server" ID="ltrBullet"></asp:Literal>                             
                                <span class="row box-eve-cattxt"><%# DataBinder.Eval(Container.DataItem, "Descricao")%></span>
                            </div>
                        </ItemTemplate>
                        <FooterTemplate>
                        </FooterTemplate>
                    </asp:Repeater>
                </div>
                </div>
            </div>
            </div>
            <asp:Repeater runat="server" ID="rptProximosEventos" OnItemDataBound="rptProximosEventos_ItemDataBound">
                <HeaderTemplate>
                <div class="container-fluid">
                    <div class="row">
                        <div class="box-eve-txt">
                            <%# Resources.Calendario.ProximosEventos%>
                        </div>
                    </div>
                </HeaderTemplate>
                <ItemTemplate>
                    <!-- INICIO da inserção próximos eventos sendo um por linha -->
                        <!-- INICIO da caixa de próximos eventos -->
                        <!-- Próximo evento 1 -->
                    <div class="row">
                        <div class="col-md-12 box-eve">
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
                                            <div class="col-xs-12 box-eve-tit"><a href="<%# string.Format("{0}?IdEvento={1}", DataBinder.Eval(Container.DataItem, "UrlListaEvento"), DataBinder.Eval(Container.DataItem, "IdEvento")) %>"><%# DataBinder.Eval(Container.DataItem, "Titulo")%></a></div>
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
                        <!-- INICIO do link Compartilhe -->
                        <div class="row">
                            <div class="col-md-12 box-eve-lnk">
                                <div class="pull-right">
                                    <img src="../../../img/icone-corrente.png"  width="15" height="15" /><a class="compartilhe-lnk" href="<%# DataBinder.Eval(Container.DataItem, "UrlListaEvento")%>"><%# Resources.Calendario.Compartilhar%></a>
                                </div>
                            </div>
                        </div>
                        <!-- FIM do link Compartilhe -->
                    <!-- FIM da inserção próximos eventos sendo um por linha -->
                </ItemTemplate>
                <FooterTemplate>
                </div>
                </FooterTemplate>
            </asp:Repeater>

        </div>

        <!-- INÍCIO DO CONTEÙDO COM LARGURA MENOR -->
        <div class="row">
            <div class="box-eve-tot">
                <!-- INÍCIO DO TEXTO PERÌODO DE SILÊNCIO -->
                <div class="eve-siltxt-tit"><%# Resources.Calendario.PeriodoSilencio%></div>
                <br />
                <div class="eve-siltxt-con">
                    <%# Resources.Calendario.TextoPeriodo%>
                </div>
                <!-- FIM DO TEXTO PERÌODO DE SILÊNCIO -->
            </div>
        </div>
        <!-- FIM DO CONTEÙDO COM LARGURA MENOR -->
        <div class="row">
           <div class="eve-filtro"> 
                <div class="eve-filtro-ano">
                    <span class="labelsel">Período</span>
                    <asp:DropDownList ID="ddlAnoPeriodo" class="eve-select" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlAnoPeriodo_SelectedIndexChanged"></asp:DropDownList>
                </div>
            </div>
        </div>
        <!-- FIM DA BARRA DE FILTRAGEM DE DADOS  -->
        <!-- INÍCIO DO CONTEÙDO COM LARGURA MENOR -->
        <div class="row">
            <div class="col-centered box-eve-tot">
                <!-- INÍCIO DO QUADRO DE DIVULGAÇÂO DO PERÌODO DE SILÊNCIO -->
                <asp:Repeater runat="server" ID="rptPeriodoSilencio">
                    <HeaderTemplate>
                        <div class="row box-eve-calleg">
                            <div class="col-xs-6 box-eve-silesq">
                    </HeaderTemplate>
                    <ItemTemplate>
                        <div class="row eve-sil-div">
                            <span class="eve-sil-divtit"><%# Resources.Calendario.DataDivulgacao%></span><br />
                            <span class="eve-sil-divcon"><%# DataBinder.Eval(Container.DataItem, "DataDivulgacao", "{0:d/M/yyyy}")%></span>
                        </div>
                        <div class="row eve-sil-per">
                            <span class="eve-sil-divtit"><%# Resources.Calendario.PeriodoSilencio%></span><br />
                            <span class="eve-sil-divcon"><%# DataBinder.Eval(Container.DataItem, "DataInicio", "{0:d/M/yyyy}")%> a <%# DataBinder.Eval(Container.DataItem, "DataFim", "{0:d/M/yyyy}")%></span>
                        </div>
                    </ItemTemplate>
                    <FooterTemplate>
                        </div>
                            <div class="col-xs-6 box-eve-sildir"></div>
                        </div>
                    </FooterTemplate>
                </asp:Repeater>
            </div>
        </div>
    </div>
    <!-- FIM DO CONTEÙDO COM LARGURA MENOR -->
</div>
