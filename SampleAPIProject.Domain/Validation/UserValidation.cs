using System;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace MLMProject.Domain.Validation
{
    public class EmailComplexValidationAttribute : ValidationAttribute
    {
        private readonly string _pattern = @"^(?=.*[A-Z])(?=.*\d)(?=.*[@#$%^&+=]).{8,}$";

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var email = value as string;

            if (string.IsNullOrEmpty(email))
            {
                return new ValidationResult("Email is required.");
            }

            var regex = new Regex(_pattern);
            if (!regex.IsMatch(email))
            {
                return new ValidationResult("Email must contain at least one uppercase letter, one numeric digit, one special character, and be at least 8 characters long.");
            }

            return ValidationResult.Success;
        }
    }

    public class PhoneNumberValidationAttribute : ValidationAttribute
    {
        private readonly string _pattern = @"^[6789]\d{9}$";

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var phoneNumber = value as string;

            if (string.IsNullOrEmpty(phoneNumber))
            {
                return new ValidationResult("Phone number is required.");
            }

            var regex = new Regex(_pattern);
            if (!regex.IsMatch(phoneNumber))
            {
                return new ValidationResult("Invalid Indian phone number. Must start with 6, 7, 8, or 9 and be 10 digits long.");
            }

            return ValidationResult.Success;
        }
    }
}
