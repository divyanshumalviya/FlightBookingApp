using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using UserServices.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace UserServices.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        DBFlightBookingContext context = new DBFlightBookingContext();

        [Route("SearchFlight")]
        [HttpGet]
        public IActionResult SearchFlight(string fromPlace = "", string to = "", TimeSpan start = new TimeSpan(), TimeSpan end = new TimeSpan(), int totalPassanger = 1)
        {
            try
            {
                var result = from s in context.Schedule
                             join a in context.Airline on s.FlightNumber equals a.FlightNumber
                             where s.Status == "Active" && s.To.Contains(to) && s.From.Contains(fromPlace)
                             && s.Start == start && s.End == end
                             select new
                             {
                                 FlightName = a.Name,
                                 FlightNumber = a.FlightNumber,
                                 Source = s.From,
                                 Destination = s.To,
                                 Departure = s.Start,
                                 Arrival = s.End,
                                 TotalPrice = s.Nbcprice * totalPassanger
                             };
                return Ok(result.ToList());
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public class BookingDTO
        {
            public BookingDetails bDetails { get; set; }
            public List<Passanger> listPassangers { get; set; }
            public string email { get; set; }
        }

        [HttpPost]
        [Route("AddBooking/{flightId}")]
        public void AddBooking(BookingDTO booking, string flightId)
        {
            Schedule schedule = context.Schedule.Where(x => x.FlightNumber == flightId).FirstOrDefault();
            User user = context.User.Where(x => x.Email == booking.email).FirstOrDefault();

            booking.bDetails.UserId = user.Id;
            booking.bDetails.FlightNumber = flightId;
            booking.bDetails.Pnr = Guid.NewGuid();
            booking.bDetails.Status = "Active";
            booking.bDetails.TotalPrice = schedule.Nbcprice * booking.bDetails.TotalSeats;
            context.BookingDetails.Add(booking.bDetails);
            foreach (Passanger pass in booking.listPassangers)
            {
                pass.Pnr = booking.bDetails.Pnr;
                context.Passanger.Add(pass);
            }
            context.SaveChanges();

        }

        [Route("ViewBooking")]
        [HttpGet]
        public IActionResult ViewBooking(Guid pnr)
        {
            try
            {
                var result = from bd in context.BookingDetails
                             join a in context.Airline on bd.FlightNumber equals a.FlightNumber
                             join s in context.Schedule on a.FlightNumber equals s.FlightNumber
                             join p in context.Passanger on bd.Pnr equals p.Pnr
                             join u in context.User on bd.UserId equals u.Id
                             where bd.Pnr == pnr
                             select new
                             {
                                 FlightName = a.Name,
                                 FlightNumber = a.FlightNumber,
                                 Source = s.From,
                                 Destination = s.To,
                                 Departure = s.Start,
                                 Arrival = s.End,
                                 TotalSeats = bd.TotalSeats,
                                 TotalPrice = bd.TotalPrice,
                                 PassangerName = p.Name,
                                 Age = p.Age,
                                 Gender = p.Gender,
                                 SeatNo = p.SeatNo,
                                 BookingStatus = bd.Status
                             };
                return Ok(result.ToList());
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        [Route("CancelBooking/{pnr}")]
        public void CancelBooking(Guid pnr)
        {
            BookingDetails bdetail = context.BookingDetails.FirstOrDefault(x => x.Pnr == pnr);
            bdetail.Status = "Canceled";
            context.SaveChanges();
        }

        [Route("BookingHistory")]
        [HttpGet]
        public IActionResult BookingHistory(string email)
        {
            try
            {
                var result = from bd in context.BookingDetails
                             join a in context.Airline on bd.FlightNumber equals a.FlightNumber
                             join s in context.Schedule on a.FlightNumber equals s.FlightNumber
                             join p in context.Passanger on bd.Pnr equals p.Pnr
                             join u in context.User on bd.UserId equals u.Id
                             where u.Email == email
                             select new
                             {
                                 PNR = bd.Pnr,
                                 FlightName = a.Name,
                                 FlightNumber = a.FlightNumber,
                                 Source = s.From,
                                 Destination = s.To,
                                 Departure = s.Start,
                                 Arrival = s.End,
                                 TotalSeats = bd.TotalSeats,
                                 TotalPrice = bd.TotalPrice,
                                 PassangerName = p.Name,
                                 Age = p.Age,
                                 Gender = p.Gender,
                                 SeatNo = p.SeatNo,
                                 BookingStatus = bd.Status,
                                 BookedBy= u.Email
                             };
                return Ok(result.ToList());
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        // GET: api/<UserController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<UserController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<UserController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<UserController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<UserController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
