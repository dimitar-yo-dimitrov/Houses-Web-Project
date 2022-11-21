//Floating point Regex validation. Double and Decimal can work whit point and comma
jQuery(function ($) {
    $.validator.addMethod("number",
        function(value, element) {
            return this.optional(element) || /^(?:-?\d+)(?:(\.|,)\d+)?$/.test(value);
        });
});