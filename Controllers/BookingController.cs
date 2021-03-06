using Flightb2b.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TourVisio.WebService.Adapter.Enums;
using TourVisio.WebService.Adapter.Models;
using TourVisio.WebService.Adapter.Models.Booking;
using TourVisio.WebService.Adapter.ServiceModels.BookingModels;
using TourVisio.WebService.Adapter.ServiceModels.LookupModels;
using TourVisio.WebService.Client;

namespace Flightb2b.Controllers
{
    public class BookingController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public ActionResult BeginTransaction(FlightModel flightSearchForm)
        {

            var T3_servicebase_url = "http://t3-services.tourvisio.com/v2/";
            PassengerModel passForm = new PassengerModel();
            var prequest = new BeginTransactionRequest()
            {
                OfferIds = new string[] { flightSearchForm.OfferIDIn },
                Culture = flightSearchForm.Culture,
                Currency = flightSearchForm.Currency
            };
          
          
            BookingRepository book = new BookingRepository("eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJEQiI6IlRPVVJWSVNJTyIsIldJZCI6IjUiLCJBRyI6IkIyQiIsIkFOYW1lIjoiQjJCIEFnZW5jeSIsIk1SIjoiR0VSTUFOIiwiT0YiOiJCRVIiLCJPUCI6IlNBTiIsIlVTIjoiQjJCIiwiQVQiOiIwIiwiV1QiOiIxIiwiT0EiOiIxIiwiUEYiOiIwIiwiUFQiOlsiMiIsIjMiLCIyLDMiLCIxMiIsIjQiLCI1IiwiMSIsIjYiLCIxNCIsIjExIl0sIkRQIjoiMSIsIlRUIjoiMSIsIlVSb2xlIjpbIjEiLCIyIiwiMyIsIjYiLCI3IiwiOCJdLCJUaWQiOiIyNDcyNDAiLCJuYmYiOjE2MzA2NDk0MTAsImV4cCI6MTYzMDY4NTQxMCwiaWF0IjoxNjMwNjQ5NDEwfQ.uYVMP4EBWiFbOa6mETjLUo51DfM59WUevxDPPHHZyQM", T3_servicebase_url);
            var response = book.BeginTransaction(prequest);
            if (!response.Header.Success)
            {
                flightSearchForm.Error = response.Header.Messages.First().Message;
                return RedirectToAction("Flight", "Product");
            }
            else
            {
                passForm.TransactionResponse= response.Body;
                passForm.Travellers = response.Body.ReservationData.Travellers;
                LookupRepository lookup1 = new LookupRepository("eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJEQiI6IlRPVVJWSVNJTyIsIldJZCI6IjUiLCJBRyI6IkIyQiIsIkFOYW1lIjoiQjJCIEFnZW5jeSIsIk1SIjoiR0VSTUFOIiwiT0YiOiJCRVIiLCJPUCI6IlNBTiIsIlVTIjoiQjJCIiwiQVQiOiIwIiwiV1QiOiIxIiwiT0EiOiIxIiwiUEYiOiIwIiwiUFQiOlsiMiIsIjMiLCIyLDMiLCIxMiIsIjQiLCI1IiwiMSIsIjYiLCIxNCIsIjExIl0sIkRQIjoiMSIsIlRUIjoiMSIsIlVSb2xlIjpbIjEiLCIyIiwiMyIsIjYiLCI3IiwiOCJdLCJUaWQiOiIyNDcyNDAiLCJuYmYiOjE2MzA2NDk0MTAsImV4cCI6MTYzMDY4NTQxMCwiaWF0IjoxNjMwNjQ5NDEwfQ.uYVMP4EBWiFbOa6mETjLUo51DfM59WUevxDPPHHZyQM", "https://t3-services.tourvisio.com/v2/");
                var rResponse = lookup1.GetNationalities(new GetNationalitiesRequest());
                if (rResponse.Header.Success)
                {
                    passForm.Nationalities = new List<mdlNationality>();
                    foreach (var nationality in rResponse.Body.Nationalities)
                    {
                        var pNationality = new mdlNationality { Name = nationality.Name, TwoLetterCode = nationality.Id, ISDCode = nationality.ISDCode };
                        passForm.Nationalities.Add(pNationality);
                    }
                }
                else {
                    flightSearchForm.Error = rResponse.Header.Messages.First().Message;
                }
                
               
            }
            return PartialView("SetReservation", passForm);
        }
        [HttpPost]
        public ActionResult SetReservation(PassengerModel passForm)
        {
            
            var T3_servicebase_url = "http://t3-services.tourvisio.com/v2/";
            
            foreach (var pas in passForm.Travellers.ToList())
            {

                if (!pas.RequiredFields.Contains("address.contactPhone.areaCode"))
                {
                    pas.Address.ContactPhone.PhoneNumber = pas.Address.ContactPhone.AreaCode + pas.Address.ContactPhone.PhoneNumber;

                }
            };
            var prequest = new SetReservationInfoRequest()
            {
                TransactionId = passForm.TransactionResponse.TransactionId,
                Travellers = passForm.Travellers.ToArray()          
                
            };
            
            BookingRepository addServ = new BookingRepository("eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJEQiI6IlRPVVJWSVNJTyIsIldJZCI6IjUiLCJBRyI6IkIyQiIsIkFOYW1lIjoiQjJCIEFnZW5jeSIsIk1SIjoiR0VSTUFOIiwiT0YiOiJCRVIiLCJPUCI6IlNBTiIsIlVTIjoiQjJCIiwiQVQiOiIwIiwiV1QiOiIxIiwiT0EiOiIxIiwiUEYiOiIwIiwiUFQiOlsiMiIsIjMiLCIyLDMiLCIxMiIsIjQiLCI1IiwiMSIsIjYiLCIxNCIsIjExIl0sIkRQIjoiMSIsIlRUIjoiMSIsIlVSb2xlIjpbIjEiLCIyIiwiMyIsIjYiLCI3IiwiOCJdLCJUaWQiOiIyNDcyNDAiLCJuYmYiOjE2MzA2NDk0MTAsImV4cCI6MTYzMDY4NTQxMCwiaWF0IjoxNjMwNjQ5NDEwfQ.uYVMP4EBWiFbOa6mETjLUo51DfM59WUevxDPPHHZyQM", T3_servicebase_url);
            var response = addServ.SetReservationInfo(prequest);
            if (!response.Header.Success)
            {
                passForm.Error = response.Header.Messages.First().Message;
                return RedirectToAction("BeginTransaction", "Booking");
            }

           return CommitTransaction(passForm);
        }

        [HttpPost]
        public ActionResult CommitTransaction(PassengerModel passForm)
        {

            var T3_servicebase_url = "http://t3-services.tourvisio.com/v2/";

            var prequest = new CommitTransactionRequest()
            {
                TransactionId = passForm.TransactionResponse.TransactionId,
      
            };
            BookingRepository addServ = new BookingRepository("eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJEQiI6IlRPVVJWSVNJTyIsIldJZCI6IjUiLCJBRyI6IkIyQiIsIkFOYW1lIjoiQjJCIEFnZW5jeSIsIk1SIjoiR0VSTUFOIiwiT0YiOiJCRVIiLCJPUCI6IlNBTiIsIlVTIjoiQjJCIiwiQVQiOiIwIiwiV1QiOiIxIiwiT0EiOiIxIiwiUEYiOiIwIiwiUFQiOlsiMiIsIjMiLCIyLDMiLCIxMiIsIjQiLCI1IiwiMSIsIjYiLCIxNCIsIjExIl0sIkRQIjoiMSIsIlRUIjoiMSIsIlVSb2xlIjpbIjEiLCIyIiwiMyIsIjYiLCI3IiwiOCJdLCJUaWQiOiIyNDcyNDAiLCJuYmYiOjE2MzA2NDk0MTAsImV4cCI6MTYzMDY4NTQxMCwiaWF0IjoxNjMwNjQ5NDEwfQ.uYVMP4EBWiFbOa6mETjLUo51DfM59WUevxDPPHHZyQM", T3_servicebase_url);
            var response = addServ.CommitTransaction(prequest);
            if (!response.Header.Success)
            {
                passForm.Error = response.Header.Messages.First().Message;
                return RedirectToAction("BeginTransaction", "Booking");
            }
            else if (response.Header.Success)
            {
                passForm.TransactionId = response.Body.TransactionId;
                passForm.BookingNumber = response.Body.ReservationNumber;
                var request = new GetReservationDetailRequest()
                {
                    ReservationNumber = response.Body.ReservationNumber
                }; 
                var response1 = addServ.GetReservationDetail(request);

                if (response1.Header.Success)
                {
                    passForm.Travellers = response1.Body.ReservationData.Travellers;
                    passForm.Services = response1.Body.ReservationData.Services;
                    passForm.TotalPrice = response1.Body.ReservationData.ReservationInfo.TotalPrice;

                }
                else {

                    passForm.Error = response.Header.Messages.First().Message;
                }
            }

            return PartialView("ReservationResult",passForm);
        }
 
        [Route("Booking/GetReservationDetail/{rezNo?}")]
         public ActionResult GetReservationDetail(string rezNo, PassengerModel passForm)
        {

            var T3_servicebase_url = "http://t3-services.tourvisio.com/v2/";

      
            var prequest = new GetReservationDetailRequest()
            {
               ReservationNumber= rezNo

            };

            BookingRepository addServ = new BookingRepository("eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJEQiI6IlRPVVJWSVNJTyIsIldJZCI6IjUiLCJBRyI6IkIyQiIsIkFOYW1lIjoiQjJCIEFnZW5jeSIsIk1SIjoiR0VSTUFOIiwiT0YiOiJCRVIiLCJPUCI6IlNBTiIsIlVTIjoiQjJCIiwiQVQiOiIwIiwiV1QiOiIxIiwiT0EiOiIxIiwiUEYiOiIwIiwiUFQiOlsiMiIsIjMiLCIyLDMiLCIxMiIsIjQiLCI1IiwiMSIsIjYiLCIxNCIsIjExIl0sIkRQIjoiMSIsIlRUIjoiMSIsIlVSb2xlIjpbIjEiLCIyIiwiMyIsIjYiLCI3IiwiOCJdLCJUaWQiOiIyNDcyNDAiLCJuYmYiOjE2MzA2NDk0MTAsImV4cCI6MTYzMDY4NTQxMCwiaWF0IjoxNjMwNjQ5NDEwfQ.uYVMP4EBWiFbOa6mETjLUo51DfM59WUevxDPPHHZyQM", T3_servicebase_url);
            var response = addServ.GetReservationDetail(prequest);
            if (response.Header.Success)
            {
                passForm.Travellers = response.Body.ReservationData.Travellers;
                passForm.ReservationInfo = response.Body.ReservationData.ReservationInfo;
                passForm.Services = response.Body.ReservationData.Services;
            }
            else
            {

                passForm.Error = response.Header.Messages.First().Message;
            }

            return PartialView("GetReservationDetail",passForm);
        }




    }
}
