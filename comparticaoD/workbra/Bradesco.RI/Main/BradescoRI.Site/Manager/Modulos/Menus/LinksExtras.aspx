<%@ Page Language="C#" AutoEventWireup="true" CodeFile="LinksExtras.aspx.cs" Inherits="Modulos_Menus_LinksExtras" MasterPageFile="~/Modulos/Modulos.master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="contentInterna" runat="Server">
    <script src="<%=ResolveUrl("~/JS/jquery-1.10.2.min.js")%>"></script>
    <script src="<%=ResolveUrl("~/JS/jquery.mask.js")%>"></script>
    <script>

        $(document).ready(function () {

            $(".ddlPaginas").change(function () {
                if (this.value != "0") {
                    $(".txtUrl").val(this.value);
                } else {
                    $(".txtUrl").val('');
                }
            });
        });

    </script>

    <div id="title">
        <h1>
            <asp:Label ID="lblTitulo" runat="server"><%=Resources.Menu.LinksExtrasTitulo %></asp:Label>
        </h1>
    </div>
    <br />
    <div class="fields">
        <label>
            <span>
                <asp:Label ID="lblNome" runat="server" AssociatedControlID="ddlNome" title="Cultura"><%=Resources.Menu.Nome %>*</asp:Label>
            </span>
            <asp:DropDownList ID="ddlNome" runat="server" ToolTip="Informe o link" AutoPostBack="true" OnSelectedIndexChanged="ddlNome_SelectedIndexChanged"></asp:DropDownList>
        </label>

        <label>
            <span>
                <asp:Label ID="lblCultura" runat="server" AssociatedControlID="ddlIdioma" title="Cultura"><%=Resources.Menu.Cultura %>*</asp:Label>
            </span>
            <asp:DropDownList ID="ddlIdioma" runat="server" ToolTip="Informe o idioma" Enabled="false"></asp:DropDownList>
        </label>

        <label>
            <span>
                <asp:Label ID="lblChave" runat="server" AssociatedControlID="txtChave" title="Chave de Tradução do Menu"><%=Resources.Menu.ChaveTraducao %>*</asp:Label>
            </span>
            <asp:TextBox runat="server" ID="txtChave" CssClass="frmTxt" MaxLength="50" title="Informe a chave de tradução"></asp:TextBox>
        </label>

        <label>
            <span>
                <asp:Label ID="lblTexto" runat="server" AssociatedControlID="txtTexto" title="Texto"><%=Resources.Menu.Texto %>*</asp:Label>
            </span>
            <asp:TextBox runat="server" ID="txtTexto" CssClass="frmTxt" MaxLength="200" title="Informe o texto"></asp:TextBox>
            <asp:RequiredFieldValidator ID="rfvTexto" runat="server" ControlToValidate="txtTexto" Display="Dynamic" title="Campo obrigatório"></asp:RequiredFieldValidator>
        </label>

        <label>
            <span>
                <asp:Label ID="lblTarget" runat="server" AssociatedControlID="ddlTarget" title="Target"><%=Resources.Menu.Target %></asp:Label>
            </span>
            <asp:DropDownList runat="server" ID="ddlTarget" title="Selecione o target"></asp:DropDownList>
            <asp:RequiredFieldValidator ID="rfvTarget" runat="server" ControlToValidate="ddlTarget" InitialValue="0" Display="Dynamic" title="Campo obrigatório"></asp:RequiredFieldValidator>
        </label>

        <label>
            <span>
                <asp:Label ID="lblUrl" runat="server" AssociatedControlID="txtUrl" title="Url"><%=Resources.Menu.Url %></asp:Label>
            </span>
            <asp:TextBox ID="txtUrl" runat="server" class="txtUrl"></asp:TextBox>
            <asp:RequiredFieldValidator ID="rfvUrl" runat="server" ControlToValidate="txtUrl" Display="Dynamic" title="Campo obrigatório"></asp:RequiredFieldValidator>
        </label>
        <label>
            <asp:DropDownList ID="ddlPaginas" CssClass="frmDropdown ddlPaginas" runat="server"></asp:DropDownList>
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
