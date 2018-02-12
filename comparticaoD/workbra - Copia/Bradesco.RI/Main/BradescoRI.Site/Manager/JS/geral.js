/// <reference path="jquery.js" />
/// <reference path="modernizr.js" />

$(document).ready(function () {
    $('.menu-principal ul ul:has(li)').hide();

    $('.menu-principal li:has(ul) > a').parent("li").each(function () {

        var controller = function (obj) {
            var opening = false;
            var closing = false;

            function finishOpening() {
                opening = false;
            }

            function finishClosing() {
                closing = false;
            }

            return {
                hideMenu: function () {
                    if (!closing) {
                        clo = true;
                        var obj = $(this);
                        $("ul:first", obj).slideUp('normal', finishClosing);
                        obj.removeClass('ativo');
                    }
                },
                showMenu: function () {
                    if (!opening) {
                        opening = true;
                        var obj = $(this);

                        $("ul:first", obj).slideDown('normal', finishOpening);
                        obj.addClass('ativo');
                    }
                }
            };
        } (this);

        $(this).hover(controller.showMenu, controller.hideMenu);
    });

    ////////////////////////////////////////////////////////

    $('.menu-hotsite ul ul:has(li)').hide();

    $('.menu-hotsite li:has(ul) > a').parent("li").each(function () {

        var controller = function (obj) {
            var opening = false;
            var closing = false;

            function finishOpening() {
                opening = false;
            }

            function finishClosing() {
                closing = false;
            }

            return {
                hideMenu: function () {
                    if (!closing) {
                        clo = true;
                        var obj = $(this);
                        $("ul:first", obj).slideUp('normal', finishClosing);
                        obj.removeClass('ativo');
                    }
                },
                showMenu: function () {
                    if (!opening) {
                        opening = true;
                        var obj = $(this);

                        $("ul:first", obj).slideDown('normal', finishOpening);
                        obj.addClass('ativo');
                    }
                }
            };
        } (this);

        $(this).hover(controller.showMenu, controller.hideMenu);
    });

    ////////////////////////////////////////////////////////

   

    ////////////////////////////////////////////////////////

    var slug = window.location.pathname.toUpperCase();

    $("li.lateral a[href]").each(function () {
        var h = $(this).attr("href");
        if (h && typeof (h) === "string" && h != "#") {
            if (slug.indexOf(h.toUpperCase()) >= 0) {
                $(this).parent("li").addClass("atual");
            }
        }
    });

    $(".como-trabalhamos a.barra-topo").each(function () {
        var barra = $(this).parent(".barra");

        var clk = function (href, barra) {
            return function () {
                var speed = 700;

                var l = parseInt(barra.css("marginLeft")) + 73;
                $("#como-trabalhamos-seta").show().animate({ left: l }, speed, "swing");
                $(".como-trabalhamos-content").hide();
                $(href).show();
                $(".como-trabalhamos .barra.atual").removeClass("atual");
                barra.addClass("atual").animate({ bottom: 2 }, speed, "swing");
                return false;
            };
        } ($(this).attr("href"), barra);

        barra.click(clk);
        $(this).click(clk);

    });
});