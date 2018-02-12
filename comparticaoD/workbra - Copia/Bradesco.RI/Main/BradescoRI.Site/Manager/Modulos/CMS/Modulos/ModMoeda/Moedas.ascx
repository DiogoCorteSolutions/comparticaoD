<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Moedas.ascx.cs" Inherits="Modulos_CMS_Modulos_ModMoeda_Moedas" %>

<link rel="stylesheet" href="<%=ResolveUrl("~/CSS/ModCotacoes.css")%>">

<div style="width: 275px;">
    <asp:Repeater ID="rptAcao" runat="server">
        <HeaderTemplate>
            <table cellpadding="0" cellspacing="0" class="tableDefault">
                <colgroup>
                    <col style="width: 95px;" />
                    <col style="width: 45px;" />
                    <col style="width: 45px;" />
                    <col style="width: 37px;" />
                </colgroup>
                <thead>
                    <tr>
                        <th class="colLft" colspan="4">
                            <asp:Literal ID="ltrCotacoes" runat="server" Text='<%# Resources.Cotacoes.Moeda %>'></asp:Literal><br />
                        </th>
                    </tr>
                </thead>
                <tbody>
        </HeaderTemplate>
        <ItemTemplate>
            <tr>
                <td>
                    <label title='<%# Eval("Descricao") %>'><%# Eval("Papel") %></label>
                </td>
                <td>
                    <label><%# Convert.ToDouble(Eval("UltimaCotacao")).ToString("###,##0.00") %></label>
                </td>
                <td>
                    <label class='<%# Eval("Percentual").ToString().Contains("-") ? "labelDown":"labelUp" %>'>
                        <%# Eval("Percentual") %>
                    </label>
                </td>
                <td>
                    <label><%# Convert.ToDateTime(Eval("Data")).ToString("g") %></label>
                </td>
            </tr>
        </ItemTemplate>
        <AlternatingItemTemplate>
            <tr class="even">
                <td>
                    <label title='<%# Eval("Descricao") %>'><%# Eval("Papel") %></label>
                </td>
                <td>
                    <label><%# Convert.ToDouble(Eval("UltimaCotacao")).ToString("###,##0.00") %></label>
                </td>
                <td>
                    <label class='<%# Eval("Percentual").ToString().Contains("-") ? "labelDown":"labelUp" %>'>
                        <%# Eval("Percentual") %>
                    </label>
                </td>
                <td>
                    <label><%# Convert.ToDateTime(Eval("Data")).ToString("g") %></label>
                </td>
            </tr>
        </AlternatingItemTemplate>
        <FooterTemplate>
            </tbody> </table>
        </FooterTemplate>
    </asp:Repeater>
</div>
