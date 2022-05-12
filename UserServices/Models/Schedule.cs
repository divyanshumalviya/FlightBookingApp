using System;
using System.Collections.Generic;

namespace UserServices.Models
{
    public partial class Schedule
    {
        public int Id { get; set; }
        public string FlightNumber { get; set; }
        public string From { get; set; }
        public string To { get; set; }
        public TimeSpan Start { get; set; }
        public TimeSpan End { get; set; }
        public string ScheduledDays { get; set; }
        public int Bcseats { get; set; }
        public int Nbcseats { get; set; }
        public int Bcprice { get; set; }
        public int Nbcprice { get; set; }
        public string Meal { get; set; }
        public string Status { get; set; }
    }
}
