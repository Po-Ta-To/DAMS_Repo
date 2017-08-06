﻿using DAMS_03.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Web.Http;
using System.Web.Http.Description;

namespace DAMS_03.API
{
    public class AppointmentsByUserController : ApiController
    {
        private DAMS_01Entities db = new DAMS_01Entities();

        // GET: api/AppointmentsByUser/2
        [Authorize]
        [ResponseType(typeof(Appointment))]
        public IHttpActionResult GetAppointmentsByUserID()
        {
            ClaimsPrincipal principal = Request.GetRequestContext().Principal as ClaimsPrincipal;
            string username = ClaimsPrincipal.Current.Identity.Name;
            //string alsoName = User.Identity.Name;

            var appointments = from Appointment in db.Appointments
                               join User in db.UserAccounts on Appointment.UserID equals User.ID
                               join anu in db.AspNetUsers on User.AspNetID equals anu.Id
                               join ch in db.ClinicHospitals on Appointment.ClinicHospitalID equals ch.ID
                               where anu.UserName == username
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

            List<AppointmentApiHelperModel> returnApptList = new List<AppointmentApiHelperModel>();

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

                List<TimeslotApiHelperModel> timeslots = (from cht in db.ClinicHospitalTimeslots
                                                          join ch in db.ClinicHospitals on cht.ClinicHospitalID equals ch.ID
                                                          orderby cht.TimeslotIndex ascending
                                                          where ch.ID == appointment.ClinicHospitalID
                                                          select new TimeslotApiHelperModel()
                                                          {
                                                              TimeslotIndex = cht.TimeslotIndex,
                                                              TimeRangeSlotString = cht.TimeRangeSlotString,
                                                          }).ToList();

                string insertTimeslotPreferred = timeslots[appointment.PreferredTime].TimeRangeSlotString;
                string insertTimeslotFinal = "Not Confirmed";
                if (appointment.AppointmentTime != null)
                {
                    insertTimeslotFinal = timeslots[(int)appointment.AppointmentTime].TimeRangeSlotString;
                }
                
                AppointmentApiHelperModel addAppt = new AppointmentApiHelperModel()
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
                    AppointmentTime = appointment.AppointmentTime,
                    PreferredTime_s = insertTimeslotPreferred,
                    AppointmentTime_s = insertTimeslotFinal
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

        private class TimeslotApiHelperModel
        {
            public int TimeslotIndex { get; set; }
            public string TimeRangeSlotString { get; set; }
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

    } // End of AppointmentsByUserController
} // End of namespace
