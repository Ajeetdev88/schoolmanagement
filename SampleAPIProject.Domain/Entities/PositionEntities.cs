using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MLMProject.Domain.Entities
{
    public class PositionEntities
    {
        [Key]
        public int PositionId { get; set; }

        public string PositionName { get; set; }

        public DateTime CreatedDate { get; set; } = DateTime.Now;
        public int CreatedBy { get; set; }
        public bool? IsDeleted { get; set; } = false;
    }
}
