
using Flightb2b.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TourVisio.WebService.Adapter.Models;
using TourVisio.WebService.Adapter.ServiceModels.ProductModels;
using TourVisio.WebService.Client;
using TourVisio.WebService.Adapter.Enums;
using Microsoft.AspNetCore.Http;
using TourVisio.WebService.Adapter.ServiceModels.BookingModels;

namespace Flightb2b.Controllers
{
    public class ProductController : Controller
    {
        
        public IActionResult Flight()
        {
            var flightSearchForm = new FlightModel();
            return View(flightSearchForm);
        }

        public ActionResult ChangeServiceType(FlightModel flightSearchForm, string type) {

            flightSearchForm.ServiceType = type;
            return View("Flight", flightSearchForm);
        }

        [HttpPost]
        public ActionResult PriceSearch([FromForm] FlightModel flightSearchForm)
        {
            var T3_servicebase_url = "http://t3-services.tourvisio.com/v2/";
            char[] delimiterChars = { '(', ')' };
            string[] Iddep = flightSearchForm.DepartureLocation.Name.Split(delimiterChars);
            string[] Idarr = flightSearchForm.ArrivalLocation.Name.Split(delimiterChars);

            var prequest = new PriceSearchRequest()
            {
                ProductType = flightSearchForm.ProductType,
                ServiceTypes = new List<string>() { flightSearchForm.ServiceType },
                DepartureLocations=new List<mdlLocation>() { new mdlLocation{ Id=Iddep[1].ToString(), Type=enmLocationType.Airport} },
                ArrivalLocations = new List<mdlLocation>() { new mdlLocation { Id = Idarr[1].ToString(), Type = enmLocationType.Airport } },
                CheckIn = flightSearchForm.CheckIn,
                Passengers = flightSearchForm.Passengers,
                Culture = flightSearchForm.Culture,
                Currency = flightSearchForm.Currency,
              
                
               
            };
            if (flightSearchForm.ServiceType == "2")
            {
                prequest.Night = (flightSearchForm.CheckOut - flightSearchForm.CheckIn).Days;
            }

            ProductRepository product = new ProductRepository("eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJEQiI6IlRPVVJWSVNJTyIsIldJZCI6IjUiLCJBRyI6IkIyQiIsIkFOYW1lIjoiQjJCIEFnZW5jeSIsIk1SIjoiR0VSTUFOIiwiT0YiOiJCRVIiLCJPUCI6IlNBTiIsIlVTIjoiQjJCIiwiQVQiOiIwIiwiV1QiOiIxIiwiT0EiOiIxIiwiUEYiOiIwIiwiUFQiOlsiMiIsIjMiLCIyLDMiLCIxMiIsIjQiLCI1IiwiMSIsIjYiLCIxNCIsIjExIl0sIkRQIjoiMSIsIlRUIjoiMSIsIlVSb2xlIjpbIjEiLCIyIiwiMyIsIjYiLCI3IiwiOCJdLCJUaWQiOiIyNDcxNjYiLCJuYmYiOjE2MzA1NjMwNjksImV4cCI6MTYzMDU5OTA2OSwiaWF0IjoxNjMwNTYzMDY5fQ.R6kpvHymtlUGGmtVstHRyqxioAL0rLuKTV2gik9G0KE", T3_servicebase_url);
            var response = product.PriceSearch(prequest);
            if (!response.Header.Success)
            {
                ViewBag.error = "No search results were found matching your criteria.!";
                return RedirectToAction("Flight","Product");
            }
            if (response.Header.Success) {
                flightSearchForm.Flight = response.Body.Flights;
            }

            return PartialView("FlightList", flightSearchForm);
        }
     

    }
}
