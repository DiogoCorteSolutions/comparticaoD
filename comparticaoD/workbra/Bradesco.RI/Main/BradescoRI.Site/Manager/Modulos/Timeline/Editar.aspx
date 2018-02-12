<%--<%@ Page Title="" Language="C#" MasterPageFile="~/Modulos/Modulos.master" AutoEventWireup="true" CodeFile="Editar.aspx.cs" Inherits="Modulos_Timeline_Editar" %>

<%@ Register Namespace="Controles.FCKeditor" TagPrefix="FCKEditor" %>
<asp:Content ID="Content1" ContentPlaceHolderID="contentInterna" runat="Server">

    <div id="title">
        <h1>
            <asp:Label ID="lblTitulo" runat="server"><%=Resources.Noticias.Titulo %></asp:Label>
            <asp:HiddenField ID="hdnNoticiaId" runat="server" />
        </h1>
    </div>
    <br />
    <div>
        <label>
            <span>
                <asp:Label ID="lblIdioma" runat="server" AssociatedControlID="ddlIdioma" title="Idioma"><%=Resources.Noticias.Idioma %>*</asp:Label>
            </span>
            <asp:DropDownList runat="server" ID="ddlIdioma" title="Selecione o Idioma"></asp:DropDownList>
            <asp:RequiredFieldValidator ID="rfvidioma" runat="server" ControlToValidate="ddlIdioma" ValidationGroup="vgrNoticia" Display="Dynamic" title="Campo obrigatório"></asp:RequiredFieldValidator>
        </label>
    </div>
    <div>
        <label>
            <span>
                <asp:Label ID="lblTipoNoticia" runat="server" AssociatedControlID="ddlTipoNoticia" title="Idioma"><%=Resources.Noticias.LabelTipoNoticia %>*</asp:Label>
            </span>
            <asp:DropDownList runat="server" ID="ddlTipoNoticia" title="Selecione o Idioma"></asp:DropDownList>
            <asp:RequiredFieldValidator ID="rfvTipoNoticia" runat="server" ControlToValidate="ddlTipoNoticia" ValidationGroup="vgrNoticia" Display="Dynamic" title="Campo obrigatório"></asp:RequiredFieldValidator>
        </label>
    </div>
    <div>
        <label>
            <span>
                <asp:Label ID="lblTituloNoticia" runat="server" AssociatedControlID="txtTituloNoticia" title="Resumo"><%=Resources.Noticias.LabelTitulo %>*</asp:Label>
            </span>
            <asp:TextBox runat="server" TextMode="SingleLine" ID="txtTituloNoticia" CssClass="frmTxt" MaxLength="4096" title="Informe o resumo"></asp:TextBox>
            <asp:RequiredFieldValidator ID="rfvtitulo" runat="server" ControlToValidate="txtTituloNoticia" ValidationGroup="vgrNoticia" Display="Dynamic" title="Campo obrigatório"></asp:RequiredFieldValidator>
        </label>
    </div>
    <div>
        <label>
            <span>
                <asp:Label ID="lblDataNoticia" runat="server" AssociatedControlID="txtDataNoticia" title="Notícia"><%=Resources.Noticias.LabelDataNoticia %>*</asp:Label>
            </span>
            <asp:Calendar ID="caledarDataNoticia" runat="server" Visible="false" Width="100px" Height="100px" OnSelectionChanged="caledarDataNoticia_SelectionChanged"></asp:Calendar>
            <asp:TextBox ID="txtDataNoticia" runat="server" CssClass="frmTxt" MaxLength="10" Width="120px"></asp:TextBox>
            <asp:ImageButton ID="btnShowCalendario" runat="server" CausesValidation="false" ImageUrl="~/Imagens/calendar.png" OnClick="btnDataNoticia_Click" />
            <asp:RequiredFieldValidator ID="rfvDataNoticia" runat="server" ControlToValidate="txtDataNoticia" ValidationGroup="vgrNoticia" Display="Dynamic" title="Campo obrigatório"></asp:RequiredFieldValidator>
        </label>
    </div>
    <div style="height: 400px">
        <label>
            <span>
                <asp:Label ID="lblResumo" runat="server" AssociatedControlID="txtResumo" title="Resumo"><%=Resources.Noticias.LabelResumo %>*</asp:Label>
            </span>
            <FCKEditor:FCKeditor ID="txtResumo" runat="server" Height="350px" BasePath="~/fckeditor/" DefaultLanguage="pt-BR" EnableSourceXHTML="false"
                EnableXHTML="false" Debug="false">
            </FCKEditor:FCKeditor>

        </label>
    </div>
    <div style="height: 400px">
        <label>
            <span>
                <asp:Label ID="lblIntegra" runat="server" AssociatedControlID="txtIntegra" title="Resumo"><%=Resources.Noticias.LabelIntegra %>*</asp:Label>
            </span>
            <FCKEditor:FCKeditor ID="txtIntegra" runat="server" Height="350px" BasePath="~/fckeditor/" DefaultLanguage="pt-BR" EnableSourceXHTML="false"
                EnableXHTML="false" Debug="false">
            </FCKEditor:FCKeditor>
        </label>
    </div>
    <div>
        <label>
            <asp:CheckBox ID="chkDestaque" runat="server" ToolTip="Destaque" />
        </label>
    </div>
    <br />
    <div>
        <label>
            <span>
                <asp:Label ID="lblFonte" runat="server" AssociatedControlID="txtFonte" title="Resumo"><%=Resources.Noticias.LabelFonte %>*</asp:Label>
            </span>
            <asp:TextBox runat="server" TextMode="SingleLine" ID="txtFonte" CssClass="frmTxt" MaxLength="2048" title="Informe a fonte"></asp:TextBox>
            <asp:RequiredFieldValidator ID="rfvFonte" runat="server" ControlToValidate="txtFonte" ValidationGroup="vgrNoticia" Display="Dynamic" title="Campo obrigatório"></asp:RequiredFieldValidator>
        </label>
    </div>

    <div>
        <label>
            <span>
                <asp:Label ID="lblCamposObrigatorios" runat="server" title="Campos obrigatórios"><%=Resources.Textos.Campos_Obrigatorios %></asp:Label>
            </span>
        </label>
    </div>
    <br />
    <div class="btn-acoes2">
        <asp:PlaceHolder runat="server" ID="phOptions" Visible="true">
            <asp:Button runat="server" ID="btnCancelar" CssClass="cancelar" Text="Voltar" OnClick="btnCancelar_Click" CausesValidation="false" title="Cancelar" alt="Cancelar" />
            <asp:Button runat="server" ID="btnSalvar" CssClass="submit" Text="Salvar" ValidationGroup="vgrNoticia" OnClick="btnSalvar_Click" title="Salvar" alt="Salvar" />
        </asp:PlaceHolder>
    </div>

</asp:Content>--%>
