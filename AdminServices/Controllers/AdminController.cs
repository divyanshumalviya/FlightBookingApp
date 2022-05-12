using AdminServices.Models;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace AdminServices.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        DBFlightBookingContext context = new DBFlightBookingContext();

        [HttpPost]
        [Route("AddUser")]
        public void AddUser(User user)
        {
            context.User.Add(user);
            context.SaveChanges();
        }

        [HttpGet]
        [Route("GetAllUser")]
        public List<User> GetAllUser()
        {
            return context.User.ToList();
        }

        public class LoginRequest
        {           
            public string email { get; set; }
            public string password { get; set; }
            public string userType { get; set; }
        }

        [HttpPost]
        [Route("LoginAdmin")]
        public string LoginAdmin( [FromBody] LoginRequest req)
        {
            if(req.userType=="Admin")
            {
                User user = context.User.Where(x => x.Email == req.email && x.Password == req.password && x.IsAdmin == true).FirstOrDefault();
                if (user != null)
                    return "Login succesfull";
                else
                    return "Bad Credentials";
            }
            else if(req.userType == "User")
            {
                User user = context.User.Where(x => x.Email == req.email && x.Password == req.password && x.IsAdmin == false).FirstOrDefault();
                if (user != null)
                    return "Login succesfull";
                else
                    return "Bad Credentials";
            }
            else return "Bad Credentials";

        }

        [HttpPost]
        [Route("AddAirline")]        
        public void AddAirline(Airline airline)
        {
            context.Airline.Add(airline);
            context.SaveChanges();
        }

        [HttpGet]
        [Route("GetAllAirline")]
        public List<Airline> GetAllAirline()
        {
            return context.Airline.ToList();
        }


        [HttpPost]
        [Route("AddSchedule")]
        public void AddSchedule(Schedule sched)
        {
            context.Schedule.Add(sched);
            context.SaveChanges();
        }

        [HttpGet]
        [Route("GetAllSchedule")]
        public List<Schedule> GetAllSchedule()
        {
            return context.Schedule.ToList();
        }

        [HttpGet]
        [Route("GetScheduleByFlightId")]
        public IActionResult GetScheduleByFlightId(string flightNo)
        {
            var result= context.Schedule.Where(x => x.FlightNumber == flightNo).FirstOrDefault();
            return Ok(result);
        }

        [HttpPost]
        [Route("UpdateSchedule/{flightNo}")]
        public void UpdateSchedule(Schedule newSch, string flightNo)
        {
            Schedule oldSch = context.Schedule.Where(x => x.FlightNumber == flightNo).FirstOrDefault();
            oldSch.FlightNumber = newSch.FlightNumber;
            oldSch.From = newSch.From;
            oldSch.To = newSch.To;
            oldSch.Start = newSch.Start;
            oldSch.End = newSch.End;
            oldSch.ScheduledDays = newSch.ScheduledDays;
            oldSch.Bcseats = newSch.Bcseats;
            oldSch.Nbcseats = newSch.Nbcseats;
            oldSch.Bcprice = newSch.Bcprice;
            oldSch.Nbcprice = newSch.Nbcprice;
            oldSch.Meal = newSch.Meal;
            oldSch.Status = newSch.Status;

            context.SaveChanges();
        }

        [HttpPost("authenticate")]
        public IActionResult Authenticate ([FromBody] UserCred userCred)
        {
            return Ok();
        }

        // GET: api/<AdminController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "TestValue", "value2" };
        }

        // GET api/<AdminController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<AdminController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<AdminController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<AdminController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
