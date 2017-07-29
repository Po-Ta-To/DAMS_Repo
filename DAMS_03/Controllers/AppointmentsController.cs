using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using DAMS_03.Models;
using DAMS_03.Authorization;

namespace DAMS_03.Controllers
{
    [AuthorizeAdmin(Roles = "SysAdmin, HospAdmin, ClerkAdmin")]
    public class AppointmentsController : Controller
    {
        private DAMS_01Entities db = new DAMS_01Entities();

        // GET: Appointments
        public ActionResult Index()
        {
            //var appointments = db.Appointments.Include(a => a.ClinicHospital).Include(a => a.DoctorDentist).Include(a => a.DoctorDentist1).Include(a => a.UserAccount);

            var appointments = from apt in db.Appointments
                               join u in db.UserAccounts on apt.UserID equals u.ID
                               join ch in db.ClinicHospitals on apt.ClinicHospitalID equals ch.ID
                               //join d in db.DoctorDentists on apt.RequestDoctorDentistID equals d.ID
                               //join d2 in db.DoctorDentists on apt.DoctorDentistID equals d2.ID
                               select new
                               {
                                   ID = apt.ID,
                                   AppointmentID = apt.AppointmentID,
                                   UserName = u.Name,
                                   UserID = u.ID,
                                   ClinicHospitalName = ch.ClinicHospitalName,
                                   ClinicHospitalID = ch.ID,
                                   ApprovalState = apt.ApprovalState,
                                   PreferredDate = apt.PreferredDate,
                                   PreferredTime = apt.PreferredTime,
                                   Remarks = apt.Remarks,
                                   AppointmentDate = apt.AppointmentDate,//
                                   AppointmentTime = apt.AppointmentTime//
                               };

            List<AppointmentDetailViewModel> returnList = new List<AppointmentDetailViewModel>();

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

                returnList.Add(addAppt);
            }


            return View(returnList);
        }

        // GET: Appointment IndexBy
        public ActionResult IndexBy(int? id)
        {
            if (id < 1 || id > 5)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            if (id == 2 || id == 4)
            {

                var appointments = from apt in db.Appointments
                                   join u in db.UserAccounts on apt.UserID equals u.ID
                                   join ch in db.ClinicHospitals on apt.ClinicHospitalID equals ch.ID
                                   where apt.ApprovalState == 2 || apt.ApprovalState == 4
                                   //join d in db.DoctorDentists on apt.RequestDoctorDentistID equals d.ID
                                   //join d2 in db.DoctorDentists on apt.DoctorDentistID equals d2.ID
                                   select new
                                   {
                                       ID = apt.ID,
                                       AppointmentID = apt.AppointmentID,
                                       UserName = u.Name,
                                       UserID = u.ID,
                                       ClinicHospitalName = ch.ClinicHospitalName,
                                       ClinicHospitalID = ch.ID,
                                       ApprovalState = apt.ApprovalState,
                                       PreferredDate = apt.PreferredDate,
                                       PreferredTime = apt.PreferredTime,
                                       Remarks = apt.Remarks,
                                       AppointmentDate = apt.AppointmentDate,//
                                       AppointmentTime = apt.AppointmentTime//
                                   };

                List<AppointmentDetailViewModel> returnList = new List<AppointmentDetailViewModel>();

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

                    returnList.Add(addAppt);
                }



                return View(returnList);
            }
            else
            {

                var appointments = from apt in db.Appointments
                                   join u in db.UserAccounts on apt.UserID equals u.ID
                                   join ch in db.ClinicHospitals on apt.ClinicHospitalID equals ch.ID
                                   where apt.ApprovalState == id
                                   //join d in db.DoctorDentists on apt.RequestDoctorDentistID equals d.ID
                                   //join d2 in db.DoctorDentists on apt.DoctorDentistID equals d2.ID
                                   select new
                                   {
                                       ID = apt.ID,
                                       AppointmentID = apt.AppointmentID,
                                       UserName = u.Name,
                                       UserID = u.ID,
                                       ClinicHospitalName = ch.ClinicHospitalName,
                                       ClinicHospitalID = ch.ID,
                                       ApprovalState = apt.ApprovalState,
                                       PreferredDate = apt.PreferredDate,
                                       PreferredTime = apt.PreferredTime,
                                       Remarks = apt.Remarks,
                                       AppointmentDate = apt.AppointmentDate,//
                                       AppointmentTime = apt.AppointmentTime//
                                   };

                List<AppointmentDetailViewModel> returnList = new List<AppointmentDetailViewModel>();

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

                    returnList.Add(addAppt);
                }



                return View(returnList);
            }

        }

        // GET: Appointments/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Appointment appointment = db.Appointments.Find(id);
            if (appointment == null)
            {
                return HttpNotFound();
            }

            #region prev ver
            //var appt = (from apt in db.Appointments
            //            join u in db.UserAccounts on apt.UserID equals u.ID
            //            join ch in db.ClinicHospitals on apt.ClinicHospitalID equals ch.ID
            //            join d in db.DoctorDentists on apt.RequestDoctorDentistID equals d.ID
            //            join d2 in db.DoctorDentists on apt.DoctorDentistID equals d2.ID
            //            where apt.ID == id
            //            select new
            //            {
            //                ID = apt.ID,
            //                AppointmentID = apt.AppointmentID,
            //                UserName = u.Name,
            //                UserID = u.ID,
            //                ClinicHospitalName = ch.ClinicHospitalName,
            //                ClinicHospitalID = ch.ID,
            //                ApprovalState = apt.ApprovalState,
            //                PreferredDate = apt.PreferredDate,
            //                PreferredTime = apt.PreferredTime,
            //                DoctorDentistName = d.Name,
            //                DoctorDentistID = d.ID,
            //                RequestDoctorDentistName = d2.Name,
            //                RequestDoctorDentistID = d2.ID,
            //                Remarks = apt.Remarks,
            //                AppointmentDate = apt.AppointmentDate,
            //                AppointmentTime = apt.AppointmentTime
            //            }).SingleOrDefault();

            #endregion

            var appt = (from apt in db.Appointments
                        join u in db.UserAccounts on apt.UserID equals u.ID
                        join ch in db.ClinicHospitals on apt.ClinicHospitalID equals ch.ID
                        where apt.ID == id
                        select new
                        {
                            ID = apt.ID,
                            AppointmentID = apt.AppointmentID,
                            UserName = u.Name,
                            UserID = u.ID,
                            ClinicHospitalName = ch.ClinicHospitalName,
                            ClinicHospitalID = ch.ID,
                            ApprovalState = apt.ApprovalState,
                            PreferredDate = apt.PreferredDate,
                            PreferredTime = apt.PreferredTime,
                            Remarks = apt.Remarks,
                            AppointmentDate = apt.AppointmentDate,
                            AppointmentTime = apt.AppointmentTime
                        }).SingleOrDefault();

            string approvalString = "";

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
            };

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

            AppointmentDetailViewModel returnAppt = new AppointmentDetailViewModel()
            {
                ID = appt.ID,
                AppointmentID = appt.AppointmentID,
                UserName = appt.UserName,
                UserID = appointment.UserID,
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
                returnAppt.RequestDoctorDentistName = reqDoc.RequestDoctorDentistName;
                returnAppt.RequestDoctorDentistID = reqDoc.RequestDoctorDentistID;
            }
            else
            {
                returnAppt.RequestDoctorDentistName = "No preference";
                //addModel.RequestDoctorDentistID = 0;
            }

            if (doc != null)
            {
                returnAppt.DoctorDentistName = doc.DoctorDentistName;
                returnAppt.DoctorDentistID = doc.DoctorDentistID;
            }
            else
            {
                returnAppt.DoctorDentistName = "Unassigned";
                //addModel.DoctorDentistID = 0;
            }

            returnAppt.listOfTreatments = (from t in db.Treatments
                                           join at in db.AppointmentTreatments on t.ID equals at.TreatmentID
                                           join a in db.Appointments on at.AppointmentID equals a.ID
                                           where a.ID == returnAppt.ID
                                           select t).ToList();

            return View(returnAppt);
        }

        // GET: Appointments/CreateSelection
        public ActionResult CreateSelection()
        {
            //ViewBag.ClinicHospitalID = new SelectList(db.ClinicHospitals, "ID", "ClinicHospitalID");
            //ViewBag.DoctorDentistID = new SelectList(db.DoctorDentists, "ID", "DoctorDentistID");
            //ViewBag.RequestDoctorDentistID = new SelectList(db.DoctorDentists, "ID", "DoctorDentistID");
            //ViewBag.UserID = new SelectList(db.UserAccounts, "ID", "ID");

            //returnModel.listOfTreatments = (from t in db.Treatments
            //                                select new Treatment()).ToList();

            return View(db.ClinicHospitals.ToList());

            //return View(returnModel);
        }

        // GET: Appointments/Create
        public ActionResult Create(int? id)
        {

            AppointmentCreateViewModel returnModel = new AppointmentCreateViewModel()
            {
                AppointmentDate = System.DateTime.Now,
                PreferredDate = System.DateTime.Now
            };

            //returnModel.selectClinicHospital = new List<SelectListItem>();
            returnModel.selectDoctorDentist = new List<SelectListItem>();
            returnModel.selectUser = new List<SelectListItem>();

            //var listOfHosp = from ClinHosp in db.ClinicHospitals
            //                 select new SelectListItem()
            //                 {
            //                     Value = ClinHosp.ID.ToString(),
            //                     Text = ClinHosp.ClinicHospitalName
            //                 };
            //returnModel.selectClinicHospital.Add(new SelectListItem()
            //{
            //    Value = "",
            //    Text = " - "
            //});
            //returnModel.selectClinicHospital.AddRange(listOfHosp.ToList<SelectListItem>());

            var listOfDoc = from doc in db.DoctorDentists
                            where doc.ClinicHospitalID == id
                            select new SelectListItem()
                            {
                                Value = doc.ID.ToString(),
                                Text = doc.Name
                            };
            returnModel.selectDoctorDentist.Add(new SelectListItem()
            {
                Value = "0",
                Text = "No preference"
            });
            returnModel.selectDoctorDentist.AddRange(listOfDoc.ToList<SelectListItem>());

            var listOfUser = from u in db.UserAccounts
                             select new SelectListItem()
                             {
                                 Value = u.ID.ToString(),
                                 Text = u.Name
                             };
            returnModel.selectUser.Add(new SelectListItem()
            {
                Value = "",
                Text = " - "
            });
            returnModel.selectUser.AddRange(listOfUser.ToList<SelectListItem>());

            //AppointmentTreatmentsCreateModel returnModel = new AppointmentTreatmentsCreateModel();
            returnModel.listOfTreatments = (from t in db.Treatments
                                            join cht in db.ClinicHospitalTreatments on t.ID equals cht.TreatmentID
                                            join hc in db.ClinicHospitals on cht.ClinicHospitalID equals hc.ID
                                            where hc.ID == id
                                            select new TreatmentHelperModel()
                                            {
                                                ID = t.ID,
                                                TreatmentID = t.TreatmentID,
                                                TreatmentName = t.TreatmentName,
                                                TreatmentDesc = t.TreatmentDesc,
                                                IsFollowUp = t.IsFollowUp,
                                                PriceLow = cht.PriceLow,
                                                PriceHigh = cht.PriceHigh,
                                                IsChecked = false
                                            }).ToList();

            foreach (TreatmentHelperModel t in returnModel.listOfTreatments)
            {
                string priceLow = String.Format("{0:C}", t.PriceLow);
                if (t.PriceHigh == t.PriceLow)
                {
                    t.Price = priceLow;
                }
                else
                {
                    string priceHigh = String.Format("{0:C}", t.PriceHigh);
                    t.Price = priceLow + " - " + priceHigh;
                }
            }

            return View(returnModel);
        }

        // POST: Appointments/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(AppointmentCreateViewModel newAppointment)
        {
            if (ModelState.IsValid)
            {

                var url = Url.RequestContext.RouteData.Values["id"];
                int hospid = Int32.Parse((string)url);

                Appointment addAppointment = new Appointment()
                {
                    AppointmentID = newAppointment.AppointmentID,
                    UserID = Int32.Parse(newAppointment.UserID),
                    ClinicHospitalID = hospid,//Int32.Parse(newAppointment.ClinicHospitalID),
                    ApprovalState = newAppointment.ApprovalState,
                    PreferredDate = newAppointment.PreferredDate,
                    PreferredTime = newAppointment.PreferredTime,
                    Remarks = newAppointment.Remarks,
                    AppointmentTime = newAppointment.AppointmentTime
                };

                if (newAppointment.DoctorDentistID != null && !newAppointment.DoctorDentistID.Equals("") && !newAppointment.DoctorDentistID.Equals("0"))
                {
                    addAppointment.DoctorDentistID = Int32.Parse(newAppointment.DoctorDentistID);
                }
                if (newAppointment.RequestDoctorDentistID != null && !newAppointment.RequestDoctorDentistID.Equals("") && !newAppointment.RequestDoctorDentistID.Equals("0"))
                {
                    addAppointment.RequestDoctorDentistID = Int32.Parse(newAppointment.RequestDoctorDentistID);
                }
                if (newAppointment.AppointmentDate != null)
                {
                    addAppointment.AppointmentDate = newAppointment.AppointmentDate;
                }
                //if (newAppointment.AppointmentTime != null)
                //{
                //    addAppointment.AppointmentTime = newAppointment.AppointmentTime;
                //}

                db.Appointments.Add(addAppointment);
                db.SaveChanges();

                foreach (TreatmentHelperModel treatment in newAppointment.listOfTreatments)
                {
                    if (treatment.IsChecked == true)
                    {
                        AppointmentTreatment addNewRelation = new AppointmentTreatment()
                        {
                            TreatmentID = treatment.ID,
                            AppointmentID = addAppointment.ID
                        };
                        db.AppointmentTreatments.Add(addNewRelation);
                    }

                }
                db.SaveChanges();

                //Change booking int for date of doctor/dentist
                if (addAppointment.ApprovalState == 3 && addAppointment.AppointmentDate != null && addAppointment.DoctorDentistID != null)
                {
                    DoctorDentistDateBooking bookingDay = (from dd in db.DoctorDentists
                                                           join dddb in db.DoctorDentistDateBookings on dd.ID equals dddb.DoctorDentistID
                                                           where addAppointment.AppointmentDate == dddb.DateOfBookings
                                                           select dddb).SingleOrDefault();

                    if (bookingDay != null)
                    {
                        bookingDay.Bookings++;
                        db.Entry(bookingDay).State = EntityState.Modified;
                    }
                    else
                    {
                        DoctorDentistDateBooking newBookingDay = new DoctorDentistDateBooking()
                        {
                            DateOfBookings = addAppointment.AppointmentDate.Value,
                            Bookings = 1,
                            DoctorDentistID = addAppointment.DoctorDentistID.Value
                        };
                        db.DoctorDentistDateBookings.Add(newBookingDay);
                    }

                    db.SaveChanges();

                }

                return RedirectToAction("Index", "Appointments");
            }
            else
            {
                newAppointment.AppointmentDate = System.DateTime.Now;
                newAppointment.PreferredDate = System.DateTime.Now;

                //newAppointment.selectClinicHospital = new List<SelectListItem>();
                newAppointment.selectDoctorDentist = new List<SelectListItem>();
                newAppointment.selectUser = new List<SelectListItem>();

                //var listOfHosp = from ClinHosp in db.ClinicHospitals
                //                 select new SelectListItem()
                //                 {
                //                     Value = ClinHosp.ID.ToString(),
                //                     Text = ClinHosp.ClinicHospitalName
                //                 };
                //newAppointment.selectClinicHospital.Add(new SelectListItem()
                //{
                //    Value = "",
                //    Text = " - "
                //});
                //newAppointment.selectClinicHospital.AddRange(listOfHosp.ToList<SelectListItem>());

                var url = Url.RequestContext.RouteData.Values["id"];
                int hospid = Int32.Parse((string)url);

                var listOfDoc = from doc in db.DoctorDentists
                                where doc.ClinicHospitalID == hospid
                                select new SelectListItem()
                                {
                                    Value = doc.ID.ToString(),
                                    Text = doc.Name
                                };
                newAppointment.selectDoctorDentist.Add(new SelectListItem()
                {
                    Value = "0",
                    Text = "No preference"
                });
                newAppointment.selectDoctorDentist.AddRange(listOfDoc.ToList<SelectListItem>());

                var listOfUser = from u in db.UserAccounts
                                 select new SelectListItem()
                                 {
                                     Value = u.ID.ToString(),
                                     Text = u.Name
                                 };
                newAppointment.selectUser.Add(new SelectListItem()
                {
                    Value = "",
                    Text = " - "
                });
                newAppointment.selectUser.AddRange(listOfUser.ToList<SelectListItem>());

                newAppointment.listOfTreatments = (from t in db.Treatments
                                                   join cht in db.ClinicHospitalTreatments on t.ID equals cht.TreatmentID
                                                   join hc in db.ClinicHospitals on cht.ClinicHospitalID equals hc.ID
                                                   where hc.ID == hospid
                                                   select new TreatmentHelperModel()
                                                   {
                                                       ID = t.ID,
                                                       TreatmentID = t.TreatmentID,
                                                       TreatmentName = t.TreatmentName,
                                                       TreatmentDesc = t.TreatmentDesc,
                                                       IsFollowUp = t.IsFollowUp,
                                                       PriceLow = cht.PriceLow,
                                                       PriceHigh = cht.PriceHigh,
                                                       IsChecked = false
                                                   }).ToList();

                foreach (TreatmentHelperModel t in newAppointment.listOfTreatments)
                {
                    string priceLow = String.Format("{0:C}", t.PriceLow);
                    if (t.PriceHigh == t.PriceLow)
                    {
                        t.Price = priceLow;
                    }
                    else
                    {
                        string priceHigh = String.Format("{0:C}", t.PriceHigh);
                        t.Price = priceLow + " - " + priceHigh;
                    }
                }

            }


            return View(newAppointment);
        }

        // GET: Appointments/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Appointment appointment = db.Appointments.Find(id);
            if (appointment == null)
            {
                return HttpNotFound();
            }
            //ViewBag.ClinicHospitalID = new SelectList(db.ClinicHospitals, "ID", "ClinicHospitalID", appointment.ClinicHospitalID);
            //ViewBag.DoctorDentistID = new SelectList(db.DoctorDentists, "ID", "DoctorDentistID", appointment.DoctorDentistID);
            //ViewBag.RequestDoctorDentistID = new SelectList(db.DoctorDentists, "ID", "DoctorDentistID", appointment.RequestDoctorDentistID);
            //ViewBag.UserID = new SelectList(db.UserAccounts, "ID", "ID", appointment.UserID);


            var appt = (from apt in db.Appointments
                        join u in db.UserAccounts on apt.UserID equals u.ID
                        join ch in db.ClinicHospitals on apt.ClinicHospitalID equals ch.ID
                        where apt.ID == id
                        select new
                        {
                            ID = apt.ID,
                            AppointmentID = apt.AppointmentID,
                            UserName = u.Name,
                            UserID = u.ID,
                            ClinicHospitalName = ch.ClinicHospitalName,
                            ClinicHospitalID = ch.ID,
                            ApprovalState = apt.ApprovalState.ToString(),
                            PreferredDate = apt.PreferredDate,
                            PreferredTime = apt.PreferredTime,
                            Remarks = apt.Remarks,
                            AppointmentDate = apt.AppointmentDate,
                            AppointmentTime = apt.AppointmentTime
                        }).SingleOrDefault();

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

            AppointmentEditViewModel returnAppt = new AppointmentEditViewModel()
            {
                ID = appt.ID,
                AppointmentID = appt.AppointmentID,
                UserName = appt.UserName,
                UserID = appointment.UserID.ToString(),
                ClinicHospitalName = appt.ClinicHospitalName,
                ClinicHospitalID = appt.ClinicHospitalID.ToString(),
                ApprovalState = appt.ApprovalState,
                PreferredDate = appt.PreferredDate,
                PreferredTime = appt.PreferredTime,
                Remarks = appt.Remarks,
                AppointmentDate = appt.AppointmentDate,
                AppointmentTime = appt.AppointmentTime
            };

            if (reqDoc != null)
            {
                returnAppt.RequestDoctorDentistName = reqDoc.RequestDoctorDentistName;
                returnAppt.RequestDoctorDentistID = reqDoc.RequestDoctorDentistID.ToString();
            }
            else
            {
                returnAppt.RequestDoctorDentistName = "No preference";
                //addModel.RequestDoctorDentistID = 0;
            }

            if (doc != null)
            {
                returnAppt.DoctorDentistName = doc.DoctorDentistName;
                returnAppt.DoctorDentistID = doc.DoctorDentistID.ToString();
            }
            else
            {
                returnAppt.DoctorDentistName = "Unassigned";
                //addModel.DoctorDentistID = 0;
            }

            //newAppointment.AppointmentDate = System.DateTime.Now;
            //newAppointment.PreferredDate = System.DateTime.Now;

            returnAppt.selectClinicHospital = new List<SelectListItem>();
            returnAppt.selectDoctorDentist = new List<SelectListItem>();
            returnAppt.selectUser = new List<SelectListItem>();

            var listOfHosp = from ClinHosp in db.ClinicHospitals
                             select new SelectListItem()
                             {
                                 Value = ClinHosp.ID.ToString(),
                                 Text = ClinHosp.ClinicHospitalName
                             };
            returnAppt.selectClinicHospital.Add(new SelectListItem()
            {
                Value = "",
                Text = " - "
            });
            returnAppt.selectClinicHospital.AddRange(listOfHosp.ToList<SelectListItem>());

            var listOfDoc = from docden in db.DoctorDentists
                            where docden.ClinicHospitalID == id
                            select new SelectListItem()
                            {
                                Value = docden.ID.ToString(),
                                Text = docden.Name
                            };
            returnAppt.selectDoctorDentist.Add(new SelectListItem()
            {
                Value = "0",
                Text = "No preference"
            });
            returnAppt.selectDoctorDentist.AddRange(listOfDoc.ToList<SelectListItem>());

            var listOfUser = from u in db.UserAccounts
                             select new SelectListItem()
                             {
                                 Value = u.ID.ToString(),
                                 Text = u.Name
                             };
            returnAppt.selectUser.Add(new SelectListItem()
            {
                Value = "",
                Text = " - "
            });
            returnAppt.selectUser.AddRange(listOfUser.ToList<SelectListItem>());


            List<Treatment> treatmentRelation = (from t in db.Treatments
                                                 join at in db.AppointmentTreatments on t.ID equals at.TreatmentID
                                                 join a in db.Appointments on at.AppointmentID equals a.ID
                                                 where a.ID == returnAppt.ID
                                                 select t).ToList();

            var listOfTreatments = (from t in db.Treatments
                                    join cht in db.ClinicHospitalTreatments on t.ID equals cht.TreatmentID
                                    where cht.ClinicHospitalID == appt.ClinicHospitalID
                                    select new
                                    {
                                        ID = t.ID,
                                        TreatmentID = t.TreatmentID,
                                        TreatmentName = t.TreatmentName,
                                        TreatmentDesc = t.TreatmentDesc,
                                        IsFollowUp = t.IsFollowUp
                                    }).ToList();

            returnAppt.listOfTreatments = new List<TreatmentHelperModel>();

            foreach (var t in listOfTreatments)
            {
                bool added = false;
                foreach (Treatment r in treatmentRelation)
                {
                    if (t.ID == r.ID)
                    {
                        TreatmentHelperModel addTreatment = new TreatmentHelperModel()
                        {
                            ID = t.ID,
                            TreatmentID = t.TreatmentID,
                            TreatmentName = t.TreatmentName,
                            TreatmentDesc = t.TreatmentDesc,
                            IsFollowUp = t.IsFollowUp,
                            IsChecked = true
                        };
                        returnAppt.listOfTreatments.Add(addTreatment);
                        added = true;
                        break;
                    }
                }//end foreach
                if (added == false)
                {
                    TreatmentHelperModel addTreatment = new TreatmentHelperModel()
                    {
                        ID = t.ID,
                        TreatmentID = t.TreatmentID,
                        TreatmentName = t.TreatmentName,
                        TreatmentDesc = t.TreatmentDesc,
                        IsFollowUp = t.IsFollowUp,
                        IsChecked = false
                    };
                    returnAppt.listOfTreatments.Add(addTreatment);
                }
            }//end foreach



            return View(returnAppt);
        }

        // POST: Appointments/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(AppointmentEditViewModel model)
        {
            if (ModelState.IsValid)
            {

                Appointment appointment = (from a in db.Appointments
                                           where a.ID == model.ID
                                           select a).SingleOrDefault();

                int prevApprovalState = appointment.ApprovalState;

                appointment.AppointmentID = model.AppointmentID;
                appointment.UserID = Int32.Parse(model.UserID);
                appointment.ClinicHospitalID = Int32.Parse(model.ClinicHospitalID);
                appointment.ApprovalState = Int32.Parse(model.ApprovalState);
                appointment.PreferredDate = model.PreferredDate;
                appointment.PreferredTime = model.PreferredTime;
                appointment.Remarks = model.Remarks;
                appointment.AppointmentTime = model.AppointmentTime;

                if (model.DoctorDentistID != null && !model.DoctorDentistID.Equals("") && !model.DoctorDentistID.Equals("0"))
                {
                    appointment.DoctorDentistID = Int32.Parse(model.DoctorDentistID);
                }
                if (model.RequestDoctorDentistID != null && !model.RequestDoctorDentistID.Equals("") && !model.DoctorDentistID.Equals("0"))
                {
                    appointment.RequestDoctorDentistID = Int32.Parse(model.RequestDoctorDentistID);
                }
                if (model.AppointmentDate != null)
                {
                    appointment.AppointmentDate = model.AppointmentDate;
                }
                //if (newAppointment.AppointmentTime != null)
                //{
                //    addAppointment.AppointmentTime = newAppointment.AppointmentTime;
                //}

                db.Entry(appointment).State = EntityState.Modified;

                List<AppointmentTreatment> listDelRel = (from at in db.AppointmentTreatments
                                                         join a in db.Appointments on at.AppointmentID equals a.ID
                                                         where a.ID == appointment.ID
                                                         select at).ToList();

                db.AppointmentTreatments.RemoveRange(listDelRel);

                foreach (TreatmentHelperModel treatment in model.listOfTreatments)
                {
                    if (treatment.IsChecked == true)
                    {
                        AppointmentTreatment addNewRelation = new AppointmentTreatment()
                        {
                            TreatmentID = treatment.ID,
                            AppointmentID = appointment.ID
                        };
                        db.AppointmentTreatments.Add(addNewRelation);
                    }

                }

                db.SaveChanges();

                //Change booking int for date of doctor/dentist
                if ((model.ApprovalState.Equals("3") && model.AppointmentDate != null && model.DoctorDentistID != null)
                    && (prevApprovalState != 3 && prevApprovalState != 5))
                {
                    DoctorDentistDateBooking bookingDay = (from dd in db.DoctorDentists
                                                           join dddb in db.DoctorDentistDateBookings on dd.ID equals dddb.DoctorDentistID
                                                           where model.AppointmentDate == dddb.DateOfBookings
                                                           select dddb).SingleOrDefault();

                    if (bookingDay != null)
                    {
                        bookingDay.Bookings++;
                        db.Entry(bookingDay).State = EntityState.Modified;
                    }
                    else
                    {
                        DoctorDentistDateBooking newBookingDay = new DoctorDentistDateBooking()
                        {
                            DateOfBookings = model.AppointmentDate.Value,
                            Bookings = 1,
                            DoctorDentistID = Int32.Parse(model.DoctorDentistID)
                        };
                        db.DoctorDentistDateBookings.Add(newBookingDay);
                    }

                    db.SaveChanges();

                }
                else if (!model.ApprovalState.Equals(prevApprovalState.ToString())
                    && (prevApprovalState == 3 || prevApprovalState == 5) &&
                    !model.ApprovalState.Equals("3") && !model.ApprovalState.Equals("5"))
                {
                    DoctorDentistDateBooking bookingDay = (from dd in db.DoctorDentists
                                                           join dddb in db.DoctorDentistDateBookings on dd.ID equals dddb.DoctorDentistID
                                                           where model.AppointmentDate == dddb.DateOfBookings
                                                           select dddb).SingleOrDefault();

                    if (bookingDay != null)
                    {
                        bookingDay.Bookings--;
                        db.Entry(bookingDay).State = EntityState.Modified;
                    }
                    else
                    {
                        DoctorDentistDateBooking newBookingDay = new DoctorDentistDateBooking()
                        {
                            DateOfBookings = model.AppointmentDate.Value,
                            Bookings = 0,
                            DoctorDentistID = Int32.Parse(model.DoctorDentistID)
                        };
                    }

                    db.SaveChanges();
                }


                return RedirectToAction("Details", "Appointments", new { id = appointment.ID });
            }
            else
            {
                model.AppointmentDate = System.DateTime.Now;
                model.PreferredDate = System.DateTime.Now;

                model.selectClinicHospital = new List<SelectListItem>();
                model.selectDoctorDentist = new List<SelectListItem>();
                model.selectUser = new List<SelectListItem>();

                var listOfHosp = from ClinHosp in db.ClinicHospitals
                                 select new SelectListItem()
                                 {
                                     Value = ClinHosp.ID.ToString(),
                                     Text = ClinHosp.ClinicHospitalName
                                 };
                model.selectClinicHospital.Add(new SelectListItem()
                {
                    Value = "",
                    Text = " - "
                });
                model.selectClinicHospital.AddRange(listOfHosp.ToList<SelectListItem>());

                //int chid = Int32.Parse(model.ClinicHospitalID);
                var url = Url.RequestContext.RouteData.Values["id"];
                int urlid = Int32.Parse((string)url);
                int hospid = (from appt in db.Appointments
                              where appt.ID == urlid
                              select appt.ClinicHospitalID).SingleOrDefault();

                var listOfDoc = from doc in db.DoctorDentists
                                where doc.ClinicHospitalID == hospid
                                select new SelectListItem()
                                {
                                    Value = doc.ID.ToString(),
                                    Text = doc.Name
                                };
                model.selectDoctorDentist.Add(new SelectListItem()
                {
                    Value = "0",
                    Text = "No preference"
                });
                model.selectDoctorDentist.AddRange(listOfDoc.ToList<SelectListItem>());

                var listOfUser = from u in db.UserAccounts
                                 select new SelectListItem()
                                 {
                                     Value = u.ID.ToString(),
                                     Text = u.Name
                                 };
                model.selectUser.Add(new SelectListItem()
                {
                    Value = "",
                    Text = " - "
                });
                model.selectUser.AddRange(listOfUser.ToList<SelectListItem>());


                List<Treatment> treatmentRelation = (from t in db.Treatments
                                                     join at in db.AppointmentTreatments on t.ID equals at.TreatmentID
                                                     join a in db.Appointments on at.AppointmentID equals a.ID
                                                     where a.ID == model.ID
                                                     select t).ToList();

                var listOfTreatments = (from t in db.Treatments
                                        select new
                                        {
                                            ID = t.ID,
                                            TreatmentID = t.TreatmentID,
                                            TreatmentName = t.TreatmentName,
                                            TreatmentDesc = t.TreatmentDesc,
                                            IsFollowUp = t.IsFollowUp
                                        }).ToList();

                model.listOfTreatments = new List<TreatmentHelperModel>();

                foreach (var t in listOfTreatments)
                {
                    bool added = false;
                    foreach (Treatment r in treatmentRelation)
                    {
                        if (t.ID == r.ID)
                        {
                            TreatmentHelperModel addTreatment = new TreatmentHelperModel()
                            {
                                ID = t.ID,
                                TreatmentID = t.TreatmentID,
                                TreatmentName = t.TreatmentName,
                                TreatmentDesc = t.TreatmentDesc,
                                IsFollowUp = t.IsFollowUp,
                                IsChecked = true
                            };
                            model.listOfTreatments.Add(addTreatment);
                            added = true;
                            break;
                        }
                    }//end foreach
                    if (added == false)
                    {
                        TreatmentHelperModel addTreatment = new TreatmentHelperModel()
                        {
                            ID = t.ID,
                            TreatmentID = t.TreatmentID,
                            TreatmentName = t.TreatmentName,
                            TreatmentDesc = t.TreatmentDesc,
                            IsFollowUp = t.IsFollowUp,
                            IsChecked = false
                        };
                        model.listOfTreatments.Add(addTreatment);
                    }
                }//end foreach

            }
            //ViewBag.ClinicHospitalID = new SelectList(db.ClinicHospitals, "ID", "ClinicHospitalID", appointment.ClinicHospitalID);
            //ViewBag.DoctorDentistID = new SelectList(db.DoctorDentists, "ID", "DoctorDentistID", appointment.DoctorDentistID);
            //ViewBag.RequestDoctorDentistID = new SelectList(db.DoctorDentists, "ID", "DoctorDentistID", appointment.RequestDoctorDentistID);
            //ViewBag.UserID = new SelectList(db.UserAccounts, "ID", "ID", appointment.UserID);
            return View(model);
        }

        // GET: Appointments/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Appointment appointment = db.Appointments.Find(id);
            if (appointment == null)
            {
                return HttpNotFound();
            }
            return View(appointment);
        }

        // POST: Appointments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Appointment appointment = db.Appointments.Find(id);
            db.Appointments.Remove(appointment);
            db.SaveChanges();
            return RedirectToAction("Index", "Appointments");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
