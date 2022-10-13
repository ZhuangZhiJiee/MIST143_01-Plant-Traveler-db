using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MIST143_Traveler.Models;
using MIST143_Traveler.Models.miViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace MIST143_Traveler.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly PlanetTravelContext _PlanetTravelContext;
        public LoginController(PlanetTravelContext PlanetTrave)
        {
            _PlanetTravelContext = PlanetTrave; 
        }
        public string LoginApi()
        {
            return "123";
        }
        [HttpPost]
        public string LoginApi(CLogin Value)
        {
            var User = _PlanetTravelContext.Members.
                Where(a => a.Email == Value.Email && a.Password == Value.Password).SingleOrDefault();
            if (User == null)
            {
                return "無法登入";
            }
            else
            {
                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name,User.Email),
                    new Claim("FullName",User.Password)
                };
                var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity));
                return "OK";
            }

        }
        [HttpDelete]
        public void logout()
        {
            HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        }
        [HttpGet("NoLogin")]
        public string noLogin()
        {
            return "未登入";
        }
    }
}
