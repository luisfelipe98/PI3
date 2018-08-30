//EFEITO LUPA
$(".img_lupa").click(function () {
    $(".menu_produto").css("display", "none");
    $("#busca").slideDown("slow").css("display", "block");
    $("#buscar_botao").slideDown("slow").css("display", "block");
    $(this).css('display', 'none');
    $(".x").css('display', 'block');
});

$(".x").click(function () {
    $("#busca").slideUp("slow");
    $("#buscar_botao").slideUp("slow");
    $(".menu_produto").css("display", "block");
    $(this).css('display', 'none');
    $(".img_lupa").css('display', 'block');
});


//EFEITO AUTOCOMPLETE
$(function () {
    var Categorias = [
        "Doces",
        "Bebidas",
        "Hortifruti",
        "Frios e laticínios",
        "Higiene",
        "Marcearia",
        "Pães",
        "Coca-Cola",
        "Pepsi",
        "Batata",
        "Guaraná Antartica",
        "Kiwi",
        "Avelã",
        "Fini Tubes",
        "Trindent",
        "Papel Higiênico",
        "Maça",
        "Banana"
    ];
    $("#busca").autocomplete({
        source: Categorias
    });
});

//EFEITO DRAG AND DROP
$(".product-image").draggable({
    helper: "clone"
});
$(".carrinho").droppable({
    drop: function (event, ui) {
        var vezes;
        vezes++;
        $(this)
            .find("span")
            .text();      
    }
});


//Mais e Menos do Carrinho

$(".mais").click(function () {

    var cont = 1;
    var qtde = $(".qtde").val();
    var cont = cont + parseInt(qtde);
    $(".qtde").val(cont);

});

$(".menos").click(function () {

    var cont = 1;
    var qtde = $(".qtde").val();
    var cont = parseInt(qtde) - cont;

    if (cont == 1) {
        $(".qtde").val(1);
    } else {
        $(".qtde").val(cont);
    }
 
});


// Mostrar/Esconder Senha

var clickou = true;

$("#mostrar_senha").click(function () {

        if (clickou) {

            $("#inputSenha").find("input").attr("type", "text");
            $("#inputSenha").find("i").attr("class", "far fa-eye-slash");
            clickou = false;

        } else {

            $("#inputSenha").find("input").attr("type", "password");
            $("#inputSenha").find("i").attr("class", "far fa-eye");
            clickou = true;

        }
    
});
