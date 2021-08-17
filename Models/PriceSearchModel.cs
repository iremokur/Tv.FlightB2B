using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Flightb2b.Models
{
    public class PriceSearchModel
    {
        public string ProductType { get; set; }
        public string ServiceType { get; set; }
        public string CheckIn { get; set; } //roundtrip includes checkout 
        public string Passengers { get; set; }
        public string departureLocations { get; set; }
        public string arrivalLocations { get; set; }
        public string Culture { get; set; }
        public string Currency { get; set; }
 
      
    }
}
