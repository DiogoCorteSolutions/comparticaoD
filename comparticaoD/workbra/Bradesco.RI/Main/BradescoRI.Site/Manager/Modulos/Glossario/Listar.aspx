<%@ Page Title="" Language="C#" MasterPageFile="~/Modulos/Modulos.master" AutoEventWireup="true" CodeFile="Listar.aspx.cs" Inherits="Modulos_Glossario_Listar" %>

<%@ Register Namespace="WebFoundations.ServerControls" TagPrefix="wfc" %>

<asp:Content ID="Content1" ContentPlaceHolderID="contentInterna" Runat="Server">
    <div id="title">
        <h1>
            <asp:Label ID="lblTitulo" runat="server"><%=Resources.Glossario.Titulo %></asp:Label>
            <asp:HiddenField ID="hdnGlossarioId" runat="server" />
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
        <div class="fields-edit">
            <label>
                Idioma
                <asp:DropDownList ID="ddlIdioma" runat="server" alt="Selecione o idioma" AutoPostBack="true" OnSelectedIndexChanged="ddlIdioma_SelectedIndexChanged" title="Selecione o idioma">
                    <asp:ListItem Text="Todos" Value="-1"></asp:ListItem>
                    <asp:ListItem Text="Portugues" Value="1"></asp:ListItem>
                    <asp:ListItem Text="Inglês" Value="2"></asp:ListItem>
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

    <asp:GridView summary="Lista de glossário" ID="grdDados" runat="server" AutoGenerateColumns="false" CssClass="listagem-Grid" AlternatingRowStyle-CssClass="par" Width="100%" AllowPaging="True"
        PageSize="50" AllowSorting="True" OnRowDataBound="grdDados_RowDataBound" OnSelectedIndexChanged="grdDados_SelectedIndexChanged" OnSorting="grdDados_Sorting">
        <Columns>
            <asp:TemplateField HeaderText="Seleção"  HeaderStyle-CssClass="tabelaHeader" ItemStyle-Width="5%" ItemStyle-HorizontalAlign="Center">
                <HeaderTemplate>
                    <asp:CheckBox ID="chkSelecionaTodos" CssClass="checkAll" runat="server" />
                </HeaderTemplate>
                <ItemTemplate>
                    <asp:CheckBox ID="chkSeleciona" CssClass="checkItem" runat="server" />
                </ItemTemplate>
                <ItemStyle Width="50" Height="50" />
            </asp:TemplateField>
            <asp:BoundField DataField="Id" SortExpression="Id" HeaderText="Id" ItemStyle-Width="5%" HeaderStyle-CssClass="tabelaHeader asc" />
            <asp:BoundField DataField="Titulo" SortExpression="Titulo" HeaderText="Título" ItemStyle-Width="85%" HeaderStyle-CssClass="tabelaHeader asc" />
            <asp:TemplateField HeaderText="Ações" HeaderStyle-CssClass="tabelaHeader" ItemStyle-Width="5%" ItemStyle-HorizontalAlign="Center">
                <ItemTemplate>
                    <a id="A1" title="Arquivo" runat="server" href='<%# String.Format("EditarGlossario.aspx?glossario={0}", Eval("Id"))%>'>
                        <img src="/Imagens/edit-icon.png" style="width: 1.5em;" /></a>
                </ItemTemplate>
                <ItemStyle />
            </asp:TemplateField>
        </Columns>
        <HeaderStyle CssClass="topo-tb"></HeaderStyle>
        <RowStyle CssClass="impar"></RowStyle>
        <AlternatingRowStyle CssClass="par"></AlternatingRowStyle>
        <PagerStyle Width="0px" />
    </asp:GridView>
    <asp:Label ID="lblNoRecordsFound" Text="Nenhum resultado encontrado." runat="server" Visible="false" title="Nenhum resultado encontrado"></asp:Label>
    <div class="footer-paginacao" title="Paginação">
        <wfc:Paginador ID="listPager" runat="server" RootElement="Ul" TextFirst="Primeira" TextLast="Última" TextNext="Próxima" TextPrevious="Anterior" OnPageChanged="listPager_PageChanged"
            CssClassPageLink="nav-footer" CssClassFirst="" CssClassLast="" CssClassNext="btn-navdir ir" CssClassPrevious="btn-navesq ir" PageNumbersSeparator="  " MaxVisiblePages="10" PageSize="1" />
    </div>
</asp:Content>

