using System.ComponentModel.DataAnnotations;

namespace ECommerce.PL
{
    public class IsExpiredAttribute : ValidationAttribute
    {
        override protected ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            if (value is DateTime date)
            {
                if (date <= DateTime.Now)
                {
                    return new ValidationResult("The product is expired.");
                }
            }
            return ValidationResult.Success;
        }
    }
}
