<%@ Page Language="C#" MasterPageFile="~/Modulos/Modulos.master" AutoEventWireup="true" CodeFile="Editar.aspx.cs" Inherits="Modulos_Eventos_Editar" %>


<asp:Content ID="Content1" ContentPlaceHolderID="contentInterna" runat="Server">

    <div id="title">
        <h1>
            <asp:Label ID="lblTitulo" runat="server"><%=Resources.Eventos.Titulo %></asp:Label>
        </h1>
    </div>
    <br />
    <div class="fields">
        <label>
            <span>
                <asp:Label ID="lblIdioma" runat="server" AssociatedControlID="ddlIdioma" title="Idioma"><%=Resources.Eventos.Idioma %>*</asp:Label>
            </span>
            <asp:DropDownList runat="server" ID="ddlIdioma" title="Selecione o Idioma"></asp:DropDownList>
            <asp:RequiredFieldValidator ID="rfvidioma" runat="server" ControlToValidate="ddlIdioma" Display="Dynamic" title="Campo obrigatório"></asp:RequiredFieldValidator>
        </label>
        <label>
            <span>
                <asp:Label ID="lblTipoEvento" runat="server" AssociatedControlID="ddlTipoEvento" title="Tipo Evento"><%=Resources.Eventos.TipoEvento %>*</asp:Label>
            </span>
            <asp:DropDownList runat="server" ID="ddlTipoEvento" title="Selecione o tipo de evento"></asp:DropDownList>
            <asp:RequiredFieldValidator ID="rfvTipoEvento" runat="server" ControlToValidate="ddlTipoEvento" Display="Dynamic" title="Campo obrigatório"></asp:RequiredFieldValidator>
        </label>
        <label>
            <span>
                <asp:Label ID="lblTituloEvento" runat="server" AssociatedControlID="txtTitulo" title="Grupo"><%=Resources.Eventos.TituloEvento %>*</asp:Label>
            </span>
            <asp:TextBox runat="server" ID="txtTitulo" CssClass="frmTxt" MaxLength="200"></asp:TextBox>
            <asp:RequiredFieldValidator ID="rfvTitulo" runat="server" ControlToValidate="txtTitulo" Display="Dynamic" title="Campo obrigatório"></asp:RequiredFieldValidator>
        </label>
        <label>
            <span>
                <asp:Label ID="lblDescricao" runat="server" AssociatedControlID="txtDescricao" title="Descrição"><%=Resources.Eventos.Descricao %>*</asp:Label>
            </span>
            <asp:TextBox runat="server" ID="txtDescricao" CssClass="frmTxt" MaxLength="200" title="Informe a descrição"></asp:TextBox>
            <asp:RequiredFieldValidator ID="rfvDescricao" runat="server" ControlToValidate="txtDescricao" Display="Dynamic" title="Campo obrigatório"></asp:RequiredFieldValidator>
        </label>
        <label>
            <span>
                <asp:Label ID="lblTexto" runat="server" AssociatedControlID="txtTexto" title="Tooltip"><%=Resources.Eventos.Texto %>*</asp:Label>
            </span>
            <asp:TextBox runat="server" ID="txtTexto" CssClass="frmTxt" title="Informe o texto"></asp:TextBox>
            <asp:RequiredFieldValidator ID="rfvTexto" runat="server" ControlToValidate="txtTexto" Display="Dynamic" title="Campo obrigatório"></asp:RequiredFieldValidator>
        </label>
        <label>
            <span>
                <asp:Label ID="lblResposavel" runat="server" AssociatedControlID="txtResponsavel" title="Responsável"><%=Resources.Eventos.Responsavel %></asp:Label>
            </span>
            <asp:TextBox runat="server" ID="txtResponsavel" CssClass="frmTxt" MaxLength="50" title="Informe o responsável"></asp:TextBox>
        </label>
         <label>
            <span>
                <asp:Label ID="lblCidade" runat="server" AssociatedControlID="txtCidade" title="Cidade"><%=Resources.Eventos.Cidade %></asp:Label>
            </span>
            <asp:TextBox runat="server" ID="txtCidade" CssClass="frmTxt" MaxLength="50" title="Informe a cidade"></asp:TextBox>
        </label>
        <label>
            <span>
                <asp:Label ID="lblLocal" runat="server" AssociatedControlID="txtLocal" title="Local"><%=Resources.Eventos.Local %></asp:Label>
            </span>
            <asp:TextBox runat="server" ID="txtLocal" CssClass="frmTxt" MaxLength="100" title="Informe o local"></asp:TextBox>
        </label>
        <label>
            <span>
                <asp:Label ID="lblDataInicio" runat="server" AssociatedControlID="txtDataInicio" title="Data Inicio"><%=Resources.Eventos.DataInicio %>*</asp:Label>
            </span>
            <asp:TextBox runat="server" ID="txtDataInicio" CssClass="frmTxt txtDate" MaxLength="10" title="Informe a data de início"></asp:TextBox>
            <asp:RequiredFieldValidator ID="rfvDataInicio" runat="server" ControlToValidate="txtDataInicio" Display="Dynamic" title="Campo obrigatório"></asp:RequiredFieldValidator>
            <asp:CustomValidator ID="ctvDataInicio" runat="server" ErrorMessage="Data inválida" ControlToValidate="txtDataInicio" OnServerValidate="ValidateDate"></asp:CustomValidator>
        </label>
        <label>
            <span>
                <asp:Label ID="lblDataFim" runat="server" AssociatedControlID="txtDataFim" title="Data Fim"><%=Resources.Eventos.DataFim %></asp:Label>
            </span>
            <asp:TextBox runat="server" ID="txtDataFim" CssClass="frmTxt txtDate" MaxLength="10" title="Informe a data de fim"></asp:TextBox>
            <asp:CustomValidator ID="ctvDataFim" runat="server" ErrorMessage="Data inválida" ControlToValidate="txtDataFim" OnServerValidate="ValidateDate"></asp:CustomValidator>
        </label>
        <label>
            <span>
                <asp:Label ID="lblArquivo" runat="server" title="Arquivo"><%=Resources.Eventos.Arquivo %></asp:Label>
            </span>
            <asp:FileUpload ID="fupArquivo" runat="server" />
            <%--<asp:RegularExpressionValidator ID="rev" runat="server" ValidationExpression="^.*\.(jpg|JPG|jpeg|JPEG|png|PNG)$" ControlToValidate="fupArquivo" ErrorMessage="*"></asp:RegularExpressionValidator>--%>
            <asp:LinkButton ID="lnbArquivo" runat="server"></asp:LinkButton>
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


