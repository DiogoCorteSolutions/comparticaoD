<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Arquivos.ascx.cs" Inherits="Modulos_CMS_Modulos_ModArquivos_Arquivos" %>
<%@ Register Namespace="WebFoundations.ServerControls" TagPrefix="wfc" %>

<div id="divSemConteudo" runat="server" class="moduloSemConteudo">
    <asp:Label ID="lblTitulo" runat="server"><%= string.Format(Resources.Textos.Modulo_Sem_Conteudo, "Arquivos") %></asp:Label>
</div>

<div id="divConteudo" runat="server">
    <div class="container-fluid">

        <asp:Panel ID="pnlDestaquePodCast" runat="server" Visible="false">
            <div class="row">
                <!-- Começo do destaque -->
                <div id="divImagemDestaquePodCast" runat="server" class="col-md-12 pod-alta-cont" style="height: 428px; width: 1170px;">
                    <div class="btn btn-danger">em alta</div>
                    <h3>
                        <asp:Label ID="lblTituloDestaquePodCast" runat="server"></asp:Label></h3>
                    <p>
                        <asp:Label ID="lblDescricaoDestaquePodCast" runat="server"></asp:Label></p>
                    <div class="row">
                        <div class="col-xs-12 pod-alta-player">
                           <asp:Literal ID="litPlayer" runat="server"></asp:Literal>
                        </div>
                    </div>
                    <div class="row pod-alta-down">
                        <div class="col-xs-6 pod-alta-arq">
                            <asp:Label ID="lblExtensaoDestaquePodCast" runat="server"></asp:Label>
                            <asp:Label ID="lblTamanhoDestaquePosCast" runat="server"></asp:Label></div>
                        <div class="col-xs-6 pod-alta-lnk">
                            <img src="../../../img/ico-down-tr.png" width="15" height="15" />
                            <asp:LinkButton ID="lnkDownload" runat="server" CommandName="Download" OnCommand="lnkDownload_Command" Text="Download" CssClass="down-link"></asp:LinkButton>
                        </div>
                    </div>
                </div>
                <!-- Final do destaque -->
            </div>
        </asp:Panel>
        <div class="row">
        <asp:Panel ID="pnlDestaqueVideos" runat="server" Visible="false">
            <div class="row">
                <div class="col-lg-9 col-lg-offset-1 col-md-12 col-md-offset-0 vid-alta-btn">
                    <div class="btn btn-danger">em alta</div>
                </div>
            </div>
            <div class="row">
                <div class="col-md-12 vid-alta-player">
                    <asp:Literal ID="litVideoDestaque" runat="server"></asp:Literal>
                </div>
            </div>
        </asp:Panel>
        </div>
        <div class="row">
            <asp:UpdatePanel ID="updTela" runat="server">
                <ContentTemplate>

                    <asp:Panel ID="pnlFiltro" runat="server" Visible="false">
                        <!-- BARRA DE FILTRO DE DADOS  -->
                        
                            <div class="down-filtro">
                                <div class="inner-addon right-addon down-filtro-termo">
                                    <asp:TextBox ID="txtPalavraChave" runat="server" class="down-text" ToolTip="Busca por palavra chave" title="Busca por palavra chave"></asp:TextBox>
                                    <asp:ImageButton ID="btnBuscar" runat="server" CssClass="glyphicon glyphicon-search" OnClick="btnBuscar_Click" AlternateText=" " />
                                </div>
                                <div class="down-filtro-txt">Filtrar</div>
                                <div class="down-filtro-ano">
                                    <span class="labelsel">Ano</span>
                                    <asp:DropDownList ID="ddlAno" class="down-select" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlAno_SelectedIndexChanged"></asp:DropDownList>
                                </div>
                                <div class="down-filtro-tempo">
                                    <div class="labelsel">Ordenado por</div>
                                    <asp:DropDownList ID="ddlTempo" runat="server" CssClass="down-select" OnSelectedIndexChanged="ddlTempo_SelectedIndexChanged" AutoPostBack="true">
                                        <asp:ListItem Text="Todos" Value="-1" Selected="True"></asp:ListItem>
                                        <asp:ListItem Text="Mais Recentes" Value="1"></asp:ListItem>
                                        <asp:ListItem Text="Mais Antigos" Value="2"></asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                            </div>
                        
                        <!-- FIM DA BARRA DE FILTRO DE DADOS  -->
                    </asp:Panel>
                    <asp:Repeater ID="rptDownloadMultiplo" OnItemCommand="rptDownloadMultiplo_ItemCommand" Visible="false" runat="server" OnItemCreated="rptDownloadMultiplo_ItemCreated" OnItemDataBound="rptDownloadMultiplo_ItemDataBound">
                        <HeaderTemplate>
                        <div class="box-down-linha">
                        </HeaderTemplate>
                        <ItemTemplate>
                            <asp:HiddenField ID="hdnArquivoId" runat="server" />
                            <div class="col-md-4 box-down-check" id="boxdowncheck<%# Eval("Id") %>">
                                <div class="box-down-check-input">
                                    <asp:CheckBox ID="chkDownload" runat="server" AutoPostBack="true" OnCheckedChanged="chkDownload_CheckedChanged" /><label for="chkDownload"></label>
                                </div>
                                <div class="box-down-check-tot">
                                    <div class="box-down-check-topo">
                                        <asp:Label ID="lblTituloMultiplo" runat="server"></asp:Label>
                                    </div>
                                    <div class="box-down-check-content">
                                        <asp:Label ID="lblDescricaoMultiplo" runat="server"></asp:Label>
                                    </div>
                                    <div class="box-down-check-comp">
                                        <span class="box-down-check-ico">
                                            <a href="<%# DataBinder.Eval(Container.DataItem, "Caminho") %>">
                                                <asp:ImageButton ID="imgDownload" runat="server" CommandArgument='<%# Eval("Id") %>' CommandName="DownloadFile" ImageUrl="~/img/icone-down-cb.png" Width="18px" Height="23px" />
                                            </a>
                                        </span>
                                        <span class="box-down-check-file">
                                            <a href="<%# DataBinder.Eval(Container.DataItem, "Caminho") %>">
                                                <asp:Label ID="lblExtensaoMultiplo" runat="server"></asp:Label>
                                                <asp:Label ID="lblTamanhoMultiplo" runat="server" /></a>
                                        </span>
                                        <span class="box-down-link">
                                            <a href="<%# DataBinder.Eval(Container.DataItem, "Caminho") %>">Compartilhar link</a>
                                        </span>
                                        <span class="box-down-lnkcad">
                                            <a href="<%# DataBinder.Eval(Container.DataItem, "Caminho") %>">
                                                <img src="../../../img/icone-corrente.png" width="15" height="15" /></a>
                                        </span>
                                    </div>
                                </div>
                            </div>

                        </ItemTemplate>
                        <FooterTemplate>
                        </div>
                        <div class="container-fluid">
                            <div class="row box-down-send">
                                    <asp:UpdatePanel ID="udpDownload" runat="server">
                                        <ContentTemplate>
                                            <asp:Button ID="btnDownload" Text="Download" runat="server" OnClick="btnDownload_Click" />
                                        </ContentTemplate>
                                        <Triggers>
                                            <asp:PostBackTrigger ControlID="btnDownload" />
                                        </Triggers>
                                    </asp:UpdatePanel>
                            </div>
                        </div>
                        </FooterTemplate>
                    </asp:Repeater>

                    <asp:Repeater ID="rptArquivoDownloadUnico" Visible="false" runat="server" OnItemDataBound="rptArquivoDownloadUnico_ItemDataBound" OnItemCommand="rptArquivoDownloadUnico_ItemCommand">
                        <HeaderTemplate>
                            <div class="box-down-linha">
                        </HeaderTemplate>
                        <ItemTemplate>

                            <div class="col-md-4 box-down">
                                <div class="box-down-topo">
                                    <asp:Label ID="lblTituloArquivo" runat="server"></asp:Label>
                                </div>
                                <div class="box-down-bot">
                                    <a href="<%# DataBinder.Eval(Container.DataItem, "Caminho") %>">
                                        <asp:ImageButton ID="imgDownload" runat="server" CommandArgument='<%# Eval("Id") %>' CommandName="DownloadFile" ImageUrl="~/img/icone-download.gif" Width="24px" Height="24px" /></a>
                                </div>
                                <div class="box-down-content">
                                    <asp:Label ID="lblDescricaoArquivo" runat="server"></asp:Label>
                                </div>
                                <div class="box-down-comp">
                                    <span class="box-down-file">
                                        <asp:Label ID="lblExtensaoArquivo" runat="server"></asp:Label>
                                        <asp:Label ID="lblTamanhoArquivo" runat="server"></asp:Label></span>
                                    <span class="box-down-link">
                                        <a href="<%# DataBinder.Eval(Container.DataItem, "Caminho") %>">Compartilhar link</a>
                                    </span>
                                    <span class="box-down-lnkcad">
                                        <a href="<%# DataBinder.Eval(Container.DataItem, "Caminho") %>">
                                            <img src="../../../img/icone-corrente.png" width="15" height="15" /></a>
                                    </span>
                                </div>
                            </div>

                        </ItemTemplate>
                        <FooterTemplate>
                            </div>
                        </FooterTemplate>
                    </asp:Repeater>

                    <asp:Repeater ID="rptDownloadPodCast" Visible="false" runat="server">
                        <HeaderTemplate>
                            <!--  INÍCIO DO MÓDULO PODCAST  -->
                            <script>$(function () { $('audio').audioPlayer(); });</script>
                            <div class="container">
                                <div class="row">
                                    <div class="box-pod-linha">
                        </HeaderTemplate>
                        <ItemTemplate>
                            <!-- Insere caixas de Podcast -->
                            <div class="col-md-4 box-pod">
                                <div class="box-pod-topo"><%# DataBinder.Eval(Container.DataItem, "Titulo") %></div>
                                <div class="box-pod-player">
                                    <audio id="audio1" src='<%# DataBinder.Eval(Container.DataItem, "Caminho") %>' type="audio/mpeg" controls="controls">
                                        <source src='<%# DataBinder.Eval(Container.DataItem, "Caminho") %>' type="audio/mpeg">
                                    </audio>
                                </div>
                                <div class="box-pod-content">
                                    <%# DataBinder.Eval(Container.DataItem, "Descricao") %><br />
                                    Publicado em: <%# DataBinder.Eval(Container.DataItem, "DataCadastro", "{0:dd/MM/yyyy}") %>
                                </div>
                                <div class="box-pod-down">
                                    <span class="box-pod-file"><%# DataBinder.Eval(Container.DataItem, "Extensao") %> <%# DataBinder.Eval(Container.DataItem, "Tamanho") %></span>
                                    <span class="box-down-link">
                                        <a href="<%# DataBinder.Eval(Container.DataItem, "Caminho") %>">Download</a>
                                    </span>
                                    <span class="box-down-lnkcad">
                                        <a href="<%# DataBinder.Eval(Container.DataItem, "Caminho") %>">
                                            <img src="../../../img/ico-download-small.png" width="15" height="15" /></a>
                                    </span>
                                </div>
                            </div>
                            <!-- Fim de inserção de caixas  -->
                        </ItemTemplate>
                        <FooterTemplate>
                            </div> 
                                            </div>
                                        </div>
                                    <!--  FIM DO MÓDULO PODCAST  -->
                        </FooterTemplate>
                    </asp:Repeater>

                    <asp:Repeater ID="rptDownloadVideos" Visible="false" runat="server" OnItemDataBound="rptDownloadVideos_ItemDataBound">
                        <HeaderTemplate>
                            <!--  INÍCIO DO MÓDULO VÌDEO -->
                            <script>
                                $(document).on('click', '[data-toggle="lightbox"]', function (event) {
                                    event.preventDefault();
                                    $(this).ekkoLightbox();
                                });
                            </script>
                            <div class="row">
                                <div class="col-lg-9 col-lg-offset-1 col-md-12 col-md-offset-0">
                                    <div class="vid-outros">Outros vídeos</div>
                                </div>
                            </div>
                            <div class="box-vid-linha">
                        </HeaderTemplate>
                        <ItemTemplate>
                            <!-- Insere caixas de vídeo -->
                            <div class="col-md-4 box-vid">
                                <div class="box-vid-dat"><%# DataBinder.Eval(Container.DataItem, "DataArquivo", "{0:dd/MM/yyyy}") %></div>
                                <div class="box-vid-vid" runat="server" id="divCapa">
                                    <a href="<%# DataBinder.Eval(Container.DataItem, "Caminho") %>" data-toggle="lightbox"><img class="vid-alta-play" src="../../../img/play-small.png" /></a>
                                </div>
                                <div class="box-vid-tit"><%# DataBinder.Eval(Container.DataItem, "Titulo") %></div>
                            </div>
                            <!-- Fim de inserção de caixas  -->
                        </ItemTemplate>
                        <FooterTemplate>
                            </div>
                                        <!--  FIM DO MÓDULO VÌDEO -->
                        </FooterTemplate>
                    </asp:Repeater>
                    <!-- Fim de inserção de caixas  -->
                    <div class="container-fluid">
                        <div class="row">
                            <div class="col-lg-1 box-pagi" id="divPaginacao" runat="server" visible="false">
                                <asp:LinkButton ID="lbtnFirst" runat="server" Visible="false" OnClick="lbtnFirst_Click">Primeira</asp:LinkButton> < 
                                <asp:LinkButton ID="lbtnPrev" runat="server" Visible="false" OnClick="lbtnPrev_Click">Anterior</asp:LinkButton> | 
                                <asp:LinkButton ID="lbtnNext" runat="server" Visible="false" OnClick="lbtnNext_Click">Próxima</asp:LinkButton> > 
                                <asp:LinkButton ID="lbtnLast" runat="server" Visible="false" OnClick="lbtnLast_Click">Última</asp:LinkButton>
                                <asp:Label ID="lblStatus" runat="server" Visible="false" Text="Label"></asp:Label>
                            </div>
                        </div>
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    </div>
</div>
