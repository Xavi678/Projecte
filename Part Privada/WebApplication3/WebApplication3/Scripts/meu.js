
$(document).ready(function () {

    if ($("#tipus").val() == 1|| $("#tipus").val() == 3) {
        $("#divemail").hide();
        $("#divpassword").hide();
        $("#divcognoms").hide();
        $("#divdata").hide();
        $("#divtelefon").hide();
    } else if ($("#tipus").val() == 2) {

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


    $('#tipus').on('change', function () {
        if (this.value == 1 || this.value == 3) {
            $("#divemail").hide();
            $("#divpassword").hide();
            $("#divcognom").hide();
            $("#divtelefon").hide();
            $("#divdata").hide();

        } else {
            $("#divemail").show()
            $("#divpassword").show();
            $("#divtelefon").show();
            $("#divdata").show();
        }

        if (this.value == 2) {
            $("#divcognom").hide();
        } else if (this.value == 0) {
            $("#divcognom").show();
        }
    });

});