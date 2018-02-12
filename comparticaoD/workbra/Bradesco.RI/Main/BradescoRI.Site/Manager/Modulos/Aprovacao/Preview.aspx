<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Preview.aspx.cs" Inherits="Modulos_Aprovacao_Preview" EnableViewState="false" %>

<%@ Register Src="~/Modulos/CMS/Paginas/Header.ascx" TagPrefix="uc1" TagName="Header" %>
<%@ Register Src="~/Modulos/CMS/Paginas/Footer.ascx" TagPrefix="uc1" TagName="Footer" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="X-UA-Compatible" content="IE=Edge" />
    <meta charset="utf-8" />
    <title>Bradesco | RI</title>
    <meta name="viewport" content="width=device-width, initial-scale=1.0" />
    <meta name="format-detection" content="telephone=no" />
    <meta name="keywords" content="%%METATAGSKEYWORDS%%" />
    <meta name="description" content="%%METATAGSDESCRIPTION%%" />

    <link rel="shortcut icon" type="image/x-icon" href="<%=ResolveUrl("~/Imagens/favicon.ico")%>" />
    <link href="<%=ResolveUrl("~/CSS/main.css")%>" rel="stylesheet" />
    <!-- Bootstrap Core CSS -->
    <link href="<%=ResolveUrl("~/CSS/bootstrap.min.css")%>" rel="stylesheet" />
    <link href="<%=ResolveUrl("~/CSS/bootstrap-theme.min.css")%>" rel="stylesheet" />
    <!-- Custom Fonts -->
    <link href="<%=ResolveUrl("~/vendor/font-awesome/css/font-awesome.min.css")%>" rel="stylesheet" type="text/css" />
    <link href='https://fonts.googleapis.com/css?family=Lora:400,700,400italic,700italic' rel='stylesheet' type='text/css' />
    <link href='https://fonts.googleapis.com/css?family=Open+Sans:300italic,400italic,600italic,700italic,800italic,400,300,600,700,800' rel='stylesheet' type='text/css' />
    <!-- Custom Menu circular -->
    <link href="<%=ResolveUrl("~/CSS/horizon-swiper.min.css")%>" rel="stylesheet" />
    <link href="<%=ResolveUrl("~/CSS/horizon-theme.min.css")%>" rel="stylesheet" />
    <!-- Bradesco RI CSS -->
    <link rel="stylesheet" type="text/css" href="<%=ResolveUrl("~/CSS/plug.css")%>" media="all" />
    <link href="<%=ResolveUrl("~/CSS/clean-blog.min.css")%>" rel="stylesheet" />
    <link href="<%=ResolveUrl("~/CSS/Style_Bri.css")%>" rel="stylesheet" />

    <link rel="stylesheet" href="<%=ResolveUrl("~/CSS/style.css")%>" />
    <script src="<%=ResolveUrl("~/JS/template.js")%>"></script>
    <%--<script src="<%=ResolveUrl("~/JS/jquery-1.10.2.min.js")%>"></script>--%>
    <script src="<%=ResolveUrl("~/JS/jquery.js")%>"></script>

    <script src="<%=ResolveUrl("~/JS/timeline.js")%>"></script>
    <script src="<%=ResolveUrl("~/JS/timeline/timelinejs.js")%>"></script>
    <script src="<%=ResolveUrl("~/JS/tabela.js")%>"></script>
    <script src="<%=ResolveUrl("~/JS/modernizr.js")%>"></script>
    <script src="<%=ResolveUrl("~/JS/audioplayer.js")%>"></script>
    <script type="text/javascript">
        jQuery(function ($) {
            // init the state from the input
            $(".image-checkbox").each(function () {
                if ($(this).find('input[type="checkbox"]').first().attr("checked")) {
                    $(this).addClass('image-checkbox-checked');
                }
                else {
                    $(this).removeClass('image-checkbox-checked');
                }
            });

            // sync the state to the input
            $(".image-checkbox").on("click", function (e) {
                if ($(this).hasClass('image-checkbox-checked')) {
                    $(this).removeClass('image-checkbox-checked');
                    $(this).find('input[type="checkbox"]').first().removeAttr("checked");
                }
                else {
                    $(this).addClass('image-checkbox-checked');
                    $(this).find('input[type="checkbox"]').first().attr("checked", "checked");
                }

                e.preventDefault();
            });
        });

        function downsel(num, numcheck) {
            if (document.getElementById(numcheck).checked == "0") {
                document.getElementById(num).style.backgroundColor = "#FFFFFF";
            }
            else {
                document.getElementById(num).style.backgroundColor = "#F7F7F7";
            }
        }
    </script>


    <script type="text/javascript">
        function openModalMensagem() {
            $("#divModalMensagem").show();
            $("#black_overlay").show();
        }

        function openModalReprovarPagina() {
            $("#divModalReprovaPagina").show();
            $("#black_overlay").show();
        }

        function closeModal() {
            $(".ModalMsg").hide();
            $("#black_overlay").hide();
        }

        function refreshParent() {
            window.close();
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">

        <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
        <!-- ------- HEADER ------- -->
        <uc1:Header runat="server" ID="Header" />
        <header class="intro-header">
            <asp:PlaceHolder ID="placeHolderHeader" runat="server"></asp:PlaceHolder>
        </header>

        <!-- ------- HEADER ------- -->
        <!-- Main Content -->
        <div class="container-fluid container">

            <div>
                <asp:HiddenField ID="hidpaginaId" runat="server" />
                <asp:PlaceHolder ID="placeHolderTemplate" runat="server"></asp:PlaceHolder>
            </div>
        </div>

        <!-- ------- FOOTER ------- -->
        <uc1:Footer runat="server" ID="Footer" />
        <!-- ------- FOOTER ------- -->

        <!--COMANDOS_TELA -->
        <asp:Button ID="btnPublicar" runat="server" OnClick="btnPublicar_Click" />
        <asp:Button ID="btnReprovar" runat="server" OnClick="btnReprovar_Click" />

        <div id="divModalMensagem" class="ModalMsg" tabindex="Mensagem">
            <a href="javascript:refreshParent();" onclick="closeModal();" class="cms-fechar-popup" alt="Fechar" title="Fechar" runat="server" id="btnFecharTela">Fechar (X)</a>
            <asp:Label ID="lblMensagem" runat="server"></asp:Label>

            <div id="divMotivo" runat="server" visible="false">
                <asp:Label ID="lblMotivo" Text="Motivo" runat="server"></asp:Label>
                <asp:TextBox ID="txtMotivo" runat="server" TextMode="MultiLine"></asp:TextBox><asp:RequiredFieldValidator ID="reqMotivo" runat="server" Text="*" ControlToValidate="txtMotivo"></asp:RequiredFieldValidator>
                <asp:Button ID="btnReprovarMotivo" runat="server" OnClick="btnReprovarMotivo_Click" Style="height: 26px" />
                <a href="javascript:void(0);" onclick="closeModal();" class="cms-fechar-popup" alt="Fechar" title="Fechar" runat="server" id="btnFecharDiv">Fechar (X)</a>
            </div>
        </div>

        <div id="black_overlay"></div>
        <!--/COMANDOS_TELA -->
    </form>

    <!-- jQuery Bradesco RI-->
    <script src="<%=ResolveUrl("~/vendor/jquery/jquery.min.js")%>"></script>
    <%--<script src="<%=ResolveUrl("~/Scripts/jquery.min.js")%>"></script>--%>
    <!-- Bootstrap Core JavaScript -->
    <script src="<%=ResolveUrl("~/Scripts/bootstrap.min.js")%>"></script>
    <!-- Tema base JavaScript -->
    <script src="<%=ResolveUrl("~/Scripts/clean-blog.min.js")%>"></script>
    <script src="<%=ResolveUrl("~/Scripts/horizon-swiper.js")%>"></script>
    <script>$('.horizon-swiper').horizonSwiper();</script>
    <script>$(document).ready(function () {

    $(window).scroll(function () {
        if ($(this).scrollTop() > 100) {
            $('.scroll-top').fadeIn();
        } else {
            $('.scroll-top').fadeOut();
        }
    });

    $('.scroll-top').click(function () {
        $("html, body").animate({
            scrollTop: 0
        }, 100);
        return false;
    });

});</script>
</body>
</html>
