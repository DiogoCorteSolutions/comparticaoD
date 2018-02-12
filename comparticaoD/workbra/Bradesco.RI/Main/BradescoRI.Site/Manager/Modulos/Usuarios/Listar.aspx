<%@ Page Language="C#" MasterPageFile="~/Modulos/Modulos.master" AutoEventWireup="true" CodeFile="Listar.aspx.cs" Inherits="Modulos_Usuarios_Listar" %>

<%@ Register Namespace="WebFoundations.ServerControls" TagPrefix="wfc" %>
<asp:Content ID="Content1" ContentPlaceHolderID="contentInterna" runat="Server">
    <div id="title">
        <h1>
            <asp:Label ID="lblTitulo" runat="server"><%=Resources.Usuario.Titulo %></asp:Label>
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
                    <asp:Label ID="lblNome" runat="server" AssociatedControlID="txtNome" title="Nome"><%=Resources.Usuario.Nome %></asp:Label>
                </span>
                <asp:TextBox runat="server" ID="txtNome" CssClass="frmTxt" MaxLength="200" title="Informe o nome"></asp:TextBox>
            </label>
            <label>
                <span>
                    <asp:Label ID="lblLogin" runat="server" AssociatedControlID="txtNome" title="Login"><%=Resources.Usuario.Login %></asp:Label>
                </span>
                <asp:TextBox runat="server" ID="txtLogin" CssClass="frmTxt" MaxLength="50" title="Informe o login"></asp:TextBox>
            </label>
            <label>
                <span>
                    <asp:Label ID="lblEmail" runat="server" AssociatedControlID="txtNome" title="Email"><%=Resources.Usuario.Email %></asp:Label>
                </span>
                <asp:TextBox runat="server" ID="txtEmail" CssClass="frmTxt" MaxLength="200" title="Informe o email"></asp:TextBox>
            </label>
            <label>
                <span>
                    <asp:Label ID="lblPerfil" runat="server" title="Perfil"><%=Resources.Usuario.Perfil %></asp:Label>
                </span>
                <asp:DropDownList runat="server" ID="ddlPerfil" title="Selecione o perfil"></asp:DropDownList>
            </label>
            <label>
                <span>
                    <asp:Label ID="lblStatus" runat="server" title="Status"><%=Resources.Usuario.Status %></asp:Label>
                </span>
                <asp:DropDownList runat="server" ID="ddlStatus" title="Selecione status">
                    <asp:ListItem Text="Todos" Value="-1"></asp:ListItem>
                    <asp:ListItem Text="Ativo" Value="1"></asp:ListItem>
                    <asp:ListItem Text="Inativo" Value="0"></asp:ListItem>
                </asp:DropDownList>
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
            <asp:BoundColumn Visible="false" DataField="Id" HeaderText="ID" SortExpression="Id" HeaderStyle-CssClass="tabelaHeader" />
            <asp:BoundColumn DataField="Nome" SortExpression="Nome" HeaderText="Nome" HeaderStyle-CssClass="tabelaHeader asc" ItemStyle-Width="20%" />
            <asp:BoundColumn DataField="Login" SortExpression="Login" HeaderText="Login" HeaderStyle-CssClass="tabelaHeader" ItemStyle-Width="15%" />
            <asp:BoundColumn DataField="Email" SortExpression="Email" HeaderText="E-mail" HeaderStyle-CssClass="tabelaHeader" ItemStyle-Width="15%" />
            <asp:BoundColumn DataField="NomePerfil" HeaderText="Perfil" SortExpression="Perfil" HeaderStyle-CssClass="tabelaHeader" ItemStyle-Width="15%" ItemStyle-HorizontalAlign="Center" />

            <asp:TemplateColumn HeaderText="Status" HeaderStyle-CssClass="tabelaHeader" ItemStyle-Width="10%" ItemStyle-HorizontalAlign="Center">
                <ItemTemplate>
                    <%# (Convert.ToBoolean(Eval("Ativo"))? "Ativo":"Inativo")%>
                </ItemTemplate>
                <ItemStyle />
            </asp:TemplateColumn>
            <asp:BoundColumn DataField="DataUltimoAcesso" SortExpression="DataUltimoAcesso" HeaderText="Último Acesso"
                HeaderStyle-CssClass="tabelaHeader" ItemStyle-Width="15%" ItemStyle-HorizontalAlign="Center" />
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

