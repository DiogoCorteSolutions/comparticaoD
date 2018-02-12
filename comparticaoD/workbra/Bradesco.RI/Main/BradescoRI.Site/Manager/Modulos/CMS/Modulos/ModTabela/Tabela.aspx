<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Tabela.aspx.cs" Inherits="Modulos_CMS_Modulos_ModTabela_Tabela" %>

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
        <div class="coluna1">
            <label>COLUNA:</label>
            <asp:TextBox ID="txtColuna" runat="server" MaxLength="100"></asp:TextBox><asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtColuna" ValidationGroup="salvar" Text="*"></asp:RequiredFieldValidator>
        </div>
        <div>
            <label>IDTABELA:</label>
            <asp:TextBox ID="txtIdModTabela" runat="server" MaxLength="100"></asp:TextBox><asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtIdModTabela"></asp:RequiredFieldValidator>
        </div>
        <div>
            <label>DADOCOLUNA:</label>
            <asp:TextBox ID="txtValorColuna" runat="server" MaxLength="100"></asp:TextBox><asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtValorColuna"></asp:RequiredFieldValidator>
        </div>
        <div class="buttonCol1">
            <asp:Button ID="btnSalvarColuna" runat="server" Text="Adicionar" OnClick="btnSalvarColuna_Click" ValidationGroup="salvar" />
        </div>
        <label>
            COLUNA:
            <asp:DropDownList ID="ddlColuna" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlColuna_SelectedIndexChanged"></asp:DropDownList>
        </label>

        <div>
            <asp:Label ID="lblMensagem" runat="server"></asp:Label>
        </div>

        <asp:DataGrid summary="Lista de Colunas" ID="grdDados" runat="server" AutoGenerateColumns="false" CssClass="listagem-Grid"
            AlternatingRowStyle-CssClass="par" Width="100%" OnItemCommand="grdDados_ItemCommand">
            <Columns>
                <asp:BoundColumn Visible="false" DataField="IdModTabela" HeaderText="IdModTabela" HeaderStyle-CssClass="tabelaHeader" />
                <asp:BoundColumn DataField="IdModTabela" HeaderText="IdModTabela" HeaderStyle-CssClass="tabelaHeader" />
                <asp:BoundColumn DataField="NomeAcionario" HeaderText="NomeTabela" HeaderStyle-CssClass="tabelaHeader" />
                <asp:BoundColumn DataField="NomeColuna" HeaderText="NomeColuna" HeaderStyle-CssClass="tabelaHeader" />
                <asp:BoundColumn DataField="ValorColuna" HeaderText="ValorColuna" HeaderStyle-CssClass="tabelaHeader" />
                <asp:TemplateColumn HeaderText="Excluir" HeaderStyle-CssClass="tabelaHeader" ItemStyle-Width="5%" ItemStyle-HorizontalAlign="Center">
                    <ItemTemplate>
                        <asp:ImageButton ID="imgExcluir" runat="server" AlternateText="Excluir" CommandName="excluir" CommandArgument='<%#Eval("IdModTabela")%>'
                            ImageUrl="/Manager/Imagens/remove-icon.png" Style="width: 1.5em;" />
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
       <%-- <div>
            <asp:Button ID="btnFechar" runat="server" Text="Salvar" OnClick="btnFechar_Click" />
        </div>--%>
        <div>
            <asp:Button ID="btnSalvarNovaColuna" runat="server" Text="AdicionarColuna" OnClick="btnSalvarNovaColuna_Click" ValidationGroup="salvar" CommandArgument='<%#Eval("IdModTabela")%>' />
        </div>
          <div>
            <asp:Button ID="btnSalvarValorColuna" runat="server" Text="AdicionarValorColuna" OnClick="btnSalvarValorColuna_Click" ValidationGroup="salvar" CommandArgument='<%#Eval("IdModTabela")%>' />
        </div>

        

        <%--  <div>
            <asp:Button ID="Button3" runat="server" Text="AdicionarValorColunas" OnClick="btnSalvarValorColuna_Click" ValidationGroup="salvar" CommandArgument='<%#Eval("IdModTabela")%>' />
        </div>--%>
    </form>
</body>
</html>

