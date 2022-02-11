
using Administration.Domain.Models.Login;
using Administration.Service.Interfaice.Admin;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

using Microsoft.AspNetCore.Cors;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using JwtRegisteredClaimNames = Microsoft.IdentityModel.JsonWebTokens.JwtRegisteredClaimNames;

namespace Administration.Microservice.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController : BaseController
    {
        #region Fields
        private readonly UserManager<IdentityUser<int>> _userManager;
        private readonly IAdminService _adminService;
        private IConfiguration _configuration;
        #endregion

        #region Ctor

        public AdminController(IAdminService adminService, UserManager<IdentityUser<int>> userManager, IConfiguration configuration)
        {
            this._adminService = adminService;
            this._userManager = userManager;
            this._configuration = configuration;
        }

        #endregion

        #region Methods
        
        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login(LoginModel model)
        {

            var user = await this._userManager.FindByNameAsync(model.UserName);
            if (user != null && await _userManager.CheckPasswordAsync(user, model.Password))
            {
                var userRoles = await _userManager.GetRolesAsync(user);

                var authClaims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, user.UserName),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                };

                foreach (var userRole in userRoles)
                {
                    authClaims.Add(new Claim(ClaimTypes.Role, userRole));
                }
                var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Secret"]));

                var token = new JwtSecurityToken(
                    issuer: _configuration["JWT:ValidIssuer"],
                    audience: _configuration["JWT:ValidAudience"],
                    expires: DateTime.Now.AddHours(3),
                    claims: authClaims,
                    signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
                );

                return Ok(new
                {
                    token = new JwtSecurityTokenHandler().WriteToken(token),
                    expiration = token.ValidTo
                });

            }
            return Unauthorized();
        }



        [HttpGet("user/list")]
        public async Task<ActionResult> GetUsers()
        {
            // var users = await _adminService.GetUsers();
            return Ok(true);
        }

        //[HttpPost("user/create")]
        //public async Task<ActionResult> CreateUser()
        //{

        //   // var users = await _adminService.CreateUser();
        //    return Ok(true);
        //}


        #endregion
    }
}
