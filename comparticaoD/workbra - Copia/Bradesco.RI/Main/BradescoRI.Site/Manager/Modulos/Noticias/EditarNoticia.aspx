<%@ Page Title="" Language="C#" MasterPageFile="~/Modulos/Modulos.master"  AutoEventWireup="true" CodeFile="EditarNoticia.aspx.cs" Inherits="Modulos_Noticias_EditarNoticia" %>

<%@ Register Namespace="Controles.FCKeditor" TagPrefix="FCKEditor" %>
<asp:Content ID="Content1" ContentPlaceHolderID="contentInterna" runat="Server">

    <div id="title">
        <h1>
            <asp:Label ID="lblTitulo" runat="server"><%=Resources.Noticias.Titulo %></asp:Label>
            <asp:HiddenField ID="hdnNoticiaId" runat="server" />
        </h1>
    </div>
    <br />
    <div>
        <label>
            <span>
                <asp:Label ID="lblIdioma" runat="server" AssociatedControlID="ddlIdioma" title="Idioma"><%=Resources.Noticias.Idioma %>*</asp:Label>
            </span>
            <asp:DropDownList runat="server" ID="ddlIdioma" title="Selecione o Idioma"></asp:DropDownList>
            <asp:RequiredFieldValidator ID="rfvidioma" runat="server" ControlToValidate="ddlIdioma"  ValidationGroup="vgrNoticia" Display="Dynamic" title="Campo obrigatório"></asp:RequiredFieldValidator>
        </label>
    </div>
    <div>
        <label>
            <span>
                <asp:Label ID="lblTipoNoticia" runat="server" AssociatedControlID="ddlTipoNoticia" title="Idioma"><%=Resources.Noticias.LabelTipoNoticia %>*</asp:Label>
            </span>
            <asp:DropDownList runat="server" ID="ddlTipoNoticia" title="Selecione o Idioma"></asp:DropDownList>
            <asp:RequiredFieldValidator ID="rfvTipoNoticia" runat="server" ControlToValidate="ddlTipoNoticia"  ValidationGroup="vgrNoticia" Display="Dynamic" title="Campo obrigatório"></asp:RequiredFieldValidator>
        </label>
    </div>
    <div>
        <label>
            <span>
                <asp:Label ID="lblTituloNoticia" runat="server" AssociatedControlID="txtTituloNoticia" title="Resumo"><%=Resources.Noticias.LabelTitulo %>*</asp:Label>
            </span>
            <asp:TextBox runat="server" TextMode="SingleLine" ID="txtTituloNoticia" CssClass="frmTxt" MaxLength="4096" title="Informe o resumo"></asp:TextBox>
            <asp:RequiredFieldValidator ID="rfvtitulo" runat="server" ControlToValidate="txtTituloNoticia"  ValidationGroup="vgrNoticia" Display="Dynamic" title="Campo obrigatório"></asp:RequiredFieldValidator>
        </label>
    </div>
    <div>
        <label>
            <span>
                <asp:Label ID="lblDataNoticia" runat="server" AssociatedControlID="txtDataNoticia" title="Notícia"><%=Resources.Noticias.LabelDataNoticia %>*</asp:Label>
            </span>
            <asp:Calendar ID="caledarDataNoticia"  runat="server" Visible="false"  Width="100px" Height="100px" OnSelectionChanged="caledarDataNoticia_SelectionChanged"></asp:Calendar>
            <asp:TextBox  ID="txtDataNoticia" runat="server" CssClass="frmTxt" MaxLength="10" Width="120px"></asp:TextBox>
            <asp:ImageButton ID="btnShowCalendario" runat="server" CausesValidation="false" ImageUrl="~/Imagens/calendar.png" OnClick="btnDataNoticia_Click" />
            <asp:RequiredFieldValidator ID="rfvDataNoticia" runat="server" ControlToValidate="txtDataNoticia"  ValidationGroup="vgrNoticia" Display="Dynamic" title="Campo obrigatório"></asp:RequiredFieldValidator>
        </label>
    </div>
    <div style="height: 400px">
        <label>
            <span>
                <asp:Label ID="lblResumo" runat="server" AssociatedControlID="txtResumo" title="Resumo"><%=Resources.Noticias.LabelResumo %>*</asp:Label>
            </span>
            <FCKEditor:FCKeditor ID="txtResumo" runat="server" Height="350px" BasePath="~/fckeditor/" DefaultLanguage="pt-BR" EnableSourceXHTML="false"
                EnableXHTML="false" Debug="false">
            </FCKEditor:FCKeditor>

        </label>
    </div>
    <div style="height: 400px">
        <label>
            <span>
                <asp:Label ID="lblIntegra" runat="server" AssociatedControlID="txtIntegra" title="Resumo"><%=Resources.Noticias.LabelIntegra %>*</asp:Label>
            </span>
            <FCKEditor:FCKeditor ID="txtIntegra" runat="server" Height="350px" BasePath="~/fckeditor/" DefaultLanguage="pt-BR" EnableSourceXHTML="false"
                EnableXHTML="false" Debug="false">
            </FCKEditor:FCKeditor>
        </label>
    </div>
    <div>
        <label>
            <asp:CheckBox ID="chkDestaque" runat="server" ToolTip="Destaque" />
        </label>
    </div>
    <br />
    <div>
        <label>
            <span>
                <asp:Label ID="lblFonte" runat="server" AssociatedControlID="txtFonte" title="Resumo"><%=Resources.Noticias.LabelFonte %>*</asp:Label>
            </span>
            <asp:TextBox runat="server" TextMode="SingleLine" ID="txtFonte" CssClass="frmTxt" MaxLength="2048" title="Informe a fonte"></asp:TextBox>
            <asp:RequiredFieldValidator ID="rfvFonte" runat="server" ControlToValidate="txtFonte"  ValidationGroup="vgrNoticia" Display="Dynamic" title="Campo obrigatório"></asp:RequiredFieldValidator>
        </label>
    </div>

    <div>
        <label>
            <span>
                <asp:Label ID="lblCamposObrigatorios" runat="server" title="Campos obrigatórios"><%=Resources.Textos.Campos_Obrigatorios %></asp:Label>
            </span>
        </label>
    </div>
    <br />
    <%--<div>
        <asp:Panel ID="pnlFormArquivos" runat="server" Visible="false">
            <div class="fields">

                <label>
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
                </label>
                <label>
                    <span>
                        <asp:RadioButtonList ID="rdoTipoArquivo" runat="server" RepeatDirection="Horizontal" AutoPostBack="true" OnSelectedIndexChanged="rdoTipoArquivo_SelectedIndexChanged">
                            <asp:ListItem Text="Capa" Value="1" Selected="True" />
                            <asp:ListItem Text="Lista" Value="2" />
                            <asp:ListItem Text="Detalhe" Value="3" />
                        </asp:RadioButtonList>
                        <asp:RequiredFieldValidator ID="rfvTipoArquivo" ValidationGroup="rdoTipoArquivo" runat="server" ControlToValidate="fulArquivo" Display="Dynamic" title="Campo obrigatório"></asp:RequiredFieldValidator>
                        <asp:DropDownList ID="ddlNoticiaLayout" Visible="false" runat="server">
                        </asp:DropDownList>
                    </span>
                </label>
                <asp:Button ID="btnAddFile" runat="server" ToolTip="Adicionar arquivo" ValidationGroup="vgrArquivos" OnClick="btnAddFile_Click" />
            </div>

        </asp:Panel>
        <asp:Panel ID="pnlGridArquivos" runat="server">

            <asp:GridView summary="Lista de Arquivos" ID="grdArquivos" runat="server" AutoGenerateColumns="false" CssClass="listagem-Grid" AlternatingRowStyle-CssClass="par"
                Width="100%" AllowPaging="True" PageSize="50" AllowSorting="True" EmptyDataText="Não há arquivos para visualização" 
                OnRowCommand="grdArquivos_RowCommand" OnRowDataBound="grdArquivos_RowDataBound">
                <Columns>
                    <asp:BoundField DataField="ID" HeaderText="ID" SortExpression="ArquivoNoticiaId" ItemStyle-HorizontalAlign="Center" HeaderStyle-CssClass="tabelaHeader" />
                    <asp:BoundField DataField="Nome" SortExpression="Nome" HeaderText="Título" HeaderStyle-CssClass="tabelaHeader asc" />
                    <asp:BoundField DataField="Capa" SortExpression="Capa"  HeaderText="Capa" HeaderStyle-CssClass="tabelaHeader asc" />
                    <asp:BoundField DataField="Lista" SortExpression="Lista" HeaderText="Lista" HeaderStyle-CssClass="tabelaHeader asc" />
                    <asp:BoundField DataField="Detalhe" SortExpression="Detalhe"  HeaderText="Detalhe" HeaderStyle-CssClass="tabelaHeader asc" />
                    <asp:TemplateField HeaderText="Tipo de Arquivo" ItemStyle-HorizontalAlign="Center">
                        <ItemTemplate>
                            <asp:Label ID="lblTipoArquivo" runat="server" ></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    
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

    </div>--%>
    <div class="btn-acoes2">
        <asp:PlaceHolder runat="server" ID="phOptions" Visible="true">
            <asp:Button runat="server" ID="btnCancelar" CssClass="cancelar" Text="Voltar" OnClick="btnCancelar_Click" CausesValidation="false" title="Cancelar" alt="Cancelar" />
            <asp:Button runat="server" ID="btnSalvar" CssClass="submit" Text="Salvar" ValidationGroup="vgrNoticia"  OnClick="btnSalvar_Click" title="Salvar" alt="Salvar"  />
        </asp:PlaceHolder>
    </div>

</asp:Content>
