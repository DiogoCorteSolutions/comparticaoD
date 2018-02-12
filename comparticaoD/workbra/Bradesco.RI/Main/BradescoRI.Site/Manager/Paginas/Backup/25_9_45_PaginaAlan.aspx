<%@ Register TagPrefix='ContDinamico' Namespace='Manager.Controls' %>  

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

        function refreshParent() {
            window.opener.location.href = window.opener.location.href;
            window.close();
        }
    </script>
</head>
<body>
    <form method="post" action="./Preview.aspx?AprovacaoId=L5SczzAlS4o%3d&amp;Aprovar=WppcAZkVPMU%3d" onsubmit="javascript:return WebForm_OnSubmit();" id="form1">
<div class="aspNetHidden">
<input type="hidden" name="__EVENTTARGET" id="__EVENTTARGET" value="" />
<input type="hidden" name="__EVENTARGUMENT" id="__EVENTARGUMENT" value="" />
<input type="hidden" name="__VIEWSTATE" id="__VIEWSTATE" value="/wEPDwUKLTk4NDI5MjAwNQ9kFgICAQ9kFgQCCQ9kFgQCAQ8PFgIeB1Zpc2libGVoZGQCAw8PFgIfAGdkFiACAQ8PFgIeBFRleHQFEVNhYyBBbMO0IEJyYWRlc2NvZGQCAw8PFgIfAQUNMDgwMCA3MDQgODM4M2RkAgUPDxYCHwEFIkF0ZW5kaW1lbnRvIDI0aCwgNyBkaWFzIHBvciBzZW1hbmFkZAIHDw8WAh8BBQlPdXZpZG9yaWFkZAIJDw8WAh8BBQ0wODAwIDcyNyA5OTMzZGQCCw8PFgIfAQUmQXRlbmRpbWVudG8gZGUgc2VnIGEgc2V4IGRhcyA4aCBhcyAxOGhkZAINDw8WAh8BBRxEZWZpY2nDqm5jaWEgYXVkaXRpdmEgLyBmYWxhZGQCDw8PFgIfAQUNMDgwMCA3MjcgOTkzM2RkAhEPDxYCHwEFJkF0ZW5kaW1lbnRvIGRlIHNlZyBhIHNleCBkYXMgOGggYXMgMThoZGQCEw8PFgIfAQVrQmFuY28gQnJhZGVzY28gU0EgQ05QSjogNjAuNzQ2Ljk0OC4wMDAxLTEyIHwgQ2lkYWRlIGRlIERldXMsIHMvbsK6IFZpbGEgWWFyYSB8IE9zYXNjbyB8IFNQIHwgQ0VQOiAwNjAyOS05MDBkZAIVDw8WBB8BBSBDb2RpZ28gZGUgRGVzZWZlc2EgZG8gQ29uc3VtaWRvch4LTmF2aWdhdGVVcmwFASNkZAIXDw8WBB8BBRFDb25zdW1pZG9yLmdvdi5ich8CBQEjZGQCGQ8PFgQfAQUPUG9ydGFsIEJyYWRlc2NvHwIFASNkZAIbDw8WBB8BBRVPdXRyb3MgU2l0ZXMgQnJhZGVzY28fAgUBI2RkAh0PDxYEHwEFA0ZBUR8CBQEjZGQCHw8PFgIfAQVrQmFuY28gQnJhZGVzY28gU0EgQ05QSjogNjAuNzQ2Ljk0OC4wMDAxLTEyIHwgQ2lkYWRlIGRlIERldXMsIHMvbsK6IFZpbGEgWWFyYSB8IE9zYXNjbyB8IFNQIHwgQ0VQOiAwNjAyOS05MDBkZAIRDw8WAh8BBR5Qw6FnaW5hIHB1YmxpY2FkYSBjb20gc3VjZXNzby5kZGRQ9L8Yzz5lIT+BebLq+NLN6Q97Tstp1Ls3JG4eASQ3TA==" />
</div>

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


<script src="/Manager/WebResource.axd?d=I0E7bnMPcnGLzUnEp1EcbnvKzXxYJY0udWvQzVPvBszhoNYbHASll8ciBK5pH8deZgEUVgWiYzr2sMntgDC1rp7287DMYUeESM8F_Zm4cJ01&amp;t=636398313817438952" type="text/javascript"></script>


<script src="/Manager/WebResource.axd?d=NwwD44fY1fbcp001_IwXngOPeMufUR09teOnMkjEfWqc0RM1_hUV7Q8b4kf-zvijsO7AX9TGPkbH1a6_5T21nMUKE92H-dkP-EJNzEJZSCw1&amp;t=636398313817438952" type="text/javascript"></script>
<script type="text/javascript">
//<![CDATA[
function WebForm_OnSubmit() {
if (typeof(ValidatorOnSubmit) == "function" && ValidatorOnSubmit() == false) return false;
return true;
}
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
    <input type="hidden" name="Header$Menu$hdnCorMenu" id="Header_Menu_hdnCorMenu" />
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
            
        </header>

        <!-- ------- HEADER ------- -->
        <!-- Main Content -->
        <div class="container-fluid container">

            <div>                
                <input type="hidden" name="hidpaginaId" id="hidpaginaId" />
                

<div id="ctl10_CTT_828_ctl00_divConteudo">
    <div class="container">
        <div class="row">
            <div class="col-lg-9 col-lg-offset-1 col-md-12 col-md-offset-0">
                <div class="panel-group" id="accordion" role="tablist" aria-multiselectable="true">
                    <div id="dvAccordian">
                        
                                <div class="panel panel-default">
                                    <div class="panel-heading" role="tab" id="heading1">
                                        <h4 class="panel-title">
                                            <a role="button" class="accordion-toggle" data-toggle="collapse" data-parent="#accordion" href="#collapse1" aria-expanded="false" aria-controls="collapseOne">teste</a>
                                        </h4>
                                    </div>
                                    <div id="collapse1" class="panel-collapse collapse in" role="tabpanel" aria-labelledby="heading1">
                                        <div class="panel-body">
                                            

<div id="ctl10_CTT_828_ctl00_rptAccordian_CTT_845_0_ctl00_0_divConteudo_0">
    <p>asdadasdasd</p>
<p>asdasdasdasd</p>
<p>asd</p>
</div>

                                        </div>
                                    </div>
                                </div>
                            
                    </div>
                </div>
            </div>
        </div>
    </div>
</div>






<div id="ctl10_CTT_831_ctl00_divConteudo">
    <div class="container">
        <div class="row">
            <div class="col-lg-9 col-lg-offset-1 col-md-12 col-md-offset-0">
                <div class="panel-group" id="accordion" role="tablist" aria-multiselectable="true">
                    <div id="dvAccordian">
                        
                                <div class="panel panel-default">
                                    <div class="panel-heading" role="tab" id="heading2">
                                        <h4 class="panel-title">
                                            <a role="button" class="accordion-toggle" data-toggle="collapse" data-parent="#accordion" href="#collapse2" aria-expanded="false" aria-controls="collapseOne">dgfgdgdg</a>
                                        </h4>
                                    </div>
                                    <div id="collapse2" class="panel-collapse collapse" role="tabpanel" aria-labelledby="heading2">
                                        <div class="panel-body">
                                            <div id="ctl10_CTT_831_ctl00_rptAccordian_CTT_846_0_ctl00_0_divSemConteudo_0" class="moduloSemConteudo">
    <span id="ctl10_CTT_831_ctl00_rptAccordian_CTT_846_0_ctl00_0_lblTitulo_0">Módulo 'Arquivos' sem conteúdo.</span>
</div>



                                        </div>
                                    </div>
                                </div>
                            
                    </div>
                </div>
            </div>
        </div>
    </div>
</div>






<div id="ctl10_CTT_878_ctl00_divConteudo">
    <div class="row form-alerta-ri">
        <div>
            
        </div>
        <div>
            
        </div>

        <div class="col-lg-12 col-lg-offset-0 col-md-12 col-md-offset-0 label-form">
            <label>Nome*</label>
        </div>
        <div class="col-lg-12 col-lg-offset-0 col-md-12 col-md-offset-0 campo-form">
            <input name="ctl10$CTT_878$ctl00$txtNome" type="text" maxlength="128" id="ctl10_CTT_878_ctl00_txtNome" tabindex="1" title="Digite seu nome" />
            <span id="ctl10_CTT_878_ctl00_rfvNome" style="visibility:hidden;">Campo obrigatório</span>
        </div>

        <div class="col-lg-12 col-lg-offset-0 col-md-12 col-md-offset-0 label-form">
            <label>Email*</label>
        </div>
        <div class="col-lg-12 col-lg-offset-0 col-md-12 col-md-offset-0 campo-form">
            <input name="ctl10$CTT_878$ctl00$txtEmail" type="text" maxlength="128" id="ctl10_CTT_878_ctl00_txtEmail" tabindex="2" title="exemplo@mail.com" placeholder="exemplo@mail.com" />
            <span id="ctl10_CTT_878_ctl00_rfvEmail" style="display:none;">Campo obrigatório</span>
            <span id="ctl10_CTT_878_ctl00_revEmail" style="display:none;">Digite um e-mail válido</span>
        </div>

        <div class="col-lg-12 col-lg-offset-0 col-md-12 col-md-offset-0 label-form">
            <label>Telefone</label>
        </div>
        <div class="col-lg-12 col-lg-offset-0 col-md-12 col-md-offset-0 campo-form">
            <input name="ctl10$CTT_878$ctl00$txtTelefoneDDD" type="text" maxlength="2" id="ctl10_CTT_878_ctl00_txtTelefoneDDD" tabindex="3" title="DDD" class="txtNumeroDdd" placeholder="(DDD)" />
            <input name="ctl10$CTT_878$ctl00$txtTelefone" type="text" maxlength="15" id="ctl10_CTT_878_ctl00_txtTelefone" tabindex="4" title="xxxxxxxx" class="txtNumero" placeholder="xxxx-xxxx" />
        </div>

        <div class="col-lg-12 col-lg-offset-0 col-md-12 col-md-offset-0 label-form">
            <label>Empresa</label>
        </div>
        <div class="col-lg-12 col-lg-offset-0 col-md-12 col-md-offset-0 campo-form">
            <input name="ctl10$CTT_878$ctl00$txtEmpresa" type="text" maxlength="128" id="ctl10_CTT_878_ctl00_txtEmpresa" tabindex="5" title="Digite a empresa da qual faz parte" placeholder="Digite a empresa da qual você faz parte" />
        </div>


        <div class="col-lg-12 col-lg-offset-0 col-md-12 col-md-offset-0 label-form">
            <label>Selecione um Assunto</label>
        </div>
        <div class="col-lg-12 col-lg-offset-0 col-md-12 col-md-offset-0 campo-form">
            <select name="ctl10$CTT_878$ctl00$ddlAssunto" id="ctl10_CTT_878_ctl00_ddlAssunto" tabindex="6" class="componente-form">
	<option selected="selected" value="Teste">Teste</option>

</select>
        </div>

        <div class="col-lg-12 col-lg-offset-0 col-md-12 col-md-offset-0 label-form">
            <label>Mensagem*</label>
        </div>
        <div class="col-lg-12 col-lg-offset-0 col-md-12 col-md-offset-0 campo-form">
            <input name="ctl10$CTT_878$ctl00$txtMensagem" type="text" maxlength="128" id="ctl10_CTT_878_ctl00_txtMensagem" tabindex="7" title="Digite a mensagem" placeholder="Digite a mensagem" />
            <span id="ctl10_CTT_878_ctl00_rfvMensagem" style="visibility:hidden;">Campo obrigatório</span>
        </div>
        <!-- Inicio Captcha DBDN  -->
        <div class='captcha-bdn'><span style='margin:5px;float:left;'><img src="CaptchaImage.aspx?guid=f25e88e1-ccc6-45c8-a38f-260944197bd8" border='0' width=180 height=50></span><span style='margin:5px;float:left;'>Digite o código da imagem<br><input name=ctl10$CTT_878$ctl00$CaptchaFaleConosco type=text title="Para ouvir o áudio do captcha pressione as teclas Control U e digite os caracteres narrados" id=captchaInput size=5 maxlength=5 value=''>&nbsp;&nbsp;<object alt="Iniciar audio captcha" classid="clsid:d27cdb6e-ae6d-11cf-96b8-444553540000" codebase="https://download.macromedia.com/pub/shockwave/cabs/flash/swflash.cab#version=9,0,0,0" width="16" height="16" id="CaptchaAudio" align="middle"><param name="allowScriptAccess" value="sameDomain" /><param name="allowFullScreen" value="false" /><param name="movie" value="SWF/CaptchaAudio.swf?link=https://institucional.bradesco.com.br/WSAudioCaptcha/AudioCaptcha/captcha/YfA73AYbFMSKB81BSB115BjLbVCMiA69Abn4A77AA51A.mp3" /><param name="quality" value="high" /><param name="bgcolor" value="#ffffff" /><embed src="SWF/CaptchaAudio.swf?link=https://institucional.bradesco.com.br/WSAudioCaptcha/AudioCaptcha/captcha/YfA73AYbFMSKB81BSB115BjLbVCMiA69Abn4A77AA51A.mp3" quality="high" bgcolor="#ffffff" width="16" height="16" name="CaptchaAudio" align="middle" allowScriptAccess="sameDomain" allowFullScreen="false" type="application/x-shockwave-flash" pluginspage="https://www.macromedia.com/go/getflashplayer" /></object></span><br clear='all'></div>
        <!-- Fim Captcha DBDN  -->
        <div class="col-lg-3 col-lg-offset-0 col-md-12 col-md-offset-0">
            <input type="submit" name="ctl10$CTT_878$ctl00$btnEnviar" value="Enviar" onclick="javascript:WebForm_DoPostBackWithOptions(new WebForm_PostBackOptions(&quot;ctl10$CTT_878$ctl00$btnEnviar&quot;, &quot;&quot;, true, &quot;ModFaleConosco&quot;, &quot;&quot;, false, false))" id="ctl10_CTT_878_ctl00_btnEnviar" tabindex="8" />
        </div>
    </div>
</div>

<script src="/Manager/JS/jquery-1.10.2.min.js"></script>
<script src="/Manager/JS/jquery.mask.js"></script>





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
var Page_Validators =  new Array(document.getElementById("ctl10_CTT_878_ctl00_rfvNome"), document.getElementById("ctl10_CTT_878_ctl00_rfvEmail"), document.getElementById("ctl10_CTT_878_ctl00_revEmail"), document.getElementById("ctl10_CTT_878_ctl00_rfvMensagem"));
//]]>
</script>

<script type="text/javascript">
//<![CDATA[
var ctl10_CTT_878_ctl00_rfvNome = document.all ? document.all["ctl10_CTT_878_ctl00_rfvNome"] : document.getElementById("ctl10_CTT_878_ctl00_rfvNome");
ctl10_CTT_878_ctl00_rfvNome.controltovalidate = "ctl10_CTT_878_ctl00_txtNome";
ctl10_CTT_878_ctl00_rfvNome.validationGroup = "ModFaleConosco";
ctl10_CTT_878_ctl00_rfvNome.evaluationfunction = "RequiredFieldValidatorEvaluateIsValid";
ctl10_CTT_878_ctl00_rfvNome.initialvalue = "";
var ctl10_CTT_878_ctl00_rfvEmail = document.all ? document.all["ctl10_CTT_878_ctl00_rfvEmail"] : document.getElementById("ctl10_CTT_878_ctl00_rfvEmail");
ctl10_CTT_878_ctl00_rfvEmail.controltovalidate = "ctl10_CTT_878_ctl00_txtEmail";
ctl10_CTT_878_ctl00_rfvEmail.display = "Dynamic";
ctl10_CTT_878_ctl00_rfvEmail.validationGroup = "ModFaleConosco";
ctl10_CTT_878_ctl00_rfvEmail.evaluationfunction = "RequiredFieldValidatorEvaluateIsValid";
ctl10_CTT_878_ctl00_rfvEmail.initialvalue = "";
var ctl10_CTT_878_ctl00_revEmail = document.all ? document.all["ctl10_CTT_878_ctl00_revEmail"] : document.getElementById("ctl10_CTT_878_ctl00_revEmail");
ctl10_CTT_878_ctl00_revEmail.controltovalidate = "ctl10_CTT_878_ctl00_txtEmail";
ctl10_CTT_878_ctl00_revEmail.display = "Dynamic";
ctl10_CTT_878_ctl00_revEmail.validationGroup = "ModFaleConosco";
ctl10_CTT_878_ctl00_revEmail.evaluationfunction = "RegularExpressionValidatorEvaluateIsValid";
ctl10_CTT_878_ctl00_revEmail.validationexpression = "\\w+([-+.\']\\w+)*@\\w+([-.]\\w+)*\\.\\w+([-.]\\w+)*";
var ctl10_CTT_878_ctl00_rfvMensagem = document.all ? document.all["ctl10_CTT_878_ctl00_rfvMensagem"] : document.getElementById("ctl10_CTT_878_ctl00_rfvMensagem");
ctl10_CTT_878_ctl00_rfvMensagem.controltovalidate = "ctl10_CTT_878_ctl00_txtMensagem";
ctl10_CTT_878_ctl00_rfvMensagem.validationGroup = "ModFaleConosco";
ctl10_CTT_878_ctl00_rfvMensagem.evaluationfunction = "RequiredFieldValidatorEvaluateIsValid";
ctl10_CTT_878_ctl00_rfvMensagem.initialvalue = "";
//]]>
</script>

<div class="aspNetHidden">

	<input type="hidden" name="__VIEWSTATEGENERATOR" id="__VIEWSTATEGENERATOR" value="BEE71651" />
	<input type="hidden" name="__EVENTVALIDATION" id="__EVENTVALIDATION" value="/wEdAA0VYAcThWdYYfDB1iYxi4nkGcicgaajUcUXyt26tzTMEmsPqgDdqPD/3RBSkywCuZdgW9T7fiuZYiXmpCOUXAg0IqNLhrLcOLLTryGia+dayCE/+BXh6o4yXQPZoJdRyBDdaOrLNUm2FasSGrOBESouZ1vKn14B7wOMsQlqIe5JbMMKXrC3zIf87nzv18n8OSbSAagxTb2MfSbmgVfcMipF2XfsokN0qCXzWxZuYvV7Ac+xZ8sH006oWS8blTYOql0VoEk2GN1usZ7LW9aP4+UTD/SewUhmsj7dDA70FFk6sYXGMFwJ5ZTLaNWVU4WESjE=" />
</div>

<script type="text/javascript">
//<![CDATA[
javascript:openModalMensagem();
var Page_ValidationActive = false;
if (typeof(ValidatorOnLoad) == "function") {
    ValidatorOnLoad();
}

function ValidatorOnSubmit() {
    if (Page_ValidationActive) {
        return ValidatorCommonOnSubmit();
    }
    else {
        return true;
    }
}
        //]]>
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

