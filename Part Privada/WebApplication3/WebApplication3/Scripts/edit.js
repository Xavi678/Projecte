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

    alert($('select').val());

    if ($('select').val() == 1 || $('select').val() == 3) {
        $("#divemail").hide();
        $("#divpassword").hide();
    } else {
        $("#divemail").show()
        $("#divpassword").show();
    }
})