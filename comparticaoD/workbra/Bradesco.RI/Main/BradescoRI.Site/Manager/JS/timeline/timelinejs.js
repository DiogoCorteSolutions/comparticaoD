$(document).ready(function () {

    $('body').undelegate('.bnImgVisivel', 'click');
    $('body').delegate('.bnImgVisivel', 'click', function () {

        var id = $(this).data("id");
        var div = '.imgVisivel-' + id;

        $('.divImgOpenClose').hide();
        $(div).show(1000);
    });
    var teste = $('#table-timeline');
    console.log(teste);
});



