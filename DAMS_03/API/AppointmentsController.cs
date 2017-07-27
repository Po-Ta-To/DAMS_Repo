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
            Appointment foundAppt = db.Appointments.Find(id);
            if (foundAppt == null)
            {
                return NotFound();
            }

            var appointment = from Appointment in db.Appointments
                                    where Appointment.ID == id
                                    join User in db.UserAccounts on Appointment.UserID equals User.ID
                                    join ClinicHospital in db.ClinicHospitals on Appointment.ClinicHospitalID equals ClinicHospital.ID
                                    select new
                                    {
                                        ID = Appointment.ID,
                                        AppointmentID = Appointment.AppointmentID,
                                        UserName = User.Name,
                                        UserID = User.ID,
                                        ClinicHospitalName = ClinicHospital.ClinicHospitalName,
                                        ClinicHospitalID = ClinicHospital.ID,
                                        ApprovalState = Appointment.ApprovalState,
                                        PreferredDate = Appointment.PreferredDate,
                                        PreferredTime = Appointment.PreferredTime,
                                        Remarks = Appointment.Remarks,
                                        AppointmentDate = Appointment.AppointmentDate,//
                                        AppointmentTime = Appointment.AppointmentTime//
                                    };

            string approvalString = "";

            AppointmentDetailViewModel returnAppointment = new AppointmentDetailViewModel();

            foreach (var appt in appointment)
            {
                switch (appt.ApprovalState)
                {
                    case 1:
                        approvalString = "Pending";
                        break;
                    case 2:
                        approvalString = "Cancelled";
                        break;
                    case 3:
                        approvalString = "Confirmed";
                        break;
                    case 4:
                        approvalString = "Declined";
                        break;
                    case 5:
                        approvalString = "Completed";
                        break;
                    default:
                        approvalString = "Error";
                        break;
                }

                var reqDoc = (from apt in db.Appointments
                              join d in db.DoctorDentists on apt.RequestDoctorDentistID equals d.ID
                              where apt.ID == appt.ID
                              select new
                              {
                                  RequestDoctorDentistName = d.Name,
                                  RequestDoctorDentistID = d.ID
                              }).SingleOrDefault();

                var doc = (from apt in db.Appointments
                           join d in db.DoctorDentists on apt.DoctorDentistID equals d.ID
                           where apt.ID == appt.ID
                           select new
                           {
                               DoctorDentistName = d.Name,
                               DoctorDentistID = d.ID
                           }).SingleOrDefault();

                returnAppointment = new AppointmentDetailViewModel()
                {
                    ID = appt.ID,
                    AppointmentID = appt.AppointmentID,
                    UserName = appt.UserName,
                    UserID = appt.UserID,
                    ClinicHospitalName = appt.ClinicHospitalName,
                    ClinicHospitalID = appt.ClinicHospitalID,
                    ApprovalState = approvalString,
                    PreferredDate = appt.PreferredDate,
                    PreferredTime = appt.PreferredTime,
                    Remarks = appt.Remarks,
                    AppointmentDate = appt.AppointmentDate,
                    AppointmentTime = appt.AppointmentTime
                };

                if (reqDoc != null)
                {
                    returnAppointment.RequestDoctorDentistName = reqDoc.RequestDoctorDentistName;
                    returnAppointment.RequestDoctorDentistID = reqDoc.RequestDoctorDentistID;
                }
                else
                {
                    returnAppointment.RequestDoctorDentistName = "No preference";
                    //addModel.RequestDoctorDentistID = 0;
                }

                if (doc != null)
                {
                    returnAppointment.DoctorDentistName = doc.DoctorDentistName;
                    returnAppointment.DoctorDentistID = doc.DoctorDentistID;
                }
                else
                {
                    returnAppointment.DoctorDentistName = "Unassigned";
                    //addModel.DoctorDentistID = 0;
                }
            }
            return Ok(returnAppointment);
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