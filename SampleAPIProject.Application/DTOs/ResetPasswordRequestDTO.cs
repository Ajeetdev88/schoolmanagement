using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MLMProject.Application.DTOs
{
    public class ResetPasswordRequestDTO
    {
        public string token { get; set; }
        public int? UserId { get; set; }
        public string Password { get; set; }

    }


    public class AdminResetPasswordRequestDTO
    {
        
        public int UserId { get; set; }
        public string Password { get; set; }

    }
}
