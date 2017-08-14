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
    public class AppointmentsController : ApiController
    {
        private DAMS_01Entities db = new DAMS_01Entities();

        //// GET: api/Appointments
        //public IQueryable<Appointment> GetAppointments()
        //{
        //    return db.Appointments;
        //}

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
                                        AppointmentDate = Appointment.AppointmentDate,
                                        AppointmentTime = Appointment.AppointmentTime
                                    };

            string approvalString = "";

            AppointmentApiHelperModel returnAppointment = new AppointmentApiHelperModel();

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

                returnAppointment = new AppointmentApiHelperModel()
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



            }//end foreach
            
            return Ok(returnAppointment);
        }

        // POST: api/Appointments
        [ResponseType(typeof(Appointment))]
        public IHttpActionResult PostAppointment(AppointmentCreateModel appointment)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Appointment newAppointment = new Appointment();

            newAppointment.AppointmentID = appointment.AppointmentID;
            newAppointment.UserID = appointment.UserID;
            newAppointment.ClinicHospitalID = appointment.ClinicHospitalID;
            newAppointment.ApprovalState = 1;//Approval state should be pending (1) for requests 
            newAppointment.PreferredDate = appointment.PreferredDate;
            newAppointment.PreferredTime = appointment.PreferredTime;
            newAppointment.RequestDoctorDentistID = appointment.RequestDoctorDentistID;
            newAppointment.Remarks = appointment.Remarks;

            // Add the new appointment 
            db.Appointments.Add(newAppointment);
            db.SaveChanges();

            for (int i = 0; i < appointment.Treatments.Length; i++)
            {
                db.AppointmentTreatments.Add(new AppointmentTreatment()
                {
                    AppointmentID = newAppointment.ID,
                    TreatmentID = appointment.Treatments[i]
                });
            }
            db.SaveChanges();

            var returnObj = new
            {
                apptID = newAppointment.ID,
                apptState = newAppointment.ApprovalState
            };

            return Ok(returnObj);
        }

        // PUT: api/Appointments/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutAppointment(int id, AppointmentCreateModel appointment)
        {
            if (db.Appointments.Find(id) == null)
            {
                return NotFound();
            }

            try
            {
                Appointment apptToBeUpdated = db.Appointments.Find(id);

                // Update the Appointment table
                apptToBeUpdated.ClinicHospitalID = appointment.ClinicHospitalID;
                apptToBeUpdated.ApprovalState = 1; // Approval state will be set to pending
                apptToBeUpdated.PreferredDate = appointment.PreferredDate;
                apptToBeUpdated.PreferredTime = appointment.PreferredTime;
                apptToBeUpdated.RequestDoctorDentistID = appointment.RequestDoctorDentistID;
                apptToBeUpdated.Remarks = appointment.Remarks;
                db.SaveChanges();

                // Remove the entities in AppointmentTreatment table 
                if (db.AppointmentTreatments.Any())
                {
                    db.AppointmentTreatments.RemoveRange(db.AppointmentTreatments.
                    Where(apptTreat => apptTreat.AppointmentID == id));
                }

                // Add new entities to AppointmentTreatment table
                for (int i = 0; i < appointment.Treatments.Length; i++)
                {
                    db.AppointmentTreatments.Add(new AppointmentTreatment()
                    {
                        AppointmentID = id,
                        TreatmentID = appointment.Treatments[i]
                    });
                }
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            return Ok();
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

        private class AppointmentApiHelperModel
        {
            public int ID { get; set; }
            public string AppointmentID { get; set; }
            public string UserName { get; set; }
            public int UserID { get; set; }
            public string ClinicHospitalName { get; set; }
            public int ClinicHospitalID { get; set; }
            public string ApprovalState { get; set; }
            public System.DateTime PreferredDate { get; set; }
            public int PreferredTime { get; set; }
            public string PreferredTime_s { get; set; }
            public string DoctorDentistName { get; set; }
            public int? DoctorDentistID { get; set; }
            public string RequestDoctorDentistName { get; set; }
            public int? RequestDoctorDentistID { get; set; }
            public string Remarks { get; set; }
            public System.DateTime? AppointmentDate { get; set; }
            public int? AppointmentTime { get; set; }
            public string AppointmentTime_s { get; set; }
            public List<Treatment> listOfTreatments { get; set; }
            public string approvalString { get; set; }
        }

    }
}