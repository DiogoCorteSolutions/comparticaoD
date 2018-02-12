<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Timeline.ascx.cs" Inherits="Modulos_CMS_Modulos_ModTimeline_Timeline" %>

<%--<link rel="stylesheet" href="<%=ResolveUrl("~/CSS/ModTimeline.css")%>">--%>

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
                    <%--          <tr>
                        <th class="colLft" colspan="4">
                            <asp:Literal ID="ltrCotacoes" runat="server" Text='<%# Resources.Cotacoes.Moeda %>'></asp:Literal><br />
                        </th>
                    </tr>--%>
                </thead>
                <tbody>
        </HeaderTemplate>
        <ItemTemplate>
            <tr>
                <td>
                    <%--<img id="Image2" src='<%# Eval("imagem")%>' height="150" width="150" />--%>
                    <%--<label id="ano-time1" title='<%# Eval("Titulo") %>'><%# Eval("Titulo") %></label>--%>
                    <%-- <button type="button" data-id="<%# Eval("Timelineid") %>" id="titulo-time1" class="bnImgVisivel" title='<%# Eval("Ano") %>'><%# Eval("Ano") %></button>
                    <div class="imgVisivel-<%# Eval("Timelineid") %> divImgOpenClose" style="display: none;">
                        <img id="Image2" src='<%# Eval("imagem")%>' height="150" width="150" />
                        <label id="ano-time1" title='<%# Eval("Titulo") %>'><%# Eval("Titulo") %></label>
                    </div>--%>
                </td>
                <%--   <td>
                    <label id="text-time1" title='<%# Eval("texto") %>'><%# Eval("texto") %></label>
                </td>--%>
            </tr>
        </ItemTemplate>
        <AlternatingItemTemplate>
            <tr class="even">
                <%--<img id="Image2" src='<%# Eval("imagem")%>' height="150" width="150" />--%>

                <label id="ano-time1" title='<%# Eval("Titulo") %>'><%# Eval("Titulo") %></label>
                   <button type="button" data-id="<%# Eval("Timelineid") %>" id="titulo-time1" class="bnImgVisivel" title='<%# Eval("Ano") %>'><%# Eval("Ano") %></button>
                <label id="ano-time2" title='<%# Eval("Titulo") %>'><%# Eval("Titulo") %></label>
                <%--     <button type="button" data-id="<%# Eval("Timelineid") %>" id="titulo-time1" class="bnImgVisivel" title='<%# Eval("Ano") %>'><%# Eval("Ano") %></button>
                <div class="imgVisivel-<%# Eval("Timelineid") %> divImgOpenClose" style="display: none;">
                    <img id="Image2" src='<%# Eval("imagem")%>' height="150" width="150" />
                    <label id="ano-time1" title='<%# Eval("Titulo") %>'><%# Eval("Titulo") %></label>
                </div>--%>
                <%--       <td>
                    <label id="texto-time2" title='<%# Eval("texto") %>'><%# Eval("texto") %></label>
                </td>--%>
            </tr>
        </AlternatingItemTemplate>
        <FooterTemplate>
            </tbody> </table>
        </FooterTemplate>
    </asp:Repeater>
</div>



<script src="../../../../JS/jquery-1.10.2.min.js"></script>
<script src="../../../../JS/timeline/timelinejs.js"></script>


<%--<script>


$(document).ready(function () {
        var retorno = $('#titulo-time1').val();
        var retorno2 = $("#titulo-time2").val();
        console.log(retorno);
        console.log(retorno2);

    });

</script>--%>
