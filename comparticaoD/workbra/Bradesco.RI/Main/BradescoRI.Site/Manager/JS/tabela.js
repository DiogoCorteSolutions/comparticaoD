$(document).ready(function () {
    var next = 1;
    $(".add-more").click(function (e) {
        e.preventDefault();
        var addto = "#field" + next;
        var addRemove = "#field" + (next);
        next = next + 1;
        var newIn = '<input class="input form-control collection-table" id="field' + next + '" name="field' + next + '" type="text">';
        var newInput = $(newIn);
        var removeBtn = '<button id="remove' + (next - 1) + '" class="btn btn-danger remove-me" >-</button></div><div id="field">';
        var removeButton = $(removeBtn);
        $(addto).after(newInput);
        $(addRemove).after(removeButton);
        $("#field" + next).attr('data-source', $(addto).attr('data-source'));
        $("#count").val(next);

        $('.remove-me').click(function (e) {
            e.preventDefault();
            var fieldNum = this.id.charAt(this.id.length - 1);
            var fieldID = "#field" + fieldNum;
            $(this).remove();
            $(fieldID).remove();
        });
    });


    $(document).on('click', '#btnFechar', function (e) {
        e.preventDefault();
        
        var collection = [];

        $(".collection-table").each(function (index) {
            collection.push($(this).val())
        });
      
        var model = {
            collection: collection,
            camo: 1
        };

        $.ajax({
            type: 'POST',
            url: '/default/EnviarPush',
            contentType: "application/json; charset=utf-8",
            data: JSON.stringify(model),
            success: function (data) {
                // TODO: obter resultados
            },
            error: function (data) {
                // TODO: obter resultados
            }
        });

    });
});




//var botao = document.querySelector("#adicionar-dados");
//console.log(botao);
//botao.addEventListener("click", function (event) {
//    event.preventDefault();

//    var form = document.querySelector("#form1");
//    var colunas = obterInformacao(form);

//    var erros = validaColunas(colunas);
//    console.log(erros);

//    if (erros.length > 0) {
//        exibeMensagemDeerros(erros);
//        return;
//    }

//    adicionaColunasTabela(colunas);

//    form.reset();
//    var msgErro = document.querySelector("#mensagem-erro")
//    msgErro.innerHTML = "";


//});







//function obterInformacao(form) {
//    var objeto = {
//        nomeColuna: form.nomeColuna.value
//    }
//    return objeto;
//}

//function montarTR(colunas) {
//    var colunaTR = document.createElement("tr");
//    colunaTR.classList.add("colunas");

//    colunaTR.appendChild(montarTd(colunas.nomeColuna, "info-nome"));

//    return colunaTR;
//}

//function montarTd(dado, classe) {
//    //var td = document.createElement("td");
//    var td = document.createElement("tr");
//    td.textContent = dado;
//    td.classList.add(classe);

//    return td;
//}

//function validaColunas(colunas) {

//    var erros = [];

//    if (colunas.nomeColuna.length == 0) erros.push("O nome da coluna não pode ser nullo");

//    return erros;

//}

//function exibeMensagemDeerros(erros) {
//    var ul = document.querySelector("#mensagem-erro");
//    ul.innerHTML = "";


//    erros.forEach(function (erro) {
//        var li = document.createElement("li");
//        li.textContent = erro;
//        ul.appendChild(li);
//    });
//}

//function adicionaColunasTabela(colunas) {

//    var pacienteTr = montarTR(colunas);
//    var tabela = document.querySelector("#tabela-colunas");
//    tabela.appendChild(pacienteTr);
//}



//function CriarCampo(event) {
//    event.preventDefault();
//    $("#Form1").append('<div><label>Coluna:</label><asp:TextBox ID="txtColuna" runat="server" MaxLength="400"></asp:TextBox><asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtColuna" ValidationGroup="salvar" Text="*"></asp:RequiredFieldValidator></div>')
//}

//function crairCampo(event) {
//    event.preventDefault();
//    var i = 1;
//    i++;
//    $("#table-time").appendChild('<tr id="row' + i + '"><td><input type="text" name="name[]" id="name" placeholder="Enter Name" class="form-control name_list" /></td><td><button name="remove" id="' + i + '" class=btn-danger btn-remove>X</button></td></tr>')

//    $(document).on('click', 'btn_remove', function () {
//        var button_id = $(this).attr("id");
//        $("#row" + button_id + "").remove();
//        $('#row' + butt)
//    });


//    $("#submit").click(function () {
//        $.ajax({
//            url: "http://localhost:90/Manager/Modulos/CMS/Modulos/ModTabela/Tabela.aspx",
//            method: "POST",
//            data: $("add_name").serialize(),
//            success: function (data) {
//                alert(data);
//                $('#add_name')[0].reset();
//            }
//        });
//    });
//}
