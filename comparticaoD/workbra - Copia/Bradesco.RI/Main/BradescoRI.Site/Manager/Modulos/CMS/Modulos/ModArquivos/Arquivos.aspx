<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Arquivos.aspx.cs" Inherits="Modulos_CMS_Modulos_ModArquivos_Arquivos" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>   
     <script>
        function refreshParent() {
            window.opener.location.href = window.opener.location.href;
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            Idioma:
            <asp:DropDownList ID="ddlIdioma" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlIdioma_SelectedIndexChanged"></asp:DropDownList>
            <asp:HiddenField ID="hdnModArquivo" runat="server" />
        </div>
        <div>
            Tipo de Layout:
            <asp:DropDownList ID="ddlTipoLayout" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlTipoLayout_SelectedIndexChanged">
            </asp:DropDownList>
        </div>
        <div>
            Filtros:
            <asp:DropDownList ID="ddlFiltros" runat="server">
            </asp:DropDownList>
        </div>

        <div>
            Titulo:
            <asp:TextBox runat="server" TextMode="SingleLine" ID="txtTitulo" CssClass="frmTxt" MaxLength="4096" title="Informe o titulo"></asp:TextBox>
            <asp:CheckBox ID="chkMostraTitulo" runat="server" Text="Mostrar título" Checked="true" />
        </div>
        
        <div>
            Tipo deArquivo:
            <asp:DropDownList ID="ddltipoArquivo" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddltipoArquivo_SelectedIndexChanged"></asp:DropDownList>
            Arquivo:
            <asp:DropDownList ID="ddlArquivo" runat="server"></asp:DropDownList>
            <asp:Button ID="btnIncluirArquivo" runat="server" Text="Incluir"  OnClick="btnIncluirArquivo_Click"/>
        </div>
        <asp:panel ID="pnlArquivos" runat="server">
            <asp:GridView ID="grvArquivos" AutoGenerateColumns="false" OnRowDataBound="grvArquivos_RowDataBound" runat="server"  EmptyDataText="Nenhum arquivo encontrado">
                <Columns>
                    <asp:BoundField HeaderText="ID" DataField="Id" ItemStyle-Width="5%" />

                    <asp:TemplateField HeaderText="Tipo" HeaderStyle-CssClass="tabelaHeader" ItemStyle-Width="35%" ItemStyle-HorizontalAlign="Center">
                        <ItemTemplate>
                            <asp:Label ID="lblTipoArquivo" runat="server"></asp:Label>
                        </ItemTemplate>
                        <ItemStyle />
                    </asp:TemplateField>
                    <asp:BoundField HeaderText="Titulo" ItemStyle-Width="50%" DataField="Titulo" />

                    <asp:TemplateField HeaderText="Ações" HeaderStyle-CssClass="tabelaHeader" ItemStyle-Width="10%" ItemStyle-HorizontalAlign="Center">
                        <ItemTemplate>

                            <asp:ImageButton runat="server" ID="btnExcluir" ToolTip="Excluir" Width="1.5em" ImageUrl="~/Imagens/deny.png"
                                OnClientClick="return confirm('Deseja excluir realmente?');" OnCommand="btnExcluir_Command" CommandName="ExcluirArquivo"  CommandArgument='<%# ((GridViewRow) Container).RowIndex %>' />

                        </ItemTemplate>
                        <ItemStyle />
                    </asp:TemplateField>
                </Columns>

            </asp:GridView>
        </asp:panel>
        <div>
            <asp:Button ID="btnSalvar" runat="server" Text="Salvar" OnClick="btnSalvar_Click" />
        </div>
                            
    </form>
</body>
</html>
