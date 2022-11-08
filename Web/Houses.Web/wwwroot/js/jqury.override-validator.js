var jQuery = function() { throw new Error("Not implemented"); };
jQuery(function ($) {
    $.validator.addMethod("number",
        function(value, element) {
            return this.optional(element) || /^(?:-?\d+|-?\d{1,3}(?:,\d{3})+)?(?:(\.|,)\d+)?$/.test(value);
        });
});