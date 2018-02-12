<%@ Page Language="C#" MasterPageFile="~/Modulos/Modulos.master" AutoEventWireup="true" CodeFile="Listar.aspx.cs" Inherits="Modulos_AlertaRI_Listar" %>

<%@ Register Namespace="WebFoundations.ServerControls" TagPrefix="wfc" %>
<asp:Content ID="Content1" ContentPlaceHolderID="contentInterna" runat="Server">
    <div id="title">
        <h1>
            <asp:Label ID="lblTitulo" runat="server"><%=Resources.AlertaRI.Titulo %></asp:Label>
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
                    <asp:Label ID="lblNome" runat="server" AssociatedControlID="txtNome" title="Nome"><%=Resources.AlertaRI.Nome %></asp:Label>
                </span>
                <asp:TextBox runat="server" ID="txtNome" CssClass="frmTxt" MaxLength="128" title="Informe o nome"></asp:TextBox>
            </label>
            <label>
                <span>
                    <asp:Label ID="lblEmail" runat="server" AssociatedControlID="txtEmail" title="Email"><%=Resources.AlertaRI.Email %></asp:Label>
                </span>
                <asp:TextBox runat="server" ID="txtEmail" CssClass="frmTxt" MaxLength="128" title="Informe o email"></asp:TextBox>
            </label>
            <label class="txtPeriodo">
                <span>
                    <asp:Label ID="lblDataInicio" runat="server" AssociatedControlID="txtDataInicio" title="Período de Cadastro"><%=Resources.AlertaRI.Periodo %></asp:Label>
                </span>
                <asp:TextBox runat="server" ID="txtDataInicio" CssClass="frmTxt txtDate" MaxLength="10" title="Informe Data de ìnicio" alt="Informe Data de ìnicio"></asp:TextBox>
                <span>Até</span>
                <asp:TextBox runat="server" ID="txtDataFim" CssClass="frmTxt txtDate" MaxLength="10" title="Informe Data de Fim" alt="Informe Data de Fim"></asp:TextBox>
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

    <div class="btn-acoes">
        <asp:Button ID="btnExportarXls" runat="server" OnClick="btnExportarXls_Click" title="Exportar Excel" />
    </div>

    <asp:Panel ID="pnlNenhumRegistro" Visible="false" runat="server">
        <div class="txt-n-reg" title="Nenhum registro encontrado"><%=Resources.Textos.Nenhum_Registro %></div>
    </asp:Panel>

    <asp:Panel ID="pnlRegistrosEncontrados" runat="server">
        <div class="txt-n-reg">
            <asp:Literal runat="server" ID="ltlRegistrosEncontrados"></asp:Literal><span><asp:Literal runat="server" ID="ltlQuantidadeRegistrosEncontrados"></asp:Literal></span>
        </div>
    </asp:Panel>

    <asp:DataGrid summary="Lista de Usuários" ID="grdDados" runat="server" AutoGenerateColumns="false" CssClass="listagem-Grid" AlternatingRowStyle-CssClass="par" Width="100%" AllowPaging="True"
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
            <asp:BoundColumn DataField="Id" HeaderText="ID" SortExpression="Id" HeaderStyle-CssClass="tabelaHeader desc" ItemStyle-Width="5%" />
            <asp:BoundColumn DataField="Nome" SortExpression="Nome" HeaderText="Nome" HeaderStyle-CssClass="tabelaHeader" ItemStyle-Width="15%" />
            <asp:BoundColumn DataField="Email" SortExpression="Email" HeaderText="E-mail" HeaderStyle-CssClass="tabelaHeader" ItemStyle-Width="15%" />
            <asp:TemplateColumn SortExpression="ProfissionalMercado" HeaderText="Profissional do Mercado" HeaderStyle-CssClass="tabelaHeader" ItemStyle-Width="10%" ItemStyle-HorizontalAlign="Center">
                <ItemTemplate>
                    <%# (Convert.ToBoolean(Eval("ProfissionalMercado"))? "Sim":"Não")%>
                </ItemTemplate>
                <ItemStyle />
            </asp:TemplateColumn>
            <asp:BoundColumn DataField="NomeSegmento" SortExpression="NomeSegmento" HeaderText="Segmento da Empresa" HeaderStyle-CssClass="tabelaHeader" ItemStyle-Width="15%" />
            <asp:BoundColumn DataField="Empresa" HeaderText="Empresa" SortExpression="Empresa" HeaderStyle-CssClass="tabelaHeader" ItemStyle-Width="10%" ItemStyle-HorizontalAlign="Center" />
            <asp:BoundColumn DataField="NomePais" SortExpression="NomePais" HeaderText="País" HeaderStyle-CssClass="tabelaHeader" ItemStyle-Width="10%" />
            <asp:BoundColumn DataField="Data" DataFormatString="{0:dd/MM/yyyy}" SortExpression="Data" HeaderText="Data Cadastro"
                HeaderStyle-CssClass="tabelaHeader" ItemStyle-Width="10%" ItemStyle-HorizontalAlign="Center" />
            <asp:TemplateColumn HeaderText="Editar" HeaderStyle-CssClass="tabelaHeader" ItemStyle-Width="5%" ItemStyle-HorizontalAlign="Center">
                <ItemTemplate>
                    <a id="A1" title="Editar" runat="server" href='<%# String.Format("Editar.aspx?Id={0}", Eval("Id"))%>'>
                        <img src="/manager/Imagens/edit-icon.png" style="width: 1.5em;" /></a>
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

