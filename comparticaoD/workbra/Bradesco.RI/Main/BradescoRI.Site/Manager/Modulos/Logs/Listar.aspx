<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Listar.aspx.cs" MasterPageFile="~/Modulos/Modulos.master" Inherits="Modulos_Logs_Listar" %>
<%@ Register Namespace="WebFoundations.ServerControls" TagPrefix="wfc" %>
<asp:Content ID="Content1" ContentPlaceHolderID="contentInterna" runat="Server">
    <div id="title">
        <h1>
            <asp:Label ID="lblTitulo" runat="server"><%=Resources.Log.Titulo %></asp:Label>
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
                    <asp:Label ID="lblDescricao" runat="server" AssociatedControlID="txtDescricao" title="Descrição"><%=Resources.Log.Descricao %></asp:Label>
                </span>
                <asp:TextBox runat="server" ID="txtDescricao" CssClass="frmTxt" MaxLength="50" title="Informe a descrição"></asp:TextBox>
            </label>
            <label class="txtPeriodo">
                <span>
                    <asp:Label ID="lblDataInicio" runat="server" AssociatedControlID="txtDataInicio" title="Período"><%=Resources.Textos.Periodo %></asp:Label>
                </span>
                <asp:TextBox runat="server" ID="txtDataInicio" CssClass="frmTxt txtDate" MaxLength="10" title="Informe a data de inicio"></asp:TextBox>
                Até
                        <asp:TextBox runat="server" ID="txtDataFim" CssClass="frmTxt txtDate" MaxLength="10" title="Informe a data de término"></asp:TextBox>
            </label>
        </div>
         <label class="btn-acoes-right">
            <asp:Button runat="server" ID="btnBuscar" OnClick="btnBuscar_Click" />
        </label>
    </div>
    <asp:Panel ID="pnlNenhumRegistro" Visible="false" runat="server">
        <div class="txt-n-reg" title="Nenhum registro encontrado"><%=Resources.Textos.Nenhum_Registro %></div>
    </asp:Panel>

    <asp:Panel ID="pnlRegistrosEncontrados" runat="server">
        <div class="txt-n-reg">
            <asp:Literal runat="server" ID="ltlRegistrosEncontrados"></asp:Literal><span><asp:Literal runat="server" ID="ltlQuantidadeRegistrosEncontrados"></asp:Literal></span>
        </div>
    </asp:Panel>

    <asp:DataGrid summary="Lista de Logs" ID="grdDados" runat="server" AutoGenerateColumns="false" CssClass="listagem-Grid" AlternatingRowStyle-CssClass="par" Width="100%" AllowPaging="True" PageSize="50" AllowSorting="True"
        OnSortCommand="grdDados_SortCommand">
        <Columns>
            <asp:BoundColumn DataField="Id" HeaderText="ID" SortExpression="Id" HeaderStyle-CssClass="tabelaHeader desc" ItemStyle-Width="5%" ItemStyle-HorizontalAlign="Center" />
            <asp:BoundColumn DataField="Data_hora" SortExpression="Data_hora" HeaderText="Data" HeaderStyle-CssClass="tabelaHeader" ItemStyle-Width="15%" ItemStyle-HorizontalAlign="Center" />
            <asp:BoundColumn DataField="LoginUsuario" SortExpression="LoginUsuario" HeaderText="Login" HeaderStyle-CssClass="tabelaHeader" ItemStyle-Width="15%" ItemStyle-HorizontalAlign="Center" />
            <asp:BoundColumn DataField="Mensagem" SortExpression="Mensagem" HeaderText="Descrição" HeaderStyle-CssClass="tabelaHeader" ItemStyle-Width="65%" />
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

