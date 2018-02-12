<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Noticia.ascx.cs" Inherits="Modulos_CMS_Modulos_ModNoticia_Noticia" %>
<%@ Register Namespace="WebFoundations.ServerControls" TagPrefix="wfc" %>

<div id="divSemConteudo" runat="server" class="moduloSemConteudo">
    <asp:Label ID="lblTitulo" runat="server"><%= string.Format(Resources.Textos.Modulo_Sem_Conteudo, "Notícias") %></asp:Label>
</div>

<div id="divConteudo" runat="server">
    <asp:Panel ID="pnlNoticiaHome" runat="server" Visible="false">
        <div class="row">
            <div class="col-lg-12 col-lg-offset-0 col-md-12 col-md-offset-0" style="margin-top: 10px;">
                <h2 class="section-heading heading_h2">Notícias</h2>
                <hr class="small2">

                <div class="col-md-6 noticias-1">
                    <div class="btn btn-danger">em alta</div>
                    <p class="noti-subt">
                        <asp:Label ID="lblSubTipoEsquerdo" runat="server"></asp:Label></p>
                    <h3 class="noti-tit">
                        <asp:Label ID="lblTituloEsquerdo" runat="server"></asp:Label></h3>
                    <asp:Label ID="lblResumoEsquerdo" runat="server"></asp:Label>
                    <%--<a class="btn saibamais-noti" href="#">
                        <span class="glyphicon glyphicon-play-circle"></span>SAIBA MAIS</a>--%>
                </div>
                <div class="col-md-6 noticias-2">
                    <p class="noti-subt">
                        <asp:Label ID="lblSubTipoDireitoSuperior" runat="server"></asp:Label></p>
                    <h4 class="noti-tit">
                        <asp:Label ID="lblTituloDireitoSuperior" runat="server"></asp:Label></h4>
                    <asp:Label ID="lblResumoDireitoSuperior" runat="server"></asp:Label>
                    <%--<a class="btn saibamais-noti" href="#">
                        <span class="glyphicon glyphicon-play-circle"></span>SAIBA MAIS</a>--%>
                </div>
                <div class="col-md-6 noticias-3">
                    <p class="noti-subt">
                        <asp:Label ID="lblSubTipoDireitoCentral" runat="server"></asp:Label></p>
                    <h4 class="noti-tit">
                        <asp:Label ID="lblTituloDireitoCentral" runat="server"></asp:Label></h4>
                    <asp:Label ID="lblResumoDireitoCentral" runat="server"></asp:Label>
                    <%--<a class="btn saibamais-noti" href="#">
                        <span class="glyphicon glyphicon-play-circle"></span>SAIBA MAIS</a>--%>
                </div>
                <div class="col-md-6 noticias-4">
                    <h4 class="noti-tit">
                        <asp:Label ID="lblSubTipoDireitoInferior" runat="server"></asp:Label></h4>
                    <p class="noti-subt">
                        <asp:Label ID="lblTituloDireitoInferior" runat="server"></asp:Label>
                    </p>
                    <asp:Label ID="lblResumoDireitoInferior" runat="server"></asp:Label>
                    <%--<a class="btn saibamais-noti" href="#">
                        <span class="glyphicon glyphicon-play-circle"></span>SAIBA MAIS</a>--%>
                </div>
            </div>
            <div class="col-lg-12 col-lg-offset-0 col-md-12 col-md-offset-0 noti-footer" style="margin-bottom: 60px;">
                <a class="btn btn-default vertodas" href="#">
                    <span class="lplay-icon_red"></span>VER TODAS</a>
            </div>
        </div>
    </asp:Panel>
    <asp:Panel ID="pnlNoticiaDestaque" runat="server" Visible="false">
        <!-- INÍCIO DO DESTAQUE DA SUBHOME -->
        <div class="container">
            <div class="row">
                <div class="col-lg-12 col-lg-offset-0 col-md-12 col-md-offset-0 not-sub-tot">
                    <h2 class="section-heading heading_h2">Destaques</h2>
                    <hr class="small2">
                    <div class="col-md-6 noticias-1" style="background-image: url('/img/img-notici-1.jpg');" onclick="funcao_aqui();">
                        <div class="btn btn-danger">em alta</div>
                        <p class="noti-subt"><asp:Label ID="lblSubTipoEsquerdaDestaque" runat="server"></asp:Label></p>
                        <h3 class="noti-tit"><asp:Label ID="lblTituloEsquerdaDestaque" runat="server"></asp:Label></h3>
                        <p><asp:Label ID="lblResumoEsquerdaDestaque" runat="server"></asp:Label></p>
                    </div>
                    <div class="col-md-6 not-sub-2" style="background-image: url('/img/img-notici-2.jpg');" onclick="funcao_aqui();">
                        <p class="noti-subt"><asp:Label ID="lblSubTipoDireitaSuperiorDestaque" runat="server"></asp:Label> </p>
                        <h4 class="noti-tit"><asp:Label ID="lblTituloDireitaSuperiorDestaque" runat="server"></asp:Label></h4>
                    </div>
                    <div class="col-md-6 not-sub-3" style="background-image: url('/img/img-notici-3.jpg');" onclick="funcao_aqui();">
                        <p class="noti-subt"><asp:Label ID="lblSubTipoDireitaInferiorDestaque" runat="server"></asp:Label></p>
                        <h4 class="noti-tit"><asp:Label ID="lblTituloDireitaInferiorDestaque" runat="server"></asp:Label></h4>
                    </div>
                </div>
            </div>
        </div>
        <!-- FIM DO DESTAQUE DA SUBHOME -->
    </asp:Panel>
    <asp:Panel ID="pnlNoticiaInterna" runat="server" Visible="false">
        <!-- INÍCIO DO MÓDULO DE NOTÍCIAS -->
        <div class="container-fluid container">
            <div class="row">
                <div class="col-md-12 noticias-1">
                    <div class="btn btn-danger">em alta</div>
                    <p class="noti-subt">SIMULADORES</p>
                    <h3 class="noti-tit">Notícia Gustavo</h3>
                    <p>is simply dummy text of the printing and typesetting industry.</p>
                    <a class="btn saibamais-noti" href="#">
                        <span class="glyphicon glyphicon-play-circle"></span>SAIBA MAIS</a>
                </div>
            </div>
            <!-- Começo dos filtros de busca -->

            <div class="row">
                <div class="inner-addon right-addon down-filtro-termo">
                    <i class="glyphicon glyphicon-search" onclick="submit()"></i>
                    <asp:TextBox ID="txtPalavraChave" runat="server" class="down-text" ToolTip="Busca por palavra chave" title="Busca por palavra chave"></asp:TextBox>
                    <asp:ImageButton ID="btnBuscar" runat="server" CssClass="glyphicon glyphicon-search" OnClick="btnBuscar_Click" AlternateText=" " />
                    <%--<input class="down-text" type="text" placeholder="Busca por palavra chave" name="termo" id="termo">--%>
                </div>
                <div class="down-filtro-ano">
                    <asp:DropDownList ID="ddlAno" runat="server" CssClass="down-select" AutoPostBack="true" OnSelectedIndexChanged="ddlAno_SelectedIndexChanged"></asp:DropDownList>
                </div>
            </div>
            <!-- Fim dos filtros de busca -->
            <!-- Insere as notícias -->
            <asp:Repeater ID="rptNoticia" runat="server" OnItemDataBound="rptNoticia_ItemDataBound">
                <HeaderTemplate>
                    <div class="row mod-not" id="exibe_conteudo">
                </HeaderTemplate>
                <ItemTemplate>
                    <div class="col-md-12">
                        <div class="row">
                            <div class="not-data-mobile">
                                <asp:Label ID="lblDataNoticiaMobil" runat="server"></asp:Label></div>
                            <div class="media-left mod-not-img">
                                <img class="not-img" src="img/img-noticia.jpg" width="140" />
                            </div>
                            <div class="media-body mod-not-content">
                                <div class="not-data">
                                    <asp:Label ID="lblDataNoticia" runat="server"></asp:Label>
                                </div>
                                <div class="not-tit">
                                    <asp:Label ID="lblTitulo" runat="server"></asp:Label>
                                </div>
                                <div class="not-txt">
                                    <asp:Label ID="lblResumo" runat="server"></asp:Label>
                                </div>
                                <div class="not-lnk"><span class="glyphicon glyphicon-play-circle"></span>VER NOTÍCIA</div>
                            </div>
                        </div>
                    </div>
                </ItemTemplate>

                <FooterTemplate>
                    </div>
                </FooterTemplate>
            </asp:Repeater>
            <div class="row">
                <asp:LinkButton ID="lbtnFirst" runat="server" OnClick="lbtnFirst_Click">Primeira</asp:LinkButton>
                <asp:LinkButton ID="lbtnPrev" runat="server" OnClick="lbtnPrev_Click">Anterior</asp:LinkButton>
                <asp:LinkButton ID="lbtnNext" runat="server" OnClick="lbtnNext_Click">Próxima</asp:LinkButton>
                <asp:LinkButton ID="lbtnLast" runat="server" OnClick="lbtnLast_Click">Última</asp:LinkButton>
                <asp:Label ID="lblStatus" runat="server" Text="Label"></asp:Label>

            </div>
            <!-- Insere caixas sendo 3 caixa por linha sempre alinhadas à esquerda -->
        </div>
        <!-- Fim da inserção de notícias  -->
        <!-- FIM DO MÓDULO DE NOTÍCIAS -->

    </asp:Panel>

    <asp:Literal ID="litArquivos" Visible="false" runat="server"></asp:Literal>


</div>
