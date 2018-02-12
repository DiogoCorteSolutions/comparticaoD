<%@ Page Language="C#" MasterPageFile="~/Modulos/Modulos.master" AutoEventWireup="true" CodeFile="Listar.aspx.cs" Inherits="Modulos_Enquete_Listar" %>

<%@ Register Namespace="WebFoundations.ServerControls" TagPrefix="wfc" %>
<asp:Content ID="Content1" ContentPlaceHolderID="contentInterna" runat="Server">

    <div id="title">
        <h1>
            <asp:Label ID="lblTitulo" runat="server"><%=Resources.Enquetes.Titulo %></asp:Label>
        </h1>
    </div>
    <br />
    <div class="filtros">
        <div class="fields-edit">
            <label>
                <span>
                    <asp:Label ID="lblRegistros" runat="server" title="Registros por página"><%=Resources.Textos.Registros_Pagina %></asp:Label>
                </span>
                <asp:DropDownList runat="server" ID="ddlRegistros" AutoPostBack="true" OnSelectedIndexChanged="ddlRegistros_SelectedIndexChanged" alt="Selecione a quantidade de registros por página" title="Selecione a quantidade de registros por página">
                    <asp:ListItem Text="5" Value="5"></asp:ListItem>
                    <asp:ListItem Text="10" Value="10"></asp:ListItem>
                    <asp:ListItem Text="15" Value="15"></asp:ListItem>
                    <asp:ListItem Text="20" Value="20"></asp:ListItem>
                    <asp:ListItem Text="50" Value="50"></asp:ListItem>
                </asp:DropDownList>
            </label>
        </div>
    </div>
    <div id="novo" class="btn-acoes">
        <asp:Button ID="btnNovo" CssClass="adicionar" runat="server" OnClick="btnNovo_Click" alt="Novo" title="Novo" />
    </div>

    <div id="excluir" class="btn-acoes">
        <asp:Button ID="btnExcluir" CssClass="btnExcluir" runat="server" OnClick="btnExcluir_Click" title="Excluir" />
    </div>

    <asp:Panel ID="pnlNenhumRegistro" Visible="false" runat="server">
        <div class="txt-n-reg" title="Nenhum registro encontrado"><%=Resources.Textos.Nenhum_Registro %></div>
    </asp:Panel>

    <asp:Panel ID="pnlRegistrosEncontrados" runat="server">
        <div class="txt-n-reg">
            <asp:Literal runat="server" ID="ltlRegistrosEncontrados"></asp:Literal><span><asp:Literal runat="server" ID="ltlQuantidadeRegistrosEncontrados"></asp:Literal></span>
        </div>
    </asp:Panel>

    <asp:DataGrid summary="Lista de Enquetes" ID="grdDados" runat="server" AutoGenerateColumns="false" CssClass="listagem-Grid" AlternatingRowStyle-CssClass="par" Width="100%" AllowPaging="True"
        PageSize="50" AllowSorting="True" OnSortCommand="grdDados_SortCommand">
        <Columns>
            <asp:TemplateColumn HeaderText="Seleção" HeaderStyle-CssClass="tabelaHeader" ItemStyle-Width="5%" ItemStyle-HorizontalAlign="Center">
                <HeaderTemplate>
                    <asp:CheckBox ID="chkSelecionaTodos" CssClass="checkAll" runat="server" />
                </HeaderTemplate>
                <ItemTemplate>
                    <asp:CheckBox ID="chkSeleciona" CssClass="checkItem" runat="server" />
                </ItemTemplate>
                <ItemStyle Width="50" Height="50" />
            </asp:TemplateColumn>
            <asp:BoundColumn Visible="false" DataField="IdEnquete" HeaderText="IdEnquete" SortExpression="IdEnquete" HeaderStyle-CssClass="tabelaHeader" />
            <asp:BoundColumn DataField="Descricao" SortExpression="Descricao" HeaderText="Grupo" HeaderStyle-CssClass="tabelaHeader asc" />
            <asp:TemplateColumn HeaderText="Adicionar Perguntas" HeaderStyle-CssClass="tabelaHeader" ItemStyle-Width="5%" ItemStyle-HorizontalAlign="Center">
                <ItemTemplate>
                    <a id="A1" title="Perguntas" runat="server" href='<%# String.Format("ListarPerguntas.aspx?EnqueteId={0}", Eval("IdEnquete"))%>'>
                        <img src="/manager/Imagens/input-txt.png" style="width: 1.5em;" /></a>
                    <a id="A2" title="Respostas" runat="server" href='<%# String.Format("ListarRespostas.aspx?EnqueteId={0}", Eval("IdEnquete"))%>'>
                        <img src="/manager/Imagens/input-txt.png" style="width: 1.5em;" /></a>
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
    <div class="footer-paginacao" title="Paginação">
        <wfc:Paginador ID="listPager" runat="server" RootElement="Ul" TextFirst="Primeira" TextLast="Última" TextNext="Próxima" TextPrevious="Anterior" OnPageChanged="listPager_PageChanged"
            CssClassPageLink="nav-footer" CssClassFirst="" CssClassLast="" CssClassNext="btn-navdir ir" CssClassPrevious="btn-navesq ir" PageNumbersSeparator="  " MaxVisiblePages="10" PageSize="1" />
    </div>
</asp:Content>
