$ = window.$;
jQuery = window.jQuery;

// helper for work with other fields value
sandtrapValidation = {
    getDependentElement: function (validationElement, dependentProperty) {
        var dependentElement = $('#' + dependentProperty);
        if (dependentElement.length === 1) {
            return dependentElement;
        }
        var name = validationElement.name;
        var index = name.lastIndexOf(".") + 1;
        var id = (name.substr(0, index) + dependentProperty).replace(/[\.\[\]]/g, "_");
        dependentElement = $('#' + id);
        if (dependentElement.length === 1) {
            return dependentElement;
        }
        // Try using the name attribute
        name = (name.substr(0, index) + dependentProperty);
        dependentElement = $('[name="' + name + '"]');
        if (dependentElement.length > 0) {
            return dependentElement.first();
        }
        return null;
    }
}

$(function () {

    /*  start required file if id zero */
    jQuery.validator.addMethod('requiredfile', function (value, element, params) {
        var isValid = true;
        if ((value !== '') && (value !== null) && (value !== undefined))
            return true;

        if (($(params.classidproperty).val() === '') ||
            ($(params.classidproperty).val() === null) ||
            ($(params.classidproperty).val() === undefined) ||
            ($(params.classidproperty).val() === '0'))
            isValid = false;
        return isValid;
    });

    jQuery.validator.unobtrusive.adapters.add('requiredfile', ['classidproperty'], function (options) {
        var element = options.element;
        var classidporpertyName = options.params.classidproperty;
        var classidproperty = sandtrapValidation.getDependentElement(element, classidporpertyName);
        options.rules['requiredfile'] = {
            classidproperty: classidproperty
        };
        options.messages['requiredfile'] = options.message;
    });
    /*  start required file if id zero */

}(jQuery));