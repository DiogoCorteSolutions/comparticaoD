jQuery(document).ready(function () {

    var botaoCol = document.querySelector("#add-coluna");
    botaoCol.addEventListener("click", function (event) {
        event.preventDefault();

        var formCol = document.querySelector("#adicona-coluna");
        var Coluna = obterInformacao(formCol);


        adiocnarColunaTabela(Coluna);

    });

    //$("#btcoluna").click(teste);

});

var boton = $("#coluna1").val();


function obterInformacao(formCol) {
    var objeto = {
        coluna: formCol.coluna.value
    }

    return objeto;
}

function motarTr(Coluna) {
    var colunaTr = document.createElement("th");
    colunaTr.classList.add("coluna");

    colunaTr.appendChild(montarTh(Coluna.coluna, "col"));

    return colunaTr;

}

function montarTh(dado, classe) {
    var th = document.createElement("th");
    th.textContent = dado;
    th.classList.add(classe);

    return th;
}

function adiocnarColunaTabela(Coluna) {

    var colunaTr = motarTr(Coluna);
    var tabela = document.querySelector("#colunas-table");
    tabela.appendChild(colunaTr);
}



function teste(event) {
    event.preventDefault();
    var vendaMediaMensal = $("#colunas-table").parent().text();

    console.log(vendaMediaMensal);

}
