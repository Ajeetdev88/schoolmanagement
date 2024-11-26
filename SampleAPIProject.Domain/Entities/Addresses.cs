using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MLMProject.Domain.Entities
{
    public class Addresses
    {
        [Key]
        public int AddressId { get; set; }
        public required string Name { get; set; }
        public int UserId { get; set; }
        public required string MobileNumber { get; set; }
        public required string Pincode { get; set; }
        public string? HouseNumber { get; set; }
        public string? Locality { get; set; }
        public string? Landmark { get; set; }
        public required string City { get; set; }
        public required string State { get; set; }
        public DateTime? CreatedDate { get; set; } = DateTime.UtcNow;
        public DateTime? ModifiedDate { get; set; } 
        public bool IsDeleted { get; set; } = false;
        public bool IsActive { get; set; } = false;
        public bool IsBillingAddress { get; set; } = false;
        public bool IsShippingAddress { get; set; } = false;
    }
}
