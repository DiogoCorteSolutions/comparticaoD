<%@ Page Title="" Language="C#" MasterPageFile="~/Modulos/Modulos.master" AutoEventWireup="true" CodeFile="Editar.aspx.cs" Inherits="Modulos_Footer_Editar" %>

<asp:Content ID="Content1" ContentPlaceHolderID="contentInterna" runat="Server">
    <div id="title">
        <h1>
            <asp:Label ID="lblTitulo" runat="server"><%=Resources.Footer.Titulo %></asp:Label>
            <asp:HiddenField ID="hndFooterId" runat="server" />
        </h1>
    </div>
    <br />
    <div>
        <label>
            <span>
                <asp:Label ID="lblTituloN1" runat="server" AssociatedControlID="txtTituloN1" title=""><%=Resources.Footer.LabelTituloN1 %>*</asp:Label>
            </span>
            <asp:TextBox ID="txtTituloN1" runat="server"></asp:TextBox>
            <asp:RequiredFieldValidator ID="rfvTituloN1" runat="server" ControlToValidate="txtTituloN1" ValidationGroup="vgrFooter" Display="Dynamic" title="Campo obrigatório"></asp:RequiredFieldValidator>
        </label>
        <label>
            <span>
                <asp:Label ID="lblTelefoneN1" runat="server" AssociatedControlID="txtTelefoneN1" title=""><%=Resources.Footer.LabelTelefoneN1 %>*</asp:Label>
            </span>
            <asp:TextBox ID="txtTelefoneN1" runat="server"></asp:TextBox>
            <asp:RequiredFieldValidator ID="rfvTelefoneN1" runat="server" ControlToValidate="txtTelefoneN1" ValidationGroup="vgrFooter" Display="Dynamic" title="Campo obrigatório"></asp:RequiredFieldValidator>
        </label>
        <label>
            <span>
                <asp:Label ID="lblTextoN1" runat="server" AssociatedControlID="txtTextoN1" title=""><%=Resources.Footer.LabelTextoN1 %>*</asp:Label>
            </span>
            <asp:TextBox ID="txtTextoN1" runat="server"></asp:TextBox>
            <asp:RequiredFieldValidator ID="rfvTextoN1" runat="server" ControlToValidate="txtTextoN1" ValidationGroup="vgrFooter" Display="Dynamic" title="Campo obrigatório"></asp:RequiredFieldValidator>
        </label>
    </div>
    <div>
        <label>
            <span>
                <asp:Label ID="lblTituloN2" runat="server" AssociatedControlID="txtTituloN2" title=""><%=Resources.Footer.LabelTituloN2 %>*</asp:Label>
            </span>
            <asp:TextBox ID="txtTituloN2" runat="server"></asp:TextBox>
            <asp:RequiredFieldValidator ID="rfvTitloN2" runat="server" ControlToValidate="txtTituloN2" ValidationGroup="vgrFooter" Display="Dynamic" title="Campo obrigatório"></asp:RequiredFieldValidator>
        </label>
        <label>
            <span>
                <asp:Label ID="lblTelefoneN2" runat="server" AssociatedControlID="txtTelefoneN2" title=""><%=Resources.Footer.LabelTelefoneN2 %>*</asp:Label>
            </span>
            <asp:TextBox ID="txtTelefoneN2" runat="server"></asp:TextBox>
            <asp:RequiredFieldValidator ID="rfvTelefoneN2" runat="server" ControlToValidate="txtTelefoneN2" ValidationGroup="vgrFooter" Display="Dynamic" title="Campo obrigatório"></asp:RequiredFieldValidator>
        </label>
        <label>
            <span>
                <asp:Label ID="lblTextoN2" runat="server" AssociatedControlID="txtTextoN2" title=""><%=Resources.Footer.LabelTextoN2 %>*</asp:Label>
            </span>
            <asp:TextBox ID="txtTextoN2" runat="server"></asp:TextBox>
            <asp:RequiredFieldValidator ID="rfvTextoN2" runat="server" ControlToValidate="txtTextoN2" ValidationGroup="vgrFooter" Display="Dynamic" title="Campo obrigatório"></asp:RequiredFieldValidator>
        </label>
    </div>
    <div>
        <label>
            <span>
                <asp:Label ID="lblTituloN3" runat="server" AssociatedControlID="txtTituloN3" title=""><%=Resources.Footer.LabelTituloN3 %>*</asp:Label>
            </span>
            <asp:TextBox ID="txtTituloN3" runat="server"></asp:TextBox>
            <asp:RequiredFieldValidator ID="rfvTituloN3" runat="server" ControlToValidate="txtTituloN3" ValidationGroup="vgrFooter" Display="Dynamic" title="Campo obrigatório"></asp:RequiredFieldValidator>
        </label>
        <label>
            <span>
                <asp:Label ID="lblTelefoneN3" runat="server" AssociatedControlID="txtTelefoneN3" title=""><%=Resources.Footer.LabelTelefoneN3 %>*</asp:Label>
            </span>
            <asp:TextBox ID="txtTelefoneN3" runat="server"></asp:TextBox>
            <asp:RequiredFieldValidator ID="rfvTelefoneN3" runat="server" ControlToValidate="txtTelefoneN3" ValidationGroup="vgrFooter" Display="Dynamic" title="Campo obrigatório"></asp:RequiredFieldValidator>
        </label>
        <label>
            <span>
                <asp:Label ID="lblTextoN3" runat="server" AssociatedControlID="txtTextoN3" title=""><%=Resources.Footer.LabelTextoN3 %>*</asp:Label>
            </span>
            <asp:TextBox ID="txtTextoN3" runat="server"></asp:TextBox>
            <asp:RequiredFieldValidator ID="rfvTextoN3" runat="server" ControlToValidate="txtTextoN3" ValidationGroup="vgrFooter" Display="Dynamic" title="Campo obrigatório"></asp:RequiredFieldValidator>
        </label>
    </div>
    <div>
        <label>
            <span>
                <asp:Label ID="lblTextoCentral" runat="server" AssociatedControlID="txtTextoCentral" title="Idioma"><%=Resources.Footer.TextoCentral %>*</asp:Label>
            </span>
            <asp:TextBox ID="txtTextoCentral" runat="server"></asp:TextBox>
            <asp:RequiredFieldValidator ID="rfvTextoCentral" runat="server" ControlToValidate="txtTextoCentral" ValidationGroup="vgrFooter" Display="Dynamic" title="Campo obrigatório"></asp:RequiredFieldValidator>
        </label>
    </div>
    <div>
            <label>
            <span>
                <asp:Label ID="lblTituloLinkN1" runat="server" AssociatedControlID="txtTituloLinkN1" title=""><%=Resources.Footer.LabelTituloLinkN1 %>*</asp:Label>
            </span>
            <asp:TextBox ID="txtTituloLinkN1" runat="server"></asp:TextBox>
            <asp:RequiredFieldValidator ID="rfvTituloLinkN1" runat="server" ControlToValidate="txtTituloLinkN1" ValidationGroup="vgrFooter" Display="Dynamic" title="Campo obrigatório"></asp:RequiredFieldValidator>
        </label>
        <label>
            <span>
                <asp:Label ID="lblUrlLinkN1" runat="server" AssociatedControlID="txtUrlLinkN1" title=""><%=Resources.Footer.LabelUrlLinkN1 %>*</asp:Label>
            </span>
            <asp:TextBox ID="txtUrlLinkN1" runat="server"></asp:TextBox>
            <asp:RequiredFieldValidator ID="rfvUrlLinkN1" runat="server" ControlToValidate="txtUrlLinkN1" ValidationGroup="vgrFooter" Display="Dynamic" title="Campo obrigatório"></asp:RequiredFieldValidator>
        </label>
    </div>
    <div>
        <label>
            <span>
                <asp:Label ID="lblTituloLinkN2" runat="server" AssociatedControlID="txtTituloLinkN2" title=""><%=Resources.Footer.LabelTituloLinkN2 %>*</asp:Label>
            </span>
            <asp:TextBox ID="txtTituloLinkN2" runat="server"></asp:TextBox>
            <asp:RequiredFieldValidator ID="rfvTituloLinkN2" runat="server" ControlToValidate="txtTituloLinkN2" ValidationGroup="vgrFooter" Display="Dynamic" title="Campo obrigatório"></asp:RequiredFieldValidator>
        </label>
        <label>
            <span>
                <asp:Label ID="lblUrlLinkN2" runat="server" AssociatedControlID="txtUrlLinkN2" title=""><%=Resources.Footer.LabelUrlLinkN2 %>*</asp:Label>
            </span>
            <asp:TextBox ID="txtUrlLinkN2" runat="server"></asp:TextBox>
            <asp:RequiredFieldValidator ID="rfvUrlLinkN2" runat="server" ControlToValidate="txtUrlLinkN2" ValidationGroup="vgrFooter" Display="Dynamic" title="Campo obrigatório"></asp:RequiredFieldValidator>
        </label>    
    </div>
    <div>
        <label>
            <span>
                <asp:Label ID="lblTituloLinkN3" runat="server" AssociatedControlID="txtTituloLinkN3" title=""><%=Resources.Footer.LabelTituloLinkN3 %>*</asp:Label>
            </span>
            <asp:TextBox ID="txtTituloLinkN3" runat="server"></asp:TextBox>
            <asp:RequiredFieldValidator ID="rfvTituloLinkN3" runat="server" ControlToValidate="txtTituloLinkN3" ValidationGroup="vgrFooter" Display="Dynamic" title="Campo obrigatório"></asp:RequiredFieldValidator>
        </label>
        <label>
            <span>
                <asp:Label ID="lblUrlLinkN3" runat="server" AssociatedControlID="txtUrlLinkN3" title=""><%=Resources.Footer.LabelUrlLinkN3 %>*</asp:Label>
            </span>
            <asp:TextBox ID="txtUrlLinkN3" runat="server"></asp:TextBox>
            <asp:RequiredFieldValidator ID="rfvUrlLinkN3" runat="server" ControlToValidate="txtUrlLinkN3" ValidationGroup="vgrFooter" Display="Dynamic" title="Campo obrigatório"></asp:RequiredFieldValidator>
        </label>
    </div>
    <div>
    <label>
        <span>
            <asp:Label ID="lblTituloLinkN4" runat="server" AssociatedControlID="txtTituloLinkN4" title=""><%=Resources.Footer.LabelTituloLinkN4 %>*</asp:Label>
        </span>
        <asp:TextBox ID="txtTituloLinkN4" runat="server"></asp:TextBox>
        <asp:RequiredFieldValidator ID="rfvTituloLinkN4" runat="server" ControlToValidate="txtTituloLinkN4" ValidationGroup="vgrFooter" Display="Dynamic" title="Campo obrigatório"></asp:RequiredFieldValidator>
        <span>
            <asp:Label ID="lblTituloLinkN5" runat="server" AssociatedControlID="txtTituloLinkN5" title=""><%=Resources.Footer.LabelUrlLinkN4 %>*</asp:Label>
        </span>
        <asp:TextBox ID="txtTituloLinkN5" runat="server"></asp:TextBox>
        <asp:RequiredFieldValidator ID="rfvTituloLinkN5" runat="server" ControlToValidate="txtTituloLinkN5" ValidationGroup="vgrFooter" Display="Dynamic" title="Campo obrigatório"></asp:RequiredFieldValidator>
    </label>
    </div>
    <div>
        
        <label>
            <span>
                <asp:Label ID="lblUrlLinkN4" runat="server" AssociatedControlID="txtUrlLinkN4" title=""><%=Resources.Footer.LabelTituloLinkN5 %>*</asp:Label>
            </span>
            <asp:TextBox ID="txtUrlLinkN4" runat="server"></asp:TextBox>
            <asp:RequiredFieldValidator ID="rfvUrlLinkN4" runat="server" ControlToValidate="txtUrlLinkN4" ValidationGroup="vgrFooter" Display="Dynamic" title="Campo obrigatório"></asp:RequiredFieldValidator>
        </label>
        <label>
            <span>
                <asp:Label ID="lblUrlLinkN5" runat="server" AssociatedControlID="txtUrlLinkN5" title=""><%=Resources.Footer.LabelUrlLinkN5 %>*</asp:Label>
            </span>
            <asp:TextBox ID="txtUrlLinkN5" runat="server"></asp:TextBox>
            <asp:RequiredFieldValidator ID="rfvUrlLinkN5" runat="server" ControlToValidate="txtUrlLinkN5" ValidationGroup="vgrFooter" Display="Dynamic" title="Campo obrigatório"></asp:RequiredFieldValidator>
        </label>
    </div>
    <div class="btn-acoes2">
        <asp:PlaceHolder runat="server" ID="phOptions" Visible="true">
            <asp:Button runat="server" ID="btnCancelar" CssClass="cancelar" Text="Voltar" OnClick="btnCancelar_Click" CausesValidation="false" title="Cancelar" alt="Cancelar" />
            <asp:Button runat="server" ID="btnSalvar" CssClass="submit" Text="Salvar" ValidationGroup="vgrFooter" OnClick="btnSalvar_Click" title="Salvar" alt="Salvar" />
        </asp:PlaceHolder>
    </div>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphModalAlerta" runat="Server">
</asp:Content>

