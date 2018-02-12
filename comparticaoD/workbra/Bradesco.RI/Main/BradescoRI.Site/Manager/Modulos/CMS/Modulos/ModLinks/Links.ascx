<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Links.ascx.cs" Inherits="Modulos_CMS_Modulos_ModLinks_Links" %>

<div id="divSemConteudo" runat="server" class="moduloSemConteudo">
    <asp:Label ID="lblTitulo" runat="server"><%= string.Format(Resources.Textos.Modulo_Sem_Conteudo, "Links Relacionados") %></asp:Label>
</div>

<div id="divConteudo" runat="server">

    <asp:Repeater runat="server" ID="rptLinks">
        <HeaderTemplate>
            <div class="container-fluid">
            <!-- Row #1 - Estratégia de atuação - menu vermelho  -->
                <div class="row menu-verm">
                    <h3>Links relacionados</h3>
                    <ul class="nav nav-pills nav-justified">
            </HeaderTemplate>
            <ItemTemplate>
                <li class="active"><a href="<%# DataBinder.Eval(Container.DataItem, "Url")%>" target="<%# DataBinder.Eval(Container.DataItem, "Target")%>"><%# DataBinder.Eval(Container.DataItem, "Titulo")%></a></li>
            </ItemTemplate>
            <FooterTemplate>
                    </ul>
                </div>
            </div>
        </FooterTemplate>
    </asp:Repeater>


</div>
