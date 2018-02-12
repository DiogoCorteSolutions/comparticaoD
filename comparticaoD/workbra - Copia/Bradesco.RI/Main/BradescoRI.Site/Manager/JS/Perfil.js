$(document).ready(function () {
    $('.secaoFilho > input').change(function () {
        var itemsSelecionados = 0;
        var total = 0;

        $(this).closest("div").find('.secaoFilho :checkbox').bdneach(function () {
            total += 1;
            if ($(this).is(":checked")) {
                itemsSelecionados += 1;
            }
        });

        if (total == itemsSelecionados) {
            $(this).closest("div").find('.secaoPai :checkbox').attr('checked', true);
        } else {
            $(this).closest("div").find('.secaoPai :checkbox').prop('checked', false);
        }

    });

    $('.secaoPai > input').change(function () {
        var pai = $(this);
        pai.closest("div").find('.secaoFilho :checkbox').bdneach(function () {
            if (pai.is(":checked")) {
                $(this).attr('checked', 'true');
            } else {
                $(this).prop('checked', false);
            }

        });

    });

    $('.more').on('click', function (e) {
        AlterarMaisMenos(this);
        $(this).parent().parent().next().slideToggle();
    });

    $('.exibirEsconderTudo').on('click', function (e) {
        AlterarMaisMenos(this);

        $('.more').bdneach(function () {
            AlterarMaisMenos(this);
        });

        //$('.item-filho').slideToggle();

        if ($("#hdnEsconder").val() == "true") {
            exibirTodos();
            $("#hdnEsconder").val('false');
        } else {
            esconderTodos();
            $("#hdnEsconder").val('true');
        }

    });

    function AlterarMaisMenos(c) {
        //if ($(c).html() == '-') {
        //    $(c).html('+');
        //} else {
        //    $(c).html('-');
        //}
    }
});

function esconderTodos() {
    $(".fields.no-columns.item-filho").css('display', 'none');
}

function exibirTodos() {
    $(".fields.no-columns.item-filho").css('display', 'block');
}