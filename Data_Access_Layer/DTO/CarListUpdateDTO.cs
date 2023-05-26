using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Access_Layer.Models.DTO
{
    public class CarListUpdateDTO
    {
        public string? Name { get; set; }
        
        public string? Description { get; set; }
        
        public string? SpecialTag { get; set; }
        
        public string? Category { get; set; }
        
        public double? Rent { get; set; }
    }
}
