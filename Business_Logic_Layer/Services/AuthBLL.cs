using Data_Access_Layer.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data_Access_Layer.Models.DTO;
using Business_Logic_Layer.Interfaces;

namespace Business_Logic_Layer.Services
{
    public class AuthBLL : IAuthBLL
    {
        private readonly IAuthRepository _authRepo;
        public AuthBLL(IAuthRepository authRepo)
        {

            _authRepo = authRepo;
        }
        public Task<Status> Login([FromBody] LoginRequestDTO model)
        {
            //if(model == null) throw new ArgumentNullException("model");
            var res = _authRepo.Login(model);
            return res;
        }

        public Task<Status> Register([FromBody] RegisterRequestDTO model)
        {
            var res = _authRepo.Register(model);
            return res;
        }
    }
}
