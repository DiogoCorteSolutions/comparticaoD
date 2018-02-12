<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Editar.aspx.cs" Inherits="Modulos_Menus_Editar" MasterPageFile="~/Modulos/Modulos.master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="contentInterna" runat="Server">
    <style>
        .btnDeleteMenu, .btnAddMenu {
            min-width: 0px !important;
            width: 20px !important;
            cursor: pointer;
        }

        .btnSelectMenu {
            cursor: pointer;
        }
    </style>
    <script src="<%=ResolveUrl("~/JS/Menu.js")%>"></script>
    <div id="title">
        <h1>
            <span>
                <%=Resources.Menu.Titulo %>
            </span>
        </h1>
    </div>
    <br />

    <div class="fields">
        <label class="treeViewMenus">
            <asp:TreeView ID="trvMenus" runat="server" Font-Names="Arial"
                ForeColor="Black"
                SelectedNodeStyle-ForeColor="Red"
                SelectedNodeStyle-VerticalPadding="0"
                child="0">
            </asp:TreeView>
        </label>
        <br />
        <div id="detalhesMenu" style="display: none;" runat="server">
            <label>
                <asp:HiddenField ID="hdnAcao" runat="server" />
                <asp:HiddenField ID="hdnHierarquia" runat="server" />
            </label>
            <label>
                <h4>
                    <asp:Label ID="lblDescricao" runat="server">Novo Item de menu</asp:Label></h4>
            </label>
            <label>
                <span>
                    <asp:Label ID="lblNome" runat="server" AssociatedControlID="txtNome" title="Nome do Menu"><%=Resources.Menu.Nome %>*</asp:Label>
                </span>
                <asp:TextBox runat="server" ID="txtNome" CssClass="frmTxt" MaxLength="100" title="Informe o nome do Menu"></asp:TextBox>
                <asp:RequiredFieldValidator ID="rfvNome" runat="server" ControlToValidate="txtNome" Display="Dynamic" title="Campo obrigatório"></asp:RequiredFieldValidator>
            </label>
            <label>
                <span>
                    <asp:Label ID="lblUrl" runat="server" AssociatedControlID="txtUrl" title="URL do Menu"><%=Resources.Menu.Url %>*</asp:Label>
                </span>
                <asp:TextBox runat="server" ID="txtUrl" CssClass="frmTxt" MaxLength="200" title="Informe a url do Menu"></asp:TextBox>
                <asp:DropDownList ID="ddlPaginas" CssClass="frmDropdown ddlPaginas" runat="server"></asp:DropDownList>
                <asp:RequiredFieldValidator ID="rfvUrl" runat="server" ControlToValidate="txtUrl" Display="Dynamic" title="Campo obrigatório"></asp:RequiredFieldValidator>
            </label>
            <label>
                <span>
                    <asp:Label ID="lblTarget" runat="server" AssociatedControlID="txtUrl" title="Forma de Abertura do Menu"><%=Resources.Menu.Target %></asp:Label>
                </span>
                <asp:DropDownList ID="ddlTarget" CssClass="frmDropdown" runat="server" ToolTip="Informe a forma de abertura do menu"></asp:DropDownList>
            </label>
            <label>
                <span>
                    <asp:Label ID="lblCultura" runat="server" AssociatedControlID="ddlIdioma" title="URL do Menu"><%=Resources.Menu.Cultura %></asp:Label>
                </span>
                <asp:DropDownList ID="ddlIdioma" runat="server">
                </asp:DropDownList>
            </label>
            <label>
                <span>
                    <asp:Label ID="lblChave" runat="server" AssociatedControlID="txtChave" title="Chave de Tradução do Menu"><%=Resources.Menu.ChaveTraducao %></asp:Label>
                </span>
                <asp:TextBox runat="server" ID="txtChave" CssClass="frmTxt" MaxLength="50" title="Informe a chave de tradução"></asp:TextBox>
            </label>
            <label>
                <span>
                    <asp:Label ID="lblEstilo" runat="server" AssociatedControlID="txtEstilo" title="Estilo do item de Menu"><%=Resources.Menu.Estilo %></asp:Label>
                </span>
                <asp:TextBox runat="server" ID="txtEstilo" CssClass="frmTxt" MaxLength="30" title="Informe o Estilo do item"></asp:TextBox>
            </label>
            <div class="btn-acoes2">
                <asp:PlaceHolder runat="server" ID="phOptions" Visible="true">
                    <asp:Button runat="server" ID="btnOK" CssClass="submit" Text="Salvar" OnClick="btnOK_Click" title="Salvar" />
                    <asp:Button runat="server" ID="btnFechar" CssClass="cancelar" Text="Fechar" title="Fechar" OnClientClick="fecharDetalhes(); return false;" />
                </asp:PlaceHolder>
            </div>
        </div>
    </div>



    <div class="btn-acoes2">
        <asp:PlaceHolder runat="server" ID="PlaceHolder1" Visible="true">
            <asp:Button runat="server" ID="btnCancelar" CssClass="cancelar" Text="Voltar"
                CausesValidation="false" OnClick="btnCancelar_Click" title="Cancelar" />
        </asp:PlaceHolder>
    </div>
</asp:Content>

