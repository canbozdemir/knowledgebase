using KnowledgeBase.Api.Models;
using KnowledgeBase.Core.Infrastructure.Context;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace KnowledgeBase.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class TokenController : ControllerBase
    {
        private readonly IConfiguration _config;
        private readonly IHttpRequest _httpRequest;

        public TokenController(IConfiguration config, IHttpRequest httpRequest)
        {
            this._config = config;
            this._httpRequest = httpRequest;
        }

        [AllowAnonymous]
        [HttpPost]
        public IActionResult CreateToken([FromBody]LoginModel login)
        {
            IActionResult response = Unauthorized();
            UserModel user = Authenticate(login);

            if (user != null)
            {
                string token = BuildToken(user);
                user.Token = token;

                response = Ok(user);
            }

            return response;
        }

        private string BuildToken(UserModel user)
        {
            Claim[] claims = new[] {
                new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
               };

            SymmetricSecurityKey key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            SigningCredentials creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            JwtSecurityToken token = new JwtSecurityToken(_config["Jwt:Issuer"],
              _config["Jwt:Issuer"],
              claims,
              expires: DateTime.Now.AddMinutes(30),
              signingCredentials: creds);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        private UserModel Authenticate(LoginModel login)
        {
            UserModel user = null;

            //for testing purposes - user domain/service not yet created
            if (login.UserName == "test" && login.Password == "test")
            {
                user = new UserModel { Id = 1, Name = "Can Bozdemir", UserName = "test" };
            }

            if (login.UserName == "test2" && login.Password == "test")
            {
                user = new UserModel { Id = 2, Name = "Mertkan Bozdemir", UserName = "test" };
            }

            return user;
        }
    }
}