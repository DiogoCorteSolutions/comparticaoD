function openModal(tipo) {      
    if (tipo == 'E') {
        $("#divModalErro").show();
        $("#divModalEdicaoModulos").hide();
    } else if (tipo == 'M') {
        $("#divModalErro").hide();
        $("#divModalEdicaoModulos").show();
    }
    $("#black_overlay").show();
}

function closeModal() {
    $(".ModalMsg").hide();
    $("#black_overlay").hide();
}
