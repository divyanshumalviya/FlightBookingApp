﻿using System;
using System.Collections.Generic;

namespace FBAPIGateway.Models
{
    public partial class Airline
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string FlightNumber { get; set; }
        public string ContactNo { get; set; }
        public string Address { get; set; }
    }
}
