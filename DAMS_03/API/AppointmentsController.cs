﻿using System;
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
    [RoutePrefix("api/Appointments")]
    public class AppointmentsController : ApiController
    {
        private DAMS_01Entities db = new DAMS_01Entities();

        // GET: api/Appointments
        public IQueryable<Appointment> GetAppointments()
        {
            return db.Appointments;
        }

        // GET: api/Appointments/5
        [ResponseType(typeof(Appointment))]
        public IHttpActionResult GetAppointment(int id)
        {
            Appointment appointment = db.Appointments.Find(id);
            if (appointment == null)
            {
                return NotFound();
            }

            var returnAppointment = from Appointment in db.Appointments
                                    where Appointment.ID == id
                                    select new
                                    {
                                        Appointment.ID,
                                        Appointment.AppointmentID,
                                        Appointment.UserID,
                                        Appointment.ClinicHospitalID,
                                        Appointment.ApprovalState,
                                        //Appointment.PreferredDate,
                                        //Appointment.PreferredTime,
                                        Appointment.DoctorDentistID,
                                        //Appointment.RequestDoctorDentistID,
                                        Appointment.Remarks,
                                        Appointment.AppointmentDate,
                                        Appointment.AppointmentTime
                                    };

            return Ok(returnAppointment);
        }

        // #KK    
        // GET: api/Appointments/GetAppointmentsByUserID/2
        [Route("GetAppointmentsByUserID")]
        //[ResponseType(typeof(Appointment))]
        public IHttpActionResult GetAppointmentsByUserID(int id)
        {
            var appointments = from Appointment in db.Appointments
                               where Appointment.UserID == id
                               select new
                               {
                                   Appointment.ID,
                                   Appointment.AppointmentID,
                                   Appointment.UserID,
                                   Appointment.ClinicHospitalID,
                                   Appointment.ApprovalState,
                                   //Appointment.PreferredDate,
                                   //Appointment.PreferredTime,
                                   Appointment.DoctorDentistID,
                                   //Appointment.RequestDoctorDentistID,
                                   Appointment.Remarks,
                                   Appointment.AppointmentDate,
                                   Appointment.AppointmentTime
                               };

            if (appointments == null)
            {
                return NotFound();
            }

            return Ok(appointments);
        }

        // PUT: api/Appointments/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutAppointment(int id, Appointment appointment)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != appointment.ID)
            {
                return BadRequest();
            }

            db.Entry(appointment).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AppointmentExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/Appointments
        [ResponseType(typeof(Appointment))]
        public IHttpActionResult PostAppointment(Appointment appointment)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Appointments.Add(appointment);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = appointment.ID }, appointment);
        }

        // DELETE: api/Appointments/5
        [ResponseType(typeof(Appointment))]
        public IHttpActionResult DeleteAppointment(int id)
        {
            Appointment appointment = db.Appointments.Find(id);
            if (appointment == null)
            {
                return NotFound();
            }

            db.Appointments.Remove(appointment);
            db.SaveChanges();

            return Ok(appointment);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool AppointmentExists(int id)
        {
            return db.Appointments.Count(e => e.ID == id) > 0;
        }
    }
}