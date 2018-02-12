<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Contato.ascx.cs" Inherits="Modulos_CMS_Modulos_ModContato_Contato" %>

<div id="divSemConteudo" runat="server" class="moduloSemConteudo">
    <asp:Label ID="lblTitulo" runat="server"><%= string.Format(Resources.Textos.Modulo_Sem_Conteudo, "Contato") %></asp:Label>
</div>

<div id="divConteudo" runat="server">
    <div>
        <label><%= Resources.ModContato.Nome %></label>
        <asp:TextBox ID="txtNome" runat="server" MaxLength="200"></asp:TextBox>
        <asp:RequiredFieldValidator ID="rfvNome" runat="server" ControlToValidate="txtNome" Text="*" ValidationGroup="vgrModContato"></asp:RequiredFieldValidator>
    </div>
    <div>
        <label><%= Resources.ModContato.Email %></label>
        <asp:TextBox ID="txtEmail" runat="server" MaxLength="200"></asp:TextBox>
        <asp:RequiredFieldValidator ID="rfvEmail" runat="server" ControlToValidate="txtEmail" Text="*" ValidationGroup="vgrModContato"></asp:RequiredFieldValidator>
    </div>
    <div>
        <label><%= Resources.ModContato.Telefone %></label>
        <asp:TextBox ID="txtTelefoneDdd" runat="server" MaxLength="2" Width="30px"></asp:TextBox>
        <asp:TextBox ID="txtTelefone" runat="server" MaxLength="15"></asp:TextBox>
    </div>
    <div>
        <label><%= Resources.ModContato.Empresa %></label>
        <asp:TextBox ID="txtEmpresa" runat="server" MaxLength="200"></asp:TextBox>
    </div>
    <div>
        <label><%= Resources.ModContato.Assunto %></label>
        <asp:DropDownList ID="ddlAssuntos" runat="server"></asp:DropDownList>
        <asp:RequiredFieldValidator ID="rfvAssunto" runat="server" ControlToValidate="ddlAssuntos" InitialValue="0" Text="*" ValidationGroup="vgrModContato"></asp:RequiredFieldValidator>
    </div>
    <div>
        <label><%= Resources.ModContato.Mensagem %></label>
        <asp:TextBox ID="txtMensagem" runat="server" Columns="100" Rows="10" TextMode="MultiLine"></asp:TextBox>
        <asp:RequiredFieldValidator ID="rfvMensagem" runat="server" ControlToValidate="txtMensagem" Text="*" ValidationGroup="vgrModContato"></asp:RequiredFieldValidator>
    </div>
    <div>
        <asp:Button ID="btnEnviar" runat="server" OnClick="btnEnviar_Click" ValidationGroup="vgrModContato" />
    </div>
    <div>
        <label id="lblMensagem" runat="server" visible="false"><%= Resources.ModContato.MensagemSucesso %></label>
    </div>
</div>
