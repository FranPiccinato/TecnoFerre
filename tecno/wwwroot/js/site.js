$(document).ready(function () {
    $(".alert").fadeTo(4000, 500).slideUp(500, function () {
        $(".alert").slideUp(500);
    });
});

function Autocompletar(i, elem) {
    var input = $(elem);
    var dropdown = input.closest('.dropdown');
    var menu = dropdown.find('.dropdown-menu');
    var listaContainer = dropdown.find('.lista-autocomplete');
    var listaItems = listaContainer.find('.dropdown-item');
    var sinResultados = dropdown.find('.sinResultados');

    listaItems.hide();
    listaItems.each(function () {
        $(this).data('value', $(this).text());
    });

    input.on("input", function (e) {
        var busqueda = input.val().toLowerCase();

        if (busqueda.length > 0) {

            menu.addClass('show');

            listaItems.each(function () {

                var txt = $(this).data('value');
                if (txt.toLowerCase().includes(busqueda.toLowerCase())) {
                    var txtComienzo = txt.toLowerCase().indexOf(busqueda);
                    var txtTermina = txtComienzo + busqueda.length;
                    var htmlR = txt.substring(0, txtComienzo) + '<em>' + txt.substring(txtComienzo, txtTermina) + '</em>' + txt.substring(txtTermina + length);
                    $(this).html(htmlR);
                    $(this).show();

                } else {

                    $(this).hide();

                }
            });

            var count = listaItems.filter(':visible').length;
            (count > 0) ? sinResultados.hide() : sinResultados.show();

        } else {
            listaItems.hide();
            dropdown.removeClass('open').removeClass('in');
            sinResultados.show();
        }
    });

    listaItems.on('click', function (e) {
        var txt = $(this).text().replace(/^\s+|\s+$/g, ""); 
        input.val(txt);
        menu.removeClass('show');
    });

}

$('.jAuto').each(Autocompletar);

$(document).mouseup(function (e) {
    if ($(e.target).closest(".dropdown").length === 0) {
        $('.dropdown-menu').removeClass('show');
    }
});


function buscar(i, elem) {
    var input = $(elem);
    input.on("input", function (e) {
        var busqueda = input.val().toLowerCase();
        if (busqueda.length > 0) {
            $('.btnBuscar').attr('href', '/Home/resultadoProducto/' + input.val() );

        } else {
            $('.btnBuscar').attr('href', '#');
        }
    });

}

$('.jAuto').each(buscar);



function AutocompletarC(i, elem) {
    var input = $(elem);
    var dropdown = input.closest('.dropdown');
    var menu = dropdown.find('.dropdown-menu');
    var listaContainer = dropdown.find('.lista-autocomplete');
    var listaItems = listaContainer.find('.dropdown-item');
    listaItems.each(function () {
        $(this).data('value', $(this).text());
    });

    input.on("click", function (e) {
        menu.addClass('show');
    });

    input.on("input", function (e) {
        var busqueda = input.val().toLowerCase();

        listaItems.each(function () {

            var txt = $(this).data('value');
            if (txt.toLowerCase().includes(busqueda.toLowerCase())) {
                $(this).show();
            } else {
                $(this).hide();
            }
        });
    });

    listaItems.on('click', function (e) {
        var txt = $(this).text().replace(/^\s+|\s+$/g, ""); 
        input.val(txt);
        menu.removeClass('show');
    });

}

$('.categoriaAuto').each(AutocompletarC);

