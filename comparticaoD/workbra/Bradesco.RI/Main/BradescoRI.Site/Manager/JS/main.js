/// <reference path="jqueryAdjr.js" />

function PageLoad() {
    $('.selecionarTodos').click(function () {
        if ($(this).is(':checked')) {
            $('input[type=checkbox]:not(.selecionarTodos)').attr('checked', 'checked');
        } else {
            $('input[type=checkbox]:not(.selecionarTodos)').removeAttr('checked');
        }
    });
}

function escondeItemsMenu() {
    var current;

    //Obtém o caminho da página selecionada sem o nome do arquivo
    var currentPath = window.location.pathname.substring(0, window.location.pathname.lastIndexOf('/'));

    console.log(window.location.pathname);
    if (window.location.pathname.indexOf('PaginaConstrucao') > -1||
        window.location.pathname.indexOf('Default.aspx') > -1) {
        currentPath = null;
    }

    $(".liItemMenu").bdneach(function () {
        if (currentPath) {
            //Seta item de menu selecionado
            if ($(this).attr('href').lastIndexOf(currentPath) > -1) {
                $(this).addClass('current');
            }
        }

        if (!$(this).hasClass("current")) {
            $(this).hide();
        } else {
            current = this;
        }
    });

    //Exibe todos os controles do mesmo menu pai do item corrente
    $(current).parent().parent().find('li').bdneach(function (i) {
        $(this).find('a').show();
    });
}

function setCurrent(control) {

    control.parent().find('ul li').bdneach(function (i) {
        if ($(this).find('a').is(":visible")) {
            $(this).find('a').hide(150);
        } else {
            $(this).find('a').show(150);
        }
    });
}

$(function () {

    $('#ctl00_ctl00_content_contentInterna_btnDuplicar').click(function () {
        return confirm("Deseja duplicar esta pergunta para zerar o contador de acessos?");
    });


    $(document)
    	.delegate('#ln-change-pass, #bt-close-bx-change-pass', 'click', function () {
    	    $('#bx-change-pass').toggle();
    	    return false;
    	})
		.delegate('input.deletar', 'click', function () { return confirm('Deseja excluir esse item?'); })
		.delegate('input.executar', 'click', function () {
		    if ($('.checkbox input[type="checkbox"]:checked', '#ctl00_ctl00_content_contentInterna_upConteudo').length) {
		        return confirm('Deseja excluir os itens selecionados?');
		    } else {
		        alert('Nenhum item selecionado.');
		        return false;
		    }
		})
        .delegate('.tbLista td:not(.irgnoreClick)', 'click', function () { location.href = 'Editar.aspx?ID=' + $(this).closest('tr').find('input[type=hidden]').eq(0).val(); })
    ;
    if ($.fn.datePicker) {
        $(document)
            .delegate("input.txtDate", 'focus.datepicker', function (h) {
                var
                	a = function (c) {
                	    if ((c + "").length < 2) {
                	        return "0" + c;
                	    }
                	    return c;
                	},
                    t = h.currentTarget,
                    f = $(t),
                	g = f.parent()[0],
                    j = $(g),
                    e = j.find('input')[0],
                    k = j.children(".form-icon-calendar")
                ;
                k.datePicker({
                    createButton: false,
                    startDate: "01/01/1900",
                    endDate: (new Date()).asString()
                }).bind("dateSelected", function (n, m, o, l) {
                    e.value = a(m.getDate()) + '/' + a(m.getMonth() + 1) + '/' + a(m.getFullYear());
                });
                f.bind("focus", function (l) {
                    window.setTimeout(function () {
                        k.dpDisplay();
                    }, 50);
                }).bind("keydown.datepicker", function (m) {
                    var l = this;
                    m.which == 9 && k.dpClose();
                    window.setTimeout(function () {
                        if (!j.find(":focus").length) {
                            k.dpClose();
                        }
                    }, 100);
                });
                j.undelegate("focus.datepicker");
            })
        ;
    }
    ;
    PageLoad();

    $(".txtDate").datePicker({ constrainInput: false, autoOpen: false, changeMonth: true, yearRange: '1950:2013' });

});


var Sys;

(function () {
    function initToggleFunctions() {
        function menuOpenNextUlClick(obj) {
            if (!obj) {
                obj = $(this);
            } else {
                obj = $(obj);
            }
            var x = $(obj);


            x.parent().siblings()
            obj.next("ul:first").slideToggle('normal');
            obj.toggleClass('open');

            /*se estiver no header, fecha outros menus que estejam abertos ao abrir um novo.*/
            if (obj.parents("header").length) {
                var ctx = obj.parent().siblings();
                $("a + ul", ctx).slideUp('normal');
                $("a", ctx).removeClass("open");
            }

            return false;
        }

        var slug = window.location.pathname.toUpperCase();
        //console.log(slug);
        //$('.menu ul ul:has(li)').hide();
        //$('.menu li:has(ul) > a').not("[onclick]").not(".menuhandled").addClass("menuhandled").click(function () { return menuOpenNextUlClick($(this)); });

        $(".menu a[href]").bdneach(function () {
            var h = $(this).attr("href");
            //console.log(h);
            if (h && typeof (h) === "string" && h != "#") {
                //console.log("indexOf slug = " + slug.indexOf(h.toUpperCase()));
                if (slug.indexOf(h.toUpperCase()) >= 0) {
                    $(this).addClass("current");
                    /*abre os itens pai do link de menu para a página atual apenas se o menu não estiver no header*/
                    if (!$(this).parents("header").length) {
                        $(this).parents("ul").show();
                    }
                }
            }
        });
    }

    $(document).ready(function () {
        initToggleFunctions();

        escondeItemsMenu();

        if (Sys && Sys.WebForms && Sys.WebForms.PageRequestManager) {
            var requestManager = Sys.WebForms.PageRequestManager.getInstance();

            requestManager.add_endRequest(
                function (sender, args) {
                    try {
                        initToggleFunctions();
                    } catch (erro) { }
                }
            );
        }
    });
})();

$("#btnFecharPopup").click(function () {
    $(this).parent().hide();
});


$(".linkGrupo").click(function () {
    setCurrent($(this));
});



