<%@ Page Title="" Language="C#" MasterPageFile="~/Modulos/Modulos.master" AutoEventWireup="true" CodeFile="EditarComunicados.aspx.cs" Inherits="Modulos_Comunicados_EditarComunicados" %>

<%@ Register Namespace="Controles.FCKeditor" TagPrefix="FCKEditor" %>
<asp:Content ID="Content1" ContentPlaceHolderID="contentInterna" runat="Server">
    <div id="title">
        <h1>
            <asp:Label ID="lblTitulo" runat="server"><%=Resources.Comunicados.Titulo %></asp:Label>
            <asp:HiddenField ID="hdnComunicadoId" runat="server" />
        </h1>
    </div>
    <br />
    <div>
        <label>
            <span>
                <asp:Label ID="lblIdioma" runat="server" AssociatedControlID="ddlIdioma" title="Idioma"><%=Resources.Noticias.Idioma %>*</asp:Label>
            </span>
            <asp:DropDownList runat="server" ID="ddlIdioma" title="Selecione o Idioma"></asp:DropDownList>
            <asp:RequiredFieldValidator ID="rfvidioma" runat="server" ControlToValidate="ddlIdioma" ValidationGroup="vgrComunicado" Display="Dynamic" title="Campo obrigatório"></asp:RequiredFieldValidator>
        </label>
    </div>
    <div>
        <label>
            <span>
                <asp:Label ID="lblTipoComunicado" runat="server" AssociatedControlID="ddlTipoComunicado" title="Idioma"><%=Resources.Comunicados.LabelTipoComunicado %>*</asp:Label>
            </span>
            <asp:DropDownList runat="server" ID="ddlTipoComunicado" title="Selecione o tipo de comunicado" AutoPostBack="true" OnSelectedIndexChanged="ddlTipoComunicado_SelectedIndexChanged"></asp:DropDownList>
            <asp:RequiredFieldValidator ID="rfvTipoComunicado" runat="server" ControlToValidate="ddlTipoComunicado" ValidationGroup="vgrComunicado" Display="Dynamic" title="Campo obrigatório"></asp:RequiredFieldValidator>
        </label>
    </div>
    <div>
        <label>
            <span>
                <asp:Label ID="lblTituloComunicado" runat="server" AssociatedControlID="txtTituloComunicado" title="Resumo"><%=Resources.Comunicados.LabelTitulo %>*</asp:Label>
            </span>
            <asp:TextBox runat="server" TextMode="SingleLine" ID="txtTituloComunicado" CssClass="frmTxt" MaxLength="4096" title="Informe o Titulo"></asp:TextBox>
            <asp:RequiredFieldValidator ID="rfvtitulo" runat="server" ControlToValidate="txtTituloComunicado" ValidationGroup="vgrComunicado" Display="Dynamic" title="Campo obrigatório"></asp:RequiredFieldValidator>
        </label>
    </div>
    <div>
        <label>
            <span>
                <asp:Label ID="lblDescricaoComunicado" runat="server" AssociatedControlID="txtDescricaoComunicado" title="Idioma"><%=Resources.Comunicados.LabelDescricaoComunicado %>*</asp:Label>
            </span>
            <asp:TextBox runat="server" TextMode="SingleLine" ID="txtDescricaoComunicado" CssClass="frmTxt" MaxLength="4096" title="Informe a descrição"></asp:TextBox>
            <asp:RequiredFieldValidator ID="rfvDescricao" runat="server" ControlToValidate="txtDescricaoComunicado" ValidationGroup="vgrComunicado" Display="Dynamic" title="Campo obrigatório"></asp:RequiredFieldValidator>
        </label>
    </div>

    <div>
        <label>
            <span>
                <asp:Label ID="lblDataComunicado" runat="server" AssociatedControlID="txtDataComunicado" title="Notícia"><%=Resources.Comunicados.LabelDataComunicado %>*</asp:Label>
            </span>
            <asp:TextBox runat="server" TextMode="SingleLine" ID="txtDataComunicado" CssClass="frmTxt" MaxLength="4096" title="Informe o Titulo"></asp:TextBox>
            <asp:RequiredFieldValidator ID="rfvDataComunicado" runat="server" ControlToValidate="txtDataComunicado" ValidationGroup="vgrComunicado" Display="Dynamic" title="Campo obrigatório"></asp:RequiredFieldValidator>
        </label>
    </div>
    <br />
    <div>
        <asp:Panel ID="pnlFormArquivos" runat="server">
            <div class="fields">
                <label>
                    <span>
                        <asp:Label ID="lblArquivo" runat="server" AssociatedControlID="ddlArquivo" title="Arquivo"><%=Resources.Noticias.Arquivo %>*</asp:Label>
                    </span>
                </label>
                <asp:DropDownList ID="ddlArquivo" runat="server"></asp:DropDownList>
                <asp:Button ID="btnAddFile" runat="server" ToolTip="Adicionar arquivo" ValidationGroup="vgrArquivos" OnClick="btnAddFile_Click" />
            </div>

        </asp:Panel>
        <asp:Panel ID="pnlGridArquivos" runat="server">

            <asp:GridView summary="Lista de Arquivos" ID="grdArquivos" runat="server" AutoGenerateColumns="false" CssClass="listagem-Grid" AlternatingRowStyle-CssClass="par"
                Width="100%" AllowPaging="True" PageSize="50" AllowSorting="True" EmptyDataText="Não há arquivos para visualização"
                OnRowCommand="grdArquivos_RowCommand" OnRowDataBound="grdArquivos_RowDataBound">
                <Columns>
                    <asp:BoundField DataField="ID" HeaderText="ID" SortExpression="ArquivoId" ItemStyle-HorizontalAlign="Center" HeaderStyle-CssClass="tabelaHeader" />
                    <asp:BoundField DataField="Titulo" SortExpression="Titulo" ItemStyle-Width="55%" HeaderText="Título" HeaderStyle-CssClass="tabelaHeader asc" />
                    <asp:BoundField DataField="Caminho" SortExpression="Caminho" ItemStyle-Width="30%" HeaderText="Path" HeaderStyle-CssClass="tabelaHeader asc" />
                    <asp:BoundField DataField="Extensao" SortExpression="Extensao" ItemStyle-Width="10%" HeaderText="Extensao" HeaderStyle-CssClass="tabelaHeader asc" />
                    <asp:TemplateField HeaderText="Ações" HeaderStyle-CssClass="tabelaHeader" ItemStyle-Width="5%" ItemStyle-HorizontalAlign="Center">
                        <ItemTemplate>

                            <asp:ImageButton runat="server" ID="A3" ToolTip="Excluir" Width="1.5em" ImageUrl="~/Imagens/deny.png"
                                OnClientClick="return confirm('Deseja excluir realmente?');" CommandName="ExcluirArquivo" CommandArgument='<%# ((GridViewRow) Container).RowIndex %>' />

                        </ItemTemplate>
                        <ItemStyle />
                    </asp:TemplateField>
                </Columns>
                <HeaderStyle CssClass="topo-tb"></HeaderStyle>
                <RowStyle CssClass="impar"></RowStyle>
                <AlternatingRowStyle CssClass="par"></AlternatingRowStyle>
                <PagerStyle Width="0px" />
            </asp:GridView>
        </asp:Panel>

    </div>
    <div class="btn-acoes2">
        <asp:PlaceHolder runat="server" ID="phOptions" Visible="true">
            <asp:Button runat="server" ID="btnCancelar" CssClass="cancelar" Text="Voltar" OnClick="btnCancelar_Click" CausesValidation="false" title="Cancelar" alt="Cancelar" />
            <asp:Button runat="server" ID="btnSalvar" CssClass="submit" Text="Salvar" ValidationGroup="vgrNoticia" OnClick="btnSalvar_Click" title="Salvar" alt="Salvar" />
        </asp:PlaceHolder>
    </div>

</asp:Content>

