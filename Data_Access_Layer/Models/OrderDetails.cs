using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Access_Layer.Models
{
    public class OrderDetails
    {

        [Key]
        public int OrderDetailId { get; set; }
        [Required]
        public int OrderHeaderId { get; set; }
        [Required]
        public int CarListId { get ; set; }
        [ForeignKey("CarListId")]
        public CarList CarList { get; set; }

        [Required]
        public string ItemName { get; set; }
        [Required]
        public double Price { get; set; }
    }
}
