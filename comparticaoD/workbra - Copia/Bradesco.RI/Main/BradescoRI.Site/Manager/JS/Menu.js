$(document).ready(function () {

    novo();

    $('#treeViewMenus').click(function (event) {
        event.stopPropagation();
    });

    $(".ddlPaginas").change(function () {
        if (this.value != "0") {
            $("#content_contentInterna_txtUrl").val(this.value);
        } else {
            $("#content_contentInterna_txtUrl").val('');
        }
    });
});

function selecionarNode(hierarquia, nome) {
    $.ajax({
        type: "POST",
        url: "Editar.aspx/SelecionarItem",
        data: "{'pstrHierarquia':'" + hierarquia + "'}",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        async: false,
        error: function (e) {
            console.log(e);
        },
        success: function (object) {
            $("#content_contentInterna_detalhesMenu").css('display', 'block');
            $("#content_contentInterna_txtNome").focus();
            $("#content_contentInterna_hdnAcao").val('Editar');
            $("#content_contentInterna_lblDescricao").text('Edição de item de Menu: ' + nome + '');
            $("#content_contentInterna_hdnHierarquia").val(hierarquia);

            //Limpar formulário
            limparFormulario();

            //Preenche campos
            preencherFormulario(object.d);
        }
    });
    this.event.stopPropagation();
}

function addNodeAbaixo(hierarquia, nome) {
    //Limpar formulário
    limparFormulario();

    $("#content_contentInterna_detalhesMenu").css('display', 'block');
    $("#content_contentInterna_txtNome").focus();
    $("#content_contentInterna_hdnAcao").val('Adicionar');
    $("#content_contentInterna_lblDescricao").text('Novo item de Menu em: ' + nome);
    $("#content_contentInterna_hdnHierarquia").val(hierarquia);

    this.event.stopPropagation();
}

function excluirNode(hierarquia, nome) {
    if (confirm('Deseja realmente excluir o item de menu ' + nome + '?')) {
        $.ajax({
            type: "POST",
            url: "Editar.aspx/DeletarItemMenu",
            data: "{'pstrHierarquia':'" + hierarquia + "'}",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            async: false,
            error: function (e) {
                console.log(e);
            },
            success: function (object) {
                window.location.href = window.location.href;
            }
        });
    }

    this.event.stopPropagation();
}

function fecharDetalhes() {
    $("#content_contentInterna_detalhesMenu").css('display', 'none');
}

function limparFormulario() {
    $(".frmTxt").val('');
    $(".frmDropdown").val('_self');
}

function preencherFormulario(data) {
    $("#content_contentInterna_hdnHierarquia").val(data.Hierarquia);
    $("#content_contentInterna_txtNome").val(data.Nome);
    $("#content_contentInterna_txtUrl").val(data.Url);
    $("#content_contentInterna_ddlTarget").val(data.Target);
    $("#content_contentInterna_ddlIdioma").val(data.IdiomaId);
    $("#content_contentInterna_txtChave").val(data.ChaveNome);
    $("#content_contentInterna_txtEstilo").val(data.CssClass);
}

function novo() {
    if (getParameterByName('Hierarquia') == null || $(".btnAddMenu").length == 0) {
        $("#content_contentInterna_detalhesMenu").css('display', 'block');
    }
}

function getParameterByName(name, url) {
    if (!url) url = window.location.href;
    name = name.replace(/[\[\]]/g, "\\$&");
    var regex = new RegExp("[?&]" + name + "(=([^&#]*)|&|#|$)"),
        results = regex.exec(url);
    if (!results) return null;
    if (!results[2]) return '';
    return decodeURIComponent(results[2].replace(/\+/g, " "));
}