using System;
using System.Collections.Generic;

namespace FBAPIGateway.Models
{
    public partial class Passanger
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Gender { get; set; }
        public int Age { get; set; }
        public Guid Pnr { get; set; }
        public string SeatNo { get; set; }
    }
}
