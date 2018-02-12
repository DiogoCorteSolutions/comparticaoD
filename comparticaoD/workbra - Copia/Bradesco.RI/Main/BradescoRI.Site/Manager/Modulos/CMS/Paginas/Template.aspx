<%@ Page Language="C#" AutoEventWireup="true" EnableEventValidation="false" CodeFile="Template.aspx.cs" Inherits="Modulos_Paginas_Template" %>

<%@ Register Src="~/Modulos/CMS/Paginas/Header.ascx" TagPrefix="uc1" TagName="Header" %>
<%@ Register Src="~/Modulos/CMS/Paginas/Footer.ascx" TagPrefix="uc1" TagName="Footer" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="X-UA-Compatible" content="IE=Edge" />
    <meta charset="utf-8" />
    <title>Bradesco | RI</title>
    <meta name="description" content="RI" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0" />
    <meta name="format-detection" content="telephone=no" />

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
    <link href="<%=ResolveUrl("~/CSS/magic-check.css")%>" rel="stylesheet" />
    
    <link rel="stylesheet" href="<%=ResolveUrl("~/CSS/style.css")%>" />
    <script src="<%=ResolveUrl("~/JS/template.js")%>"></script>
    <script src="https://ajax.googleapis.com/ajax/libs/jquery/2.1.4/jquery.min.js"></script>

    <script src="<%=ResolveUrl("~/JS/jquery-ui.js")%>"></script>
    <script src="<%=ResolveUrl("~/JS/timeline.js")%>"></script>
    <script src="<%=ResolveUrl("~/JS/modernizr.js")%>"></script>
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
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <!-- ------- HEADER ------- -->
        <uc1:Header runat="server" ID="Header" />
        <header class="intro-header">
            <asp:PlaceHolder ID="placeHolderHeader" runat="server"></asp:PlaceHolder>
        </header>
        <!-- ------- HEADER ------- -->

        <!-- Main Content -->
        <div class="container-fluid container container-modulos">

            <div class="side-menu" id="sideMenu">
    <menu>
        <ul class="nav nav-tabs nav-stacked top-modulos-config">
            <li><a href="#myModal" data-backdrop="false" data-toggle="modal">Módulos - Config</a></li>
                  <li><asp:Button ID="btnPreview" runat="server" OnClick="btnPreview_Click" alt="Visualizar página" title="Visualizar página" CausesValidation="false" />
            </li>
        </ul>
    </menu>
</div>
<div id="myModal" class="modal fade">
    <div class="modal-dialog">
        <div class="modal-content">
            <div class="modal-header">
                <button type="button" class="close" data-dismiss="modal" aria-hidden="true">&times;</button>
                 <h4 class="modal-title">Configurações de módulos</h4>

            </div>
            <div class="modal-body">
                <div class="row menu-modulos">

                <div class="col-lg-12 col-lg-offset-0 col-md-12 col-md-offset-0 menu-modulos-idcor">
                    <div class="col-md-6">
                        <asp:Label ID="lblIdioma" runat="server" alt="Idioma" title="Idioma"></asp:Label>
                        <asp:DropDownList ID="ddlIdioma" runat="server" OnSelectedIndexChanged="ddlIdioma_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
                    </div>
                    <div class="col-md-6">
                        <asp:Label ID="lblCorMenu" runat="server" alt="Cor Menu" title="Cor Menu"></asp:Label>
                        <asp:DropDownList ID="ddlCorMenu" runat="server"></asp:DropDownList>
                    </div>
                </div>

                <div id="divConfiguracoes" class="menu-modulos-config" runat="server">
                    <div class="col-lg-12 col-lg-offset-0 col-md-12 col-md-offset-0 menu-modulos-pagina">
                        <asp:HiddenField ID="hidPaginaId" runat="server" />
                        <div class="col-md-12">
                            <asp:Label ID="lblCategoria" runat="server" alt="Categoria" title="Categoria"></asp:Label>
                            <asp:DropDownList ID="ddlCategoria" runat="server" AutoPostBack="true"></asp:DropDownList>
                        </div>
                        <div class="col-md-6">
                            <asp:Label ID="lblAlias" runat="server" alt="Alias página" title="Alias página"></asp:Label>
                            <asp:TextBox ID="txtAlias" runat="server" onkeyup="this.value=this.value.replace(/[' ']/g,'')" Style="width: 70%;"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfqAlias" runat="server" ControlToValidate="txtAlias" Text="*" ForeColor="Red"></asp:RequiredFieldValidator>
                        </div>

                        <div class="col-md-6">
                            <asp:Label ID="lblTitulo" runat="server" alt="Titulo página" title="Titulo página"></asp:Label>
                            <asp:TextBox ID="txtTitulo" runat="server" Style="width: 70%;"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfqTitulo" runat="server" ControlToValidate="txtTitulo" Text="*" ForeColor="Red"></asp:RequiredFieldValidator>
                        </div>
                        <div class="col-md-12">
                            <asp:Label ID="lblDescricao" runat="server" alt="Descrição página" title="Descrição página"></asp:Label>
                            <asp:TextBox ID="txtDescricao" runat="server"></asp:TextBox>
                        </div>
                    </div>

                    <div class="col-lg-12 col-lg-offset-0 col-md-12 col-md-offset-0 menu-modulos-metas">
                        <div class="col-md-2">
                            <asp:Label ID="lblMetaTags" runat="server" alt="MetaTags página" title="MetaTags página"></asp:Label>
                        </div>
                        <div class="col-md-12">
                            <asp:Label ID="lblKeyWords" runat="server" alt="KeyWords página" title="KeyWords página"></asp:Label>
                            <asp:TextBox ID="txtMetatagsKeyWords" runat="server" Style="width: 80%;"></asp:TextBox>
                        </div>
                        <div class="col-md-12">
                            <asp:Label ID="lblDescription" runat="server" alt="Description página" title="Description página"></asp:Label>
                            <asp:TextBox ID="txtMetatagsDescription" runat="server" Style="width: 80%;"></asp:TextBox>
                        </div>
                    </div>

                    <div id="template" class="col-lg-12 col-lg-offset-0 col-md-12 col-md-offset-0 menu-modulos-template">
                        <div class="col-md-6">
                            <asp:Label ID="lblTemplate" runat="server" alt="Template" title="Template"></asp:Label>
                            <asp:DropDownList ID="ddlTemplate" runat="server" AutoPostBack="true"></asp:DropDownList>
                            <asp:Button ID="btnEditarAbas" runat="server" alt="Editar abas" title="Editar abas" OnClick="btnEditarAbas_Click" Visible="false" />  
                        </div>
                        <div class="col-md-6">
                            <%--<asp:Button ID="btnSalvar" runat="server" alt="Salvar página" title="Salvar página" OnClick="btnSalvar_Click" />--%>
                        </div>
                    </div>
                    <!-- fim de menu-modulos-config -->

                    <div id="divModulos" class="col-lg-12 col-lg-offset-0 col-md-12 col-md-offset-0 menu-modulos-abas" runat="server">
                        <div class="col-md-12">
                            <asp:Label ID="lblAba" runat="server" alt="Aba" title="Aba" Visible="false" ></asp:Label>
                            <asp:DropDownList ID="ddlAba" runat="server" alt="Abas página" title="Abas página" Visible="false" ></asp:DropDownList>
                        </div>
                        <div class="col-md-6">
                            <asp:Label ID="lblModulos" runat="server" alt="Módulos" title="Módulos"></asp:Label>
                            <asp:DropDownList ID="ddlModulos" runat="server" alt="Módulos página" title="Módulos página" Style="width: 83%;"></asp:DropDownList>
                        </div>
                        <div class="col-md-3">
                            <asp:Button ID="btnAdicionar" runat="server" OnClick="btnAdicionar_Click" alt="Adicionar Módulo" title="Adicionar Módulo" CausesValidation="false" />
                        </div>
                       
                    </div>
                </div>
                <!-- fim de menu-modulos-config  -->
            </div>
            </div>
            <div class="modal-footer">
                <button type="button" class="btn btn-default" data-dismiss="modal">Fechar</button>
                <asp:Button ID="btnSalvar" runat="server" alt="Salvar página" title="Salvar página" OnClick="btnSalvar_Click" class="btn btn-primary" />
                <%--<button type="button" class="btn btn-primary">Salvar alterações</button>--%>
            </div>
        </div>
        <!-- /.modal-content -->
    </div>
    <!-- /.modal-dialog -->
</div>
<!-- /.modal -->
	<!-- fim de menu-modulos  -->
	<!-- fim de div draggable  -->
            

            <asp:PlaceHolder ID="placeHolderTemplate" runat="server"></asp:PlaceHolder>

            <div id="divModalErro" class="ModalMsg" tabindex="Mensagem de erro">
                <a href="javascript:void(0)" onclick="closeModal();" class="cms-fechar-popup" alt="Fechar" title="Fechar">Fechar (X)</a>
                <p>Ocorreu um erro na execução do sistema, detalhes técnico:<asp:Label ID="lblErro" runat="server"></asp:Label></p>
            </div>

            <div id="black_overlay"></div>

        </div>
        <!-- fim de Main Content - .container-fluid .container-modulos - Bradesco RI - Front-end  -->

        <!-- ------- FOOTER ------- -->
        <uc1:Footer runat="server" ID="Footer" />
        <!-- ------- FOOTER ------- -->
    </form>

    <!-- jQuery Bradesco RI-->
    <script src="<%=ResolveUrl("~/vendor/jquery/jquery.min.js")%>"></script>

    <!-- Bootstrap Core JavaScript -->

    <script src="<%=ResolveUrl("~/Scripts/bootstrap.min.js")%>"></script>

    <!-- Tema base JavaScript -->
    <script src="<%=ResolveUrl("~/Scripts/clean-blog.min.js")%>"></script>
    <script src="<%=ResolveUrl("~/Scripts/horizon-swiper.js")%>"></script>
    <script type="text/javascript">$('.horizon-swiper').horizonSwiper();</script>
    <script type="text/javascript">$(document).ready(function () {

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
    <script type="text/javascript">
        $("#myModal").draggable({
            handle: ".modal-header"
        });
  </script>

</body>
</html>
