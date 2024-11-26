using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MLMProject.Domain.Entities
{
    public class ResetPasswordRequest
    {
        public required string token { get; set; }
        public int? UserId { get; set; }
        public required string Password { get; set; }

    }

    public class AdminResetPasswordRequest
    {
       
        public int UserId { get; set; }
        public required string Password { get; set; }

    }
}
