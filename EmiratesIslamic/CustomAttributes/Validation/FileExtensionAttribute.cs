using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;

namespace EmiratesIslamic.CustomAttributes.Validation;

public class FileExtensionAttribute : ValidationAttribute
{
    private readonly ICollection<string> _allowedExtensions;
    public FileExtensionAttribute(params string[] allowedExtensions)
    {
        _allowedExtensions = new List<string>();
        foreach (var allowedExtension in allowedExtensions)
            _allowedExtensions.Add($".{allowedExtension.ToLower()}");
    }
    protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
    {
        if (value == null)
            return ValidationResult.Success;

        if (value is not IFormFile file)
            throw new InvalidCastException();

        var validExtensions = _allowedExtensions.Contains(Path.GetExtension(file.FileName).ToLower());
        FormatErrorMessage(validationContext);
        return validExtensions ? ValidationResult.Success : new ValidationResult(ErrorMessage);
    }

    private void FormatErrorMessage(ValidationContext context)
    {
        var displayName = context.DisplayName;
        var joinedAllowed = string.Join(", ", _allowedExtensions);
        ErrorMessage = ErrorMessage == null ?
            $"The {displayName} file extension should be one of the following: {joinedAllowed}" :
            string.Format(ErrorMessage, displayName, joinedAllowed);
    }
}