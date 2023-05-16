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
    public class OrderHeaderUpdateDTO
    {
      
        public int OrderHeaderId { get; set; }
        public string PickupName { get; set; }
        
        public string PickupPhoneNumber { get; set; }
  
        public string PickupEmail { get; set; }

        public string StripePaymentIntentID { get; set; }
        public string Status { get; set; }
    }
}
