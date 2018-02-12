<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Menu.ascx.cs" Inherits="Modulos_CMS_Modulos_ModMenu_Menu" %>

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

                var li = $('<li class="dropdown dropdown-vertlign"><a href=' + this.Url + ' target=' + this.Target + ' class="dropdown-toggle link-sup ' + document.getElementById('<%=hdnCorMenu.ClientID%>').value + '"  data-toggle=dropdown>' + this.Nome + '</a><span class="glyphicon gliph-menu lplay-icon_red"></span>');
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

                var li = $('<li class="dropdown dropdown-submenu"><a href=' + this.Url + ' target=' + this.Target + ' class="dropdown-toggle ' + document.getElementById('<%=hdnCorMenu.ClientID%>').value + '" data-toggle=dropdown>' + this.Nome + '</a>');
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

            var corMenu = document.getElementById('<%=hdnCorMenu.ClientID%>').value;

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
    <asp:HiddenField ID="hdnCorMenu" runat="server" />
    <nav class="navbar navbar-default navbar-custom navbar-custom-bri navbar-static-top" role="navigation">
        <div class="container-fluid container">
            <div class="navbar-header page-scroll col-lg-12 col-lg-offset-0 col-md-12 col-md-offset-0">
                <button type="button" class="navbar-toggle" data-toggle="collapse" data-target="#navbar-collapse-1">
                    <span class="sr-only">Toggle navigation</span>
                    <span id="iconBar" class="icon-bar"></span>
                    <span id="iconBar2" class="icon-bar"></span>
                    <span id="iconBar3" class="icon-bar"></span>
                </button>
                <a id="logo" class="navbar-brand" href="#"><%--Bradesco--%></a>
                <a id="searchMob" href="#toggle-search" class="dropdown-toggle link-sup glyphicon glyphicon-search search-top"></a>
                <a id="relInvestMob" href="#" class="dropdown-toggle link-sup bot-relin-top ">Relações com investidores</a>
            </div>

            <div class="collapse navbar-collapse" id="navbar-collapse-1">
                <!-- Menu Nav dinâmico -->
                <ul id="menu" class="nav navbar-nav navbar-left">
                    <li><%--<span class="botao-mobile-x">X</span>--%><a href="#" class="botao-mobile-2 link-preto">Relações com investidores e Sustentabilidade</a></li>
                    <li class="divider"></li>
                    <li class="mobile-3"><a href="#" class="botao-mobile-3 link-preto">Abra sua conta</a></li>
                </ul>
                <!-- Menu Nav estático -->
                <ul id="menu2" class="nav navbar-nav navbar-right">
                    <li class="dropdown dropdown-vertlign<%-- col-lg-2 col-md-2 col-sm-4 col-xs-6--%>"><a href="#" class="dropdown-toggle link-sup bot-alertri"><span class="glyphicon glyphicon-envelope envelope-ri"></span>ALERTA RI</a></li>
                    <li class="dropdown dropdown-vertlign<%-- col-lg-2 col-md-2 col-sm-4--%>"><a id="search" href="#toggle-search" class="dropdown-toggle link-sup glyphicon glyphicon-search"></a></li>
                    <li class="dropdown dropdown-vertlign<%-- col-lg-2 col-md-2 col-sm-4 col-xs-6--%>"><a href="#" class="dropdown-toggle link-sup bot-bandeira link-preto">
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
