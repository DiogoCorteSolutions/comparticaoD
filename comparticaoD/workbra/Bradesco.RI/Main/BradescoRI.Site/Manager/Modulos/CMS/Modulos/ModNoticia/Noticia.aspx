<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Noticia.aspx.cs" Inherits="Modulos_CMS_Modulos_ModNoticia_Noticia" %>

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
        <div>
            <label>
                Módulo de notícia
            </label>
            <asp:DropDownList ID="ddlTipoModuloNoticia" AutoPostBack="true" OnSelectedIndexChanged="ddlTipoModuloNoticia_SelectedIndexChanged" runat="server">
                <asp:ListItem Text="Selecione o módulo" Value="-1"></asp:ListItem>
                <asp:ListItem Text="Home" Value="1"></asp:ListItem>
                <asp:ListItem Text="Listagem" Value="2"></asp:ListItem>
                <asp:ListItem Text="Destaques" Value="3"></asp:ListItem>
            </asp:DropDownList>
        </div>
        <asp:Panel ID="pnlNoticiaListagem" runat="server">
            <div>
                <label>
                    Tipo de Notícia:
                    <asp:DropDownList ID="ddlTipoNoticia" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlTipoNoticia_SelectedIndexChanged"></asp:DropDownList>
                </label>
            </div>
            <%--<div>
                <label>
                    Notícia:
                    <asp:DropDownList ID="ddlNoticia" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlNoticia_SelectedIndexChanged"></asp:DropDownList>
                    <asp:ImageButton ID="btnVincularModuloNoticia" runat="server" ImageUrl="~/Imagens/apply.png" OnClick="btnVincularModuloNoticia_Click" Width="20px" Height="20px" />
                </label>
            </div>
            <asp:Panel ID="pnlNoticias" runat="server">
                <div>
                    <asp:GridView ID="grvNoticias" runat="server" AutoGenerateColumns="false" OnRowCommand="grvNoticias_RowCommand">
                        <Columns>
                            <asp:BoundField HeaderText="Código" DataField="Id" ItemStyle-Width="5%" />
                            <asp:BoundField HeaderText="Título" DataField="Titulo" ItemStyle-Width="90%" />
                            <asp:TemplateField HeaderText="Ações" ItemStyle-Width="5%">
                                <ItemTemplate>
                                    <asp:ImageButton ID="btnRemoverNoticia" runat="server" ImageUrl="~/Imagens/deny.png" CommandArgument='<%# Eval("Id") %>' CommandName="RemoverNoticia" Width="20px" Height="20px" />
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                </div>
            </asp:Panel>--%>
            <div>
                <asp:Button ID="btnSalvarListagem" runat="server" Text="Salvar" OnClick="btnSalvarListagem_Click"/>
            </div>
        </asp:Panel>
        <asp:Panel ID="pnlNoticiaDestaque" runat="server">
            <div>
                <label>
                    Capa Esquerda:
                    <asp:DropDownList ID="ddlE1Destaque" runat="server" AutoPostBack="true"></asp:DropDownList>
                </label>
            </div>
            <div>
                <label>
                    Capa Direita Superior
            <asp:DropDownList ID="ddlD1Destaque" runat="server" AutoPostBack="true"></asp:DropDownList>
                </label>
            </div>
            <div>
                <label>
                    Capa Direita Inferior
            <asp:DropDownList ID="ddlD2Destaque" runat="server" AutoPostBack="true"></asp:DropDownList>
                </label>
            </div>
            <div>
                <asp:Button ID="btnSalvarDestaque" runat="server" Text="Salvar" OnClick="btnSalvarDestaque_Click" />
            </div>
        </asp:Panel>
        <asp:Panel ID="pnlNoticiaHome" runat="server">
            <div>
                <label>
                    Capa Esquerda:
                    <asp:DropDownList ID="ddlE1" runat="server" AutoPostBack="true"></asp:DropDownList>
                </label>
            </div>
            <div>
                <label>
                    Capa Direita Superior
            <asp:DropDownList ID="ddlD1" runat="server" AutoPostBack="true"></asp:DropDownList>
                </label>
            </div>

            <div>
                <label>
                    Capa Direita Central
            <asp:DropDownList ID="ddlD2" runat="server" AutoPostBack="true"></asp:DropDownList>
                </label>
            </div>
            <div>
                <label>
                    Capa Direita Inferior
            <asp:DropDownList ID="ddlD3" runat="server" AutoPostBack="true"></asp:DropDownList>
                </label>
            </div>
            <div>
                <asp:Button ID="btnSalvar" runat="server" Text="Salvar" OnClick="btnSalvar_Click" />
            </div>
        </asp:Panel>
    </form>
</body>
</html>
