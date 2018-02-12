<%@ Page Language="C#" MasterPageFile="~/Modulos/Modulos.master" AutoEventWireup="true" CodeFile="EditarBanner.aspx.cs" Inherits="Modulos_Banner_EditarBanner" %>

<asp:Content ID="Content1" ContentPlaceHolderID="contentInterna" runat="Server">
    <script src="<%=ResolveUrl("~/JS/jquery-1.10.2.min.js")%>"></script>
    <script src="<%=ResolveUrl("~/JS/jquery.mask.js")%>"></script>
    <script>
        function refreshParent() {
            window.opener.location.href = window.opener.location.href;
        }

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
            <asp:Label ID="lblTituloPag" runat="server"><%=Resources.Banners.Titulo %></asp:Label>
        </h1>
    </div>
    <br />
    <div class="fields">
        <div id="divImagem" runat="server">

            <asp:Image ID="imgImagem" runat="server" Width="200px" />
            <asp:ImageButton ID="btnExcluir" runat="server"
                ImageUrl="~/Imagens/remove-icon.png" OnClick="btnExcluir_Click" Width="20px" Height="20px"
                ToolTip="Excluir Imagem" alt="Excluir imagem" title="Excluir imagem" />

        </div>
        <div id="divUpload" runat="server">

            <span>
                <asp:Label ID="lblArquivo" runat="server" title="Arquivo"><%=Resources.Banners.Arquivo %>*</asp:Label>
            </span>
            <asp:FileUpload ID="fupArquivo" runat="server" />
            <asp:RequiredFieldValidator ID="rfvArquivo" runat="server" ControlToValidate="fupArquivo" Text="*"></asp:RequiredFieldValidator>
            <asp:RegularExpressionValidator ID="rev" runat="server" ValidationExpression="^.*\.(jpg|JPG|jpeg|JPEG|png|PNG)$" ControlToValidate="fupArquivo" ErrorMessage="*"></asp:RegularExpressionValidator>


        </div>
        <label>
            <span>
                <asp:Label ID="lblGrupo" runat="server" AssociatedControlID="txtGrupo" title="Grupo"><%=Resources.Banners.Grupo %></asp:Label>
            </span>
            <asp:TextBox runat="server" ID="txtGrupo" CssClass="frmTxt" MaxLength="200" Enabled="false"></asp:TextBox>
        </label>
        <label>
            <span>
                <asp:Label ID="lblIdioma" runat="server" AssociatedControlID="ddlIdioma" title="Idioma"><%=Resources.Banners.Idioma %></asp:Label>
            </span>
            <asp:DropDownList runat="server" ID="ddlIdioma" title="Selecione o Idioma"></asp:DropDownList>
            <asp:RequiredFieldValidator ID="rfvidioma" runat="server" ControlToValidate="ddlIdioma" Display="Dynamic" title="Campo obrigatório"></asp:RequiredFieldValidator>
        </label>
        <label>
            <span>
                <asp:Label ID="lblTexto1" runat="server" AssociatedControlID="txtTitulo" title="Texto 1"><%=Resources.Banners.Texto1 %></asp:Label>
            </span>
            <asp:TextBox runat="server" ID="txtTitulo" CssClass="frmTxt" MaxLength="500" title="Informe o título"></asp:TextBox>
        </label>
        <label>
            <span>
                <asp:Label ID="lblTexto2" runat="server" AssociatedControlID="txtSubtitulo" title="Texto 2"><%=Resources.Banners.Texto2 %></asp:Label>
            </span>
            <asp:TextBox runat="server" ID="txtSubtitulo" CssClass="frmTxt" MaxLength="500" title="Informe o subtítulo"></asp:TextBox>
        </label>
        <label>
            <span>
                <asp:Label ID="lblTextoUrl" runat="server" AssociatedControlID="txtTextoUrl" title="Texto URL"><%=Resources.Banners.TextoUrl %></asp:Label>
            </span>
            <asp:TextBox runat="server" ID="txtTextoUrl" CssClass="frmTxt" MaxLength="200" title="Informe o texto da url"></asp:TextBox>
        </label>
        <label>
            <span>
                <asp:Label ID="lblUrl" runat="server" AssociatedControlID="txtUrl" title="Url"><%=Resources.Banners.Url %></asp:Label>
            </span>
            <asp:TextBox ID="txtUrl" runat="server" class="txtUrl"></asp:TextBox>
        </label>
        <label>
            <asp:DropDownList ID="ddlPaginas" CssClass="frmDropdown ddlPaginas" runat="server"></asp:DropDownList>
        </label>
        <label>
            <span>
                <asp:Label ID="lblTarget" runat="server" AssociatedControlID="ddlTarget" title="Target"><%=Resources.MenuCircular.Target %></asp:Label>
            </span>
            <asp:DropDownList runat="server" ID="ddlTarget" title="Selecione o target"></asp:DropDownList>
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
