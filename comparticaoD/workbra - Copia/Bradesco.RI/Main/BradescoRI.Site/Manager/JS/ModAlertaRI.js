$(document).ready(function (b) {  
    b(".txtNumero").mask("000000000");
    b(".txtNumeroDdd").mask("99");

    //Posiciona o focus no próximo campo
    b('.txtNumeroDdd').keyup(function () {
        if (this.value.length == this.maxLength) {
            $(this).next('.txtNumero').focus();
        }
    });
});