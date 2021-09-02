using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TourVisio.WebService.Adapter.Models.Booking;
using TourVisio.WebService.Adapter.Enums;
using System.ComponentModel.DataAnnotations;
using TourVisio.WebService.Adapter.ServiceModels.BookingModels;
using TourVisio.WebService.Adapter.Models;

namespace Flightb2b.Models
{
    public class PassengerModel
    {
        public mdlPassportInfo PassportInfo { get; set; }
        public Guid TransactionId { get; set; }
        public TransactionResponse TransactionResponse { get; set; }
        public mdlReservationInfo ReservationInfo { get; set; }
        public mdlCustomerInfo CustomerInfo { get; set; }
        public List<mdlTraveller> Travellers { get; set; }
        public List<mdlService> Services { get; set; }
        public List<mdlNationality> Nationalities { get; set; }
        public mdlPrice TotalPrice { get; set; }
        public string BookingNumber { get; set; }
        public PassengerModel()
        {
            this.TransactionResponse = new TransactionResponse();
        }
     }
}
