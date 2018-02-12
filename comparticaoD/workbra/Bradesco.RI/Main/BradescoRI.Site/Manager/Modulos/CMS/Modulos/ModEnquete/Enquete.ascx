<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Enquete.ascx.cs" Inherits="Modulos_CMS_Modulos_ModEnquete_Enquete" %>

<div id="divSemConteudo" runat="server" class="moduloSemConteudo">
    <asp:Label ID="lblTitulo" runat="server"><%= string.Format(Resources.Textos.Modulo_Sem_Conteudo, "Form - Enquete") %></asp:Label>
</div>

<div id="divConteudo" runat="server">
    <div class="row form-enquete">
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <div runat="server">
                    <label id="lblMensagemSucesso" runat="server" visible="false"><%= Resources.Enquetes.EnvioSucesso %></label>
                </div>
                <div>
                    <label id="lblMensagemErro" runat="server" visible="false"><%= Resources.Enquetes.EnvioErro %></label>
                </div>

                <div>
                    <asp:Label ID="lblTituloEnquete" runat="server"></asp:Label>
                </div>

                <div runat="server" class="col-lg-12 col-lg-offset-0 col-md-12 col-md-offset-0 label-form">
                    <asp:Repeater runat="server" ID="rptEnquete" OnItemDataBound="rptEnquete_ItemDataBound">
                        <HeaderTemplate>
                        </HeaderTemplate>
                        <ItemTemplate>
                            <asp:HiddenField ID="hdnPerguntaId" runat="server" />
                            <div runat="server" class="row">
                            <div runat="server" class="col-lg-12 col-lg-offset-0 col-md-12 col-md-offset-0 label-form">
                                <label><%# DataBinder.Eval(Container.DataItem, "Pergunta")%></label>
                            </div></div>
                            <div runat="server" class="row">
                            <div runat="server" class="col-lg-12 col-lg-offset-0 col-md-12 col-md-offset-0 campo-form">
                                <asp:RadioButtonList ID="rdbResposta" runat="server" RepeatDirection="Horizontal" TabIndex="1" RepeatLayout="Table" CssClass="componente-form-g">
                                </asp:RadioButtonList>
                            </div></div>
                        </ItemTemplate>
                        <FooterTemplate>
                            <div class="col-lg-3 col-lg-offset-0 col-md-12 col-md-offset-0 btn-continuar">
                                <asp:Button ID="btnProximo" class="l-impor2" runat="server" OnClick="btnProximo_Click" Text="Continuar"></asp:Button>
                            </div>
                        </FooterTemplate>
                    </asp:Repeater>
                </div>

                <div id="divEtapaFinal" runat="server">
                    <div runat="server" class="col-lg-12 col-lg-offset-0 col-md-12 col-md-offset-0 label-form">
                        <label><%= Resources.Enquetes.EtapaFinal%></label>
                    </div>
                    <div runat="server" class="col-lg-12 col-lg-offset-0 col-md-12 col-md-offset-0 label-form">
                        <label><%= Resources.Enquetes.Sugestoes %></label>
                    </div>
                    <div runat="server" class="col-lg-12 col-lg-offset-0 col-md-12 col-md-offset-0 campo-form">
                        <asp:TextBox ID="txtSugestao" runat="server" TabIndex="2" MaxLength="200" placeholder="Digite a mensagem"></asp:TextBox>
                    </div>
                    <div runat="server" class="col-lg-12 col-lg-offset-0 col-md-12 col-md-offset-0 label-form">
                        <label><%= Resources.Enquetes.TextoDuvidas %></label>
                        <asp:HyperLink ID="hplFaleConosco" runat="server"></asp:HyperLink>
                    </div>

                    <div runat="server" class="col-lg-12 col-lg-offset-0 col-md-12 col-md-offset-0 label-form">
                        <label><%= Resources.Enquetes.Nome%>*</label>
                    </div>
                    <div runat="server" class="col-lg-12 col-lg-offset-0 col-md-12 col-md-offset-0 campo-form">
                        <asp:TextBox ID="txtNome" runat="server" TabIndex="3" MaxLength="128" placeholder="Digite seu nome"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="rfvNome" runat="server" ControlToValidate="txtNome" ValidationGroup="ModEnquete"></asp:RequiredFieldValidator>
                    </div>

                    <div runat="server" class="col-lg-12 col-lg-offset-0 col-md-12 col-md-offset-0 label-form">
                        <label><%= Resources.Enquetes.Email %>*</label>
                    </div>
                    <div runat="server" class="col-lg-12 col-lg-offset-0 col-md-12 col-md-offset-0 campo-form">
                        <asp:TextBox ID="txtEmail" runat="server" TabIndex="4" MaxLength="128" placeholder="exemplo@mail.com"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="rfvEmail" runat="server" ControlToValidate="txtEmail" Display="Dynamic" ValidationGroup="ModEnquete"></asp:RequiredFieldValidator>
                        <asp:RegularExpressionValidator ID="revEmail" runat="server" ControlToValidate="txtEmail" Display="Dynamic" ValidationGroup="ModEnquete"
                            ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"></asp:RegularExpressionValidator>
                    </div>

                    <div runat="server" class="col-lg-12 col-lg-offset-0 col-md-12 col-md-offset-0 campo-form">
                    <div runat="server" class="col-lg-3 col-lg-offset-0 col-md-12 col-md-offset-0 btn-enviar">
                        <asp:Button ID="btnEnviar" class="l-impor2" runat="server" TabIndex="5" ValidationGroup="ModEnquete" OnClick="btnEnviar_Click" />
                    </div></div>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>

    </div>
</div>

<script src="<%=ResolveUrl("~/JS/jquery-1.10.2.min.js")%>"></script>
<script src="<%=ResolveUrl("~/JS/jquery.mask.js")%>"></script>
