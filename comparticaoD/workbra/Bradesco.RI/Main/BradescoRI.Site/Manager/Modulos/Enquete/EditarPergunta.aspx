<%@ Page Language="C#" MasterPageFile="~/Modulos/Modulos.master" AutoEventWireup="true" CodeFile="EditarPergunta.aspx.cs" Inherits="Modulos_Enquete_EditarPerguntas" %>

<asp:Content ID="Content1" ContentPlaceHolderID="contentInterna" runat="Server">
    <div id="title">
        <h1>
            <asp:Label ID="lblTitulo" runat="server"><%=Resources.Enquetes.Titulo %></asp:Label>
        </h1>
    </div>
    <br />
    <div class="fields">
        <label>
            <span>
                <asp:Label ID="lblEnquete" runat="server" AssociatedControlID="txtEnquete" title="Enquete"><%=Resources.Enquetes.Enquete %>*</asp:Label>
            </span>
            <asp:TextBox runat="server" ID="txtEnquete" CssClass="frmTxt" Enabled="false"></asp:TextBox>
        </label>
        <label>
            <span>
                <asp:Label ID="lblPergunta" runat="server" AssociatedControlID="txtPergunta" title="Pergunta"><%=Resources.Enquetes.Pergunta %>*</asp:Label>
            </span>
            <asp:TextBox runat="server" ID="txtPergunta" CssClass="frmTxt" MaxLength="400" title="Informe a pergunta"></asp:TextBox>
            <asp:RequiredFieldValidator ID="rfvPergunta" runat="server" ControlToValidate="txtPergunta" Display="Dynamic" title="Campo obrigatório"></asp:RequiredFieldValidator>
        </label>
        <label>
            <span>
                <asp:Label ID="lblIdioma" runat="server" AssociatedControlID="ddlIdioma" title="Idioma"><%=Resources.Enquetes.Idioma %>*</asp:Label>
            </span>
            <asp:DropDownList runat="server" ID="ddlIdioma" title="Selecione o Idioma"></asp:DropDownList>
            <asp:RequiredFieldValidator ID="rfvidioma" runat="server" ControlToValidate="ddlIdioma" Display="Dynamic" title="Campo obrigatório"></asp:RequiredFieldValidator>
        </label>

        <label>
            <span>
                <asp:Label ID="lblResposta1" runat="server" AssociatedControlID="txtResposta1" title="Resposta1"><%=Resources.Enquetes.Resposta1 %>*</asp:Label>
            </span>
            <asp:TextBox runat="server" ID="txtResposta1" CssClass="frmTxt" MaxLength="100" title="Informe a resposta1"></asp:TextBox>
            <asp:RequiredFieldValidator ID="rfvResposta1" runat="server" ControlToValidate="txtResposta1" Display="Dynamic" title="Campo obrigatório"></asp:RequiredFieldValidator>
        </label>
        <label>
            <span>
                <asp:Label ID="lblResposta2" runat="server" AssociatedControlID="txtResposta2" title="Resposta2"><%=Resources.Enquetes.Resposta2 %>*</asp:Label>
            </span>
            <asp:TextBox runat="server" ID="txtResposta2" CssClass="frmTxt" MaxLength="100" title="Informe a resposta2"></asp:TextBox>
            <asp:RequiredFieldValidator ID="rfvResposta2" runat="server" ControlToValidate="txtResposta2" Display="Dynamic" title="Campo obrigatório"></asp:RequiredFieldValidator>
        </label>
        <label>
            <span>
                <asp:Label ID="lblResposta3" runat="server" AssociatedControlID="txtResposta3" title="Resposta3"><%=Resources.Enquetes.Resposta3 %></asp:Label>
            </span>
            <asp:TextBox runat="server" ID="txtResposta3" CssClass="frmTxt" MaxLength="100" title="Informe a resposta3"></asp:TextBox>            
        </label>
        <label>
            <span>
                <asp:Label ID="lblResposta4" runat="server" AssociatedControlID="txtResposta4" title="Resposta4"><%=Resources.Enquetes.Resposta4 %></asp:Label>
            </span>
            <asp:TextBox runat="server" ID="txtResposta4" CssClass="frmTxt" MaxLength="100" title="Informe a resposta4"></asp:TextBox>            
        </label>
        <label>
            <span>
                <asp:Label ID="lblResposta5" runat="server" AssociatedControlID="txtResposta5" title="Resposta5"><%=Resources.Enquetes.Resposta5 %></asp:Label>
            </span>
            <asp:TextBox runat="server" ID="txtResposta5" CssClass="frmTxt" MaxLength="100" title="Informe a resposta5"></asp:TextBox>            
        </label>



    </div>
    <div class="fields">
        <label>
            <span>
                <asp:Label ID="lblCamposObrigatorios" runat="server" title="Campos obrigatórios"><%=Resources.Textos.Campos_Obrigatorios %></asp:Label>
            </span>
        </label>

    </div>

    <div class="btn-acoes2">
        <asp:PlaceHolder runat="server" ID="phOptions" Visible="true">
            <asp:Button runat="server" ID="btnCancelar" CssClass="cancelar" Text="Voltar" OnClick="btnCancelar_Click" CausesValidation="false" title="Cancelar" alt="Cancelar" />
            <asp:Button runat="server" ID="btnOK" CssClass="submit" Text="Salvar" OnClick="btnOK_Click" title="Salvar" alt="Salvar" />
        </asp:PlaceHolder>
    </div>

</asp:Content>


