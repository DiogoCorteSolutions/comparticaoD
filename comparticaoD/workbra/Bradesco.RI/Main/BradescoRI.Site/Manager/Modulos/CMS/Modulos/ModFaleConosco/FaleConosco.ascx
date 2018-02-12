<%@ Control Language="C#" AutoEventWireup="true" CodeFile="FaleConosco.ascx.cs" Inherits="Modulos_CMS_Modulos_ModFaleConosco_FaleConosco"  EnableViewState="false" %>

<%@ Register Assembly="BDN_Captcha" Namespace="BDN_Captcha" TagPrefix="cc1" %>

<div id="divSemConteudo" runat="server" class="moduloSemConteudo">
    <asp:Label ID="lblTitulo" runat="server"><%= string.Format(Resources.Textos.Modulo_Sem_Conteudo, "Form - Fale Conosco") %></asp:Label>
</div>

<div id="divConteudo" runat="server">
    <div class="row form-alerta-ri">
        <div>
            <label id="lblMensagemSucesso" runat="server" visible="false"><%= Resources.FaleConosco.EnvioSucesso %></label>
        </div>
        <div>
            <label id="lblMensagemErro" runat="server" visible="false"><%= Resources.FaleConosco.EnvioErro %></label>
        </div>

        <div runat="server" class="col-lg-12 col-lg-offset-0 col-md-12 col-md-offset-0 label-form">
            <label><%= Resources.FaleConosco.Nome %>*</label>
        </div>
        <div runat="server" class="col-lg-12 col-lg-offset-0 col-md-12 col-md-offset-0 campo-form">
            <asp:TextBox ID="txtNome" runat="server" TabIndex="1" MaxLength="128"></asp:TextBox>
            <asp:RequiredFieldValidator ID="rfvNome" runat="server" ControlToValidate="txtNome" ValidationGroup="ModFaleConosco"></asp:RequiredFieldValidator>
        </div>

        <div class="col-lg-12 col-lg-offset-0 col-md-12 col-md-offset-0 label-form">
            <label><%= Resources.FaleConosco.Email %>*</label>
        </div>
        <div runat="server" class="col-lg-12 col-lg-offset-0 col-md-12 col-md-offset-0 campo-form">
            <asp:TextBox ID="txtEmail" runat="server" TabIndex="2" MaxLength="128" placeholder="exemplo@mail.com"></asp:TextBox>
            <asp:RequiredFieldValidator ID="rfvEmail" runat="server" ControlToValidate="txtEmail" Display="Dynamic" ValidationGroup="ModFaleConosco"></asp:RequiredFieldValidator>
            <asp:RegularExpressionValidator ID="revEmail" runat="server" ControlToValidate="txtEmail" Display="Dynamic" ValidationGroup="ModFaleConosco"
                ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"></asp:RegularExpressionValidator>
        </div>

        <div runat="server" class="col-lg-12 col-lg-offset-0 col-md-12 col-md-offset-0 label-form">
            <label><%= Resources.FaleConosco.Telefone %></label>
        </div>
        <div runat="server" class="col-lg-12 col-lg-offset-0 col-md-12 col-md-offset-0 campo-form">
            <asp:TextBox runat="server" ID="txtTelefoneDDD" CssClass="txtNumeroDdd" MaxLength="2" TabIndex="3" placeholder="(DDD)"></asp:TextBox>
            <asp:TextBox runat="server" ID="txtTelefone" CssClass="txtNumero" MaxLength="15" TabIndex="4" placeholder="xxxx-xxxx"></asp:TextBox>
        </div>

        <div class="col-lg-12 col-lg-offset-0 col-md-12 col-md-offset-0 label-form">
            <label><%= Resources.AlertaRI.Empresa %></label>
        </div>
        <div runat="server" class="col-lg-12 col-lg-offset-0 col-md-12 col-md-offset-0 campo-form">
            <asp:TextBox ID="txtEmpresa" runat="server" TabIndex="5" MaxLength="128" placeholder="Digite a empresa da qual você faz parte"></asp:TextBox>
        </div>


        <div class="col-lg-12 col-lg-offset-0 col-md-12 col-md-offset-0 label-form">
            <label><%= Resources.FaleConosco.Assunto %></label>
        </div>
        <div runat="server" class="col-lg-12 col-lg-offset-0 col-md-12 col-md-offset-0 campo-form">
            <asp:DropDownList runat="server" ID="ddlAssunto" TabIndex="6" CssClass="componente-form">
            </asp:DropDownList>
        </div>

        <div runat="server" class="col-lg-12 col-lg-offset-0 col-md-12 col-md-offset-0 label-form">
            <label><%= Resources.FaleConosco.Mensagem %>*</label>
        </div>
        <div runat="server" class="col-lg-12 col-lg-offset-0 col-md-12 col-md-offset-0 campo-form">
            <asp:TextBox ID="txtMensagem" runat="server" TabIndex="7" MaxLength="128" placeholder="Digite a mensagem"></asp:TextBox>
            <asp:RequiredFieldValidator ID="rfvMensagem" runat="server" ControlToValidate="txtMensagem" ValidationGroup="ModFaleConosco"></asp:RequiredFieldValidator>
        </div>
        <!-- Inicio Captcha DBDN  -->
        <cc1:CaptchaControl ID="CaptchaFaleConosco" runat="server"
            LayoutStyle="Horizontal"
            CaptchaBackgroundNoise="Medium"
            CaptchaFontWarping="Medium"
            CaptchaLineNoise="Medium"
            CaptchaMaxTimeout="180"
            CaptchaMinTimeout="3"
            Text="Digite o código da imagem"
            AudioCaptchaFlashPath="SWF/"
            CssClass="captcha-bdn" />
        <!-- Fim Captcha DBDN  -->
        <div runat="server" class="col-lg-3 col-lg-offset-0 col-md-12 col-md-offset-0">
            <asp:Button ID="btnEnviar" runat="server" TabIndex="8" ValidationGroup="ModFaleConosco" OnClick="btnEnviar_Click" />
        </div>
    </div>
</div>

<script src="<%=ResolveUrl("~/JS/jquery-1.10.2.min.js")%>"></script>
<script src="<%=ResolveUrl("~/JS/jquery.mask.js")%>"></script>




