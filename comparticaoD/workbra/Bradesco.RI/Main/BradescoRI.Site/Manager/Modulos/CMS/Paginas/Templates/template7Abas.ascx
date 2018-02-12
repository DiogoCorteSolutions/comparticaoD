<%@ Control Language="C#" AutoEventWireup="true" CodeFile="template7Abas.ascx.cs" Inherits="Modulos_CMS_Paginas_Templates_template7Abas" %>
<!-- Linha de Abas (tabs) com conteúdo -->
<div class="row">
    <div class="col-lg-12 col-md-12">
        <!-- Nav tabs -->
        <ul class="nav nav-tabs mod-abas" role="tablist">
            <li role="presentation" class="active"><a href="#Aba1" aria-controls="Aba1" role="tab" data-toggle="tab" runat="server" id="controle1"></a></li>
            <li role="presentation"><a href="#Aba2" aria-controls="Aba2" role="tab" data-toggle="tab" runat="server" id="controle2"></a></li>
            <li role="presentation"><a href="#Aba3" aria-controls="Aba3" role="tab" data-toggle="tab" runat="server" id="controle3"></a></li>
            <li role="presentation"><a href="#Aba4" aria-controls="Aba4" role="tab" data-toggle="tab" runat="server" id="controle4"></a></li>
            <li role="presentation"><a href="#Aba5" aria-controls="Aba5" role="tab" data-toggle="tab" runat="server" id="controle5"></a></li>
            <li role="presentation"><a href="#Aba6" aria-controls="Aba6" role="tab" data-toggle="tab" runat="server" id="controle6"></a></li>
            <li role="presentation"><a href="#Aba7" aria-controls="Aba7" role="tab" data-toggle="tab" runat="server" id="controle7"></a></li>
            
        </ul>

        <!-- Tab panes -->
        <div class="col-lg-12 col-md-12 tab-content">
            <div role="tabpanel" class="tab-pane fade in active" id="Aba1">
                <asp:PlaceHolder ID="placeHolder_1" runat="server"></asp:PlaceHolder>
            </div>
            <div role="tabpanel" class="tab-pane fade" id="Aba2">
                <asp:PlaceHolder ID="placeHolder_2" runat="server"></asp:PlaceHolder>
            </div>
            <div role="tabpanel" class="tab-pane fade" id="Aba3">
                <asp:PlaceHolder ID="placeHolder_3" runat="server"></asp:PlaceHolder>
            </div>
            <div role="tabpanel" class="tab-pane fade" id="Aba4">
                <asp:PlaceHolder ID="placeHolder_4" runat="server"></asp:PlaceHolder>
            </div>
            <div role="tabpanel" class="tab-pane fade" id="Aba5">
                <asp:PlaceHolder ID="placeHolder_5" runat="server"></asp:PlaceHolder>
            </div>
            <div role="tabpanel" class="tab-pane fade" id="Aba6">
                <asp:PlaceHolder ID="placeHolder_6" runat="server"></asp:PlaceHolder>
            </div>
            <div role="tabpanel" class="tab-pane fade" id="Aba7">
                <asp:PlaceHolder ID="placeHolder_7" runat="server"></asp:PlaceHolder>
            </div>         
        </div>

    </div>
</div>

<asp:PlaceHolder ID="placeHolder_0" runat="server"></asp:PlaceHolder>