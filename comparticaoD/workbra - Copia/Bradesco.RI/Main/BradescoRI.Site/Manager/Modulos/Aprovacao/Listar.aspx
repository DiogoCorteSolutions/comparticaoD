<%@ Page Title="" Language="C#" MasterPageFile="~/Modulos/Modulos.master" AutoEventWireup="true" CodeFile="Listar.aspx.cs" Inherits="Modulos_Aprovacao_Listar" %>

<%@ Register Namespace="WebFoundations.ServerControls" TagPrefix="wfc" %>

<asp:Content ID="Content1" ContentPlaceHolderID="contentInterna" runat="Server">
    <div id="title">
        <h1>
            <asp:Label ID="lblTitulo" runat="server"><%=Resources.Aprovacao.Titulo %></asp:Label>
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
                    <asp:CheckBox ID="chkAprovados" runat="Server"/>
                    <asp:CheckBox ID="chkReprovados" runat="Server"/>
                </span>
            </label>        
        </div>
    </div>
    <div id="buscar" class="btn-acoes">
        <asp:Button ID="btnBuscar" CssClass="adicionar" runat="server" OnClick="btnBuscar_Click" alt="Novo" title="Buscar" />
    </div>

    <asp:Panel ID="pnlNenhumRegistro" Visible="false" runat="server">
        <div class="txt-n-reg" title="Nenhum registro encontrado"><%=Resources.Textos.Nenhum_Registro %></div>
    </asp:Panel>

    <asp:DataGrid summary="Lista de aprovações" ID="grdDados" runat="server" AutoGenerateColumns="false" CssClass="listagem-Grid" AlternatingRowStyle-CssClass="par" Width="100%" AllowPaging="True" PageSize="50" AllowSorting="True" OnSortCommand="grdDados_SortCommand">
        <Columns>
            <asp:BoundColumn DataField="paginaId" SortExpression="paginaId" HeaderText="Id Página" HeaderStyle-CssClass="tabelaHeader asc" ItemStyle-Width="8%" ItemStyle-HorizontalAlign="Center" />
            <asp:BoundColumn DataField="TituloPagina" SortExpression="TituloPagina" HeaderText="Título Página" HeaderStyle-CssClass="tabelaHeader" ItemStyle-Width="42%" ItemStyle-HorizontalAlign="Center" />
            <asp:BoundColumn DataField="DataCadastro" SortExpression="DataCadastro" HeaderText="Data Publicação" HeaderStyle-CssClass="tabelaHeader" ItemStyle-Width="12%" ItemStyle-HorizontalAlign="Center" />
            <asp:BoundColumn DataField="NomeUsuario" SortExpression="NomeUsuario" HeaderText="Publicado por" HeaderStyle-CssClass="tabelaHeader" ItemStyle-Width="20%" ItemStyle-HorizontalAlign="Center" />
            <asp:BoundColumn DataField="Status" SortExpression="Status" HeaderText="Status" HeaderStyle-CssClass="tabelaHeader" ItemStyle-Width="10%" ItemStyle-HorizontalAlign="Center" />
            <asp:TemplateColumn HeaderText="" HeaderStyle-CssClass="tabelaHeader" ItemStyle-Width="8%" ItemStyle-HorizontalAlign="Center">
                <ItemTemplate>                    
                    <button id="A1" type="button" onclick="<%# string.Format("javascript:window.open('/Manager/Modulos/Aprovacao/Preview.aspx?AprovacaoId={0}', '', ',type=fullWindow,fullscreen,scrollbars=yes, menubar=no');", Utilitarios.EnCryptDecrypt.CryptorEngine.Encrypt(Eval("AprovacaoId").ToString())) %>">
                        <img src="/manager/Imagens/edit-icon.png" style="width: 1.5em;" /></a>
                    </button>
                    <asp:Image runat="server" ID="imgHome" ToolTip="HomePage" Width="1.5em" ImageUrl="~/Imagens/home-on.png" Visible='<%# (Convert.ToBoolean(Eval("HomePage")) ? true  : false)%>'  />
                </ItemTemplate>
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

</asp:Content>

