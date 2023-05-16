using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Access_Layer.Models
{
    public class RentItem
    {
        public int Id { get; set; }
        public int CarListId { get; set; }
        [ForeignKey("CarListId")]
        public CarList CarList { get; set; } = new();
        public int BookingId { get; set; }
    }
}
