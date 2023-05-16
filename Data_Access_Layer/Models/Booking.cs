using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Access_Layer.Models
{
    public class Booking
    {
        public int Id { get; set; }
        public string UserId { get; set; }
       
        public ICollection<RentItem> RentItems { get; set; }


        [NotMapped]
        public string StripePaymentIntentId { get; set; }
        [NotMapped]
        public string ClientSecret { get; set; }

        [NotMapped]
        public double Total { get; set; }
    }
}
