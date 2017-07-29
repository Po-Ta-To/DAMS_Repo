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

namespace DAMS_03.API
{
    public class DocDenDateBkngByDocDenIDController : ApiController
    {
        private DAMS_01Entities db = new DAMS_01Entities();

        // GET: api/DocDenDateBkngByDocDenID/1
        [ResponseType(typeof(DoctorDentistDateBooking))]
        public IHttpActionResult GetDocDenDateBkngByDocID(int id)
        {
            var dddbList = (from DDDB in db.DoctorDentistDateBookings
                                                       where DDDB.DoctorDentistID == id
                                                       select new
                                                       {
                                                           DateOfBookings = DDDB.DateOfBookings,
                                                           Bookings = DDDB.Bookings,
                                                           DoctorDentistID = DDDB.DoctorDentistID
                                                       }).ToList();

            if(dddbList == null)
            {
                return NotFound();
            }

            return Ok(dddbList);
        }
    }
}
