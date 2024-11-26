using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MLMProject.Application.DTOs
{
    public class LoginLogDTO
    {

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int LogId { get; set; }

        [Required]
        public int UserId { get; set; }  // Assumes a foreign key relationship to a User entity

        [Required]
        public DateTime LoginTime { get; set; }

        public DateTime? LogoutTime { get; set; }  // Nullable in case the user hasn't logged out yet

        [MaxLength(45)]
        public string? IPAddress { get; set; }

        [MaxLength(255)]
        public string? UserAgent { get; set; }  // Stores information about the user's browser or device

        // Navigation property for the related User entity
        // Uncomment the line below if you have a User entity and want to establish a relationship
        // public virtual User User { get; set; }
        public int OTP { get; set; }
        public string? OTPID { get; set; }

        public string? Token { get; set; }
    }
}
