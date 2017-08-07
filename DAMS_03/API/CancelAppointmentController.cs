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
    public class CancelAppointmentController : ApiController
    {

        private DAMS_01Entities db = new DAMS_01Entities();

        // GET: api/CancelAppointment/5
        public IHttpActionResult Get(int? id)
        {
            if (id == null)
            {
                return BadRequest();
            }
            Appointment appointment = db.Appointments.Find(id);
            if (appointment == null)
            {
                return NotFound();
            }

            appointment.ApprovalState = 2;

            db.Entry(appointment).State = EntityState.Modified;
            db.SaveChanges();
            return Ok(appointment);
        }
        
    }
}
