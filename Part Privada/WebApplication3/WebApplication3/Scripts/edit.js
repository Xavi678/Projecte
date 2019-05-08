/*$('select').on('change', function () {
    if (this.value == 1 || this.value == 3) {
        $("#divemail").hide();
        $("#divpassword").hide();
    } else {
        $("#divemail").show()
        $("#divpassword").show();
    }
});*/

$(document).ready(function () {

    if ($("#tipus").val() == "Autor" || $("#tipus").val() == "Director") {
        $("#divemail").hide();
        $("#divpassword").hide();
        $("#divcognoms").hide();
        $("#divdata").hide();
        $("#divtelefon").hide();
    } else if ($("#tipus").val() == "Administrador") {
        
        $("#divcognoms").hide();
    }


    /*alert($('select').val());

    if ($('select').val() == 1 || $('select').val() == 3) {
        $("#divemail").hide();
        $("#divpassword").hide();
    } else {
        $("#divemail").show()
        $("#divpassword").show();
    }*/
})