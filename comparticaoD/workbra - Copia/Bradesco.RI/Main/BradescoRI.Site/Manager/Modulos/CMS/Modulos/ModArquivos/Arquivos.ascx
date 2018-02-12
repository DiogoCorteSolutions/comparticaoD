<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Arquivos.ascx.cs" Inherits="Modulos_CMS_Modulos_ModArquivos_Arquivos" %>
<%@ Register Namespace="WebFoundations.ServerControls" TagPrefix="wfc" %>

<div id="divSemConteudo" runat="server" class="moduloSemConteudo">
    <asp:Label ID="lblTitulo" runat="server"><%= string.Format(Resources.Textos.Modulo_Sem_Conteudo, "Arquivos") %></asp:Label>
</div>

<div id="divConteudo" runat="server">
    <%--<asp:ScriptManager runat="server"></asp:ScriptManager>--%>

    <!-- MÓDULO DE CAIXAS DE DOWNLOAD DENTRO DE UM ACCORDION (Estilos box-down)  -->
    <div class="container-fluid container">
        <%--<asp:UpdatePanel ID="updTela" runat="server">
            <ContentTemplate>
        --%>
        <div class="row">
            <div class="panel-group" id="accordion" role="tablist" aria-multiselectable="true">
                <div class="panel panel-default panel-box-down">
                    <asp:Panel ID="pnlAccordion" runat="server" Visible="false">
                        <div class="panel-heading" role="tab" id="headingOne">
                            <h4 class="panel-title panel-title-box-down">
                                <a class="accordion-toggle accordion-down" role="button" data-toggle="collapse" data-parent="#accordion" href="#collapseDown" aria-expanded="true" aria-controls="collapseOne">
                                    <asp:Label ID="lblModuloTitulo" runat="server"></asp:Label>
                                </a>
                            </h4>
                        </div>
                    </asp:Panel>
                    <asp:Panel ID="pnlFiltro" runat="server" Visible="false">
                        <!-- BARRA DE FILTRO DE DADOS  -->
                        <div class="row down-filtro">
                            <%--<form name="form_getdown" id="getdown">--%>
                            <div class="inner-addon right-addon down-filtro-termo">
                                <asp:TextBox ID="txtPalavraChave" runat="server" class="down-text" ToolTip="Busca por palavra chave" title="Busca por palavra chave"></asp:TextBox>
                                <asp:ImageButton ID="btnBuscar" runat="server" CssClass="glyphicon glyphicon-search" OnClick="btnBuscar_Click" AlternateText=" " />
                                <%--<input  type="text" placeholder="Busca por palavra chave" name="termo" id="termo">--%>
                            </div>
                            <div class="down-filtro-txt">Filtrar</div>
                            <div class="down-filtro-ano">
                                <span class="labelsel">Ano</span>
                                <asp:DropDownList ID="ddlAno" class="down-select" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlAno_SelectedIndexChanged"></asp:DropDownList>
                            </div>
                            <div class="down-filtro-tempo">
                                <div class="labelsel">Ordenado por</div>
                                <%--<select class="down-select" name="tempo" id="tempo">--%>
                                <asp:DropDownList ID="ddlTempo" runat="server" CssClass="down-select" OnSelectedIndexChanged="ddlTempo_SelectedIndexChanged" AutoPostBack="true">
                                    <asp:ListItem Text="Todos" Value="-1" Selected="True"></asp:ListItem>
                                    <asp:ListItem Text="Mais Recentes" Value="1"></asp:ListItem>
                                    <asp:ListItem Text="Mais Antigos" Value="2"></asp:ListItem>
                                </asp:DropDownList>
                                <%--</select>--%>
                            </div>
                            <%--</form>--%>
                        </div>
                        <!-- FIM DA BARRA DE FILTRO DE DADOS  -->
                    </asp:Panel>
                    <div id="collapseDown" class="panel-collapse collapse in" role="tabpanel" aria-labelledby="heading1">
                        <div class="panel-body panel-down">
                            <div class="row">
                                <!-- Insere caixas dentro do conteúdo do accordion sendo 3 caixa por linha sempre alinhadas à esquerda -->
                                <asp:Repeater ID="rptDownloadMultiplo" OnItemCommand="rptDownloadMultiplo_ItemCommand" Visible="false" runat="server" OnItemCreated="rptDownloadMultiplo_ItemCreated" OnItemDataBound="rptDownloadMultiplo_ItemDataBound">
                                    <HeaderTemplate>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:HiddenField ID="hdnArquivoId" runat="server" />
                                        <div class="col-md-4 box-down-check" id="boxdowncheck1">
                                            <div class="row">
                                                <div class="col-xs-2 box-down-check-input">
                                                    <asp:CheckBox ID="chkDownload" runat="server" AutoPostBack="true" OnCheckedChanged="chkDownload_CheckedChanged" /><label for="chkDownload"></label>
                                                    <%--<input type="checkbox" name="checkbox1" id="checkbox1" value="ok" onclick="downsel('boxdowncheck1', 'checkbox1')"><label for="checkbox1"></label>--%>
                                                </div>
                                                <div class="col-xs-10 box-down-check-tot">
                                                    <div class="row">
                                                        <div class="col-xs-12 box-down-check-topo">
                                                            <asp:Label ID="lblTituloMultiplo" runat="server"></asp:Label>
                                                        </div>
                                                    </div>
                                                    <div class="row">
                                                        <div class="col-xs-12 box-down-check-content">
                                                            <asp:Label ID="lblDescricaoMultiplo" runat="server"></asp:Label>
                                                        </div>
                                                    </div>
                                                    <div class="row">
                                                        <div class="col-xs-1 box-down-check-ico">
                                                            <a href="#">
                                                                <img src="../../../img/icone-download.gif" width="25" height="25" /></a>
                                                        </div>
                                                        <div class="col-xs-5 box-down-check-file">
                                                            <a href="#">
                                                                <asp:Label ID="lblExtensaoMultiplo" runat="server"></asp:Label>
                                                                <asp:Label ID="lblTamanhoMultiplo" runat="server" /></a>
                                                        </div>
                                                        <div class="col-xs-6 box-down-check-comp">
                                                            <img src="../../../img/icone-corrente.png" width="15" height="15" /><a class="down-link" href="#">Compartilhar link</a>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>

                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <div class="col-md-4 box-down">
                                            <p style="text-align: right">
                                                <%--<asp:UpdatePanel ID="udpDownload" runat="server">
                                                            <ContentTemplate>--%>
                                                <asp:Button ID="btnDownload" Text="Download" runat="server" OnClick="btnDownload_Click" />
                                                <%--</ContentTemplate>
                                                            <Triggers>
                                                                <asp:PostBackTrigger ControlID="btnDownload" />
                                                            </Triggers>
                                                        </asp:UpdatePanel>--%>
                                            </p>
                                        </div>
                                    </FooterTemplate>
                                </asp:Repeater>

                                <asp:Repeater ID="rptArquivoDownloadUnico" Visible="false" runat="server" OnItemDataBound="rptArquivoDownloadUnico_ItemDataBound" OnItemCommand="rptArquivoDownloadUnico_ItemCommand">
                                    <HeaderTemplate>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <div class="col-md-4 box-down">
                                            <div class="row">
                                                <div class="col-xs-10 box-down-topo">
                                                    <asp:Label ID="lblTituloArquivo" runat="server"></asp:Label>
                                                </div>
                                                <div class="col-xs-2 box-down-bot">
                                                    <a href="#">
                                                        <asp:ImageButton ID="imgDownload" runat="server" CommandArgument='<%# Eval("Id") %>' CommandName="DownloadFile" ImageUrl="~/img/icone-download.gif" Width="25px" Height="25px" />
                                                </div>
                                            </div>
                                            <div class="row box-down-content">
                                                <div class="col-md-10">
                                                    <asp:Label ID="lblDescricaoArquivo" runat="server"></asp:Label>
                                                    <p></p>
                                                </div>
                                            </div>
                                            <div class="row box-down-comp">
                                                <div class="col-xs-6">
                                                    <asp:Label ID="lblExtensaoArquivo" runat="server"></asp:Label>
                                                    <asp:Label ID="lblTamanhoArquivo" runat="server"></asp:Label>
                                                </div>
                                                <div class="col-xs-6 box-down-link">
                                                    <img src="../../../img/icone-corrente.png" width="15" height="15" /><a class="down-link" href="#">Compartilhar Link</a>
                                                </div>
                                            </div>
                                        </div>

                                    </ItemTemplate>
                                    <FooterTemplate>
                                    </FooterTemplate>
                                </asp:Repeater>

                                <asp:Repeater ID="rptDownloadPodCast" Visible="false" runat="server"  >
                                    <HeaderTemplate>
                                        <div class="container-fluid container">
                                            <div class="row">
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <!--  INÍCIO DO MÓDULO PODCAST  -->

                                        <div class="box-down-linha">
                                            <!-- Insere caixas dentro do conteúdo do accordion sendo 3 caixa por linha sempre alinhadas à esquerda -->

                                            <div class="col-md-4 box-pod">
                                                <div class="row">
                                                    <div class="col-xs-12 box-pod-topo"><%# DataBinder.Eval(Container.DataItem, "Titulo") %></div>
                                                </div>
                                                <div class="row">
                                                    <div class="col-xs-12 box-pod-player">
                                                        <audio id="audio" controls="controls">
                                                            <source src='<%# DataBinder.Eval(Container.DataItem, "Caminho") %>' type="audio/mp3">
                                                        </audio>
                                                    </div>
                                                </div>
                                                <div class="row">
                                                    <div class="col-xs-12 box-pod-content">
                                                        <%# DataBinder.Eval(Container.DataItem, "Descricao") %><br />
                                                        Publicado em: <%# DataBinder.Eval(Container.DataItem, "DataCadastro") %>
                                                    </div>
                                                </div>
                                                <div class="row box-pod-down">
                                                    <div class="col-xs-6 box-pod-arq"><%# DataBinder.Eval(Container.DataItem, "Extensao") %> <%# DataBinder.Eval(Container.DataItem, "Tamanho") %></div>
                                                    <div class="col-xs-6 box-pod-lnk">
                                                        <img src="img/ico-download-small.png" width="15" height="15" /><a class="down-link" href='<%# DataBinder.Eval(Container.DataItem, "Caminho") %>'>Download</a>
                                                    </div>
                                                </div>
                                            </div>



                                            <!-- Fim de inserção de caixas  -->
                                        </div>

                                        <!--  FIM DO MÓDULO PODCAST  -->
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        </div>
                                        </div>
                                    </FooterTemplate>
                                </asp:Repeater>
                                <!-- Fim de inserção de caixas  -->
                            </div>
                            <div class="row">
                                <asp:LinkButton ID="lbtnFirst" runat="server" OnClick="lbtnFirst_Click">Primeira</asp:LinkButton>
                                <asp:LinkButton ID="lbtnPrev" runat="server" OnClick="lbtnPrev_Click">Anterior</asp:LinkButton>
                                <asp:LinkButton ID="lbtnNext" runat="server" OnClick="lbtnNext_Click">Próxima</asp:LinkButton>
                                <asp:LinkButton ID="lbtnLast" runat="server" OnClick="lbtnLast_Click">Última</asp:LinkButton>
                                <asp:Label ID="lblStatus" runat="server" Text="Label"></asp:Label>

                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <%--</ContentTemplate>
        </asp:UpdatePanel>--%>
    </div>

</div>
