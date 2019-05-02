$('select').on('change', function () {
    if (this.value == 1 || this.value == 3) {
        $("#divemail").hide();
        $("#divpassword").hide();
    } else {
        $("#divemail").show()
        $("#divpassword").show();
    }
});