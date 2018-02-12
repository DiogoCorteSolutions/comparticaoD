<%@ Page Language="C#" AutoEventWireup="true" CodeFile="MenuCircularHome.aspx.cs" Inherits="Modulos_CMS_Modulos_ModMenuCircularHome_MenuCircularHome" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <script src="<%=ResolveUrl("~/JS/jquery-1.10.2.min.js")%>"></script>
    <script src="<%=ResolveUrl("~/JS/jquery.mask.js")%>"></script>
    <script>
        function refreshParent() {
            window.opener.location.href = window.opener.location.href;
            window.close();
        }

        $(document).ready(function () {

            $(".ddlPaginas").change(function () {
                if (this.value != "0") {
                    $("#txtUrl").val(this.value);
                } else {
                    $("#txtUrl").val('');
                }
            });
        });

    </script>
</head>
<body>
    <form id="form1" runat="server">
        <label>
            Idioma:
            <asp:DropDownList ID="ddlIdioma" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlIdioma_SelectedIndexChanged"></asp:DropDownList>
        </label>
        <div id="divUpload" runat="server">
            <label>Arquivo:</label>
            <asp:FileUpload ID="fupArquivo" runat="server" />
            <asp:RequiredFieldValidator ID="rfvArquivo" runat="server" ControlToValidate="fupArquivo" Text="*" ValidationGroup="salvar"></asp:RequiredFieldValidator>
            <asp:RegularExpressionValidator ID="rev" runat="server" ValidationExpression="^.*\.(jpg|JPG|jpeg|JPEG|png|PNG)$" ControlToValidate="fupArquivo" ErrorMessage="*"></asp:RegularExpressionValidator>
        </div>
        <div id="divImagem" runat="server">
            <label>Arquivo:</label>
            <asp:Image ID="imgImagem" runat="server" Width="200px" />
            <asp:ImageButton ID="btnExcluir" runat="server"
                ImageUrl="~/Imagens/remove-icon.png" OnClick="btnExcluir_Click" Width="20px" Height="20px"
                ToolTip="Excluir Imagem" alt="Excluir imagem" title="Excluir imagem" />
        </div>
        <div>
            <label>Título:</label>
            <asp:TextBox ID="txtTitulo" runat="server" MaxLength="100"></asp:TextBox><asp:RequiredFieldValidator ID="rfvTitulo" runat="server" ControlToValidate="txtTitulo" ValidationGroup="salvar" Text="*"></asp:RequiredFieldValidator>
        </div>
        <div>
            <label>Tooltip:</label>
            <asp:TextBox ID="txtTooltip" runat="server" MaxLength="400"></asp:TextBox>
        </div>
        <div>
            <label>URL:</label>
            <asp:TextBox ID="txtUrl" runat="server" MaxLength="1000"></asp:TextBox><asp:RequiredFieldValidator ID="rfvUrl" runat="server" ControlToValidate="txtUrl" ValidationGroup="salvar" Text="*"></asp:RequiredFieldValidator>
        </div>
        <label>
            <asp:DropDownList ID="ddlPaginas" CssClass="frmDropdown ddlPaginas" runat="server"></asp:DropDownList>
        </label>
        <div>
            <label>Target:</label>
            <asp:DropDownList ID="ddlTarget" CssClass="frmDropdown" runat="server" ToolTip="Informe a forma de abertura da abertura"></asp:DropDownList>
        </div>

        <div>
            <asp:Button ID="btnSalvar" runat="server" Text="Adicionar" OnClick="btnSalvar_Click" ValidationGroup="salvar" />
        </div>

        <div>
            <asp:Label ID="lblMensagem" runat="server"></asp:Label>
        </div>

        <asp:DataGrid summary="Lista de Caixas" ID="grdDados" runat="server" AutoGenerateColumns="false" CssClass="listagem-Grid"
            AlternatingRowStyle-CssClass="par" Width="100%" OnItemCommand="grdDados_ItemCommand">
            <Columns>
                <asp:TemplateColumn HeaderText="Arquivo" HeaderStyle-CssClass="tabelaHeader" ItemStyle-HorizontalAlign="Center">
                    <ItemTemplate>
                        <img src="<%# String.Format("{0}/{1}/{2}", ConfigurationManager.AppSettings["BradescoRI.Path.Imagens.ModMenuCircularHome"], Request.QueryString["conteudoId"] ,Eval("Arquivo"))%>" style="width: 35px; height: 35px" /></a>
                    </ItemTemplate>
                    <ItemStyle />
                </asp:TemplateColumn>
                <asp:BoundColumn Visible="false" DataField="IdMenuCircularHome" HeaderText="IdMenuCircularHome" HeaderStyle-CssClass="tabelaHeader" />
                <asp:BoundColumn DataField="Arquivo" Visible="false" />
                <asp:BoundColumn DataField="Titulo" HeaderText="Titulo" HeaderStyle-CssClass="tabelaHeader" />
                <asp:BoundColumn DataField="Tooltip" HeaderText="Tooltip" HeaderStyle-CssClass="tabelaHeader" />
                <asp:BoundColumn DataField="Target" HeaderText="Target" HeaderStyle-CssClass="tabelaHeader" />
                <asp:BoundColumn DataField="Url" HeaderText="Url" HeaderStyle-CssClass="tabelaHeader" />
                <asp:TemplateColumn HeaderText="" HeaderStyle-CssClass="tabelaHeader" ItemStyle-Width="5%" ItemStyle-HorizontalAlign="Center">
                    <ItemTemplate>
                        <asp:ImageButton ID="imgExcluir" runat="server" AlternateText="Excluir" CommandName="excluir" CommandArgument='<%#Eval("IdMenuCircularHome")%>'
                            ImageUrl="/Manager/Imagens/remove-icon.png" Style="width: 1.5em;" />
                        <asp:ImageButton ID="imgEditar" runat="server" AlternateText="Editar" CommandName="editar" CommandArgument='<%#Eval("IdMenuCircularHome")%>'
                            ImageUrl="/Manager/Imagens/edit-icon.png" Style="width: 1.5em;" />
                    </ItemTemplate>
                    <ItemStyle />
                </asp:TemplateColumn>

            </Columns>
            <HeaderStyle CssClass="topo-tb"></HeaderStyle>
            <ItemStyle CssClass="impar"></ItemStyle>
            <AlternatingItemStyle CssClass="par"></AlternatingItemStyle>
            <PagerStyle Visible="false" />
        </asp:DataGrid>
        <asp:Label ID="lblNoRecordsFound" Text="Nenhum resultado encontrado." runat="server" Visible="false" title="Nenhum resultado encontrado"></asp:Label>
        <div>
            <asp:Button ID="btnFechar" runat="server" Text="Salvar" OnClick="btnFechar_Click" />
        </div>
    </form>
</body>
</html>

