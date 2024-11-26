using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MLMProject.Domain.Entities
{
    public class Counts
    {
        public string? ActiveUsersCount { get; set; }
        public string? DectiveUsersCount { get; set; }
        public string? OrdersCount { get; set; }
        public string? ProductsCount { get; set; }
        public string? AdminStaffCount { get; set; }
        public string? KYCCount { get; set; }
        public string? KYCPendingCount { get; set; }
        public string? UserDistributershipPendingCount { get; set; }
    }
}
