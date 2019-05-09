$('select').on('change', function () {
    if (this.value == 1 || this.value == 3) {
        $("#divemail").hide();
        $("#divpassword").hide();
        $("#divcognom").hide();

    } else {
        $("#divemail").show()
        $("#divpassword").show();
    }

    if (this.value == 2) {
        $("#divcognom").hide();
    } else if (this.value == 0) {
        $("#divcognom").show();
    }
});