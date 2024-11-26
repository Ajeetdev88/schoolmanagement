using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace MLMProject.Application.DTOs
{
    public class WelcomeMessageDTO
    {

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Heading { get; set; }  // Assumes a foreign key relationship to a User entity
        public string Content { get; set; }
        public string? pathurl { get; set; }
        public string? imageUrl { get; set; }
        //public IFormFile image { get; set; }
        public string buttonname { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public bool IsActive { get; set; }
    }
}
