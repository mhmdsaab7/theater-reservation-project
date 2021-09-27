using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TheaterPrjt.Business;
using TheaterPrjt.Entities;
using Newtonsoft.Json;

namespace TheaterPrjt.Controllers
{
    [ApiController]
    [Route("api/User")]
    public class UserController : ControllerBase
    {
        [HttpPost("Authenticate")]
        public User Authenticate(AuthenticateInput input)
        {
            return new UserManager().Authenticate(input.Username, input.Password);
        }

    }
    public class AuthenticateInput
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }
}
