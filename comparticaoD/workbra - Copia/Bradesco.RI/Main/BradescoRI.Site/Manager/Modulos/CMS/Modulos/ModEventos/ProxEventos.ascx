<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ProxEventos.ascx.cs" Inherits="Modulos_CMS_Modulos_ModEventos_ProxEventos" %>


<div id="divConteudo" runat="server">

    <asp:Repeater runat="server" ID="rptProximosEventos" OnItemDataBound="rptProximosEventos_ItemDataBound">
        <HeaderTemplate>
            <!-- Row #5 - Próximos Eventos  -->
            <div class="row">
                <div class="col-lg-12 col-lg-offset-0 col-md-12 col-md-offset-0 prox-even-home">
                    <h2 class="section-heading heading_h2">PRÓXIMOS EVENTOS</h2>
                    <hr class="small2">
        </HeaderTemplate>
        <ItemTemplate>
            <div class="col-md-4 evento-bri">
                <p class="dias">
                    <asp:Label ID="lblData" runat="server"></asp:Label></p>
                <h3><%# DataBinder.Eval(Container.DataItem, "Titulo")%></h3>
                <p><%# DataBinder.Eval(Container.DataItem, "Descricao")%></p>
                <p class="pEvent">
                    <a class="btn btn-default link-vermelho" href='<%# String.Format("{0}?Id={1}", Eval("UrlListaEvento"), Eval("IdEvento"))%>'>
                        <span class="lplay-icon_red"></span>SAIBA MAIS</a>
                </p>
            </div>
        </ItemTemplate>
        <FooterTemplate>
            </div>
                <div class="col-lg-12 col-lg-offset-0 col-md-12 col-md-offset-0 even-footer">
                    <a class="btn btn-default vertodas" href='<%# String.Format("{0}", Eval("UrlTodosEventos"))%>'>
                        <span class="lplay-icon_red"></span>VER TODOS EVENTOS</a>
                </div>
            </div>
            <!-- Fim de Row #5 - Proximos Eventos -->
        </FooterTemplate>
    </asp:Repeater>
</div>
