using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Access_Layer.Models.DTO
{
    public  class CarListCreateDTO
    {
        
        [Required]
        public string Name { get; set; }
        public string? Description { get; set; }
        public string? SpecialTag { get; set; }
        public string? Category { get; set; }
        [Range(1, int.MaxValue)]
        public double Rent { get; set; }
        public IFormFile ImageFile { get; set; }
    }
}
