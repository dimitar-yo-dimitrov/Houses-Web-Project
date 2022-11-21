//Floating point Regex validation. Double and Decimal can work whit point and comma
jQuery(function ($) {
    $.validator.addMethod("number",
        function(value, element) {
            return this.optional(element) || /^(?:-?\d+)(?:(\.|,)\d+)?$/.test(value);
        });
});

//Javascript to auto-format phone mask
$(function () {
    $("#call_caller_phone").keyup(function () {
        this.value = this.value.replace(/(\d{4})\-?(\d{3})\-?(\d{3})/, "$1-$2-$3");
        //alert ("OK");
    });
});