<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Relatorio.aspx.cs" Inherits="Modulos_CMS_Modulos_ModRelatorio_Relatorio" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <script>
        function refreshParent() {
            window.opener.location.href = window.opener.location.href;
            window.close();
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
        <div>
            <label>
                Módulo de relatório:
            <asp:DropDownList ID="ddlModuloRelatorio" runat="server" OnSelectedIndexChanged="ddlModuloRelatorio_SelectedIndexChanged" AutoPostBack="true">
                <asp:ListItem Text="Selecione o módulo de relatório" Value="-1" />
                <asp:ListItem Text="Home" Value="1" />
                <asp:ListItem Text="Interna" Value="2"></asp:ListItem>
            </asp:DropDownList>
            </label>
        </div>
        <asp:Panel ID="pnlModuloHome" runat="server" Visible="false">
            <div>
                <label>
                    Tipo de Relatório:
                    <asp:CheckBoxList ID="chkTipoRelatorio" runat="server" RepeatDirection="Horizontal"></asp:CheckBoxList>
                </label>
            </div>
            <hr />
            <div>
                <label>
                    Tipo de Comunicado:
                    <asp:DropDownList ID="ddlTipoComunicado" runat="server" OnSelectedIndexChanged="ddlTipoComunicado_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
                </label>
            </div>
            <div id="divComunicado" runat="server" visible="false">
                <label>
                    Comunicados:
                </label>
                <asp:DropDownList ID="ddlComunicado" runat="server" Width="500px"></asp:DropDownList>
                <asp:Button ID="btnAdicionarComunicado" runat="server" Text="+" OnClick="btnAdicionarComunicado_Click" />
            </div>
            <asp:Panel ID="pnlGrid" runat="server">
                <div>
                    <asp:GridView ID="grvComunicado" runat="server" AutoGenerateColumns="false" OnRowDataBound="grvComunicado_RowDataBound" OnRowCommand="grvComunicado_RowCommand" 
                        CssClass="listagem-Grid" AlternatingRowStyle-CssClass="par" EmptyDataText="Nenhum registro encontrado">
                        <Columns>
                            <asp:BoundField DataField="ComunicadoId" ItemStyle-Width="5%" HeaderText="ID" />
                            <asp:TemplateField ItemStyle-Width="90%" HeaderStyle-HorizontalAlign="Center" HeaderText="Título">
                                <ItemTemplate>
                                    <asp:Label ID="lblTituloComunicado" runat="server"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField ItemStyle-Width="5%" ItemStyle-HorizontalAlign="Center">
                                <ItemTemplate>
                                    <asp:ImageButton ID="btnExcluir" CommandName="RemoverComunicado" CommandArgument='<%# ((GridViewRow) Container).RowIndex %>' runat="server" ImageUrl="~/Imagens/remove-icon.png" Width="20px" Height="20px" ToolTip="Remover" />
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                </div>
            </asp:Panel>
            <hr />
            <div>
                <asp:Button ID="btnModuloHome" runat="server" ToolTip="Salvar" Text="Salvar" OnClick="btnModuloHome_Click" />
            </div>
        </asp:Panel>
        <asp:Panel ID="pnlModuloInterna" runat="server" Visible="false">

        </asp:Panel>
    </form>
</body>
</html>
