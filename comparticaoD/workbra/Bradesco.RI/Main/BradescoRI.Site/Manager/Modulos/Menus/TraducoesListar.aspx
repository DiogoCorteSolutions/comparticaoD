<%@ Page Language="C#" AutoEventWireup="true" CodeFile="TraducoesListar.aspx.cs" Inherits="Modulos_Menus_TraducoesListar" MasterPageFile="~/Modulos/Modulos.master" %>
<%@ Register Namespace="WebFoundations.ServerControls" TagPrefix="wfc" %>

<asp:Content ID="Content1" ContentPlaceHolderID="contentInterna" runat="Server">
    <div id="title">
        <h1>
            <asp:Label ID="lblTitulo" runat="server"><%=Resources.Menu.TraducoesTitulo %></asp:Label>
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
                <span>
                    <asp:Label ID="lblIdioma" runat="server" AssociatedControlID="ddlIdioma" title="Idioma"><%=Resources.Menu.Cultura %></asp:Label>
                </span>
                <asp:DropDownList ID="ddlIdioma" runat="server" ToolTip="Informe o idioma"></asp:DropDownList>
            </label>
            <label>
                <span>
                    <asp:Label ID="lblChave" runat="server" AssociatedControlID="txtChave" title="Chave de Tradução"><%=Resources.Menu.ChaveTraducao %></asp:Label>
                </span>
                <asp:TextBox runat="server" ID="txtChave" CssClass="frmTxt" MaxLength="50" title="Informe a chave de tradução"></asp:TextBox>
            </label>
             <label>
                <span>
                    <asp:Label ID="lblTexto" runat="server" AssociatedControlID="txtTexto" title="Texto"><%=Resources.Menu.Texto %></asp:Label>
                </span>
                <asp:TextBox runat="server" ID="txtTexto" CssClass="frmTxt" MaxLength="100" title="Informe o texto"></asp:TextBox>
            </label>
        </div>
         <label class="btn-acoes-right">
            <asp:Button runat="server" ID="btnBuscar" OnClick="btnBuscar_Click" title="Buscar" alt="Buscar" />
        </label>
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

    <asp:DataGrid summary="Lista de Menus" ID="grdDados" runat="server" AutoGenerateColumns="false" CssClass="listagem-Grid" AlternatingRowStyle-CssClass="par" Width="100%" AllowPaging="True" PageSize="50" AllowSorting="True"
        OnSortCommand="grdDados_SortCommand">
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
            <asp:BoundColumn DataField="Id" HeaderText="ID" SortExpression="Id" HeaderStyle-CssClass="tabelaHeader" ItemStyle-Width="5%" ItemStyle-HorizontalAlign="Center" />
            <asp:BoundColumn DataField="IdiomaNome" SortExpression="IdiomaNome" HeaderText="Idioma" HeaderStyle-CssClass="tabelaHeader" ItemStyle-Width="20%" ItemStyle-HorizontalAlign="Center" />
            <asp:BoundColumn DataField="ChaveNome" SortExpression="ChaveNome" HeaderText="Chave para Tradução" HeaderStyle-CssClass="tabelaHeader asc" ItemStyle-Width="20%" ItemStyle-HorizontalAlign="Center" />
            <asp:BoundColumn DataField="Texto" SortExpression="Texto" HeaderText="Texto" HeaderStyle-CssClass="tabelaHeader" ItemStyle-Width="45%" ItemStyle-HorizontalAlign="Center" />
            <asp:TemplateColumn HeaderText="Editar" HeaderStyle-CssClass="tabelaHeader" ItemStyle-Width="5%" ItemStyle-HorizontalAlign="Center">
                <ItemTemplate>
                    <a id="A1" title="Editar" runat="server" href='<%# String.Format("TraducoesEditar.aspx?Id={0}", Eval("Id"))%>'>
                        <img src="/Manager/Imagens/edit-icon.png" style="width: 1.5em;" /></a>
                </ItemTemplate>
                <ItemStyle />
            </asp:TemplateColumn>
        </Columns>
        <HeaderStyle CssClass="topo-tb"></HeaderStyle>
        <ItemStyle CssClass="impar"></ItemStyle>
        <AlternatingItemStyle CssClass="par"></AlternatingItemStyle>
        <PagerStyle Visible="false" />
    </asp:DataGrid>
    <asp:Label ID="lblNoRecordsFound" Text="Nenhum resultado encontrado." runat="server" Visible="false"></asp:Label>
    <div class="footer-paginacao" title="Paginação">
        <wfc:Paginador ID="listPager" runat="server" RootElement="Ul" TextFirst="Primeira" TextLast="Última" TextNext="Próxima" TextPrevious="Anterior" OnPageChanged="listPager_PageChanged"
            CssClassPageLink="nav-footer" CssClassFirst="" CssClassLast="" CssClassNext="btn-navdir ir" CssClassPrevious="btn-navesq ir" PageNumbersSeparator="  " MaxVisiblePages="10" PageSize="1" />
    </div>

    <div class="btn-acoes2">
        <asp:PlaceHolder runat="server" ID="PlaceHolder1" Visible="true">
            <asp:Button runat="server" ID="btnCancelar" CssClass="cancelar" Text="Voltar"
                CausesValidation="false" OnClick="btnCancelar_Click" title="Cancelar" />
        </asp:PlaceHolder>
    </div>
</asp:Content>

