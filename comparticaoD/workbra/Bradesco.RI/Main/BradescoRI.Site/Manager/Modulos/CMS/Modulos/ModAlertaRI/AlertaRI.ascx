<%@ Control Language="C#" AutoEventWireup="true" CodeFile="AlertaRI.ascx.cs" Inherits="Modulos_CMS_Modulos_ModAlertaRI_AlertaRI" %>

<div id="divConteudo" runat="server">
    <div class="row form-alerta-ri">
        <div>
            <label id="lblMensagemSucesso" runat="server" visible="false"><%= Resources.AlertaRI.EnvioSucesso %></label>
        </div>
        <div>
            <label id="lblMensagemErro" runat="server" visible="false"><%= Resources.AlertaRI.EnvioErro %></label>
        </div>
        <div>
            <label id="lblMensagemEmailExiste" runat="server" visible="false"><%= Resources.AlertaRI.EnvioErroEmail %></label>
        </div>

        <div runat="server" class="col-lg-12 col-lg-offset-0 col-md-12 col-md-offset-0 label-form">
            <label><%= Resources.AlertaRI.Nome %>*</label>
        </div>
        <div runat="server" class="col-lg-12 col-lg-offset-0 col-md-12 col-md-offset-0 campo-form">
            <asp:TextBox ID="txtNome" runat="server" TabIndex="1" MaxLength="65"></asp:TextBox>
            <asp:RequiredFieldValidator ID="rfvNome" runat="server" ControlToValidate="txtNome" ValidationGroup="ModAlertaRI"></asp:RequiredFieldValidator>
        </div>

        <div class="col-lg-12 col-lg-offset-0 col-md-12 col-md-offset-0 label-form">
            <label><%= Resources.AlertaRI.Email %>*</label>
        </div>
        <div runat="server" class="col-lg-12 col-lg-offset-0 col-md-12 col-md-offset-0 campo-form">
            <asp:TextBox ID="txtEmail" runat="server" TabIndex="2" MaxLength="50" placeholder="exemplo@mail.com"></asp:TextBox>
            <asp:RequiredFieldValidator ID="rfvEmail" runat="server" ControlToValidate="txtEmail" Display="Dynamic" ValidationGroup="ModAlertaRI"></asp:RequiredFieldValidator>
            <asp:RegularExpressionValidator ID="revEmail" runat="server" ControlToValidate="txtEmail" Display="Dynamic" ValidationGroup="ModAlertaRI"
                ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"></asp:RegularExpressionValidator>
        </div>

        <div runat="server" class="col-lg-12 col-lg-offset-0 col-md-12 col-md-offset-0 label-form">
            <label><%= Resources.AlertaRI.Telefone %></label>
        </div>
        <div runat="server" class="col-lg-12 col-lg-offset-0 col-md-12 col-md-offset-0 campo-form">
            <asp:TextBox runat="server" ID="txtTelefoneDDD" CssClass="txtNumeroDdd" MaxLength="2" TabIndex="3" placeholder="(DDD)"></asp:TextBox>
            <asp:TextBox runat="server" ID="txtTelefone" CssClass="txtNumero" MaxLength="15" TabIndex="4" placeholder="xxxx-xxxx"></asp:TextBox>
        </div>

        <div class="col-lg-12 col-lg-offset-0 col-md-12 col-md-offset-0 label-form">
            <label><%= Resources.AlertaRI.EstadoResidencia %></label>
        </div>
        <div runat="server" class="col-lg-12 col-lg-offset-0 col-md-12 col-md-offset-0 campo-form">
            <asp:TextBox ID="txtEstado" runat="server" MaxLength="65" TabIndex="5" placeholder="Digite os estado aonde mora"></asp:TextBox>
        </div>
        <div runat="server" class="col-lg-12 col-lg-offset-0 col-md-12 col-md-offset-0">
        <div runat="server" class="col-lg-6 col-lg-offset-0 col-md-12 col-md-offset-0 label-form2">
            <label><%= Resources.AlertaRI.PaisResidencia %></label>
        </div>
        <div runat="server" class="col-lg-6 col-lg-offset-0 col-md-12 col-md-offset-0 campo-form">
            <asp:DropDownList runat="server" ID="ddlPais" TabIndex="6" CssClass="componente-form"></asp:DropDownList>
        </div></div>
        
        <div runat="server" class="col-lg-12 col-lg-offset-0 col-md-12 col-md-offset-0 label-form3">
            <label><%= Resources.AlertaRI.DesejaMailing %></label>
        </div>
        <div runat="server" class="col-lg-12 col-lg-offset-0 col-md-12 col-md-offset-0 campo-form">
            <asp:RadioButtonList ID="rdlMailing" runat="server" RepeatDirection="Horizontal" TabIndex="7" CssClass="componente-form">
                <asp:ListItem Text="Sim" Value="1" ></asp:ListItem>
                <asp:ListItem Text="Não" Value="0" ></asp:ListItem>
            </asp:RadioButtonList>
        </div>

        <div class="col-lg-12 col-lg-offset-0 col-md-12 col-md-offset-0 label-form">
            <label><%= Resources.AlertaRI.SelecioneIdioma %></label>
        </div>
        <div runat="server" class="col-lg-12 col-lg-offset-0 col-md-12 col-md-offset-0 campo-form">
            <asp:RadioButtonList ID="rdlIdioma" runat="server" RepeatDirection="Horizontal" TabIndex="8" CssClass="componente-form">
                <asp:ListItem Value="1"></asp:ListItem>
                <asp:ListItem Value="2"></asp:ListItem>
                <asp:ListItem Value="3"></asp:ListItem>
            </asp:RadioButtonList>
        </div>

       <div class="col-lg-12 col-lg-offset-0 col-md-12 col-md-offset-0 label-form">
            <label><%= Resources.AlertaRI.ProfissionalMercadoCapitais %></label>
        </div>
        <div runat="server" class="col-lg-12 col-lg-offset-0 col-md-12 col-md-offset-0 campo-form">
            <asp:RadioButtonList ID="rdlProfissionalMercado" runat="server" RepeatDirection="Horizontal" TabIndex="9" CssClass="componente-form">
                <asp:ListItem Value="1"></asp:ListItem>
                <asp:ListItem Value="0"></asp:ListItem>
            </asp:RadioButtonList>
            
        </div>

       <div class="col-lg-12 col-lg-offset-0 col-md-12 col-md-offset-0 label-form">
            <label><%= Resources.AlertaRI.Empresa %></label>
        </div>
        <div runat="server" class="col-lg-12 col-lg-offset-0 col-md-12 col-md-offset-0 campo-form">
            <asp:TextBox ID="txtEmpresa" runat="server" TabIndex="10" MaxLength="65" placeholder="Digite a empresa da qual você faz parte"></asp:TextBox>
        </div>

        <div runat="server" class="col-lg-12 col-lg-offset-0 col-md-12 col-md-offset-0 campo-form">
            <div runat="server" class="col-lg-6 col-lg-offset-0 col-md-12 col-md-offset-0 label-form2">
                <asp:DropDownList runat="server" ID="ddlSegmentoEmpresa" TabIndex="11" CssClass="componente-form"></asp:DropDownList>
                 </div>
            <div runat="server" class="col-lg-3 col-lg-offset-0 col-md-12 col-md-offset-0 btn-enviar">
                <asp:Button ID="btnEnviar" class="l-impor2" runat="server" OnClick="btnEnviar_Click" TabIndex="12" ValidationGroup="ModAlertaRI" />
             </div>
        </div>

        
    </div>
</div>

<script src="<%=ResolveUrl("~/JS/jquery-1.10.2.min.js")%>"></script>
<script src="<%=ResolveUrl("~/JS/jquery.mask.js")%>"></script>
<script src="<%=ResolveUrl("~/JS/ModAlertaRI.js")%>"></script>
