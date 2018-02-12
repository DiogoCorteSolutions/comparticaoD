<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Caixa.aspx.cs" Inherits="Modulos_CMS_Modulos_ModCaixa_Caixa" %>

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
            <asp:RequiredFieldValidator ID="rfvArquivo" runat="server" ControlToValidate="fupArquivo"  ValidationGroup="salvar"  Text="*"></asp:RequiredFieldValidator>
            <asp:RegularExpressionValidator ID="rev" runat="server" ValidationExpression="^.*\.(jpg|JPG|jpeg|JPEG|png|PNG)$" ControlToValidate="fupArquivo" ErrorMessage="*"></asp:RegularExpressionValidator>

        </div>
        <div>
            <label>Título:</label>
            <asp:TextBox ID="txtTitulo" runat="server" MaxLength="100"></asp:TextBox><asp:RequiredFieldValidator ID="rfvTitulo" runat="server" ControlToValidate="txtTitulo"  ValidationGroup="salvar"  Text="*"></asp:RequiredFieldValidator>
        </div>
        <div>
            <label>Descrição:</label>
            <asp:TextBox ID="txtDescricao" runat="server" MaxLength="400"></asp:TextBox><asp:RequiredFieldValidator ID="rfvDescricao" runat="server" ControlToValidate="txtDescricao"  ValidationGroup="salvar"  Text="*"></asp:RequiredFieldValidator>
        </div>

        <div>
            <asp:Button ID="btnSalvar" runat="server" Text="Adicionar" OnClick="btnSalvar_Click" ValidationGroup="salvar"/>
        </div>

        <div>
            <asp:Label ID="lblMensagem" runat="server"></asp:Label>
        </div>

        <asp:DataGrid summary="Lista de Caixas" ID="grdDados" runat="server" AutoGenerateColumns="false" CssClass="listagem-Grid" 
            AlternatingRowStyle-CssClass="par" Width="100%" OnItemCommand="grdDados_ItemCommand" >
            <Columns>
                <asp:TemplateColumn HeaderText="Arquivo" HeaderStyle-CssClass="tabelaHeader" ItemStyle-HorizontalAlign="Center">
                    <ItemTemplate>
                        <img src="<%# String.Format("{0}/{1}/{2}",ConfigurationManager.AppSettings["BradescoRI.Path.Imagens.ModCaixas"], Request.QueryString["conteudoId"] ,Eval("Arquivo"))%>" style="width: 35px; height: 35px" /></a>
                    </ItemTemplate>
                    <ItemStyle />
                </asp:TemplateColumn>
                <asp:BoundColumn Visible="false" DataField="IdCaixa" HeaderText="IdCaixa" HeaderStyle-CssClass="tabelaHeader" />
                <asp:BoundColumn DataField="Arquivo" Visible="false" />
                <asp:BoundColumn DataField="Titulo" HeaderText="Titulo" HeaderStyle-CssClass="tabelaHeader" />
                <asp:BoundColumn DataField="Descricao" HeaderText="Descricao" HeaderStyle-CssClass="tabelaHeader" />
                <asp:TemplateColumn HeaderText="Excluir" HeaderStyle-CssClass="tabelaHeader" ItemStyle-Width="5%" ItemStyle-HorizontalAlign="Center">
                    <ItemTemplate> 
                        <asp:ImageButton ID="imgExcluir" runat="server" AlternateText="Excluir" CommandName="excluir" CommandArgument='<%#Eval("IdCaixa")%>'
                             ImageUrl="/Manager/Imagens/remove-icon.png" style="width: 1.5em;" />
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
            <asp:Button ID="btnFechar" runat="server" Text="Salvar" OnClick="btnFechar_Click"/>
        </div>
    </form>
</body>
</html>

