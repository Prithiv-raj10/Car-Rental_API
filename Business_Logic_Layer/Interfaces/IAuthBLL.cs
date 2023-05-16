using Data_Access_Layer.Models.DTO;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business_Logic_Layer.Interfaces
{
    public interface IAuthBLL
    {
        Status Login([FromBody] LoginRequestDTO model);

        bool Register([FromBody] RegisterRequestDTO model);
    }
}
