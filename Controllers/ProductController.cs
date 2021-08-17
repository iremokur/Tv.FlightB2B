
using Flightb2b.Models;
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
        
        public IActionResult Flight()
        {
            return View();
        }

        //[HttpPost]
        //public ActionResult GetPriceSearch([FromBody] PriceSearchModel priceform)
        //{
        //    var pRequest = new PriceSearchRequest()
        //    {

              
        //    };

        //    ProductRepository productRepo1 = new ProductRepository(User.Claims.First().Value, "https://t3-services.tourvisio.com/v2/");
        //    var rResponse = productRepo1.PriceSearch(pRequest);

        //    return PartialView();
        //}
    }
}
