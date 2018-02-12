<%@ Page Language="C#" MasterPageFile="~/Modulos/Modulos.master" AutoEventWireup="true" CodeFile="Listar.aspx.cs" Inherits="Modulos_Eventos_Listar" %>

<%@ Register Namespace="WebFoundations.ServerControls" TagPrefix="wfc" %>
<asp:Content ID="Content1" ContentPlaceHolderID="contentInterna" runat="Server">
    <div id="title">
        <h1>
            <asp:Label ID="lblTitulo" runat="server"><%=Resources.Eventos.Titulo %></asp:Label>
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
                    <asp:Label ID="lblIdioma" runat="server" AssociatedControlID="ddlIdioma" title="Idioma"><%=Resources.Eventos.Idioma %></asp:Label>
                </span>
                <asp:DropDownList runat="server" ID="ddlIdioma" ></asp:DropDownList>
            </label>
            <label>
                <span>
                    <asp:Label ID="lblTipoEvento" runat="server" AssociatedControlID="ddlTipoEvento" title="Tipo Evento"><%=Resources.Eventos.TipoEvento %></asp:Label>
                </span>
                <asp:DropDownList runat="server" ID="ddlTipoEvento"></asp:DropDownList>
            </label>
            <label>
                <span>
                    <asp:Label ID="lblDataInicio" runat="server" AssociatedControlID="txtDataInicio" title="Data"><%=Resources.Eventos.DataInicio %></asp:Label>
                </span>
                <asp:TextBox runat="server" ID="txtDataInicio" CssClass="frmTxt txtDate" MaxLength="10" title="Informe a data de início"></asp:TextBox>
            </label>
            <label>
                <span>
                    <asp:Label ID="lblTituloEvento" runat="server" AssociatedControlID="txtTitulo" title="Título"><%=Resources.Eventos.TituloEvento %></asp:Label>
                </span>
                <asp:TextBox runat="server" ID="txtTitulo" CssClass="frmTxt" MaxLength="50" title="Informe o título"></asp:TextBox>
            </label>
            <label>
                <span>
                    <asp:Label ID="lblResponsavel" runat="server" AssociatedControlID="txtResponsavel" title="Responsável"><%=Resources.Eventos.Responsavel %></asp:Label>
                </span>
                <asp:TextBox runat="server" ID="txtResponsavel" CssClass="frmTxt" MaxLength="50" title="Informe o responsável"></asp:TextBox>
            </label>
            <label>
                <span>
                    <asp:Label ID="lblCidade" runat="server" AssociatedControlID="txtCidade" title="Local"><%=Resources.Eventos.Cidade %></asp:Label>
                </span>
                <asp:TextBox runat="server" ID="txtCidade" CssClass="frmTxt" MaxLength="50" title="Informe a cidade"></asp:TextBox>
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

    <asp:DataGrid summary="Lista de Eventos" ID="grdDados" runat="server" AutoGenerateColumns="false" CssClass="listagem-Grid" AlternatingRowStyle-CssClass="par" Width="100%" AllowPaging="True"
        PageSize="50" AllowSorting="True" OnSortCommand="grdDados_SortCommand">
        <Columns>
            <asp:TemplateColumn HeaderText="Seleção" HeaderStyle-CssClass="tabelaHeader" ItemStyle-Width="5%" ItemStyle-HorizontalAlign="Center">
                <HeaderTemplate>
                    <asp:CheckBox ID="chkSelecionaTodos" CssClass="checkAll" runat="server" />
                </HeaderTemplate>
                <ItemTemplate>
                    <asp:CheckBox ID="chkSeleciona" CssClass="checkItem" runat="server" Enabled='<%# (DateTime.Now >= Convert.ToDateTime(Eval("DataInicio")) ? false : true)%>' />
                </ItemTemplate>
                <ItemStyle Width="50" Height="50" />
            </asp:TemplateColumn>
            <asp:BoundColumn Visible="false" DataField="IdEvento" HeaderText="ID" SortExpression="IdEvento" HeaderStyle-CssClass="tabelaHeader" />
            <asp:BoundColumn DataField="DataInicio" SortExpression="DataInicio" HeaderText="Data Início" DataFormatString="{0:d}" HeaderStyle-CssClass="tabelaHeader asc" ItemStyle-Width="8%" />
            <asp:BoundColumn DataField="DataFim" SortExpression="DataFim" HeaderText="Data Fim"  DataFormatString="{0:d}" HeaderStyle-CssClass="tabelaHeader" ItemStyle-Width="8%"  />
            <asp:BoundColumn DataField="TipoEvento" SortExpression="TipoEvento" HeaderText="Tipo Evento" HeaderStyle-CssClass="tabelaHeader" />
            <asp:BoundColumn DataField="Titulo" SortExpression="Titulo" HeaderText="Título" HeaderStyle-CssClass="tabelaHeader" />
            <asp:BoundColumn DataField="Responsavel" SortExpression="Responsavel" HeaderText="Responsável" HeaderStyle-CssClass="tabelaHeader" />
            <asp:BoundColumn DataField="Cidade" HeaderText="Cidade" SortExpression="Cidade" HeaderStyle-CssClass="tabelaHeader"/>

            <asp:TemplateColumn HeaderText="Editar" HeaderStyle-CssClass="tabelaHeader" ItemStyle-Width="5%" ItemStyle-HorizontalAlign="Center">
                <ItemTemplate>
                    <a id="A1" title="Editar" runat="server" href='<%# String.Format("Editar.aspx?IdEvento={0}", Eval("IdEvento"))%>'>
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
