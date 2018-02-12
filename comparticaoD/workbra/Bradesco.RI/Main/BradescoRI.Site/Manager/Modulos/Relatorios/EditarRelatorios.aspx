<%@ Page Title="" Language="C#" MasterPageFile="~/Modulos/Modulos.master" AutoEventWireup="true" CodeFile="EditarRelatorios.aspx.cs" Inherits="Modulos_Relatorios_EditarRelatorios" %>

<%@ Register Namespace="Controles.FCKeditor" TagPrefix="FCKEditor" %>
<asp:Content ID="Content1" ContentPlaceHolderID="contentInterna" runat="Server">
    <div id="title">
        <h1>
            <asp:Label ID="lblTitulo" runat="server"><%=Resources.Relatorios.Titulo %></asp:Label>
            <asp:HiddenField ID="hdnRelatoriosId" runat="server" />
        </h1>
    </div>
    <br />
    <div>
        <label>
            <span>
                <asp:Label ID="lblIdioma" runat="server" AssociatedControlID="ddlIdioma" title="Idioma"><%=Resources.Noticias.Idioma %>*</asp:Label>
            </span>
            <asp:DropDownList runat="server" ID="ddlIdioma" title="Selecione o Idioma"></asp:DropDownList>
            <asp:RequiredFieldValidator ID="rfvidioma" runat="server" ControlToValidate="ddlIdioma" ValidationGroup="vgrRelatorio" Display="Dynamic" title="Campo obrigatório"></asp:RequiredFieldValidator>
        </label>
    </div>
    <div>
        <label>
            <span>
                <asp:Label ID="lblTipoRelatorio" runat="server" AssociatedControlID="ddlTipoRelatorio" title="Idioma"><%=Resources.Relatorios.LabelTipoRelatorio %>*</asp:Label>
            </span>
            <asp:DropDownList runat="server" ID="ddlTipoRelatorio" title="Selecione o tipo de relatório"></asp:DropDownList>
            <asp:RequiredFieldValidator ID="rfvTipoRelatorio" runat="server" ControlToValidate="ddlTipoRelatorio" ValidationGroup="vgrRelatorio" Display="Dynamic" title="Campo obrigatório"></asp:RequiredFieldValidator>
        </label>
    </div>
    <div>
        <label>
            <span>
                <asp:Label ID="lblTituloRelatorio" runat="server" AssociatedControlID="txtTituloRelatorio" title="Resumo"><%=Resources.Relatorios.LabelTitulo %>*</asp:Label>
            </span>
            <asp:TextBox runat="server" TextMode="SingleLine" ID="txtTituloRelatorio" CssClass="frmTxt" MaxLength="4096" title="Informe o Titulo"></asp:TextBox>
            <asp:RequiredFieldValidator ID="rfvtitulo" runat="server" ControlToValidate="txtTituloRelatorio" ValidationGroup="vgrRelatorio" Display="Dynamic" title="Campo obrigatório"></asp:RequiredFieldValidator>
        </label>
    </div>
    <div>
        <label>
            <span>
                <asp:Label ID="lblDescricaoRelarorio" runat="server" AssociatedControlID="txtDescricaoRelatorio" title="Idioma"><%=Resources.Relatorios.LabelDescricaoRelatorio %>*</asp:Label>
            </span>
            <asp:TextBox runat="server" TextMode="SingleLine" ID="txtDescricaoRelatorio" CssClass="frmTxt" MaxLength="4096" title="Informe a descrição"></asp:TextBox>
            <asp:RequiredFieldValidator ID="rfvDescricao" runat="server" ControlToValidate="txtDescricaoRelatorio" ValidationGroup="vgrRelatorio" Display="Dynamic" title="Campo obrigatório"></asp:RequiredFieldValidator>
        </label>
    </div>

    <div>
        <label>
            <span>
                <asp:Label ID="lblDataRelatorio" runat="server" AssociatedControlID="txtDataRelatorio" title="Notícia"><%=Resources.Relatorios.LabelDataRelatorio %>*</asp:Label>
            </span>
            <asp:TextBox runat="server" TextMode="SingleLine" ID="txtDataRelatorio" CssClass="frmTxt" MaxLength="4096" title="Informe o Titulo"></asp:TextBox>
            <asp:RequiredFieldValidator ID="rfvddlAnoRelatorio" runat="server" ControlToValidate="txtDataRelatorio" ValidationGroup="vgrRelatorio" Display="Dynamic" title="Campo obrigatório"></asp:RequiredFieldValidator>
        </label>
    </div>
    <br />
    <div>
        <asp:Panel ID="pnlFormArquivos" runat="server">
            <div class="fields">
                <%--<label>
                    <span>
                        <asp:Label ID="lblNomeArquivo" runat="server" AssociatedControlID="txtNomeArquivo" title="Nome Arquivo"><%=Resources.Noticias.NomeArquivo %>*</asp:Label>
                    </span>
                    <asp:TextBox ID="txtNomeArquivo" runat="server" ToolTip="Nome do Arquivo"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="rfvNomeArquivo" ValidationGroup="vgrArquivos" runat="server" ControlToValidate="txtNomeArquivo" Display="Dynamic" title="Campo obrigatório"></asp:RequiredFieldValidator>
                </label>
                <label>
                    <span>
                        <asp:Label ID="lblFile" runat="server" AssociatedControlID="fulArquivo" title="Arquivo"><%=Resources.Noticias.Arquivo %>*</asp:Label>
                    </span>
                    <asp:FileUpload ID="fulArquivo" runat="server" ToolTip="Selecione o arquivo" />
                    <asp:RequiredFieldValidator ID="rfvArquivo" ValidationGroup="vgrArquivos" runat="server" ControlToValidate="fulArquivo" Display="Dynamic" title="Campo obrigatório"></asp:RequiredFieldValidator>
                </label>--%>
                <label>
                    <span>
                        <asp:Label ID="lblTipoArquivo" runat="server" AssociatedControlID="ddlTipoArquivo" title="Tipo de Arquivo"><%=Resources.Arquivos.TipoArquivo %></asp:Label>
                    </span>
                    <asp:DropDownList ID="ddlTipoArquivo" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlTipoArquivo_SelectedIndexChanged"></asp:DropDownList>
                </label>
                <label>
                    <span>
                        <asp:Label ID="lblArquivo" runat="server" AssociatedControlID="ddlArquivo" title="Arquivo"><%=Resources.Arquivos.LabelUploadArquivo %></asp:Label>
                    </span>
                    <asp:DropDownList ID="ddlArquivo" runat="server"></asp:DropDownList>
                    <asp:Button ID="btnAddFile" runat="server" ToolTip="Adicionar arquivo" ValidationGroup="vgrArquivos" OnClick="btnAddFile_Click" />
                </label>
                
            </div>

        </asp:Panel>
        <asp:Panel ID="pnlGridArquivos" runat="server">

            <asp:GridView summary="Lista de Arquivos" ID="grdArquivos" runat="server" AutoGenerateColumns="false" CssClass="listagem-Grid" AlternatingRowStyle-CssClass="par"
                Width="100%" AllowPaging="True" PageSize="50" AllowSorting="True" EmptyDataText="Não há arquivos para visualização"
                OnRowCommand="grdArquivos_RowCommand" OnRowDataBound="grdArquivos_RowDataBound">
                <Columns>
                    <asp:BoundField DataField="ID" HeaderText="ID" SortExpression="ArquivoNoticiaId" ItemStyle-HorizontalAlign="Center" HeaderStyle-CssClass="tabelaHeader" />
                    <asp:BoundField DataField="Titulo" SortExpression="Titulo" ItemStyle-Width="40%" HeaderText="Título" HeaderStyle-CssClass="tabelaHeader asc" />
                    <asp:BoundField DataField="Caminho" SortExpression="Caminho" ItemStyle-Width="40%" HeaderText="Path" HeaderStyle-CssClass="tabelaHeader asc" />
                    <asp:BoundField DataField="Extensao" SortExpression="Extensao" ItemStyle-Width="10%" HeaderText="Extensao" HeaderStyle-CssClass="tabelaHeader asc" />
                    <asp:BoundField DataField="Tamanho" SortExpression="Tamanho" ItemStyle-Width="10%" HeaderText="Tamanho" HeaderStyle-CssClass="tabelaHeader asc" />
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
