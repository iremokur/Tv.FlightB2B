using Flightb2b.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using TourVisio.WebService.Adapter.Models.Utility;
using TourVisio.WebService.Adapter.ServiceModels.AuthenticationModels;
using TourVisio.WebService.Client;

namespace Flightb2b.Controllers
{
    public class AccountController : Controller
    {
       private SignInManager<IdentityUser>_signManager;
       string tokenuser = string.Empty;
        [Route("Login")]
        public IActionResult Login()
        {
            return View();
        }
    
        [HttpPost]
        public IActionResult SignIn([FromForm] LoginRequest pRequest)
        {
            var T3_servicebase_url = "http://t3-services.tourvisio.com/v2/";
           

            AuthenticationRepository authentication = new AuthenticationRepository("", T3_servicebase_url);

            var response = authentication.Login(pRequest);

            if (!response.Header.Success)
            {
                ViewBag.error = "Invalid Account!";
                return RedirectToAction("Login");
            }

            HttpContext.Session.SetString("Token", response.Body.Token);
            HttpContext.Response.Cookies.Append("Token", response.Body.Token);

            return RedirectToAction("Flight","Product");
        }
        // culture "en-US"
        // currency "EUR"


    }
}
