using System;
using System.Collections.Generic;

namespace FBAPIGateway.Models
{
    public partial class Discount
    {
        public int Id { get; set; }
        public string CouponCode { get; set; }
        public int CouponAmount { get; set; }
    }
}
