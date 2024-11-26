using MLMProject.Domain.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MLMProject.Domain.Entities
{
    public class AdminAuth
    {
        [Key]
        public int UserId { get; set; }
        public Guid UserGuid { get; set; } = Guid.NewGuid();

        public string? UserCode { get; set; }

        [Required]
        [StringLength(100, MinimumLength = 3, ErrorMessage = "Username must be between 3 and 100 characters.")]
        public string UserName { get; set; }

        [Required]
        [StringLength(100, MinimumLength = 3, ErrorMessage = "Name must be between 3 and 100 characters.")]
        public string Name { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [Display(Name = "Date of Birth")]
        public DateTime DOB { get; set; }

        [Required]
        [StringLength(256)]
        public string EmailAddress { get; set; }

        [Required]
        [PhoneNumberValidation(ErrorMessage = "Invalid Indian phone number. Must start with 6, 7, 8, or 9 and be 10 digits long.")]
        [StringLength(10, MinimumLength = 10)]
        public string PhoneNumber { get; set; }

        public int UserTypeId { get; set; }

        [Required]
        [EmailComplexValidation(ErrorMessage = "Password must contain at least one uppercase letter, one numeric digit, one special character, and be at least 8 characters long.")]
        [StringLength(256)]
        public string Password { get; set; }

        [Required]
        [EmailComplexValidation(ErrorMessage = "Transaction Password must contain at least one uppercase letter, one numeric digit, one special character, and be at least 8 characters long.")]
        [StringLength(256)]
        public string TPassword { get; set; }
        [Required]
        public DateTime CreatedDate { get; set; } = DateTime.Now;
        public int CreatedBy { get; set; }
        public bool? IsDeleted { get; set; } = false;
        public bool? IsLocked { get; set; } = false;
        public bool? IsActive { get; set; } = true;
        public bool? IsPrivacyPolicyChecked { get; set; } = false;
        public int? FailedLoginAttempts { get; set; }
        public DateTime? LastFailedLoginAttempt { get; set; }
    }
}
