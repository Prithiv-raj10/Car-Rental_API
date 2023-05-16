using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Access_Layer.Models.DTO
{
    public  class LoginResponseDTO
    {
        public string Email { get; set; }
        public string Token { get; set; }
    }
}
