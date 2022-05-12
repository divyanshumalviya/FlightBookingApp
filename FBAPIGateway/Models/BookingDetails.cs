using System;
using System.Collections.Generic;

namespace FBAPIGateway.Models
{
    public partial class BookingDetails
    {
        public int Id { get; set; }
        public Guid Pnr { get; set; }
        public int UserId { get; set; }
        public string FlightNumber { get; set; }
        public string Status { get; set; }
        public int TotalSeats { get; set; }
        public int TotalPrice { get; set; }
        public DateTime JourneyDate { get; set; }
    }
}
