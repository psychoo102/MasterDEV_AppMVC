using System;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace MVC.Models.Validations
{
    public class IsSemanticVersion: ValidationAttribute
    {
        string pattern = @"^(0|[1-9]\d*)\.(0|[1-9]\d*)\.(0|[1-9]\d*)(?:-((?:0|[1-9]\d*|\d*[a-zA-Z-][0-9a-zA-Z-]*)(?:\.(?:0|[1-9]\d*|\d*[a-zA-Z-][0-9a-zA-Z-]*))*))?(?:\+([0-9a-zA-Z-]+(?:\.[0-9a-zA-Z-]+)*))?$";

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            string Version = (string)value;
            if(Version != null)
                if (Regex.Matches(Version, pattern).Count > 0)
                    return null;
            return new ValidationResult("Wersja nie jest poprawna");
            
        }
    }
}