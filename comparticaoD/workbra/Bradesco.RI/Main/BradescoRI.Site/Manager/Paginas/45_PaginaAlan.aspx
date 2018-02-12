

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head><meta http-equiv="X-UA-Compatible" content="IE=Edge" /><meta charset="utf-8" /><title>
	Bradesco | RI
</title><meta name="viewport" content="width=device-width, initial-scale=1.0" /><meta name="format-detection" content="telephone=no" /><meta name="keywords" content="KeywordsAlan" /><meta name="description" content="DescriptionAlan" />

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
    <script src="/Manager/JS/timeline/timelinejs.js"></script>
    <script src="/Manager/JS/modernizr.js"></script>
    <script src="/Manager/JS/audioplayer.js"></script>
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
    <form method="post" runat="server" id="form1">


<script type="text/javascript">
//<![CDATA[
var theForm = document.forms['form1'];
if (!theForm) {
    theForm = document.form1;
}
function __doPostBack(eventTarget, eventArgument) {
    if (!theForm.onsubmit || (theForm.onsubmit() != false)) {
        theForm.__EVENTTARGET.value = eventTarget;
        theForm.__EVENTARGUMENT.value = eventArgument;
        theForm.submit();
    }
}
//]]>
</script>


<script src="/Manager/WebResource.axd?d=wkDDO37tM5yth2Y5ZFaJKegRy_76wpli4p1AVC9tUwth-8ZcD8m5pZpBb3RkZe3z8waUZjw8fltTaRgbTZGi5i3ONW5Jlycxn_6Gn6b6MJU1&amp;t=636398313817438952" type="text/javascript"></script>


<script src="/Manager/ScriptResource.axd?d=zfxNANmIewHLX9F2WDLXhshj32PmGltSTgcgCldqynQ4TTWkfwXTSU50DXLJPIcutY86i4X7zpJtuYiaWwJOGaEd9K-3ju937bVDTjXHxFhYH4XJFTfyQzGrAgMMKVU1wp0cEKG8-0mRmymdW48UZeGFl_UJJq8H4kfKnZ7tgs87Zy4qBzHS23uytyzgbpQE0&amp;t=3d0d00d7" type="text/javascript"></script>
<script type="text/javascript">
//<![CDATA[
if (typeof(Sys) === 'undefined') throw new Error('A estrutura do cliente do ASP.NET Ajax falhou ao ser carregada.');
//]]>
</script>

<script src="/Manager/ScriptResource.axd?d=8qULyNYUwRM_FYdqXXG7nsvBbWlwBQvmAktP-OyVCBOGWtpN7JWxDTwHVilcRNHdya0xuAXFXhMc4kZy6cQlQ9RuE2TLRke4dOne6Xp61GzTsm11_dfspvYQIRwWhG-wqj-QKV_UF03B6Re_LO4P59f54upHrMqC7JyJVt8jSlnzX3ewl4scNwIHzkcdeySY0&amp;t=3d0d00d7" type="text/javascript"></script>
        
        <script type="text/javascript">
//<![CDATA[
Sys.WebForms.PageRequestManager._initialize('ScriptManager1', 'form1', [], [], [], 90, '');
//]]>
</script>

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

        $.ajax({
            method: "get",
            url: "/Manager/Modulos/CMS/Modulos/ModMenu/MenuLinkExtraHandler.ashx",
            dataType: "json",
            success: function (data) {
                $("#lnkAlertaRI").empty();
                var liAlerta = $("#lnkAlertaRI");
                
                $("#lnkRelInvestidores").empty();
                var liRelacoes = $("#lnkRelInvestidores");

                //Insere Link Relações com investidores
                var linkRelacoesInvestidores = data[1];
                var linkR = $('<a id="relInvest" href="' + linkRelacoesInvestidores.Url + '" target="' + linkRelacoesInvestidores.Target + '" class="dropdown-toggle link-sup bot-relin">' + linkRelacoesInvestidores.Nome + '</a>');
                linkR.appendTo(liRelacoes);

                //Insere Link Alerta RI
                var linkAlertaRi = data[0];
                var link = $('<a href="' + linkAlertaRi.Url + '" target="' + linkAlertaRi.Target + '" class="dropdown-toggle link-sup bot-alertri"><span class="glyphicon glyphicon-envelope envelope-ri"></span>' + linkAlertaRi.Nome + '</a>');
                link.appendTo(liAlerta);
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
    <input type="hidden" name="Header$Menu$hdnCorMenu" id="Header_Menu_hdnCorMenu" value="link-preto" />
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
                    <li class="dropdown dropdown-vertlign" id="lnkAlertaRI">
                    </li>
                    <li class="dropdown dropdown-vertlign">
                        <a id="search" href="#toggle-search" class="dropdown-toggle link-sup glyphicon glyphicon-search"></a>
                    </li>
                    <li class="dropdown dropdown-vertlign">
                        <a href="#" class="dropdown-toggle link-sup bot-bandeira link-preto"></a>
                    </li>
                    <li class="dropdown dropdown-vertlign" id="lnkRelInvestidores">
                        <a href="/Paginas/Area-relacoes-investidores.aspx" target="_self" id="relInvest" class="dropdown-toggle link-sup bot-relin ">Relações com Investidores</a>
                    </li>
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
            
        </header>

        <!-- ------- HEADER ------- -->
        <!-- Main Content -->
        <div class="container-fluid container">

            <div>                
                <input type="hidden" name="hidpaginaId" id="hidpaginaId" />
                
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
                        <a id="Footer_linkN1" class="btn-foot " href="../CMS/Paginas/#">Codigo de Desefesa do Consumidor</a></div>
                    <div class="col-md-5ths col-xs-12"><span class="l-impor"></span>
                        <a id="Footer_linkN2" class="btn-foot " href="../CMS/Paginas/#">Consumidor.gov.br</a></div>
                    <div class="col-md-5ths col-xs-12 bri-dis"><span class="l-impor"></span>
                        <a id="Footer_linkN3" class="btn-foot " href="../CMS/Paginas/#">Portal Bradesco</a></div>
                    <div class="col-md-5ths col-xs-12 bri-dis"><span class="l-impor"></span>
                        <a id="Footer_linkN4" class="btn-foot " href="../CMS/Paginas/#">Outros Sites Bradesco</a></div>
                    <div class="col-md-5ths col-xs-12 bri-dis"><span class="l-impor"></span>
                        <a id="Footer_linkN5" class="btn-foot " href="../CMS/Paginas/#">FAQ</a></div>
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
