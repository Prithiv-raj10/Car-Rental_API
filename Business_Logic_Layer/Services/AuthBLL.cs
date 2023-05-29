using Data_Access_Layer.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data_Access_Layer.Models.DTO;
using Business_Logic_Layer.Interfaces;
using Microsoft.Extensions.Logging;
using System.Net;

namespace Business_Logic_Layer.Services
{
    public class AuthBLL : IAuthBLL
    {
        private readonly IAuthRepository _authRepo;
        private readonly ILogger<AuthBLL> _logger;
        public AuthBLL(IAuthRepository authRepo, ILogger<AuthBLL> logger)
        {

            _authRepo = authRepo;
            _logger = logger;
        }
        public Task<Status> Login([FromBody] LoginRequestDTO model)
        {
            var status = new Status();
            try
            {
                var res = _authRepo.Login(model);
                _logger.LogInformation("Login attempt started");
                return res;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while logging in the user");
                status.StatusCode = (int)HttpStatusCode.InternalServerError;
                status.Message = "An error occurred while processing the request";
                return Task.FromResult(status);
            }
        }

        public Task<Status> Register([FromBody] RegisterRequestDTO model)
        {
            var status = new Status();
            try
            {
                var res = _authRepo.Register(model);
                _logger.LogInformation("Registration process started for user");
                return res;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while registering the user");
                status.StatusCode = (int)HttpStatusCode.InternalServerError;
                status.Message = "An error occurred while processing the request";
                return Task.FromResult(status);
            }
        }
    }
}
