<%@ Control Language="C#" AutoEventWireup="true" CodeFile="IndicadoresFinanceiros.ascx.cs" Inherits="Modulos_CMS_Modulos_ModIndicadoresFinanceiros_IndicadoresFinanceiros" %>

<link rel="stylesheet" href="<%=ResolveUrl("~/CSS/ModCotacoes.css")%>">

<div style="width:275px;">
    <asp:Repeater ID="rptAcao" runat="server">
        <HeaderTemplate>
            <table cellpadding="0" cellspacing="0" class="tableDefault">
                <colgroup>
                    <col  style="width:96px;" />
                    <col  style="width:83px;" />
                    <col  style="width:73px;" />
                </colgroup>
                <thead>
                    <tr>
                        <th class="colLft" colspan="4">
                            <asp:Literal ID="Literal1" runat="server"></asp:Literal><br />
                            <asp:Literal ID="ltrCotacoes" runat="server" Text='<%# Resources.Cotacoes.IndicadoresFinanceiros %>'></asp:Literal><br />
                        </th>
                    </tr>
                </thead>
                <tbody>
        </HeaderTemplate>
        <ItemTemplate>
            <tr>
                <td>
                    <label title='<%# Eval("Papel") %>'><%# Eval("Descricao") %></label>
                </td>
                <td>
                    <label><%# Convert.ToDouble(Eval("UltimaCotacao")).ToString("###,##0.00") %></label>
                </td>
                <td>
                    <label><%# Convert.ToDateTime(Eval("Data")).ToString("g") %></label>
                </td>
            </tr>
        </ItemTemplate>
        <AlternatingItemTemplate>
            <tr class="even">
                <td>
                    <label title='<%# Eval("Papel") %>'><%# Eval("Descricao") %></label>

                </td>
                <td>
                    <label><%# Convert.ToDouble(Eval("UltimaCotacao")).ToString("###,##0.00") %></label>
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
