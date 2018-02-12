<%@ Control Language="C#" AutoEventWireup="true" CodeFile="template2Abas.ascx.cs" Inherits="Modulos_CMS_Paginas_Templates_template2Abas" %>

<!-- Linha de Abas (tabs) com conteúdo -->
<div class="row">
        <!-- Nav tabs -->
        <ul class="nav nav-tabs mod-abas" role="tablist">
            <li role="presentation" class="active"><a href="#Aba1" aria-controls="Aba1" role="tab" data-toggle="tab" runat="server" id="controle1"></a></li>
            <li role="presentation"><a href="#Aba2" aria-controls="Aba2" role="tab" data-toggle="tab" runat="server" id="controle2"></a></li>
        </ul>

        <!-- Tab panes -->
        <div class="tab-content">
            <div role="tabpanel" class="tab-pane fade in active" id="Aba1">
                <asp:PlaceHolder ID="placeHolder_1" runat="server"></asp:PlaceHolder>
            </div>
            <div role="tabpanel" class="tab-pane fade" id="Aba2">
                <asp:PlaceHolder ID="placeHolder_2" runat="server"></asp:PlaceHolder>
            </div>
        </div>
</div>

<asp:PlaceHolder ID="placeHolder_0" runat="server"></asp:PlaceHolder>
