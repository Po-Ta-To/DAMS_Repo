using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using DAMS_03.Models;
using System.Web;

namespace DAMS_03.API
{
    public class BookingsController : ApiController
    {
        private DAMS_01Entities db = new DAMS_01Entities();

        // GET: api/Bookings/1
        [ResponseType(typeof(Booking))]
        public IHttpActionResult getBookingsByCHid(int id)
        {
            List<Booking> bookings = (from Booking in db.Bookings
                                     where Booking.ClinicHospitalID == id
                                     select new Booking
                                     {
                                         BookingDate = Booking.BookingDate,
                                         IsFullyBooked = Booking.IsFullyBooked
                                     }).ToList();

            if(bookings == null)
            {
                return NotFound();
            }

            return Ok(bookings);
        }

    }
}
