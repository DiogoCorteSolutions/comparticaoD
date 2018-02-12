<%@ Page Title="" Language="C#" MasterPageFile="~/Modulos/Modulos.master" AutoEventWireup="true" CodeFile="EditarArquivo.aspx.cs" Inherits="Modulos_Arquivos_EditarArquivo" %>

<asp:Content ID="Content1" ContentPlaceHolderID="contentInterna" runat="Server">
    <div id="title">
        <h1>
            <asp:Label ID="lblTitulo" runat="server"><%=Resources.Arquivos.Titulo %></asp:Label>
            <asp:HiddenField ID="hdnArquivosId" runat="server" />
        </h1>
    </div>
    <br />
    <div>
        <label>
            <span>
                <asp:Label ID="lblIdioma" runat="server" AssociatedControlID="ddlIdioma" title="Idioma"><%=Resources.Noticias.Idioma %>*</asp:Label>
            </span>
            <asp:DropDownList runat="server" ID="ddlIdioma" title="Selecione o Idioma"></asp:DropDownList>
            <asp:RequiredFieldValidator ID="rfvidioma" runat="server" ControlToValidate="ddlIdioma" ValidationGroup="vgrArquivos" Display="Dynamic" title="Campo obrigatório"></asp:RequiredFieldValidator>
        </label>
    </div>
    <div>
        <label>
            <span>
                <asp:Label ID="lblTipoArquivo" runat="server" AssociatedControlID="ddlTipoArquivo" title="Tipo de Arquivo"><%=Resources.Arquivos.TipoArquivo %>*</asp:Label>
            </span>
            <asp:DropDownList runat="server" ID="ddlTipoArquivo" title="Selecione o tipo de Arquivo" AutoPostBack="true" OnSelectedIndexChanged="ddlTipoArquivo_SelectedIndexChanged"></asp:DropDownList>
            <asp:RequiredFieldValidator ID="rfvTipoArquivo" runat="server" ControlToValidate="ddlTipoArquivo" ValidationGroup="vgrArquivos" Display="Dynamic" title="Campo obrigatório"></asp:RequiredFieldValidator>
        </label>
    </div>
    <asp:Panel ID="pnlCategoria" runat="server" Visible="false">
        <div>
            <label>
                <span>
                    <asp:Label ID="lblCategriaArquivo" runat="server" AssociatedControlID="chkCategoriaArquivo" title="Arquivo de"><%=Resources.Arquivos.ArquivoDe %>*</asp:Label>
                </span>
                <asp:CheckBoxList ID="chkCategoriaArquivo" runat="server">
                    <asp:ListItem Text="Capa" />
                    <asp:ListItem Text="Listagem" />
                    <asp:ListItem Text="Detalhe" />
                </asp:CheckBoxList>
            </label>
        </div>
    </asp:Panel>
    <div>
        <label>
            <span>
                <asp:Label ID="lblTituloArquivo" runat="server" AssociatedControlID="txtTituloArquivo" title="Resumo"><%=Resources.Arquivos.LabelTitulo %>*</asp:Label>
            </span>
            <asp:TextBox runat="server" TextMode="SingleLine" ID="txtTituloArquivo" CssClass="frmTxt" MaxLength="100" title="Informe o Titulo"></asp:TextBox>
            <asp:RequiredFieldValidator ID="rfvtitulo" runat="server" ControlToValidate="txtTituloArquivo" ValidationGroup="vgrArquivos" Display="Dynamic" title="Campo obrigatório"></asp:RequiredFieldValidator>
        </label>
    </div>
    <div>
        <label>
            <span>
                <asp:Label ID="lblDescricaoArquivo" runat="server" AssociatedControlID="txtDescricaoArquivo" title="Idioma"><%=Resources.Arquivos.LabelDescricao %>*</asp:Label>
            </span>
            <asp:TextBox runat="server" TextMode="SingleLine" ID="txtDescricaoArquivo" CssClass="frmTxt" MaxLength="400" title="Informe a descrição"></asp:TextBox>
            <asp:RequiredFieldValidator ID="rfvDescricao" runat="server" ControlToValidate="txtDescricaoArquivo" ValidationGroup="vgrArquivos" Display="Dynamic" title="Campo obrigatório"></asp:RequiredFieldValidator>
        </label>
    </div>
    <div>
        <label>
            <span>
                <asp:Label ID="lblDataArquivo" runat="server" AssociatedControlID="txtDescricaoArquivo" title="Idioma"><%=Resources.Arquivos.LabelDataArquivo %>*</asp:Label>
            </span>
            <asp:TextBox runat="server" TextMode="SingleLine" ID="txtDataArquivo" CssClass="frmTxt" MaxLength="400" title="Informe a data do arquivo"></asp:TextBox>
            <asp:RequiredFieldValidator ID="rfvDataArquivo" runat="server" ControlToValidate="txtDataArquivo" ValidationGroup="vgrArquivos" Display="Dynamic" title="Campo obrigatório"></asp:RequiredFieldValidator>
        </label>
    </div>
    <div>
        <label>
            <span>
                <asp:Label ID="lblStreamingArquivo" runat="server" AssociatedControlID="rdoStreaming" title="Streaming"><%=Resources.Arquivos.LabelTipoArquivoStreaming %>*</asp:Label>
                <asp:RadioButtonList ID="rdoStreaming" runat="server" RepeatDirection="Horizontal" AutoPostBack="true" OnSelectedIndexChanged="rdoStreaming_SelectedIndexChanged">
                    <asp:ListItem Text="Não" Value="0" Selected="True" />
                    <asp:ListItem Text="Sim" Value="1" />
                </asp:RadioButtonList>
                <asp:RequiredFieldValidator ID="rfvRdoStreaming" runat="server" ControlToValidate="rdoStreaming" ValidationGroup="vgrArquivos" Display="Dynamic" title="Campo obrigatório"></asp:RequiredFieldValidator>
            </span>
        </label>
    </div>
    <asp:Panel ID="pnlArquivoFisico" runat="server">
        <div>
            <label>
                <span>
                    <asp:Label ID="lblFile" runat="server" AssociatedControlID="fulArquivo" title="Arquivo"><%=Resources.Arquivos.LabelUploadArquivo %>*</asp:Label>
                </span>
                <asp:FileUpload ID="fulArquivo" runat="server" ToolTip="Selecione o arquivo" />
                <asp:RequiredFieldValidator ID="rfvArquivo" runat="server" ControlToValidate="fulArquivo" Display="Dynamic" title="Campo obrigatório"></asp:RequiredFieldValidator>
                <asp:Literal ID="litArquivoUploaded" runat="server"></asp:Literal>
            </label>
        </div>
    </asp:Panel>
    <asp:Panel ID="pnlStreaming" runat="server">
        <div>
            <label>
                <span>
                    <asp:Label ID="lblUrlStreaming" runat="server" AssociatedControlID="fulArquivo" title="Arquivo"><%=Resources.Arquivos.LabelUploadArquivo %>*</asp:Label>
                </span>
                <asp:TextBox ID="txtUrlStreaming" runat="server" ToolTip="URl da media"></asp:TextBox>
                <asp:RequiredFieldValidator ID="rfvUrlStreaming" runat="server" ControlToValidate="txtUrlStreaming" Display="Dynamic" title="Campo obrigatório"></asp:RequiredFieldValidator>
            </label>
        </div>
    </asp:Panel>
    <div class="btn-acoes2">
        <asp:PlaceHolder runat="server" ID="phOptions" Visible="true">
            <asp:Button runat="server" ID="btnCancelar" CssClass="cancelar" Text="Voltar" OnClick="btnCancelar_Click" CausesValidation="false" title="Cancelar" alt="Cancelar" />
            <asp:Button runat="server" ID="btnSalvar" CssClass="submit" Text="Salvar" ValidationGroup="vrgArquivos" OnClick="btnSalvar_Click" title="Salvar" alt="Salvar" />
        </asp:PlaceHolder>
    </div>
</asp:Content>

