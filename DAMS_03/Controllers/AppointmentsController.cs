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
using Microsoft.AspNet.Identity;

namespace DAMS_03.Controllers
{
    [AuthorizeAdmin(Roles = "SysAdmin, HospAdmin, ClerkAdmin")]
    public class AppointmentsController : Controller
    {
        private DAMS_01Entities db = new DAMS_01Entities();

        // GET: Appointments
        public ActionResult Index(int? page, int? perpage, string search, string searchby, string orderBy, string order, int? indexby)
        {
            AppointmentIndexModel returnModel = new AppointmentIndexModel();

            int noOfResults = 10;
            if (perpage != null)
            {
                noOfResults = (int)perpage;
                returnModel.perpageNo = (int)perpage;
            }
            else
            {
                returnModel.perpageNo = 10;
            }
            if (page != null)
            {
                returnModel.pageNo = (int)page;
            }
            else
            {
                returnModel.pageNo = 1;
            }
            returnModel.search = search;
            returnModel.searchby = searchby;
            int indexBy = 0;
            if(indexby != null)
            {
                indexBy = (int)indexby;
            }

            //var appointments = db.Appointments.Include(a => a.ClinicHospital).Include(a => a.DoctorDentist).Include(a => a.DoctorDentist1).Include(a => a.UserAccount);

            string userAspId = User.Identity.GetUserId();

            int hospid = (from ch in db.ClinicHospitals
                          join ach in db.AdminAccountClinicHospitals on ch.ID equals ach.ClinicHospitalID
                          join aa in db.AdminAccounts on ach.AdminID equals aa.ID
                          join aspu in db.AspNetUsers on aa.AspNetID equals aspu.Id
                          where aspu.Id == userAspId
                          select ch.ID).SingleOrDefault();

            List<AppointmentDetailViewModel> appointments = new List<AppointmentDetailViewModel>();

            if (search != null || searchby != null)
            {
                switch (searchby)
                {
                    case "user":
                        if (hospid == 0)
                        {
                            appointments = (from apt in db.Appointments
                                            join u in db.UserAccounts on apt.UserID equals u.ID
                                            join ch in db.ClinicHospitals on apt.ClinicHospitalID equals ch.ID
                                            where u.Name.Contains(search)
                                            orderby apt.ID descending
                                            select new AppointmentDetailViewModel()
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
                                                AppointmentDate = apt.AppointmentDate,//
                                                AppointmentTime = apt.AppointmentTime//
                                            }).ToList();
                        }
                        else
                        {
                            appointments = (from apt in db.Appointments
                                            join u in db.UserAccounts on apt.UserID equals u.ID
                                            join ch in db.ClinicHospitals on apt.ClinicHospitalID equals ch.ID
                                            where u.Name.Contains(search)
                                            orderby apt.ID descending
                                            where ch.ID == hospid
                                            select new AppointmentDetailViewModel()
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
                                                AppointmentDate = apt.AppointmentDate,//
                                                AppointmentTime = apt.AppointmentTime//
                                            }).ToList();
                        }

                        break;
                    case "dentist":
                        if (hospid == 0)
                        {
                            appointments = (from apt in db.Appointments
                                            join u in db.UserAccounts on apt.UserID equals u.ID
                                            join ch in db.ClinicHospitals on apt.ClinicHospitalID equals ch.ID
                                            join dd1 in db.DoctorDentists on apt.DoctorDentistID equals dd1.ID
                                            join dd2 in db.DoctorDentists on apt.RequestDoctorDentistID equals dd2.ID
                                            where dd1.Name.Contains(search) || dd2.Name.Contains(search)
                                            orderby apt.ID descending
                                            select new AppointmentDetailViewModel()
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
                                                AppointmentDate = apt.AppointmentDate,//
                                                AppointmentTime = apt.AppointmentTime//
                                            }).ToList();
                        }
                        else
                        {
                            appointments = (from apt in db.Appointments
                                            join u in db.UserAccounts on apt.UserID equals u.ID
                                            join ch in db.ClinicHospitals on apt.ClinicHospitalID equals ch.ID
                                            join dd1 in db.DoctorDentists on apt.DoctorDentistID equals dd1.ID
                                            join dd2 in db.DoctorDentists on apt.RequestDoctorDentistID equals dd2.ID
                                            where dd1.Name.Contains(search) || dd2.Name.Contains(search)
                                            orderby apt.ID descending
                                            where ch.ID == hospid
                                            select new AppointmentDetailViewModel()
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
                                                AppointmentDate = apt.AppointmentDate,//
                                                AppointmentTime = apt.AppointmentTime//
                                            }).ToList();
                        }
                        break;
                    case "date":
                        if (hospid == 0)
                        {
                            appointments = (from apt in db.Appointments
                                            join u in db.UserAccounts on apt.UserID equals u.ID
                                            join ch in db.ClinicHospitals on apt.ClinicHospitalID equals ch.ID
                                            where apt.PreferredDate.ToString().Contains(search) || apt.AppointmentDate.ToString().Contains(search)
                                            orderby apt.ID descending
                                            select new AppointmentDetailViewModel()
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
                                                AppointmentDate = apt.AppointmentDate,//
                                                AppointmentTime = apt.AppointmentTime//
                                            }).ToList();
                        }
                        else
                        {
                            appointments = (from apt in db.Appointments
                                            join u in db.UserAccounts on apt.UserID equals u.ID
                                            join ch in db.ClinicHospitals on apt.ClinicHospitalID equals ch.ID
                                            where apt.PreferredDate.ToString().Contains(search) || apt.AppointmentDate.ToString().Contains(search)
                                            orderby apt.ID descending
                                            where ch.ID == hospid
                                            select new AppointmentDetailViewModel()
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
                                                AppointmentDate = apt.AppointmentDate,//
                                                AppointmentTime = apt.AppointmentTime//
                                            }).ToList();
                        }
                        break;
                    case "apptid":
                        if (hospid == 0)
                        {
                            appointments = (from apt in db.Appointments
                                            join u in db.UserAccounts on apt.UserID equals u.ID
                                            join ch in db.ClinicHospitals on apt.ClinicHospitalID equals ch.ID
                                            where apt.ID.ToString().Contains(search) || apt.AppointmentID.ToString().Contains(search)
                                            orderby apt.ID descending
                                            select new AppointmentDetailViewModel()
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
                                                AppointmentDate = apt.AppointmentDate,//
                                                AppointmentTime = apt.AppointmentTime//
                                            }).ToList();
                        }
                        else
                        {
                            appointments = (from apt in db.Appointments
                                            join u in db.UserAccounts on apt.UserID equals u.ID
                                            join ch in db.ClinicHospitals on apt.ClinicHospitalID equals ch.ID
                                            where apt.ID.ToString().Contains(search) || apt.AppointmentID.ToString().Contains(search)
                                            orderby apt.ID descending
                                            where ch.ID == hospid
                                            select new AppointmentDetailViewModel()
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
                                                AppointmentDate = apt.AppointmentDate,//
                                                AppointmentTime = apt.AppointmentTime//
                                            }).ToList();
                        }
                        break;
                    default:

                        break;
                }
            }
            else
            {
                if (hospid == 0)
                {
                    appointments = (from apt in db.Appointments
                                    join u in db.UserAccounts on apt.UserID equals u.ID
                                    join ch in db.ClinicHospitals on apt.ClinicHospitalID equals ch.ID
                                    orderby apt.ID descending
                                    select new AppointmentDetailViewModel()
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
                                        AppointmentDate = apt.AppointmentDate,//
                                        AppointmentTime = apt.AppointmentTime//
                                    }).ToList();
                }
                else
                {
                    appointments = (from apt in db.Appointments
                                    join u in db.UserAccounts on apt.UserID equals u.ID
                                    join ch in db.ClinicHospitals on apt.ClinicHospitalID equals ch.ID
                                    orderby apt.ID descending
                                    where ch.ID == hospid
                                    select new AppointmentDetailViewModel()
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
                                        AppointmentDate = apt.AppointmentDate,//
                                        AppointmentTime = apt.AppointmentTime//
                                    }).ToList();
                }
            }//End of search if else

            switch (orderBy)
            {
                case "apptId":
                    if (order.Equals("desc"))
                    {
                        appointments = appointments.OrderByDescending(o => o.ID).ToList();
                    }
                    else
                    {
                        appointments = appointments.OrderBy(o => o.ID).ToList();
                        //appointments = (from apt in appointments
                        //                orderby apt.ID ascending
                        //                select new AppointmentDetailViewModel()
                        //                {
                        //                    ID = apt.ID,
                        //                    AppointmentID = apt.AppointmentID,
                        //                    UserName = apt.UserName,
                        //                    UserID = apt.UserID,
                        //                    ClinicHospitalName = apt.ClinicHospitalName,
                        //                    ClinicHospitalID = apt.ClinicHospitalID,
                        //                    ApprovalState = apt.ApprovalState.ToString(),
                        //                    PreferredDate = apt.PreferredDate,
                        //                    PreferredTime = apt.PreferredTime,
                        //                    Remarks = apt.Remarks,
                        //                    AppointmentDate = apt.AppointmentDate,//
                        //                    AppointmentTime = apt.AppointmentTime//
                        //                }).ToList();
                    }
                    break;
                case "apptdate":
                    if (order.Equals("desc"))
                    {
                        appointments = appointments.OrderByDescending(o => o.AppointmentDate ?? DateTime.MaxValue).ToList();
                    }
                    else
                    {
                        appointments = appointments.OrderBy(o => o.AppointmentDate ?? DateTime.MaxValue).ToList();
                    }
                    break;
                case "prefdate":
                    if (order.Equals("desc"))
                    {
                        appointments = appointments.OrderByDescending(o => o.PreferredDate).ToList();
                    }
                    else
                    {
                        appointments = appointments.OrderBy(o => o.PreferredDate).ToList();
                    }
                    break;
                default:
                    break;
            }//End of orderby switch

            if(indexby != null)
            {
                switch (indexby)
                {
                    case 1:
                        appointments.RemoveAll(o => o.ApprovalState != "1");
                        break;
                    case 2:
                        appointments.RemoveAll(o => o.ApprovalState != "2");
                        break;
                    case 3:
                        appointments.RemoveAll(o => o.ApprovalState != "3");
                        break;
                    case 4:
                        appointments.RemoveAll(o => o.ApprovalState != "4");
                        break;
                    case 5:
                        appointments.RemoveAll(o => o.ApprovalState != "5");
                        break;
                    case 6:
                        appointments.RemoveAll(o => o.ApprovalState == "1");
                        appointments.RemoveAll(o => o.ApprovalState == "3");
                        appointments.RemoveAll(o => o.ApprovalState == "5");
                        break;
                    default:
                        break;
                }
            }//End of indexby switch

            List<AppointmentDetailViewModel> returnList = new List<AppointmentDetailViewModel>();

            int endingPoint = 0;

            if (page == null || page < 1)
            {
                page = 1;
            }

            int startingPoint = ((int)page - 1) * noOfResults;

            if (appointments.Count < (noOfResults + startingPoint))
            {
                endingPoint = appointments.Count;
            }
            else
            {
                endingPoint = noOfResults + startingPoint;
            }

            int maxpage = endingPoint / noOfResults;
            if ((endingPoint % noOfResults) != 0)
            {
                maxpage++;
            }

            if (startingPoint >= endingPoint && appointments.Count != 0)
            {
                return RedirectToAction("Index", "Appointments", new { page = maxpage, perpage = perpage, search = search, searchby = searchby, order = order, orderBy = orderBy, indexby = indexBy });
            }

            int maxpageOverall = appointments.Count / noOfResults;
            if (appointments.Count != 0)
            {
                if ((endingPoint % appointments.Count) != 0)
                {
                    maxpageOverall++;
                }
            }
            returnModel.maxPages = maxpageOverall;

            returnModel.maxRecords = appointments.Count;

            //foreach (var appointment in appointments)
            for (int i = startingPoint; i < endingPoint; i++)
            {

                string approvalString = "";

                switch (appointments[i].ApprovalState)
                {
                    case "1":
                        approvalString = "Pending";
                        break;
                    case "2":
                        approvalString = "Cancelled";
                        break;
                    case "3":
                        approvalString = "Confirmed";
                        break;
                    case "4":
                        approvalString = "Declined";
                        break;
                    case "5":
                        approvalString = "Completed";
                        break;
                    default:
                        approvalString = "Error";
                        break;
                }

                int intaptid = appointments[i].ID;

                var reqDoc = (from apt in db.Appointments
                              join d in db.DoctorDentists on apt.RequestDoctorDentistID equals d.ID
                              where apt.ID == intaptid
                              select new
                              {
                                  RequestDoctorDentistName = d.Name,
                                  RequestDoctorDentistID = d.ID
                              }).SingleOrDefault();

                var doc = (from apt in db.Appointments
                           join d in db.DoctorDentists on apt.DoctorDentistID equals d.ID
                           where apt.ID == intaptid
                           select new
                           {
                               DoctorDentistName = d.Name,
                               DoctorDentistID = d.ID
                           }).SingleOrDefault();


                AppointmentDetailViewModel addAppt = new AppointmentDetailViewModel()
                {
                    ID = appointments[i].ID,
                    AppointmentID = appointments[i].AppointmentID,
                    UserName = appointments[i].UserName,
                    UserID = appointments[i].UserID,
                    ClinicHospitalName = appointments[i].ClinicHospitalName,
                    ClinicHospitalID = appointments[i].ClinicHospitalID,
                    ApprovalState = approvalString,
                    PreferredDate = appointments[i].PreferredDate,
                    PreferredTime = appointments[i].PreferredTime,
                    Remarks = appointments[i].Remarks,
                    AppointmentDate = appointments[i].AppointmentDate,
                    AppointmentTime = appointments[i].AppointmentTime
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

            returnModel.appointments = returnList;

            return View(returnModel);
        }

        // GET: Appointment IndexBy
        public ActionResult IndexBy(int? id, int? page, int? perpage)
        {
            if (id == null)
            {
                return RedirectToAction("Index", "Appointments");
            }

            if (id < 1 || id > 5)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            int noOfResults = 10;
            if (perpage != null)
            {
                noOfResults = (int)perpage;
            }

            AppointmentIndexModel returnModel = new AppointmentIndexModel();

            string userAspId = User.Identity.GetUserId();

            int hospid = (from ch in db.ClinicHospitals
                          join ach in db.AdminAccountClinicHospitals on ch.ID equals ach.ClinicHospitalID
                          join aa in db.AdminAccounts on ach.AdminID equals aa.ID
                          join aspu in db.AspNetUsers on aa.AspNetID equals aspu.Id
                          where aspu.Id == userAspId
                          select ch.ID).SingleOrDefault();

            List<AppointmentDetailViewModel> appointments = new List<AppointmentDetailViewModel>();

            if (id == 2 || id == 4)
            {
                if (hospid == 0)
                {
                    appointments = (from apt in db.Appointments
                                    join u in db.UserAccounts on apt.UserID equals u.ID
                                    join ch in db.ClinicHospitals on apt.ClinicHospitalID equals ch.ID
                                    orderby apt.ID descending
                                    where apt.ApprovalState == 2 || apt.ApprovalState == 4
                                    select new AppointmentDetailViewModel()
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
                                        AppointmentDate = apt.AppointmentDate,//
                                        AppointmentTime = apt.AppointmentTime//
                                    }).ToList();
                }
                else
                {
                    appointments = (from apt in db.Appointments
                                    join u in db.UserAccounts on apt.UserID equals u.ID
                                    join ch in db.ClinicHospitals on apt.ClinicHospitalID equals ch.ID
                                    orderby apt.ID descending
                                    where (ch.ID == hospid) && (apt.ApprovalState == 2 || apt.ApprovalState == 4)
                                    select new AppointmentDetailViewModel()
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
                                        AppointmentDate = apt.AppointmentDate,//
                                        AppointmentTime = apt.AppointmentTime//
                                    }).ToList();
                }


                List<AppointmentDetailViewModel> returnList = new List<AppointmentDetailViewModel>();

                int endingPoint = 0;

                if (page == null || page < 1)
                {
                    page = 1;
                }

                int startingPoint = ((int)page - 1) * noOfResults;

                if (appointments.Count < (noOfResults + startingPoint))
                {
                    endingPoint = appointments.Count;
                }
                else
                {
                    endingPoint = noOfResults + startingPoint;
                }

                int maxpage = endingPoint / noOfResults;
                if ((endingPoint % noOfResults) != 0)
                {
                    maxpage++;
                }

                if (startingPoint >= endingPoint && appointments.Count != 0)
                {

                    return RedirectToAction("IndexBy", "Appointments", new { id = id, page = maxpage, perpage = perpage });
                }

                int maxpageOverall = appointments.Count / noOfResults;
                if (appointments.Count != 0)
                {
                    if ((endingPoint % appointments.Count) != 0)
                    {
                        maxpageOverall++;
                    }
                }
                returnModel.maxPages = maxpageOverall;

                returnModel.maxRecords = appointments.Count;

                //foreach (var appointment in appointments)  foreach (var appointment in appointments)
                for (int i = startingPoint; i < endingPoint; i++)
                {

                    string approvalString = "";

                    switch (appointments[i].ApprovalState)
                    {
                        case "1":
                            approvalString = "Pending";
                            break;
                        case "2":
                            approvalString = "Cancelled";
                            break;
                        case "3":
                            approvalString = "Confirmed";
                            break;
                        case "4":
                            approvalString = "Declined";
                            break;
                        case "5":
                            approvalString = "Completed";
                            break;
                        default:
                            approvalString = "Error";
                            break;
                    }

                    int apptid = appointments[i].ID;

                    var reqDoc = (from apt in db.Appointments
                                  join d in db.DoctorDentists on apt.RequestDoctorDentistID equals d.ID
                                  where apt.ID == apptid
                                  select new
                                  {
                                      RequestDoctorDentistName = d.Name,
                                      RequestDoctorDentistID = d.ID
                                  }).SingleOrDefault();

                    var doc = (from apt in db.Appointments
                               join d in db.DoctorDentists on apt.DoctorDentistID equals d.ID
                               where apt.ID == apptid
                               select new
                               {
                                   DoctorDentistName = d.Name,
                                   DoctorDentistID = d.ID
                               }).SingleOrDefault();


                    AppointmentDetailViewModel addAppt = new AppointmentDetailViewModel()
                    {
                        ID = appointments[i].ID,
                        AppointmentID = appointments[i].AppointmentID,
                        UserName = appointments[i].UserName,
                        UserID = appointments[i].UserID,
                        ClinicHospitalName = appointments[i].ClinicHospitalName,
                        ClinicHospitalID = appointments[i].ClinicHospitalID,
                        ApprovalState = approvalString,
                        PreferredDate = appointments[i].PreferredDate,
                        PreferredTime = appointments[i].PreferredTime,
                        Remarks = appointments[i].Remarks,
                        AppointmentDate = appointments[i].AppointmentDate,
                        AppointmentTime = appointments[i].AppointmentTime
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

                returnModel.appointments = returnList;
                return View(returnModel);
            }
            else
            {
                if (hospid == 0)
                {
                    appointments = (from apt in db.Appointments
                                    join u in db.UserAccounts on apt.UserID equals u.ID
                                    join ch in db.ClinicHospitals on apt.ClinicHospitalID equals ch.ID
                                    orderby apt.ID descending
                                    where apt.ApprovalState == id
                                    select new AppointmentDetailViewModel()
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
                                        AppointmentDate = apt.AppointmentDate,//
                                        AppointmentTime = apt.AppointmentTime//
                                    }).ToList();
                }
                else
                {
                    appointments = (from apt in db.Appointments
                                    join u in db.UserAccounts on apt.UserID equals u.ID
                                    join ch in db.ClinicHospitals on apt.ClinicHospitalID equals ch.ID
                                    orderby apt.ID descending
                                    where (ch.ID == hospid) && (apt.ApprovalState == id)
                                    select new AppointmentDetailViewModel()
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
                                        AppointmentDate = apt.AppointmentDate,//
                                        AppointmentTime = apt.AppointmentTime//
                                    }).ToList();
                }

                List<AppointmentDetailViewModel> returnList = new List<AppointmentDetailViewModel>();

                int endingPoint = 0;

                if (page == null || page < 1)
                {
                    page = 1;
                }

                int startingPoint = ((int)page - 1) * noOfResults;

                if (appointments.Count < (noOfResults + startingPoint))
                {
                    endingPoint = appointments.Count;
                }
                else
                {
                    endingPoint = noOfResults + startingPoint;
                }

                int maxpage = endingPoint / noOfResults;
                if ((endingPoint % noOfResults) != 0)
                {
                    maxpage++;
                }

                if (startingPoint >= endingPoint && appointments.Count != 0)
                {
                    return RedirectToAction("IndexBy", "Appointments", new { id = id, page = maxpage, perpage = perpage });
                }

                int maxpageOverall = appointments.Count / noOfResults;
                if (appointments.Count != 0)
                {
                    if ((endingPoint % appointments.Count) != 0)
                    {
                        maxpageOverall++;
                    }
                }
                returnModel.maxPages = maxpageOverall;

                returnModel.maxRecords = appointments.Count;

                //foreach (var appointment in appointments)  foreach (var appointment in appointments)
                for (int i = startingPoint; i < endingPoint; i++)
                {

                    string approvalString = "";

                    switch (appointments[i].ApprovalState)
                    {
                        case "1":
                            approvalString = "Pending";
                            break;
                        case "2":
                            approvalString = "Cancelled";
                            break;
                        case "3":
                            approvalString = "Confirmed";
                            break;
                        case "4":
                            approvalString = "Declined";
                            break;
                        case "5":
                            approvalString = "Completed";
                            break;
                        default:
                            approvalString = "Error";
                            break;
                    }

                    int apptid = appointments[i].ID;

                    var reqDoc = (from apt in db.Appointments
                                  join d in db.DoctorDentists on apt.RequestDoctorDentistID equals d.ID
                                  where apt.ID == apptid
                                  select new
                                  {
                                      RequestDoctorDentistName = d.Name,
                                      RequestDoctorDentistID = d.ID
                                  }).SingleOrDefault();

                    var doc = (from apt in db.Appointments
                               join d in db.DoctorDentists on apt.DoctorDentistID equals d.ID
                               where apt.ID == apptid
                               select new
                               {
                                   DoctorDentistName = d.Name,
                                   DoctorDentistID = d.ID
                               }).SingleOrDefault();


                    AppointmentDetailViewModel addAppt = new AppointmentDetailViewModel()
                    {
                        ID = appointments[i].ID,
                        AppointmentID = appointments[i].AppointmentID,
                        UserName = appointments[i].UserName,
                        UserID = appointments[i].UserID,
                        ClinicHospitalName = appointments[i].ClinicHospitalName,
                        ClinicHospitalID = appointments[i].ClinicHospitalID,
                        ApprovalState = approvalString,
                        PreferredDate = appointments[i].PreferredDate,
                        PreferredTime = appointments[i].PreferredTime,
                        Remarks = appointments[i].Remarks,
                        AppointmentDate = appointments[i].AppointmentDate,
                        AppointmentTime = appointments[i].AppointmentTime
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

                returnModel.appointments = returnList;

                return View(returnModel);
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

            //check for auth
            string userAspId = User.Identity.GetUserId();

            int hospid = (from ch in db.ClinicHospitals
                          join ach in db.AdminAccountClinicHospitals on ch.ID equals ach.ClinicHospitalID
                          join aa in db.AdminAccounts on ach.AdminID equals aa.ID
                          join aspu in db.AspNetUsers on aa.AspNetID equals aspu.Id
                          where aspu.Id == userAspId
                          select ch.ID).SingleOrDefault();

            if (hospid != 0)
            {
                int matchHospid = (from ch in db.ClinicHospitals
                                   join ap in db.Appointments on ch.ID equals ap.ClinicHospitalID
                                   where ap.ID == id
                                   select ch.ID).SingleOrDefault();
                if (hospid != matchHospid)
                {
                    return RedirectToAction("Unauthorized", "Account");
                }

            }
            //end check for auth

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
                           DoctorDentistID = d.ID,
                           DoctorDentistMaxBookings = d.MaxBookings
                       }).SingleOrDefault();

            //int maxBooking (from )

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

                int currentDayBookings = (from ddb in db.DoctorDentistDateBookings
                                          join dd in db.DoctorDentists on ddb.DoctorDentistID equals dd.ID
                                          where ddb.DateOfBookings == appt.AppointmentDate
                                          select ddb.Bookings).SingleOrDefault();

                returnAppt.approvalString = doc.DoctorDentistName + " Bookings on " + appt.AppointmentDate.Value.Date.ToString("dd/MM/yyyy") + " : <b>" + currentDayBookings + "/" + doc.DoctorDentistMaxBookings + "</b>";
            }
            else
            {
                returnAppt.approvalString = "A doctor/dentist is required, please update the appointment.";
                returnAppt.DoctorDentistName = "Unassigned";
                //addModel.DoctorDentistID = 0;
            }

            returnAppt.listOfTreatments = (from t in db.Treatments
                                           join at in db.AppointmentTreatments on t.ID equals at.TreatmentID
                                           join a in db.Appointments on at.AppointmentID equals a.ID
                                           where a.ID == returnAppt.ID
                                           select t).ToList();

            var timeslots = (from chts in db.ClinicHospitalTimeslots
                             join ch in db.ClinicHospitals on chts.ClinicHospitalID equals ch.ID
                             where ch.ID == returnAppt.ClinicHospitalID
                             orderby chts.TimeslotIndex ascending
                             select new
                             {
                                 Text = chts.TimeRangeSlotString,
                                 Value = chts.TimeslotIndex
                             }).ToList();

            timeslots.RemoveAll(x => x.Text.Equals(""));

            foreach (var timeslot in timeslots)
            {
                if (returnAppt.PreferredTime == timeslot.Value)
                {
                    returnAppt.PreferredTime_s = timeslot.Text;
                }
            }

            if (returnAppt.AppointmentTime != null)
            {
                foreach (var timeslot in timeslots)
                {
                    if (returnAppt.AppointmentTime == timeslot.Value)
                    {
                        returnAppt.AppointmentTime_s = timeslot.Text;
                    }
                }
            }

            return View(returnAppt);
        }

        // GET: Appointments/GetApptByDocDenID
        public JsonResult GetApptByDocDenID(int? id)
        {
            var apptList = (from Appt in db.Appointments
                            where Appt.DoctorDentistID == id
                            select new
                            {
                                AppointmentID = Appt.AppointmentID,
                                AppointmentTime = Appt.AppointmentTime,
                                AppointmentDate = Appt.AppointmentDate
                            }).ToList();
            return Json(apptList, JsonRequestBehavior.AllowGet);
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


            //check for auth
            string userAspId = User.Identity.GetUserId();

            int hospid = (from ch in db.ClinicHospitals
                          join ach in db.AdminAccountClinicHospitals on ch.ID equals ach.ClinicHospitalID
                          join aa in db.AdminAccounts on ach.AdminID equals aa.ID
                          join aspu in db.AspNetUsers on aa.AspNetID equals aspu.Id
                          where aspu.Id == userAspId
                          select ch.ID).SingleOrDefault();

            if (hospid != 0)
            {
                var hosp = (from ch in db.ClinicHospitals
                            where ch.ID == hospid
                            select ch).ToList();

                return View(hosp);

            }
            //end check for auth

            return View(db.ClinicHospitals.ToList());

            //return View(returnModel);
        }

        // GET: Appointments/Create
        public ActionResult Create(int? id)
        {

            //check for auth
            string userAspId = User.Identity.GetUserId();

            int hospid = (from ch in db.ClinicHospitals
                          join ach in db.AdminAccountClinicHospitals on ch.ID equals ach.ClinicHospitalID
                          join aa in db.AdminAccounts on ach.AdminID equals aa.ID
                          join aspu in db.AspNetUsers on aa.AspNetID equals aspu.Id
                          where aspu.Id == userAspId
                          select ch.ID).SingleOrDefault();

            if (hospid != 0)
            {
                int matchHospid = (from ch in db.ClinicHospitals
                                   where ch.ID == id
                                   select ch.ID).SingleOrDefault();
                if (hospid != matchHospid)
                {
                    return RedirectToAction("Unauthorized", "Account");
                }

            }
            //end check for auth

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

            returnModel.listOfTimeslots = new List<SelectListItem>();

            List<SelectListItem> timeslots = new List<SelectListItem>();

            timeslots = (from chts in db.ClinicHospitalTimeslots
                         join ch in db.ClinicHospitals on chts.ClinicHospitalID equals ch.ID
                         where ch.ID == id
                         orderby chts.TimeslotIndex ascending
                         select new SelectListItem()
                         {
                             Text = chts.TimeRangeSlotString,
                             Value = chts.TimeslotIndex.ToString()
                         }).ToList();

            timeslots.RemoveAll(x => x.Text.Equals(""));

            returnModel.listOfTimeslots = timeslots;

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

                //check for auth
                string userAspId = User.Identity.GetUserId();

                int chkhospid = (from ch in db.ClinicHospitals
                                 join ach in db.AdminAccountClinicHospitals on ch.ID equals ach.ClinicHospitalID
                                 join aa in db.AdminAccounts on ach.AdminID equals aa.ID
                                 join aspu in db.AspNetUsers on aa.AspNetID equals aspu.Id
                                 where aspu.Id == userAspId
                                 select ch.ID).SingleOrDefault();

                if (chkhospid != 0 && hospid != chkhospid)
                {
                    return RedirectToAction("Unauthorized", "Account");
                }
                //end check for auth

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


                newAppointment.listOfTimeslots = new List<SelectListItem>();

                List<SelectListItem> timeslots = new List<SelectListItem>();

                timeslots = (from chts in db.ClinicHospitalTimeslots
                             join ch in db.ClinicHospitals on chts.ClinicHospitalID equals ch.ID
                             where ch.ID == hospid
                             orderby chts.TimeslotIndex ascending
                             select new SelectListItem()
                             {
                                 Text = chts.TimeRangeSlotString,
                                 Value = chts.TimeslotIndex.ToString()
                             }).ToList();

                timeslots.RemoveAll(x => x.Text.Equals(""));

                newAppointment.listOfTimeslots = timeslots;

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

            //check for auth
            string userAspId = User.Identity.GetUserId();

            int hospid = (from ch in db.ClinicHospitals
                          join ach in db.AdminAccountClinicHospitals on ch.ID equals ach.ClinicHospitalID
                          join aa in db.AdminAccounts on ach.AdminID equals aa.ID
                          join aspu in db.AspNetUsers on aa.AspNetID equals aspu.Id
                          where aspu.Id == userAspId
                          select ch.ID).SingleOrDefault();

            if (hospid != 0)
            {
                int matchHospid = (from ch in db.ClinicHospitals
                                   join ap in db.Appointments on ch.ID equals ap.ClinicHospitalID
                                   where ap.ID == id
                                   select ch.ID).SingleOrDefault();
                if (hospid != matchHospid)
                {
                    return RedirectToAction("Unauthorized", "Account");
                }

            }
            //end check for auth

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

            if (hospid != 0)
            {
                var listOfHosp = from ClinHosp in db.ClinicHospitals
                                 where ClinHosp.ID == hospid
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
            }
            else
            {
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
            }
            int reqHospid = Int32.Parse(returnAppt.ClinicHospitalID);
            var listOfDoc = from docden in db.DoctorDentists
                            where docden.ClinicHospitalID == reqHospid
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


            returnAppt.listOfTimeslots = new List<SelectListItem>();

            List<SelectListItem> timeslots = new List<SelectListItem>();

            timeslots = (from chts in db.ClinicHospitalTimeslots
                         join ch in db.ClinicHospitals on chts.ClinicHospitalID equals ch.ID
                         where ch.ID == reqHospid
                         orderby chts.TimeslotIndex ascending
                         select new SelectListItem()
                         {
                             Text = chts.TimeRangeSlotString,
                             Value = chts.TimeslotIndex.ToString()
                         }).ToList();

            timeslots.RemoveAll(x => x.Text.Equals(""));

            returnAppt.listOfTimeslots = timeslots;

            return View(returnAppt);
        }

        // POST: Appointments/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(AppointmentEditViewModel model)
        {
            //check for auth
            string userAspId = User.Identity.GetUserId();

            int chkhospid = (from ch in db.ClinicHospitals
                             join ach in db.AdminAccountClinicHospitals on ch.ID equals ach.ClinicHospitalID
                             join aa in db.AdminAccounts on ach.AdminID equals aa.ID
                             join aspu in db.AspNetUsers on aa.AspNetID equals aspu.Id
                             where aspu.Id == userAspId
                             select ch.ID).SingleOrDefault();
            if (ModelState.IsValid)
            {

                //check for auth
                if (chkhospid != 0)
                {
                    int matchHospid = (from ch in db.ClinicHospitals
                                       join ap in db.Appointments on ch.ID equals ap.ClinicHospitalID
                                       where ap.ID == model.ID
                                       select ch.ID).SingleOrDefault();

                    if (chkhospid != matchHospid && chkhospid != Int32.Parse(model.ClinicHospitalID))
                    {
                        return RedirectToAction("Unauthorized", "Account");
                    }

                }
                //end check for auth

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
                if (model.RequestDoctorDentistID != null && !model.RequestDoctorDentistID.Equals("") && !model.RequestDoctorDentistID.Equals("0"))
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

                if (chkhospid != 0)
                {
                    var listOfHosp = from ClinHosp in db.ClinicHospitals
                                     where ClinHosp.ID == chkhospid
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
                }
                else
                {
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
                }

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
                                        join cht in db.ClinicHospitalTreatments on t.ID equals cht.TreatmentID
                                        where cht.ClinicHospitalID == hospid
                                        select new
                                        {
                                            ID = t.ID,
                                            TreatmentID = t.TreatmentID,
                                            TreatmentName = t.TreatmentName,
                                            TreatmentDesc = t.TreatmentDesc,
                                            IsFollowUp = t.IsFollowUp
                                        }).ToList();

                //var listOfTreatments = (from t in db.Treatments
                //                        select new
                //                        {
                //                            ID = t.ID,
                //                            TreatmentID = t.TreatmentID,
                //                            TreatmentName = t.TreatmentName,
                //                            TreatmentDesc = t.TreatmentDesc,
                //                            IsFollowUp = t.IsFollowUp
                //                        }).ToList();

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

                model.listOfTimeslots = new List<SelectListItem>();

                List<SelectListItem> timeslots = new List<SelectListItem>();

                timeslots = (from chts in db.ClinicHospitalTimeslots
                             join ch in db.ClinicHospitals on chts.ClinicHospitalID equals ch.ID
                             where ch.ID == hospid
                             orderby chts.TimeslotIndex ascending
                             select new SelectListItem()
                             {
                                 Text = chts.TimeRangeSlotString,
                                 Value = chts.TimeslotIndex.ToString()
                             }).ToList();

                timeslots.RemoveAll(x => x.Text.Equals(""));

                model.listOfTimeslots = timeslots;



            }
            //ViewBag.ClinicHospitalID = new SelectList(db.ClinicHospitals, "ID", "ClinicHospitalID", appointment.ClinicHospitalID);
            //ViewBag.DoctorDentistID = new SelectList(db.DoctorDentists, "ID", "DoctorDentistID", appointment.DoctorDentistID);
            //ViewBag.RequestDoctorDentistID = new SelectList(db.DoctorDentists, "ID", "DoctorDentistID", appointment.RequestDoctorDentistID);
            //ViewBag.UserID = new SelectList(db.UserAccounts, "ID", "ID", appointment.UserID);

            return View(model);
        }

        // GET: Appointments/Delete/5
        [AuthorizeAdmin(Roles = "SysAdmin")]
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

            //check for auth
            string userAspId = User.Identity.GetUserId();

            int hospid = (from ch in db.ClinicHospitals
                          join ach in db.AdminAccountClinicHospitals on ch.ID equals ach.ClinicHospitalID
                          join aa in db.AdminAccounts on ach.AdminID equals aa.ID
                          join aspu in db.AspNetUsers on aa.AspNetID equals aspu.Id
                          where aspu.Id == userAspId
                          select ch.ID).SingleOrDefault();

            if (hospid != 0)
            {
                int matchHospid = (from ch in db.ClinicHospitals
                                   join ap in db.Appointments on ch.ID equals ap.ClinicHospitalID
                                   where ap.ID == id
                                   select ch.ID).SingleOrDefault();
                if (hospid != matchHospid)
                {
                    return RedirectToAction("Unauthorized", "Account");
                }

            }
            //end check for auth

            return View(appointment);
        }

        // POST: Appointments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [AuthorizeAdmin(Roles = "SysAdmin")]
        public ActionResult DeleteConfirmed(int id)
        {

            //check for auth
            string userAspId = User.Identity.GetUserId();

            int hospid = (from ch in db.ClinicHospitals
                          join ach in db.AdminAccountClinicHospitals on ch.ID equals ach.ClinicHospitalID
                          join aa in db.AdminAccounts on ach.AdminID equals aa.ID
                          join aspu in db.AspNetUsers on aa.AspNetID equals aspu.Id
                          where aspu.Id == userAspId
                          select ch.ID).SingleOrDefault();

            if (hospid != 0)
            {
                int matchHospid = (from ch in db.ClinicHospitals
                                   join ap in db.Appointments on ch.ID equals ap.ClinicHospitalID
                                   where ap.ID == id
                                   select ch.ID).SingleOrDefault();
                if (hospid != matchHospid)
                {
                    return RedirectToAction("Unauthorized", "Account");
                }

            }
            //end check for auth

            Appointment appointment = db.Appointments.Find(id);

            List<AppointmentTreatment> treatmentRels = (from at in db.AppointmentTreatments
                                                        join t in db.Treatments on at.TreatmentID equals t.ID
                                                        where at.AppointmentID == id
                                                        select at).ToList();
            db.AppointmentTreatments.RemoveRange(treatmentRels);
            db.Appointments.Remove(appointment);
            db.SaveChanges();
            return RedirectToAction("Index", "Appointments");
        }

        // POST: Appointments/Details/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Details(int id)
        {
            int approvalNo = 0;
            if (Request.Form["3"] != null)
            {
                approvalNo = 3;
            }
            else if (Request.Form["4"] != null)
            {
                approvalNo = 4;
            }
            else if (Request.Form["5"] != null)
            {
                approvalNo = 5;
            }

            if (approvalNo > 5 || approvalNo < 3)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Appointment appointment = db.Appointments.Find(id);
            if (appointment == null)
            {
                return HttpNotFound();
            }
            if (appointment.AppointmentDate == null || appointment.DoctorDentistID == null)
            {
                return RedirectToAction("Details", "Appointments", new { id = id });
            }
            //check for auth
            string userAspId = User.Identity.GetUserId();

            int hospid = (from ch in db.ClinicHospitals
                          join ach in db.AdminAccountClinicHospitals on ch.ID equals ach.ClinicHospitalID
                          join aa in db.AdminAccounts on ach.AdminID equals aa.ID
                          join aspu in db.AspNetUsers on aa.AspNetID equals aspu.Id
                          where aspu.Id == userAspId
                          select ch.ID).SingleOrDefault();

            if (hospid != 0)
            {
                int matchHospid = (from ch in db.ClinicHospitals
                                   join ap in db.Appointments on ch.ID equals ap.ClinicHospitalID
                                   where ap.ID == appointment.ID
                                   select ch.ID).SingleOrDefault();
                if (hospid != matchHospid)
                {
                    return RedirectToAction("Unauthorized", "Account");
                }

            }
            //end check for auth

            //Change booking int for date of doctor/dentist
            if (((approvalNo == 3 || approvalNo == 5) && appointment.AppointmentDate != null && appointment.DoctorDentistID != null)
            && (appointment.ApprovalState != 3 && appointment.ApprovalState != 5))
            {
                DoctorDentistDateBooking bookingDay = (from dd in db.DoctorDentists
                                                       join dddb in db.DoctorDentistDateBookings on dd.ID equals dddb.DoctorDentistID
                                                       where appointment.AppointmentDate == dddb.DateOfBookings
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
                        DateOfBookings = appointment.AppointmentDate.Value,
                        Bookings = 1,
                        DoctorDentistID = appointment.DoctorDentistID.Value
                    };
                    db.DoctorDentistDateBookings.Add(newBookingDay);
                }

            }
            else if ((approvalNo != appointment.ApprovalState)
                && (appointment.ApprovalState == 3 || appointment.ApprovalState == 5) &&
                (approvalNo != 3 && approvalNo != 5))
            {
                DoctorDentistDateBooking bookingDay = (from dd in db.DoctorDentists
                                                       join dddb in db.DoctorDentistDateBookings on dd.ID equals dddb.DoctorDentistID
                                                       where appointment.AppointmentDate == dddb.DateOfBookings
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
                        DateOfBookings = appointment.AppointmentDate.Value,
                        Bookings = 0,
                        DoctorDentistID = appointment.DoctorDentistID.Value
                    };
                }

            }

            appointment.ApprovalState = approvalNo;
            db.Entry(appointment).State = EntityState.Modified;
            db.SaveChanges();

            return RedirectToAction("Details", "Appointments", new { id = id });
        }

        // POST: Appointments/Index
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index(AppointmentIndexModel model)
        {

            //return View();
            return RedirectToAction("Index", "Appointments", new { page = model.pageNo, perpage = model.perpageNo, search = model.search, searchby = model.searchby, order = model.order, orderBy = model.orderBy, indexby = model.indexby });
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

