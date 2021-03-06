﻿

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head><meta http-equiv="X-UA-Compatible" content="IE=Edge" /><meta charset="utf-8" /><title>
	Bradesco | RI
</title><meta name="viewport" content="width=device-width, initial-scale=1.0" /><meta name="format-detection" content="telephone=no" /><meta name="keywords" content="Meta KeyWords" /><meta name="description" content="Meta Description" />

    <link rel="shortcut icon" type="image/x-icon" href="/Manager/Imagens/favicon.ico" />
    <link href="/Manager/CSS/main.css" rel="stylesheet" />
    <!-- Bootstrap Core CSS -->
    <link href="/Manager/CSS/bootstrap.min.css" rel="stylesheet" />
    <link href="/Manager/CSS/bootstrap-theme.min.css" rel="stylesheet" />
    <!-- Custom Fonts -->
    <link href="/Manager/vendor/font-awesome/css/font-awesome.min.css" rel="stylesheet" type="text/css" />
    <link href="https://fonts.googleapis.com/css?family=Lora:400,700,400italic,700italic" rel="stylesheet" type="text/css" /><link href="https://fonts.googleapis.com/css?family=Open+Sans:300italic,400italic,600italic,700italic,800italic,400,300,600,700,800" rel="stylesheet" type="text/css" />
    <!-- Custom Menu circular -->
    <link href="/Manager/CSS/horizon-swiper.min.css" rel="stylesheet" />
    <link href="/Manager/CSS/horizon-theme.min.css" rel="stylesheet" />
    <!-- Bradesco RI CSS -->
    <link rel="stylesheet" type="text/css" href="/Manager/CSS/plug.css" media="all" />
    <link href="/Manager/CSS/clean-blog.min.css" rel="stylesheet" />
    <link href="/Manager/CSS/Style_Bri.css" rel="stylesheet" />

    <link rel="stylesheet" href="/Manager/CSS/style.css" />
    <script src="/Manager/JS/template.js"></script>
    
    <script src="/Manager/JS/jquery.js"></script>

    <script src="/Manager/JS/timeline.js"></script>
    <script src="/Manager/JS/modernizr.js"></script>
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
    </script>
</head>
<body>
    <form method="post" action="./Preview.aspx?paginaId=fnGFfOnY8NA%3d&amp;Aprovar=WppcAZkVPMU%3d" id="form1">
<div class="aspNetHidden">
<input type="hidden" name="__VIEWSTATE" id="__VIEWSTATE" value="/wEPDwUJNTEzNjk1MjQ1D2QWAgIBD2QWDAIDD2QWAmYPZBYCZg9kFgRmDxYCHgdWaXNpYmxlaGQCAg9kFgoCAQ8WAh4EVGV4dAX+ATxkaXYgaWQ9InhfMDA2X2ltZ190aHVtYm5haWwiIGNsYXNzPSJjYXJvdXNlbCBzbGlkZSBwc19zbGlkZV95IHhfMDA2X2ltZ190aHVtYm5haWxfaW5kaWNhdG9ycyB4XzAwNl9pbWdfdGh1bWJuYWlsX2NvbnRyb2xfYnV0dG9uIHRodW1iX3Njcm9sbF95IHN3aXBlX3kgcHNfZWFzZU91dEluQ3ViaWMiIGRhdGEtcmlkZT0iY2Fyb3VzZWwiIGRhdGEtcGF1c2U9ImhvdmVyIiAgZGF0YS1pbnRlcnZhbD0iODAwMCIgZGF0YS1kdXJhdGlvbj0iMjAwMCI+ZAIDDxYCHwEFmgM8bGkgZGF0YS10YXJnZXQ9IiN4XzAwNl9pbWdfdGh1bWJuYWlsIiBkYXRhLXNsaWRlLXRvPSIwIiBjbGFzcz0iYWN0aXZlIiBzdHlsZT0iIj4gICA8aW1nIHNyYz0iL01hbmFnZXIvVXBsb2Fkcy9JbWFnZW5zL0Jhbm5lcnMvMTIvMTJfNV8xX2ltZy1pbnRyby1oZWFkZXIuanBnIiBhbHQ9ImJvb3RzdHJhcCBjYXJvdXNlbCB3aXRoIHRodW1ibmFpbCBuYXZpZ2F0aW9uIj48L2xpPjxsaSBkYXRhLXRhcmdldD0iI3hfMDA2X2ltZ190aHVtYm5haWwiIGRhdGEtc2xpZGUtdG89IjEiIGNsYXNzPSIiIHN0eWxlPSIiPiAgIDxpbWcgc3JjPSIvTWFuYWdlci9VcGxvYWRzL0ltYWdlbnMvQmFubmVycy8xMy8xM181XzFfaW1nLWludHJvLWhlYWRlcl9henVsLnBuZyIgYWx0PSJ4XzAwNl9pbWdfdGh1bWJuYWlsXzAxX3NtIj48L2xpPmQCBQ8WAh8BBcYJPGRpdiBjbGFzcz0iaXRlbSBhY3RpdmUiIHN0eWxlPSJ0cmFuc2l0aW9uLWR1cmF0aW9uOiAyMDAwbXM7Ij4gICA8aW1nIHNyYz0iL01hbmFnZXIvVXBsb2Fkcy9JbWFnZW5zL0Jhbm5lcnMvMTIvMTJfNV8xX2ltZy1pbnRyby1oZWFkZXIuanBnIiBhbHQ9InhfMDA2X2ltZ190aHVtYm5haWxfMDAiPiAgIDxkaXYgY2xhc3M9InhfMDA2X2ltZ190aHVtYm5haWxfdGV4dCB4XzAwNl9pbWdfdGh1bWJuYWlsX3RleHRfY2VudGVyIj4gICAgICAgPGgxIGRhdGEtYW5pbWF0aW9uPSJhbmltYXRlZCBmYWRlSW5VcCIgY2xhc3M9InBhZ2UtaGVhZGluZyI+SU5WRVNUSU1FTlRPIFNVU1RFTlTDgVZFTDwvaDE+PGhyIGNsYXNzPSJzbWFsbCI+ICAgICAgIDxwIGRhdGEtYW5pbWF0aW9uPSJhbmltYXRlZCBmYWRlSW5VcCIgY2xhc3M9InN1YmhlYWRpbmciPlVtIGJvbSBpbnZlc3RpbWVudG8gY29tZcOnYSBjb20gYSBjb25maWFuw6dhPC9wPiAgICAgICA8YSBocmVmPSIjIiBkYXRhLWFuaW1hdGlvbj0iYW5pbWF0ZWQgZmFkZUluVXAiIGNsYXNzPSJsaW5rLXBsYXkiPjxzcGFuIGNsYXNzPSJscGxheS1pY29uX3doaXRlIj48L3NwYW4+U2FpYmEgbWFpczwvYT4gICA8L2Rpdj48L2Rpdj48ZGl2IGNsYXNzPSJpdGVtICIgc3R5bGU9InRyYW5zaXRpb24tZHVyYXRpb246IDIwMDBtczsiPiAgIDxpbWcgc3JjPSIvTWFuYWdlci9VcGxvYWRzL0ltYWdlbnMvQmFubmVycy8xMy8xM181XzFfaW1nLWludHJvLWhlYWRlcl9henVsLnBuZyIgYWx0PSJ4XzAwNl9pbWdfdGh1bWJuYWlsXzAxIj4gICA8ZGl2IGNsYXNzPSJ4XzAwNl9pbWdfdGh1bWJuYWlsX3RleHQgeF8wMDZfaW1nX3RodW1ibmFpbF90ZXh0X2NlbnRlciI+ICAgICAgIDxoMSBkYXRhLWFuaW1hdGlvbj0iYW5pbWF0ZWQgZmFkZUluVXAiIGNsYXNzPSJwYWdlLWhlYWRpbmciPlJFTEHDh8OVRVMgQ09NIElOVkVTVElET1JFUzwvaDE+PGhyIGNsYXNzPSJzbWFsbCI+ICAgICAgIDxwIGRhdGEtYW5pbWF0aW9uPSJhbmltYXRlZCBmYWRlSW5VcCIgY2xhc3M9InN1YmhlYWRpbmciPlVtIGJvbSBpbnZlc3RpbWVudG8gY29tZcOnYSBjb20gZGFkb3MgYXR1YWxpemFkb3M8L3A+ICAgICAgIDxhIGhyZWY9IiMiIGRhdGEtYW5pbWF0aW9uPSJhbmltYXRlZCBmYWRlSW5VcCIgY2xhc3M9ImxpbmstcGxheSI+PHNwYW4gY2xhc3M9ImxwbGF5LWljb25fd2hpdGUiPjwvc3Bhbj5TYWliYSBtYWlzPC9hPiAgIDwvZGl2PjwvZGl2PmQCBw8WAh8BBY0CPGEgY2xhc3M9ImxlZnQgY2Fyb3VzZWwtY29udHJvbCIgaHJlZj0iI3hfMDA2X2ltZ190aHVtYm5haWwiIHJvbGU9ImJ1dHRvbiIgZGF0YS1zbGlkZT0icHJldiI+PHNwYW4gY2xhc3M9ImZhIGZhLWFuZ2xlLXVwIj48L3NwYW4+PC9hPjxhIGNsYXNzPSJyaWdodCBjYXJvdXNlbC1jb250cm9sIiBocmVmPSIjeF8wMDZfaW1nX3RodW1ibmFpbCIgcm9sZT0iYnV0dG9uIiBkYXRhLXNsaWRlPSJuZXh0Ij48c3BhbiBjbGFzcz0iZmEgZmEtYW5nbGUtZG93biI+PC9zcGFuPjwvYT5kAgkPFgIfAQUGPC9kaXY+ZAIHD2QWAmYPZBYCZg9kFgpmD2QWAmYPZBYEZg8WAh8AaGQCAg9kFgQCAQ8WAh8BBZsBPHVsPjxsaT48YSBocmVmPScjJz5EZW1vbnN0cmFjw6NvIEZpbmFuY2VpcmFzPC9hPjwvbGk+PGxpPjxhIGhyZWY9JyMnPlJlbGF0w7NyaW8gQW51YWw8L2E+PC9saT48bGk+PGEgaHJlZj0nIyc+UmVsYXTDs3JpbyBkZSBTdXN0ZW50YWJpbGlkYWRlPC9hPjwvbGk+PC91bD5kAgMPFgIfAQWGATx1bD48bGk+PGEgaHJlZj0nIyc+Q29tdW5pY2FkbzwvYT48L2xpPjxsaT48YSBocmVmPScjJz5Ob2ZvIEZhdG8gUmVsZXZhbnRlPC9hPjwvbGk+PGxpPjxhIGhyZWY9JyMnPlRlc3RlIGRhIGRpdnVsZ2HDp8OjbzwvYT48L2xpPjwvdWw+ZAIBD2QWAmYPZBYEZg8WAh8AaGQCAg9kFgICAQ8WAh4LXyFJdGVtQ291bnQCBRYKAgEPZBYGZg8VASBqYXZhc2NyaXB0OndpbmRvdy5vcGVuKCcjJywgJycpO2QCAQ8PFgIeCEltYWdlVXJsBUMvTWFuYWdlci9VcGxvYWRzL0ltYWdlbnMvTWVudUNpcmN1bGFySG9tZS81OTcvNTk3XzFfaWNvLXJlbC1pbnQucG5nZGQCAg8VARRSRUxBVMOTUklPIElOVEVHUkFET2QCAg9kFgZmDxUBIGphdmFzY3JpcHQ6d2luZG93Lm9wZW4oJyMnLCAnJyk7ZAIBDw8WAh8DBUMvTWFuYWdlci9VcGxvYWRzL0ltYWdlbnMvTWVudUNpcmN1bGFySG9tZS81OTcvNTk3XzFfaWNvLXN1cy1jb3IucG5nZGQCAg8VARxTVVNURU5UQUJJTElEQURFIENPUlBPUkFUSVZBZAIDD2QWBmYPFQEgamF2YXNjcmlwdDp3aW5kb3cub3BlbignIycsICcnKTtkAgEPDxYCHwMFQy9NYW5hZ2VyL1VwbG9hZHMvSW1hZ2Vucy9NZW51Q2lyY3VsYXJIb21lLzU5Ny81OTdfMV9pY28tb3V0LXJlbC5wbmdkZAICDxUBEk9VVFJPUyBSRUxBVMOTUklPU2QCBA9kFgZmDxUBIGphdmFzY3JpcHQ6d2luZG93Lm9wZW4oJyMnLCAnJyk7ZAIBDw8WAh8DBUIvTWFuYWdlci9VcGxvYWRzL0ltYWdlbnMvTWVudUNpcmN1bGFySG9tZS81OTcvNTk3XzFfaWNvLWZhbC1yaS5wbmdkZAICDxUBFkZBTEUgQ09NIEEgQlJBREVTQ08gUklkAgUPZBYGZg8VASBqYXZhc2NyaXB0OndpbmRvdy5vcGVuKCcjJywgJycpO2QCAQ8PFgIfAwVDL01hbmFnZXIvVXBsb2Fkcy9JbWFnZW5zL01lbnVDaXJjdWxhckhvbWUvNTk3LzU5N18xX2ljby1zaW0tZmluLnBuZ2RkAgIPFQEXU0lNVUxBRE9SRVMgRklOQU5DRUlST1NkAgIPZBYCZg9kFgICAg8WAh8AaGQCAw9kFgJmD2QWBGYPFgIfAGhkAgIPZBYKAgEPDxYEHwMFPS9NYW5hZ2VyL1VwbG9hZHMvSW1hZ2Vucy9JbWFnZW1VbmljYS8zMzYvMzM2XzFfY2FsbHRhLWltZy5qcGceB1Rvb2xUaXBkZGQCAw8PFgIfAQUQU3VzdGVudGFiaWxpZGFk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" />
</div>


        <!-- ------- HEADER ------- -->
        

<!-- Navigation -->

            

<script>
    $(document).ready(function () {
        $.ajax({
            method: "get",
            url: "/Manager/Modulos/CMS/Modulos/ModMenu/MenuHandler.ashx",
            dataType: "json",
            success: function (data) {
                $("#menu").empty();
                carregarMenu($('#menu'), data);
            }
        });

        function carregarMenu(parent, items) {
            $.each(items, function () {

                var li = $('<li class="dropdown dropdown-vertlign"><a href=' + this.Url + ' target=' + this.Target + ' class="dropdown-toggle link-sup ' + document.getElementById('Header_Menu_hdnCorMenu').value + '"  data-toggle=dropdown>' + this.Nome + '</a><span class="glyphicon gliph-menu lplay-icon_red"></span>');
                li.appendTo(parent);

                if (this.ItensMenu && this.ItensMenu.length > 0) {
                    var ul = $("<ul class='dropdown-menu'></ul>");
                    ul.appendTo(li);
                    carregarMenuFilho(ul, this.ItensMenu);
                }

                li.appendTo('</li>');
            });
        }

        function carregarMenuFilho(parent, items) {
            $.each(items, function () {
                var classHierarquia = this.Hierarquia.length;

                var li = $('<li class="dropdown dropdown-submenu"><a href=' + this.Url + ' target=' + this.Target + ' class="dropdown-toggle ' + document.getElementById('Header_Menu_hdnCorMenu').value + '" data-toggle=dropdown>' + this.Nome + '</a>');
                li.appendTo(parent);
                li.appendTo('</li>');

                if (this.ItensMenu && this.ItensMenu.length > 0) {
                    var ul = $("<ul class='dropdown-menu'></ul>");
                    ul.appendTo(li);
                    carregarMenuFilho(ul, this.ItensMenu);
                }

            });
        }

        $(document).ready(function () {

            var corMenu = document.getElementById('Header_Menu_hdnCorMenu').value;

            $("#logo").addClass((corMenu == 'link-preto' ? 'b-logo-preto' : 'b-logo-branco'));
            $("#search").addClass(corMenu);
            $("#relInvest").addClass(corMenu);
            $("#searchMob").addClass(corMenu);
            $("#relInvestMob").addClass(corMenu);
            if (corMenu == 'link-preto') {
                $("#iconBar").addClass("icon-bar-preto");
                $("#iconBar2").addClass("icon-bar-preto");
                $("#iconBar3").addClass("icon-bar-preto");
            }
            else {
                $("#iconBar").addClass("icon-bar-branco");
                $("#iconBar2").addClass("icon-bar-branco");
                $("#iconBar3").addClass("icon-bar-branco");
            }
        });
    });

</script>
<style>
   
</style>
<body style="font-family: Arial">
    <input type="hidden" name="Header$Menu$hdnCorMenu" id="Header_Menu_hdnCorMenu" value="link-branco" />
    <nav class="navbar navbar-default navbar-custom navbar-custom-bri navbar-static-top" role="navigation">
        <div class="container-fluid container">
            <div class="navbar-header page-scroll col-lg-12 col-lg-offset-0 col-md-12 col-md-offset-0">
                <button type="button" class="navbar-toggle" data-toggle="collapse" data-target="#navbar-collapse-1">
                    <span class="sr-only">Toggle navigation</span>
                    <span id="iconBar" class="icon-bar"></span>
                    <span id="iconBar2" class="icon-bar"></span>
                    <span id="iconBar3" class="icon-bar"></span>
                </button>
                <a id="logo" class="navbar-brand" href="#"></a>
                <a id="searchMob" href="#toggle-search" class="dropdown-toggle link-sup glyphicon glyphicon-search search-top"></a>
                <a id="relInvestMob" href="#" class="dropdown-toggle link-sup bot-relin-top ">Relações com investidores</a>
            </div>

            <div class="collapse navbar-collapse" id="navbar-collapse-1">
                <!-- Menu Nav dinâmico -->
                <ul id="menu" class="nav navbar-nav navbar-left">
                    <li><a href="#" class="botao-mobile-2 link-preto">Relações com investidores e Sustentabilidade</a></li>
                    <li class="divider"></li>
                    <li class="mobile-3"><a href="#" class="botao-mobile-3 link-preto">Abra sua conta</a></li>
                </ul>
                <!-- Menu Nav estático -->
                <ul id="menu2" class="nav navbar-nav navbar-right">
                    <li class="dropdown dropdown-vertlign"><a href="#" class="dropdown-toggle link-sup bot-alertri"><span class="glyphicon glyphicon-envelope envelope-ri"></span>ALERTA RI</a></li>
                    <li class="dropdown dropdown-vertlign"><a id="search" href="#toggle-search" class="dropdown-toggle link-sup glyphicon glyphicon-search"></a></li>
                    <li class="dropdown dropdown-vertlign"><a href="#" class="dropdown-toggle link-sup bot-bandeira link-preto">
                        <img src="../../../../img/bandeira.jpg" /></a></li>
                    <li class="dropdown dropdown-vertlign"><a id="relInvest" href="#" class="dropdown-toggle link-sup bot-relin ">Relações com investidores</a></li>

                </ul>
            </div>
            <!-- /.navbar-collapse -->
        </div>
        <!-- /.container-fluid container -->
    </nav>
    <!-- /.nav -->

</body>

            

<!-- Page Header Padrão -->

        <header class="intro-header">
            

<div id="CTT_331_ctl00_divConteudo">

    <!-- <!-- Carrossel/ Slider Superior Grande - com thumbnails -->
    <div id="x_006_img_thumbnail" class="carousel slide ps_slide_y x_006_img_thumbnail_indicators x_006_img_thumbnail_control_button thumb_scroll_y swipe_y ps_easeOutInCubic" data-ride="carousel" data-pause="hover"  data-interval="8000" data-duration="2000">

    <!-- Indicators -->
    <ol class="carousel-indicators">
        <li data-target="#x_006_img_thumbnail" data-slide-to="0" class="active" style="">   <img src="/Manager/Uploads/Imagens/Banners/12/12_5_1_img-intro-header.jpg" alt="bootstrap carousel with thumbnail navigation"></li><li data-target="#x_006_img_thumbnail" data-slide-to="1" class="" style="">   <img src="/Manager/Uploads/Imagens/Banners/13/13_5_1_img-intro-header_azul.png" alt="x_006_img_thumbnail_01_sm"></li>
    </ol>

    <!-- Wrapper for slides -->
    <div class="carousel-inner" role="listbox">
        <div class="item active" style="transition-duration: 2000ms;">   <img src="/Manager/Uploads/Imagens/Banners/12/12_5_1_img-intro-header.jpg" alt="x_006_img_thumbnail_00">   <div class="x_006_img_thumbnail_text x_006_img_thumbnail_text_center">       <h1 data-animation="animated fadeInUp" class="page-heading">INVESTIMENTO SUSTENTÁVEL</h1><hr class="small">       <p data-animation="animated fadeInUp" class="subheading">Um bom investimento começa com a confiança</p>       <a href="#" data-animation="animated fadeInUp" class="link-play"><span class="lplay-icon_white"></span>Saiba mais</a>   </div></div><div class="item " style="transition-duration: 2000ms;">   <img src="/Manager/Uploads/Imagens/Banners/13/13_5_1_img-intro-header_azul.png" alt="x_006_img_thumbnail_01">   <div class="x_006_img_thumbnail_text x_006_img_thumbnail_text_center">       <h1 data-animation="animated fadeInUp" class="page-heading">RELAÇÕES COM INVESTIDORES</h1><hr class="small">       <p data-animation="animated fadeInUp" class="subheading">Um bom investimento começa com dados atualizados</p>       <a href="#" data-animation="animated fadeInUp" class="link-play"><span class="lplay-icon_white"></span>Saiba mais</a>   </div></div>
    </div>

    <a class="left carousel-control" href="#x_006_img_thumbnail" role="button" data-slide="prev"><span class="fa fa-angle-up"></span></a><a class="right carousel-control" href="#x_006_img_thumbnail" role="button" data-slide="next"><span class="fa fa-angle-down"></span></a>
    <div class="faixa-cinza">
        <div class="container-fluid container">
            <div class="row">
                <div class="col-lg-12 col-lg-offset-0 col-md-12 col-md-offset-0 col-sm-12 col-sm-offset-0 top-row-1">
                    <span class="col-lg-12 col-lg-offset-0 col-md-12 col-md-offset-0 col-sm-12 col-sm-offset-0 faixa-cinza-int"></span>
                </div>
            </div>
        </div>
    </div>

    </div>
    <!-- /.fim de Carrossel/ Slider Grande -->

</div>

        </header>

        <!-- ------- HEADER ------- -->
        <!-- Main Content -->
        <div class="container-fluid container">

            <div>                
                <input type="hidden" name="hidpaginaId" id="hidpaginaId" />
                

<div id="ctl10_CTT_452_ctl00_divComCOnteudo" class="moduloComConteudo">
    <div class="row">
        <div class="col-lg-12 col-lg-offset-0 col-md-12 col-md-offset-0 col-sm-12 col-sm-offset-0 top-row-1">
        <div class="container-relat-com">
            <ul class="nav nav-tabs tabs-relat-com">
                <li class="active"><a data-toggle="tab" href="#home">Principais Relatórios</a></li>
                <li><a data-toggle="tab" href="#menu1">Últimos Comunicados</a></li>
            </ul>
            <div class="tab-content">
                <div id="home" class="tab-pane tab-relat-com fade in active col-md-6 col-sm-6 menu-rel-com box-tab-esq">
                    <h3>Principais Relatórios</h3>
                    <hr class="linha-curta" />
                    <ul><li><a href='#'>Demonstracão Financeiras</a></li><li><a href='#'>Relatório Anual</a></li><li><a href='#'>Relatório de Sustentabilidade</a></li></ul>
                    
                </div>
                <div id="menu1" class="tab-pane tab-relat-com fade col-md-6 col-sm-6 menu-rel-com box-tab-dir">
                    <h3>Últimos Comunicados</h3>
                    <hr class="linha-curta" />
                    <ul><li><a href='#'>Comunicado</a></li><li><a href='#'>Nofo Fato Relevante</a></li><li><a href='#'>Teste da divulgação</a></li></ul>
                </div>
            </div>

        </div>
        <span class="col-lg-12 col-lg-offset-0 col-md-12 col-md-offset-0 col-sm-12 col-sm-offset-0 faixa-cinza2"></span>
 </div>
    </div><!-- Fim de Row #1 - Relatórios e Cominicados -->

</div>


<div id="ctl10_CTT_597_ctl00_divConteudo">

    
            <!-- Row #2 - subMenu circular -->
            <div class="row horizon-circ-home">
                <div class="col-lg-12 col-lg-offset-0 col-md-12 col-md-offset-0 col-sm-12 col-sm-offset-0 top-row-1">
                    <div class="horizon-swiper">
        
            <div class="horizon-item box">
                <button id="btnLink" type="button" class="btn btn-bri btn-circle btn-xl ico-rel-int" onclick="javascript:window.open('#', '');">
                    <img id="ctl10_CTT_597_ctl00_rptMenuCircular_imgImagem_0" src="/Manager/Uploads/Imagens/MenuCircularHome/597/597_1_ico-rel-int.png" style="width:50px;" />
                </button>
                <p class="nom-bot-circ">RELATÓRIO INTEGRADO</p>
            </div>
        
            <div class="horizon-item box">
                <button id="btnLink" type="button" class="btn btn-bri btn-circle btn-xl ico-rel-int" onclick="javascript:window.open('#', '');">
                    <img id="ctl10_CTT_597_ctl00_rptMenuCircular_imgImagem_1" src="/Manager/Uploads/Imagens/MenuCircularHome/597/597_1_ico-sus-cor.png" style="width:50px;" />
                </button>
                <p class="nom-bot-circ">SUSTENTABILIDADE CORPORATIVA</p>
            </div>
        
            <div class="horizon-item box">
                <button id="btnLink" type="button" class="btn btn-bri btn-circle btn-xl ico-rel-int" onclick="javascript:window.open('#', '');">
                    <img id="ctl10_CTT_597_ctl00_rptMenuCircular_imgImagem_2" src="/Manager/Uploads/Imagens/MenuCircularHome/597/597_1_ico-out-rel.png" style="width:50px;" />
                </button>
                <p class="nom-bot-circ">OUTROS RELATÓRIOS</p>
            </div>
        
            <div class="horizon-item box">
                <button id="btnLink" type="button" class="btn btn-bri btn-circle btn-xl ico-rel-int" onclick="javascript:window.open('#', '');">
                    <img id="ctl10_CTT_597_ctl00_rptMenuCircular_imgImagem_3" src="/Manager/Uploads/Imagens/MenuCircularHome/597/597_1_ico-fal-ri.png" style="width:50px;" />
                </button>
                <p class="nom-bot-circ">FALE COM A BRADESCO RI</p>
            </div>
        
            <div class="horizon-item box">
                <button id="btnLink" type="button" class="btn btn-bri btn-circle btn-xl ico-rel-int" onclick="javascript:window.open('#', '');">
                    <img id="ctl10_CTT_597_ctl00_rptMenuCircular_imgImagem_4" src="/Manager/Uploads/Imagens/MenuCircularHome/597/597_1_ico-sim-fin.png" style="width:50px;" />
                </button>
                <p class="nom-bot-circ">SIMULADORES FINANCEIROS</p>
            </div>
        
                    </div>
                </div>
            </div>
        


</div>
<div id="ctl10_CTT_459_ctl00_divSemConteudo" class="moduloSemConteudo">
    <span id="ctl10_CTT_459_ctl00_lblTitulo">Módulo 'Notícias' sem conteúdo.</span>
</div>




<div id="ctl10_CTT_336_ctl00_divConteudo">

    <div class="row">
        <div class="col-md-12 calltoaction-home">
            <img id="ctl10_CTT_336_ctl00_imgImagemModulo" src="/Manager/Uploads/Imagens/ImagemUnica/336/336_1_callta-img.jpg" />
            <div class="CTA-texto">
            <p class="pCSubtt">
                <span id="ctl10_CTT_336_ctl00_lblTexto1">Sustentabilidade</span>

            </p>
            <h2 class="section-heading heading_h2_CA">
                <span id="ctl10_CTT_336_ctl00_lblTexto2">RESULTADOS DO BRADESCO</span>

            </h2>
            <p class="pCAction">
                <span id="ctl10_CTT_336_ctl00_lblTexto3">Veja os principais destaques no 3º trimestre de 2017</span>

            </p>
            <p class="pCAction">
                <a href="../Modulos/ModImagemUnica/#" id="ctl10_CTT_336_ctl00_linkImagem" class="link-play" target="_self">
                    <span class="glyphicon glyphicon-play-circle"></span>
                    <span id="ctl10_CTT_336_ctl00_lblTextoUrl">Saiba mais</span></a>
            </p>
            <hr class="vertical">
            </div>
        </div>
        
    </div>



</div>
<div id="ctl10_CTT_461_ctl00_divConteudo">

    
            <!-- Row #5 - Próximos Eventos  -->
            <div class="row">
                <div class="col-lg-12 col-lg-offset-0 col-md-12 col-md-offset-0 prox-even-home">
                    <h2 class="section-heading heading_h2">PRÓXIMOS EVENTOS</h2>
                    <hr class="small2">
        
            <div class="col-md-4 evento-bri">
                <p class="dias">
                    <span id="ctl10_CTT_461_ctl00_rptProximosEventos_lblData_0">21 de setembro</span></p>
                <h3>Seminário de Startups do Rio de Janeiro</h3>
                <p>Seminário de Startups do Rio de Janeiro</p>
                <p class="pEvent">
                    <a class="btn btn-default link-vermelho" href='testeUrl?Id=8'>
                        <span class="lplay-icon_red"></span>SAIBA MAIS</a>
                </p>
            </div>
        
            <div class="col-md-4 evento-bri">
                <p class="dias">
                    <span id="ctl10_CTT_461_ctl00_rptProximosEventos_lblData_1">29 de setembro</span></p>
                <h3>Seminário de Relações com Investidores</h3>
                <p>Mais de 3.000 empreendores falando sobre sua carreira.</p>
                <p class="pEvent">
                    <a class="btn btn-default link-vermelho" href='testeUrl?Id=7'>
                        <span class="lplay-icon_red"></span>SAIBA MAIS</a>
                </p>
            </div>
        
            <div class="col-md-4 evento-bri">
                <p class="dias">
                    <span id="ctl10_CTT_461_ctl00_rptProximosEventos_lblData_2">10 de dezembro</span></p>
                <h3>Seminário de Startups do Rio de Janeiro</h3>
                <p>Mais de 3.000 empreendores falando sobre sua carreira.</p>
                <p class="pEvent">
                    <a class="btn btn-default link-vermelho" href='testeUrl?Id=6'>
                        <span class="lplay-icon_red"></span>SAIBA MAIS</a>
                </p>
            </div>
        
            </div>
                <div class="col-lg-12 col-lg-offset-0 col-md-12 col-md-offset-0 even-footer">
                    <a class="btn btn-default vertodas" href=''>
                        <span class="lplay-icon_red"></span>VER TODOS EVENTOS</a>
                </div>
            </div>
            <!-- Fim de Row #5 - Proximos Eventos -->
        
</div>

            </div>
        </div>

        <!-- ------- FOOTER ------- -->
        


<!-- Footer -->

<footer>
   <div class="row" style="position:relative;"><button class="btn btn-primary scroll-top" data-scroll="up" type="button">
                   <i class="glyphicon glyphicon-arrow-up"></i>Voltar ao topo
            </button></div>
    
    <div id="Footer_pnlConteudo">
	
        <div class="container-fluid">
            <div class="row horizon-footer">
                <div class="col-lg-10 col-lg-offset-1 col-md-10 col-md-offset-1 b-telefones">
                    <div class="horizon-swiper swiper-footer">
                        <div class="col-md-3 col-md-offset-1 horizon-item horizon-footer-item box-tel bloco-tel">
                            <p class="dias">
                                <span id="Footer_lblTituloN1">Sac Alô Bradesco</span></p>
                            <h3>
                                <span id="Footer_lblTelefoneN1">0800 704 8383</span></h3>
                            <p>
                                <span id="Footer_lblTextoN1">Atendimento 24h, 7 dias por semana</span></p>
                        </div>
                        <!-- /.col-md-3 bloco-tel n1 -->
                        <div class="col-md-3 horizon-item horizon-footer-item box-tel bloco-tel">
                            <p class="dias">
                                <span id="Footer_lblTituloN2">Ouvidoria</span></p>
                            <h3>
                                <span id="Footer_lblTelefoneN2">0800 727 9933</span></h3>
                            <p>
                                <span id="Footer_lblTextoN2">Atendimento de seg a sex das 8h as 18h</span></p>
                        </div>
                        <!-- /.col-md-3 bloco-tel n2 -->
                        <div class="col-md-3 horizon-item horizon-footer-item box-tel bloco-tel2">
                            <p class="dias">
                                <span id="Footer_lblTituloN3">Deficiência auditiva / fala</span></p>
                            <h3>
                                <span id="Footer_lblTelefoneN3">0800 727 9933</span></h3>
                            <p>
                                <span id="Footer_lblTextoN3">Atendimento de seg a sex das 8h as 18h</span></p>
                        </div>
                        <!-- /.col-md-3 bloco-tel n3 -->
                        <div class="col-md-2 horizon-item horizon-footer-item box-tel bloco-tel3">
                            <p class="dias">Demais telefones, <a href="#">acesse aqui</a></p>
                        </div>
                        <!-- /.col-md-3 bloco-tel n4 -->
                    </div>
                    <!-- /.horizon-swiper -->
                    <div class="col-md-12 bloco-tel3b">
                        <p class="demais-tels">Demais telefones, <a href="#">acesse aqui</a></p>
                    </div>
                </div>
            </div>
            <!-- /. row horizon footer -->


            <div class="row row-footer">
                <div class="col-lg-10 col-lg-offset-1 col-md-10 col-md-offset-1 b-endereco">
                    <p>
                        <span id="Footer_lblTextoCentral">Banco Bradesco SA CNPJ: 60.746.948.0001-12 | Cidade de Deus, s/nº Vila Yara | Osasco | SP | CEP: 06029-900</span></p>
                </div>
                <!-- /.b-endereco -->
            </div>
            <!-- /.row footer-->
            <div class="row row-footer">
                <div class="col-lg-11 col-lg-offset-1 col-md-11 col-md-offset-1 l-importantes">
                    <div class="col-md-5ths col-xs-12"><span class="l-impor"></span>
                        <a id="Footer_linkN1" class="btn-foot " href="#">Codigo de Desefesa do Consumidor</a></div>
                    <div class="col-md-5ths col-xs-12"><span class="l-impor"></span>
                        <a id="Footer_linkN2" class="btn-foot " href="#">Consumidor.gov.br</a></div>
                    <div class="col-md-5ths col-xs-12 bri-dis"><span class="l-impor"></span>
                        <a id="Footer_linkN3" class="btn-foot " href="#">Portal Bradesco</a></div>
                    <div class="col-md-5ths col-xs-12 bri-dis"><span class="l-impor"></span>
                        <a id="Footer_linkN4" class="btn-foot " href="#">Outros Sites Bradesco</a></div>
                    <div class="col-md-5ths col-xs-12 bri-dis"><span class="l-impor"></span>
                        <a id="Footer_linkN5" class="btn-foot " href="#">FAQ</a></div>
                </div>
                <!-- /.l-importantes -->
            </div>
            <!-- /.row footer-->
            <div class="row row-footer">
                <div class="col-md-10 col-md-offset-1 b-redes-soci">
                    <p>Redes sociais </p>
                </div>
                <!-- /.b-redes-soci -->
                <div class="col-lg-10 col-lg-offset-1 col-md-10 col-md-offset-1 b-endereco-mobile">
                    <p class="site-ende">www.bradesco.com.br</p>
                    <p>
                        <span id="Footer_lblTextoCentralMobile">Banco Bradesco SA CNPJ: 60.746.948.0001-12 | Cidade de Deus, s/nº Vila Yara | Osasco | SP | CEP: 06029-900</span></p>
                </div>
                <!-- /.b-endereco -->
            </div>
            <!-- /.row footer-->

        </div>
        <!-- /.container-fluid -->
        <div class="container-fluid icons-footer-gray">
            <div class="row">

                <div class="col-lg-10 col-lg-offset-2 col-md-10 col-md-offset-2 ">
                    <div class="col-md-4 footer-marcas1"></div>
                    <div class="col-md-4 footer-marcas2"></div>
                </div>
                <!-- /.icons-footer-gray -->

            </div>
            <!-- /.row -->
        </div>
        <!-- /.container-fluid -->
    
</div>


</footer>

        <!-- ------- FOOTER ------- -->

        
    
<div class="aspNetHidden">

	<input type="hidden" name="__VIEWSTATEGENERATOR" id="__VIEWSTATEGENERATOR" value="6E2B97A8" />
	<input type="hidden" name="__EVENTVALIDATION" id="__EVENTVALIDATION" value="/wEdAAWccAp3Y/8p5n0l8/E6iD2PGcicgaajUcUXyt26tzTMEmsPqgDdqPD/3RBSkywCuZeyQmFgPJIjCaWuDmKusXQIjDKas1F5ACPU7eFk3DbDFjxSilP6TwQvQFMaOm5BopQiKETQ9Tx+c5RJL0HidRWS" />
</div>

<script type="text/javascript">
//<![CDATA[
javascript:openModalMensagem();//]]>
</script>
</form>

    <!-- jQuery Bradesco RI-->
    <script src="/Manager/vendor/jquery/jquery.min.js"></script>
    
    <!-- Bootstrap Core JavaScript -->
    <script src="/Manager/Scripts/bootstrap.min.js"></script>
    <!-- Tema base JavaScript -->
    <script src="/Manager/Scripts/clean-blog.min.js"></script>
    <script src="/Manager/Scripts/horizon-swiper.js"></script>
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

