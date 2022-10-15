using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace EmiratesIslamic.CustomAttributes.Validation;

public class RequiredFileAttribute : ValidationAttribute, IClientModelValidator
{
    private readonly string _classIdProperty;

    public RequiredFileAttribute(string classIdProperty)
    {
        if (string.IsNullOrWhiteSpace(classIdProperty))
            throw new ArgumentException("This parameter shouldn't be null or whitespace",
                nameof(classIdProperty));

        _classIdProperty = classIdProperty;
    }

    protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
    {
        if (value is not IFormFile file)
            throw new InvalidCastException();

        if (file is { Length: > 0 })
            return ValidationResult.Success;

        var propertyInfo = validationContext.ObjectType.GetProperty(_classIdProperty);
        var propertyValue = propertyInfo!.GetValue(validationContext.ObjectInstance, null);
        FormatErrorMessage(validationContext);
        if (propertyValue == null)
            return new ValidationResult(ErrorMessage);

        return int.Parse(propertyValue.ToString()!) > 0 ? ValidationResult.Success : new ValidationResult(ErrorMessage);
    }

    private void FormatErrorMessage(ValidationContext context)
    {
        var displayName = context.DisplayName;
        ErrorMessage = ErrorMessage == null ?
            $"The {displayName} field is required." :
            string.Format(ErrorMessage, displayName);
    }

    public void AddValidation(ClientModelValidationContext context)
    {
        ErrorMessage = string.IsNullOrWhiteSpace(ErrorMessage) ?
            "The file is required." : string.Format(ErrorMessage, context.Attributes["name"]);

        MergeAttribute(context.Attributes, "data-val", "true");
        MergeAttribute(context.Attributes, "data-val-requiredfile", ErrorMessage);
        MergeAttribute(context.Attributes, "data-val-requiredfile-classidproperty", _classIdProperty);
    }

    private static void MergeAttribute(IDictionary<string, string> attributes, string key, string value)
    {
        if (!attributes.ContainsKey(key))
            attributes.Add(key, value);
    }
}