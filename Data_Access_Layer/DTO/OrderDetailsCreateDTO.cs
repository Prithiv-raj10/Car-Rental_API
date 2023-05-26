using Data_Access_Layer.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Access_Layer.DTO
{
    public class OrderDetailsCreateDTO
    { 
        [Required]
        public int CarListId { get; set; }

        [Required]
        public string ItemName { get; set; }
        [Required]
        public double Price { get; set; }
    }
}
