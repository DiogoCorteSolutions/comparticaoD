<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Avaliacao.ascx.cs" Inherits="Modulos_CMS_Modulos_ModAvaliacao_Avaliacao" %>

<div id="divSemConteudo" runat="server" class="moduloSemConteudo">
    <asp:Label ID="lblTitulo" runat="server"><%= string.Format(Resources.Textos.Modulo_Sem_Conteudo, "Form - Avaliação") %></asp:Label>
</div>

<div id="divConteudo" runat="server">
    <div class="row form-alerta-ri">
        <div>
            <label id="lblMensagemSucesso" runat="server" visible="false"><%= Resources.Avaliacao.EnvioSucesso %></label>
        </div>
        <div>
            <label id="lblMensagemErro" runat="server" visible="false"><%= Resources.Avaliacao.EnvioErro %></label>
        </div>
        <div>
            <label id="lblMensagemEmailExiste" runat="server" visible="false"><%= Resources.Avaliacao.EnvioErroEmail %></label>
        </div>

        <div runat="server" class="col-lg-12 col-lg-offset-0 col-md-12 col-md-offset-0 label-form">
            <label><%= Resources.Avaliacao.FacilidadeNavegacao%></label>
        </div>
        <div runat="server" class="col-lg-12 col-lg-offset-0 col-md-12 col-md-offset-0 campo-form">
            <asp:RadioButtonList ID="rdbRespostaFacilidadeNavegacao" runat="server" RepeatDirection="Horizontal" TabIndex="1" CssClass="componente-form">
            </asp:RadioButtonList>
        </div>
        <div runat="server" class="col-lg-12 col-lg-offset-0 col-md-12 col-md-offset-0 label-form">
            <label><%= Resources.Avaliacao.QualidadeInformacoes%></label>
        </div>
        <div runat="server" class="col-lg-12 col-lg-offset-0 col-md-12 col-md-offset-0 campo-form">
            <asp:RadioButtonList ID="rdbRespostaQualidadeInformacao" runat="server" RepeatDirection="Horizontal" TabIndex="2" CssClass="componente-form">
            </asp:RadioButtonList>
        </div>
        <div runat="server" class="col-lg-12 col-lg-offset-0 col-md-12 col-md-offset-0 label-form">
            <label><%= Resources.Avaliacao.QuantidadeConteudo%></label>
        </div>
        <div runat="server" class="col-lg-12 col-lg-offset-0 col-md-12 col-md-offset-0 campo-form">
            <asp:RadioButtonList ID="rdbRespostaQuantidadeConteudo" runat="server" RepeatDirection="Horizontal" TabIndex="3" CssClass="componente-form">
            </asp:RadioButtonList>
        </div>
        <div runat="server" class="col-lg-12 col-lg-offset-0 col-md-12 col-md-offset-0 label-form">
            <label><%= Resources.Avaliacao.FrequenciaAtualizacao%></label>
        </div>
        <div runat="server" class="col-lg-12 col-lg-offset-0 col-md-12 col-md-offset-0 campo-form">
            <asp:RadioButtonList ID="rdbRespostaFrequenciaAtualizacao" runat="server" RepeatDirection="Horizontal" TabIndex="4" CssClass="componente-form">
            </asp:RadioButtonList>
        </div>
        <div runat="server" class="col-lg-12 col-lg-offset-0 col-md-12 col-md-offset-0 label-form">
            <label><%= Resources.Avaliacao.Layout%></label>
        </div>
        <div runat="server" class="col-lg-12 col-lg-offset-0 col-md-12 col-md-offset-0 campo-form">
            <asp:RadioButtonList ID="rdbRespostaLayout" runat="server" RepeatDirection="Horizontal" TabIndex="5" CssClass="componente-form">
            </asp:RadioButtonList>
        </div>

        <div runat="server" class="col-lg-12 col-lg-offset-0 col-md-12 col-md-offset-0 label-form">
            <label><%= Resources.Enquete.EtapaFinal%></label>
        </div>
        <div class="col-lg-12 col-lg-offset-0 col-md-12 col-md-offset-0 label-form">
            <label><%= Resources.Enquete.Sugestoes %></label>
        </div>
        <div runat="server" class="col-lg-12 col-lg-offset-0 col-md-12 col-md-offset-0 campo-form">
            <asp:TextBox ID="txtSugestao" runat="server" TabIndex="6" MaxLength="200" placeholder="Digite a mensagem"></asp:TextBox>
        </div>
        <div class="col-lg-12 col-lg-offset-0 col-md-12 col-md-offset-0 label-form">
            <label><%= Resources.Enquete.TextoDuvidas %></label>
            <asp:HyperLink id="hplFaleConosco" runat="server"></asp:HyperLink>
        </div>

        <div runat="server" class="col-lg-12 col-lg-offset-0 col-md-12 col-md-offset-0 label-form">
            <label><%= Resources.Enquete.Nome%>*</label>
        </div>
        <div runat="server" class="col-lg-12 col-lg-offset-0 col-md-12 col-md-offset-0 campo-form">
            <asp:TextBox ID="txtNome" runat="server" TabIndex="7" MaxLength="128" placeholder="Digite seu nome"></asp:TextBox>
            <asp:RequiredFieldValidator ID="rfvNome" runat="server" ControlToValidate="txtNome" ValidationGroup="ModAvaliacao"></asp:RequiredFieldValidator>
        </div>

        <div class="col-lg-12 col-lg-offset-0 col-md-12 col-md-offset-0 label-form">
            <label><%= Resources.Enquete.Email %>*</label>
        </div>
        <div runat="server" class="col-lg-12 col-lg-offset-0 col-md-12 col-md-offset-0 campo-form">
            <asp:TextBox ID="txtEmail" runat="server" TabIndex="8" MaxLength="128" placeholder="exemplo@mail.com"></asp:TextBox>
            <asp:RequiredFieldValidator ID="rfvEmail" runat="server" ControlToValidate="txtEmail" Display="Dynamic" ValidationGroup="ModAvaliacao"></asp:RequiredFieldValidator>
            <asp:RegularExpressionValidator ID="revEmail" runat="server" ControlToValidate="txtEmail" Display="Dynamic" ValidationGroup="ModAvaliacao"
                ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"></asp:RegularExpressionValidator>
        </div>


        <div runat="server" class="col-lg-3 col-lg-offset-0 col-md-12 col-md-offset-0">
            <asp:Button ID="btnEnviar" runat="server" TabIndex="9" ValidationGroup="ModAvaliacao" />
        </div>

    </div>
</div>

<script src="<%=ResolveUrl("~/JS/jquery-1.10.2.min.js")%>"></script>
<script src="<%=ResolveUrl("~/JS/jquery.mask.js")%>"></script>