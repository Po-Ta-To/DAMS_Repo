using DAMS_03.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;

namespace DAMS_03.API
{
    public class AppointmentsByUserController : ApiController
    {
        private DAMS_01Entities db = new DAMS_01Entities();

        // GET: api/AppointmentsByUser/2
        [ResponseType(typeof(Appointment))]
        public IHttpActionResult GetAppointmentsByUserID(int id)
        {
            var appointments = from Appointment in db.Appointments
                               where Appointment.UserID == id
                               join User in db.UserAccounts on Appointment.UserID equals User.ID
                               join ch in db.ClinicHospitals on Appointment.ClinicHospitalID equals ch.ID
                               select new
                               {
                                   ID = Appointment.ID,
                                   AppointmentID = Appointment.AppointmentID,
                                   UserName = User.Name,
                                   UserID = User.ID,
                                   ClinicHospitalName = ch.ClinicHospitalName,
                                   ClinicHospitalID = ch.ID,
                                   ApprovalState = Appointment.ApprovalState,
                                   PreferredDate = Appointment.PreferredDate,
                                   PreferredTime = Appointment.PreferredTime,
                                   Remarks = Appointment.Remarks,
                                   AppointmentDate = Appointment.AppointmentDate,//
                                   AppointmentTime = Appointment.AppointmentTime//
                               };

            List<AppointmentDetailViewModel> returnApptList = new List<AppointmentDetailViewModel>();

            foreach (var appointment in appointments)
            {
                string approvalString = "";
                switch (appointment.ApprovalState)
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
                              where apt.ID == appointment.ID
                              select new
                              {
                                  RequestDoctorDentistName = d.Name,
                                  RequestDoctorDentistID = d.ID
                              }).SingleOrDefault();

                var doc = (from apt in db.Appointments
                           join d in db.DoctorDentists on apt.DoctorDentistID equals d.ID
                           where apt.ID == appointment.ID
                           select new
                           {
                               DoctorDentistName = d.Name,
                               DoctorDentistID = d.ID
                           }).SingleOrDefault();

                AppointmentDetailViewModel addAppt = new AppointmentDetailViewModel()
                {
                    ID = appointment.ID,
                    AppointmentID = appointment.AppointmentID,
                    UserName = appointment.UserName,
                    UserID = appointment.UserID,
                    ClinicHospitalName = appointment.ClinicHospitalName,
                    ClinicHospitalID = appointment.ClinicHospitalID,
                    ApprovalState = approvalString,
                    PreferredDate = appointment.PreferredDate,
                    PreferredTime = appointment.PreferredTime,
                    Remarks = appointment.Remarks,
                    AppointmentDate = appointment.AppointmentDate,
                    AppointmentTime = appointment.AppointmentTime
                };

                if (reqDoc != null)
                {
                    addAppt.RequestDoctorDentistName = reqDoc.RequestDoctorDentistName;
                    addAppt.RequestDoctorDentistID = reqDoc.RequestDoctorDentistID;
                }
                else
                {
                    addAppt.RequestDoctorDentistName = "No preference";
                    //addModel.RequestDoctorDentistID = 0;
                }

                if (doc != null)
                {
                    addAppt.DoctorDentistName = doc.DoctorDentistName;
                    addAppt.DoctorDentistID = doc.DoctorDentistID;
                }
                else
                {
                    addAppt.DoctorDentistName = "Unassigned";
                    //addModel.DoctorDentistID = 0;
                }
                returnApptList.Add(addAppt);
            } // End of foreach()

            return Ok(returnApptList);

        } // End of GetAppointmentsByUserID() method
    } // End of AppointmentsByUserController
} // End of namespace
 