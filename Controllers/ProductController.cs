
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TourVisio.WebService.Adapter.ServiceModels.ProductModels;
using TourVisio.WebService.Client;

namespace Flightb2b.Controllers
{
    public class ProductController : Controller
    {
        //[AuthenticationFilter]
        public IActionResult Flight()
        {
            return View();
        }
  
    }
}
