﻿using Azure;
using Business_Logic_Layer.Interfaces;
using Business_Logic_Layer.Services;
using Data_Access_Layer.Interfaces;
using Data_Access_Layer.Models;
using Data_Access_Layer.Models.DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace CarRental_API.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class AuthController : ControllerBase
    {

        private readonly IAuthBLL _auth;
        public AuthController(IAuthBLL auth)
        {

            this._auth = auth;
        }


        [HttpPost("login")]
        public Task<Status> Login([FromBody] LoginRequestDTO model)
        {

            var res = _auth.Login(model);

            return res;
        }


        [HttpPost("register")]
        public Task<Status> Register([FromBody] RegisterRequestDTO model)
        {
            var status = new Status();
            var userFromDb = _auth.Register(model);

            //if (userFromDb != null)
            //{
            //    status.StatusCode = (int)HttpStatusCode.OK;

            //    status.Message = "Username already exists";

            //    return Ok(status);

            //}

            //status.StatusCode = (int)HttpStatusCode.OK;

            //status.Message = "Registered successfully";
            return userFromDb;

        }
    }
}
