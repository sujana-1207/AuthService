using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using AuthService.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace AuthService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly ILogin _loginrepo;
        private IConfiguration _config;
        static readonly log4net.ILog _log4net = log4net.LogManager.GetLogger(typeof(LoginController));

        // GET: api/Login
        public LoginController(IConfiguration config,ILogin login)
        {
            _config = config;
            _loginrepo = login;
        }
        [Route("api/Login/{name}")]
        [HttpGet]
        public User GetUser(string username)
        {
            var u = _loginrepo.GetUser(username);
            return u;
        }

  
        //[HttpGet]
        //public IActionResult Get()
        //{
          //  return Content("Ok");
        //}

        // POST: api/Login
        [HttpPost]
        public IActionResult Login([FromBody] User Login)
        {
            IActionResult response = Unauthorized();
            _log4net.Info("Login attempt made by"+Login.Username);
            User u = GetUser(Login.Username);
            if( Login.Username==u.Username && Login.Password==u.Password)
            {
                var tokenString = GenerateJWTToken(Login);
                _log4net.Info("Token generated for" + Login.Username);
                response = Ok(new { token = tokenString }); ; 
                    
            }
            _log4net.Error("Login attempt failed by" + Login.Username);
            return response;
        }
        public string GenerateJWTToken(User user)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF32.GetBytes(_config["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub,user.Username),
                new Claim(JwtRegisteredClaimNames.Jti,Guid.NewGuid().ToString())
            };

            var token = new JwtSecurityToken(
                issuer: _config["Jwt:Issuer"],
                audience: _config["Jwt:Issuer"],
                claims,
                expires: DateTime.Now.AddMinutes(120),
                signingCredentials: credentials);

            var encodetoken = new JwtSecurityTokenHandler().WriteToken(token);
            return encodetoken;


        }

    }
}
