using System;
using System.Collections.Generic;

namespace AdminServices.Models
{
    public partial class User
    {
        public int Id { get; set; }
        public string Fname { get; set; }
        public string Lname { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public bool IsAdmin { get; set; }
    }
}
