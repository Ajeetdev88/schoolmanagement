using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MLMProject.Application.DTOs
{
    public class ErrorEntitiesDTO
    {
        [Key]
        public int LogId { get; set; }
        public string? ControllerName { get; set; }
        public string? MethodName { get; set; }
        public string? ErrorMessage { get; set; }
        public DateTime? CreatedOn { get; set; }
        public string? StackTrace { get; set; }
        public string? InnerException { get; set; }
        public bool? IsDeleted { get; set; }

    }
}
