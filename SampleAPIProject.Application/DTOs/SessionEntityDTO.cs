using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MLMProject.Application.DTOs
{
    public class SessionEntityDTO
    {
        [Key]
        public int SessionID { get; set; }
        public int AdminSession { get; set; }
        public int UserSession { get; set; }
    }
}
