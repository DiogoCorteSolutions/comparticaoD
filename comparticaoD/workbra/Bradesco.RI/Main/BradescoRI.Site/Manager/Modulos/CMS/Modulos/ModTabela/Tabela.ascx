<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Tabela.ascx.cs" Inherits="Modulos_CMS_Modulos_ModTabela_Tabela" %>

<link rel="stylesheet" href="<%=ResolveUrl("~/CSS/ModCotacoes.css")%>">

<%--<div style="width: 275px;">--%>
<asp:Repeater ID="rptTabela" runat="server">
    <HeaderTemplate>
        <div class="tab-linha">
            <table class="tab-tb">
                <table cellpadding="0" cellspacing="0" class="tableDefault">
                    <colgroup>
                        <col style="width: 95px;" />
                        <col style="width: 45px;" />
                        <col style="width: 45px;" />
                        <col style="width: 37px;" />
                    </colgroup>
              <%--      <thead>
                        <tr>
                            <th class="colLft" colspan="4">
                                <asp:Literal ID="ltrCotacoes" runat="server" Text='<%# Resources.Cotacoes.Moeda %>'></asp:Literal><br />
                            </th>
                        </tr>
                    </thead>--%>
                    <tbody>
    </HeaderTemplate>
    <ItemTemplate>
        <tr>
            <td>
                <label title='<%# Eval("NomeColuna") %>'><%# Eval("NomeColuna") %></br><%# Eval("ValorColuna") %><%# Eval("ValorColuna") %> </br> </label>
            </td>
        </tr>
    </ItemTemplate>
    <AlternatingItemTemplate>
        <tr class="even">
            <td>
                <label title='<%# Eval("NomeColuna") %>'><%# Eval("NomeColuna") %></br><%# Eval("ValorColuna") %><%# Eval("ValorColuna") %> </br> </label>
            </td>
        </tr>
                <%--<tr class="even">
                     <td>
                    <label title='<%# Eval("NomeColuna") %>'><%# Eval("NomeColuna") %></label>
                    <br />
                    <label title='<%# Eval("ValorColuna") %>'><%# Eval("ValorColuna") %></label>
                </td>
                   <td>
                    <label title='<%# Eval("NomeColuna") %>'><%# Eval("NomeColuna") %></label>
                </td>
                <td>
                    <label title='<%# Eval("ValorColuna") %>'><%# Eval("ValorColuna") %></label>
                </td>
            </tr>--%>
    </AlternatingItemTemplate>
    <FooterTemplate>
        </tbody> </table>
    </FooterTemplate>
</asp:Repeater>
<%--</div>--%>
























<%--<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Tabela.ascx.cs" Inherits="Modulos_CMS_Modulos_ModTabela_Tabela" %>

<link rel="stylesheet" href="<%=ResolveUrl("~/CSS/ModCotacoes.css")%>">

<div style="width: 275px;">
<asp:Repeater ID="rptTabela" runat="server">
    <HeaderTemplate>
        <div class="tab-linha">
            <table class="tab-tb">
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
                <label title='<%# Eval("NomeColuna") %>'><%# Eval("NomeColuna") %></label>
            </td>
            <td>
                <label title='<%# Eval("ValorColuna") %>'><%# Eval("ValorColuna") %></label>
            </td>
        </tr>
    </ItemTemplate>
    <AlternatingItemTemplate>
        <tr class="even">
            <td>
                <label title='<%# Eval("NomeColuna") %>'><%# Eval("NomeColuna") %></label>
            </td>
            <td>
                <label title='<%# Eval("ValorColuna") %>'><%# Eval("ValorColuna") %></label>
            </td>
        </tr>
    </AlternatingItemTemplate>
    <FooterTemplate>
        </tbody> 
            </table>
            </div>
    </FooterTemplate>
</asp:Repeater>
</div>--%>
