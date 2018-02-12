$(document).ready(function () {

    //$('body').undelegate('.bnImgVisivel', 'click');
    //$('body').delegate('.bnImgVisivel', 'click', function () {

    //    var id = $(this).data("id");
    //    var div = '.imgVisivel-' + id;

    //    $('.divImgOpenClose').hide();
    //    $(div).show(1000);
    //});


    $('.body').undelegate('.visivel', 'click');
    $('.body').undelegate('.bngimgvisivel', 'click', function () {
        var id = $(this).data("id");
        var div = '.imvisivel-' + id;

        $('.divImpOpenClose').hide();
        $(div).show(1000);
    });

    var cont = document.querySelectorAll('#ano-time2');
    console.log(cont);
});



