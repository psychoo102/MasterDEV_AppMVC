using System.ComponentModel.DataAnnotations;

namespace MVC.Models.Validations
{
    public class IsZIPFile : ValidationAttribute
    {

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            IFormFile file = (IFormFile)value;
            if(value != null)
                if (file.FileName.EndsWith(".zip"))
                    return ValidationResult.Success;
            return new ValidationResult("Plik musi być archiwum .zip");
        }
    }
}