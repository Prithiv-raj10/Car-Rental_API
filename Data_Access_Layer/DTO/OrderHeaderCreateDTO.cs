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
    public class OrderHeaderCreateDTO
    {
        [Required]
        public string PickupName { get; set; }
        [Required]
        public string PickupPhoneNumber { get; set; }
        [Required]
        public string PickupEmail { get; set; }

        public string ApplicationUserId { get; set; }

        public double OrderTotal { get; set; }
        public string StripePaymentIntentID { get; set; }
        public string Status { get; set; }
        public IEnumerable<OrderDetailsCreateDTO> OrderDetailsDTO { get; set; }
    }
}
