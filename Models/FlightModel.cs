using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using TourVisio.WebService.Adapter.Enums;
using TourVisio.WebService.Adapter.Models;
using TourVisio.WebService.Adapter.Models.Product.Flight;
namespace Flightb2b.Models
{
    public class FlightModel
    {
     
        // ONE WAY
      
        [Required]
        [Display(Name = "From")]
        public mdlLocation DepartureLocation { get; set; }
        
        [Display(Name = "To")]
        public mdlLocation ArrivalLocation { get; set; }
        public string depId { get; set; }
        public string arrId { get; set; }

        [Display(Name = "CheckIn")]
        public DateTime CheckIn { get; set; } 
        [Display(Name = "CheckOut")]
        public DateTime CheckOut { get; set; } 
        public enmProductType ProductType { get; set; }
        public string ServiceType { get; set; }
        public List<mdlPassengerHandler> Passengers { get; set; }
        public string Culture { get; set; }
        public string Currency { get; set; }
        public string OfferIDIn { get; set; }
        public string Error { get; set; }
        public string OfferIDOut { get; set; }
        public enmLocationType? Type { get; set; }
        public IEnumerable<mdlFlight> Flight { get; set; }
        public string Query { get; set; }

        public FlightModel()
        {
            this.Passengers = new List<mdlPassengerHandler>() {
            new mdlPassengerHandler {Type = enmPassangerType.Adult, Count = 0},
            new mdlPassengerHandler {Type = enmPassangerType.Child, Count = 0},
            new mdlPassengerHandler {Type = enmPassangerType.Infant, Count = 0},

            };
            this.Type = enmLocationType.Airport;
            this.ProductType = enmProductType.Flight;
            this.Culture = "en-US";
            this.Currency = "EUR";
            this.OfferIDIn = "";
            this.OfferIDOut = "";
        }


    }
}
