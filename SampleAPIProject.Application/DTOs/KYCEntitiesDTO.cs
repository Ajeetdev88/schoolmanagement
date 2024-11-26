using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MLMProject.Application.DTOs
{
    public class KYCEntitiesDTO
    {
        [Key]
        public int KYCFormId { get; set; }
        public Guid? KYCFormGUID { get; set; } = Guid.NewGuid();
        public int UserId { get; set; }

        [Required]
        [StringLength(100)]
        public required string UserName { get; set; }

        [Required]
        [StringLength(50)]
        public required string UserCode { get; set; }

        [Required]
        [StringLength(15)]
        public required string Mobile { get; set; }

        [Required]
        [StringLength(500)]
        public required string Address { get; set; }

        [Required]
        [StringLength(100)]
        public required string City { get; set; }

        [Required]
        [StringLength(10)]
        //[RegularExpression(@"^\d{6}$", ErrorMessage = "Invalid Pin Code")]
        public required string AreaPinCode { get; set; }

        [Required]
        [StringLength(50)]
        public required string State { get; set; }

        [Required]
        [StringLength(20)]
        public required string PanNo { get; set; }

        [Required]
        [StringLength(12)]
        //[RegularExpression(@"^\d{12}$", ErrorMessage = "Invalid Aadhar number")]
        public required string AadharNo { get; set; }

        [Required]
        [StringLength(100)]
        public required string Nominee { get; set; }

        [Required]
        [StringLength(50)]
        public required string RelationshipWithNominee { get; set; }

        // Bank Details
        [Required]
        [StringLength(100)]
        public required string AccountHolderName { get; set; }

        [Required]
        [StringLength(20)]
        public required string AccountNo { get; set; }

        [Required]
        [StringLength(100)]
        public required string BankName { get; set; }

        [Required]
        [StringLength(100)]
        public required string BranchName { get; set; }

        [Required]
        [StringLength(11)]
        //[RegularExpression(@"^[A-Za-z]{4}\d{7}$", ErrorMessage = "Invalid IFSC Code")]
        public required string IFSCCode { get; set; }

        // Image URLs
        [Url]
        public string? UserPanCardUrl { get; set; }

        [Url]
        public string? AadharCardFrontUrl { get; set; }

        [Url]
        public string? AadharCardBackUrl { get; set; }

        [Url]
        public string? PassbookPhotoUrl { get; set; }

        // Image file
        [Required]
        [NotMapped]

        public required IFormFile UserPanCardfile { get; set; }

        [Required]
        [NotMapped]

        public required IFormFile AadharCardFrontfile { get; set; }

        [Required]
        [NotMapped]
        public required IFormFile AadharCardBackfile { get; set; }

        [Required]
        [NotMapped]
        public required IFormFile PassbookPhotofile { get; set; }

        public bool? IsApproved { get; set; } = false;

        public bool? IsRejected { get; set; } = false;

        public int? ApprovedBy { get; set; }

        public string? Remark { get; set; }

        public DateTime? SubmitionDate { get; set; } = DateTime.UtcNow;

        public DateTime? ApprovalDate { get; set; }
    }
}
