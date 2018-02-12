$(document).ready(function (b) {  
    b(".validacoes").hide();
    b(".txtNumero3").mask("000");
    b(".txtCodigoVan").mask("00000");
    b(".txtCodigoAgencia").mask("0000");
    b(".txtCodigoAgenciaComDigito").mask("0000-0");
    b(".txtContaCorrente").mask("0000000-0");
    b(".txtNumero").mask("000000000");
    b(".txtNumeroDdd").mask("99");
    b(".txtDate").mask("99/99/9999");
    b(".txtCnpj, .txtCnpjAcoesDia, .txtCnpjProspect").mask("99.999.999/9999-99");
    b(".txtCPF").mask("999.999.999-99");
    b('.txtValorMonetario').maskMoney({ decimal: ",", thousands: "." });
    b(".txtTelefone").mask("(00) 0000-0000");
    b(".txtCelular").mask("(00) 000000000");
    b(".txtCep").mask("99999-999");

    //Mensagens de confirmação e alerta para exclusão de registros
    $('.btnExcluir').click(function () {
        if ($(".checkItem input:checkbox:checked").length == 0) {
            alert('Selecione pelo menos um registro.');
            event.preventDefault();
        } else if (!confirm('Confirma a exclusão do(s) registro(s) selecionado(s)?')) {
            event.preventDefault();
        }
    });

    //Mensagens de confirmação e alerta para aprovação de registros
    $('.btnAprovar').click(function () {
        if ($(".checkItem input:checkbox:checked").length == 0) {
            alert('Selecione pelo menos um registro.');
            event.preventDefault();
        } else if (!confirm('Confirma a aprovação do(s) registro(s) selecionado(s)?')) {
            event.preventDefault();
        }
    });

    //Mensagens de confirmação e alerta para download de imagens
    $('.btnDownloadNow').click(function () {
        if ($(".checkItem input:checkbox:checked").length == 0) {
            alert('Selecione pelo menos um registro.');
            event.preventDefault();
        } else if (!confirm('Confirma a exclusão do(s) registro(s) selecionado(s)?')) {
            event.preventDefault();
        }
    });

    //Posiciona o focus no próximo campo
    $('.txtNumeroDdd').keyup(function () {
        if (this.value.length == this.maxLength) {
            $(this).next('.txtNumero').focus();
        }
    });

    //Check/Uncheck todos os itens
    $('.checkAll input').click(function () {
        if ($(this).is(':checked')) {
            $(".checkItem input").prop("checked", true);
        } else {
            $(".checkItem input").prop("checked", false);
        }
    });

    $('.txtCnpjAcoesDia').blur(function () {
        if ($(this).val() != "") {
            CheckCNPJ("Listar.aspx",$(this).val());
        }
    });

    $('.txtCnpjProspect').blur(function () {
        if ($(this).val() != "") {
            CheckCNPJ("Novo.aspx", $(this).val());
        }
    });


    $('.submitRelatorio').click(function () {
        //showLoading();
    });
    
});

function showLoading() {
    $("#divLoading").show();
}
function hideLoading() {
    $("#divLoading").hide();
}

function openModal(tipo) {
    if (tipo == 'E') {
        $("#divModalErro").show();
        $("#divModalMensagem").hide();
    } else if (tipo == 'M') {
        $("#divModalErro").hide();
        $("#divModalMensagem").show();
    }
    $("#black_overlay").show();
}

function openModal(tipo, mensagem) {
    if (tipo == 'E') {
        $("#divModalErro").show();
        $("#divModalMensagem").hide();
    } else if (tipo == 'M') {
        $("#divModalErro").hide();
        $("#divModalMensagem").show();
    }
    $("#black_overlay").show();
}

function closeModal() {
    $(".ModalMsg").hide();
    $("#black_overlay").hide();
}

function CheckCNPJ(strArquivo, strCNPJ) {
    var erro = false;
    var prospecaoId = 0;

    if ($("#content_contentInterna_hdnId").val()) {
        prospecaoId = $("#content_contentInterna_hdnId").val();
    }

    $(".validacoes").show();
    $.ajax({
        type: "POST",
        url: strArquivo + "/ChecaCNPJ",
        data: "{'cnpj':'" + strCNPJ + "', 'id':" + prospecaoId + "}",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        async: false,
        error: function (e) {
            alert(e);
        },
        success: function (r) {
            var intRetorno = ((r.d != null) ? r.d : r);

            blnCNPJValido = true;
            if (intRetorno != 0) {
                $(".txtCnpjAcoesDia").val('');

                blnCNPJValido = false;
                if (intRetorno == 1) { //INVALIDO
                    $(".erroCnpjInvalido").show();
                    $(".erroCnpjCadastrado").hide();
                    $(".erroCnpjMatriz").hide();
                    $(".sucessoCnpj").hide();
                }
                if (intRetorno == 2) { //JA EXISTE
                    $(".erroCnpjInvalido").hide();
                    $(".erroCnpjCadastrado").show();
                    $(".erroCnpjMatriz").hide();
                    $(".sucessoCnpj").hide();
                }
                if (intRetorno == 3) { //CNPJ DE FILIAL
                    $(".erroCnpjInvalido").hide();
                    $(".erroCnpjCadastrado").hide();
                    $(".erroCnpjMatriz").show();
                    $(".sucessoCnpj").hide();
                }
            }
            else {
                $(".erroCnpjInvalido").hide();
                $(".erroCnpjCadastrado").hide();
                $(".erroCnpjMatriz").hide();
                if (!$("#content_contentInterna_hdnId").val()) {
                    $(".sucessoCnpj").show();
                    $(".sucessoCnpj p").eq(0).html('<strong>CNPJ: ' + strCNPJ + ' não consta no sistema.</strong><br>Data consulta: ' + dataAtual());
                }
            }
        }
    });
}

function dataAtual() {
    // Obtém a data/hora atual
    var data = new Date();

    // Guarda cada pedaço em uma variável
    var dia = data.getDate();           // 1-31
    var dia_sem = data.getDay();            // 0-6 (zero=domingo)
    var mes = data.getMonth();          // 0-11 (zero=janeiro)
    var ano2 = data.getYear();           // 2 dígitos
    var ano4 = data.getFullYear();       // 4 dígitos
    var hora = data.getHours();          // 0-23
    var min = data.getMinutes();        // 0-59
    var seg = data.getSeconds();        // 0-59
    var mseg = data.getMilliseconds();   // 0-999
    var tz = data.getTimezoneOffset(); // em minutos

    // Formata a data e a hora (note o mês + 1)
    var str_data = dia + '/' + (mes + 1) + '/' + ano4;
    var str_hora = hora + ':' + min + ':' + seg;

    // Mostra o resultado
    return str_data + ' às ' + str_hora;
}