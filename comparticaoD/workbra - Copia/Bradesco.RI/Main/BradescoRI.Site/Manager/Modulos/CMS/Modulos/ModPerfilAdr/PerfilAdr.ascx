<%@ Control Language="C#" AutoEventWireup="true" CodeFile="PerfilAdr.ascx.cs" Inherits="Modulos_CMS_Modulos_ModPerfilAdr_PerfilAdr" %>

<link rel="stylesheet" href="<%=ResolveUrl("~/CSS/ModCotacoes.css")%>">

<div style="width: 275px;">
    <asp:Repeater ID="rptAcao" runat="server">
        <HeaderTemplate>
            <table cellpadding="0" cellspacing="0" class="tableDefault">
                <colgroup>
                    <col style="width: 165px;" />
                    <col style="width: 75px;" />
                </colgroup>
                <thead>
                    <tr>
                        <th class="colLft" colspan="4">
                            <asp:Literal ID="ltrCotacoes" runat="server" Text='<%# Resources.Cotacoes.Perfil %>'></asp:Literal><br />
                        </th>
                    </tr>
                </thead>
                <tbody>
        </HeaderTemplate>
        <ItemTemplate>
            <tr>
                <td>
                    <label title='<%# Eval("Titulo") %>'><%# Eval("Titulo") %></label>
                </td>
                <td>
                    <label title='<%# Eval("Valor") %>'><%# Eval("Valor") %></label>
                </td>
            </tr>
        </ItemTemplate>
        <AlternatingItemTemplate>
            <tr class="even">
                <td>
                    <label title='<%# Eval("Titulo") %>'><%# Eval("Titulo") %></label>
                </td>
                <td>
                    <label title='<%# Eval("Valor") %>'><%# Eval("Valor") %></label>
                </td>
            </tr>
        </AlternatingItemTemplate>
        <FooterTemplate>
            </tbody> </table>
        </FooterTemplate>
    </asp:Repeater>
</div>
