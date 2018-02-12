<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ModImagemUnica.ascx.cs" Inherits="Modulos_CMS_Modulos_ModImagemUnica_ModImagemUnica" %>

<div id="divSemConteudo" runat="server" class="moduloSemConteudo">
    <asp:Label ID="lblTitulo" runat="server"><%= string.Format(Resources.Textos.Modulo_Sem_Conteudo, "Imagem Única") %></asp:Label>
</div>

<div id="divConteudo" runat="server">

    <div class="row" >
        <div class="col-md-12 calltoaction-home">
            <asp:Image ID="imgImagemModulo" runat="server" />
            <div class="CTA-texto">
                <p class="pCSubtt">
                    <asp:Label ID="lblTexto1" runat="server"></asp:Label>
                </p>
                <h2 class="section-heading heading_h2_CA">
                    <asp:Label ID="lblTexto2" runat="server"></asp:Label>

                </h2>
                <p class="pCAction">
                    <asp:Label ID="lblTexto3" runat="server"></asp:Label>

                </p>
                <p class="pCAction">
                    <a id="linkImagem" runat="server" class="link-play" visible="false">
                        <span class="glyphicon glyphicon-play-circle"></span>
                        <asp:Label ID="lblTextoUrl" runat="server"></asp:Label></a>
                </p>
                <div id="divTraco" runat="server" visible="false">
                    <hr class="vertical">
                </div>
                
            </div>
            <%--fim de CTA-texto--%>
        </div>
        <%--fim de Call to action--%>
    </div>
    <%--fim de Row--%>
</div>
