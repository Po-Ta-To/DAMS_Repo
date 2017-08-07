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
using System.Data.SqlClient;

namespace DAMS_03.Controllers
{
    [AuthorizeAdmin(Roles = "SysAdmin, HospAdmin, ClerkAdmin")]
    public class ClinicHospitalsController : Controller
    {
        private DAMS_01Entities db = new DAMS_01Entities();

        // GET: ClinicHospitals
        public ActionResult Index()
        {

            string userAspId = User.Identity.GetUserId();

            int hospid = (from ch in db.ClinicHospitals
                          join ach in db.AdminAccountClinicHospitals on ch.ID equals ach.ClinicHospitalID
                          join aa in db.AdminAccounts on ach.AdminID equals aa.ID
                          join aspu in db.AspNetUsers on aa.AspNetID equals aspu.Id
                          where aspu.Id == userAspId
                          select ch.ID).SingleOrDefault();

            if (hospid == 0)
            {
                return View(db.ClinicHospitals.ToList());
            }
            else
            {
                var getClinicHospitalsByHospId = from ch in db.ClinicHospitals
                                                 where ch.ID == hospid
                                                 select ch;

                return View(getClinicHospitalsByHospId.ToList());
            }

        }

        // GET: ClinicHospitals/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ClinicHospital clinicHospital = db.ClinicHospitals.Find(id);
            if (clinicHospital == null)
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
                if (hospid != id)
                {
                    return RedirectToAction("Unauthorized", "Account");
                }

            }
            //end check for auth

            List<OpeningHour> openingHours = new List<OpeningHour>();

            openingHours = (from oh in db.OpeningHours
                            where oh.ClinicHospitalID == clinicHospital.ID
                            select oh).ToList();

            List<ClinicHospitalTimeslot> timeslots = new List<ClinicHospitalTimeslot>();

            timeslots = (from ts in db.ClinicHospitalTimeslots
                         where ts.ClinicHospitalID == clinicHospital.ID
                         orderby ts.TimeslotIndex ascending
                         select ts).ToList();



            ClinicHospitalDetailsModel returnModel = new ClinicHospitalDetailsModel()
            {
                ID = clinicHospital.ID,
                ClinicHospitalID = clinicHospital.ClinicHospitalID,
                ClinicHospitalName = clinicHospital.ClinicHospitalName,
                ClinicHospitalAddress = clinicHospital.ClinicHospitalAddress,
                ClinicHospitalOpenHours = clinicHospital.ClinicHospitalOpenHours,
                ClinicHospitalTel = clinicHospital.ClinicHospitalTel,
                ClinicHospitalEmail = clinicHospital.ClinicHospitalEmail,
                IsStringOpenHours = clinicHospital.IsStringOpenHours,
                OpeningHours = openingHours,
                Timeslot = timeslots
            };

            return View(returnModel);
        }

        // GET: ClinicHospitals/Create
        [AuthorizeAdmin(Roles = "SysAdmin")]
        public ActionResult Create()
        {
            ClinicHospitalCreateModel model = new ClinicHospitalCreateModel();



            model.OpeningHours = new List<OpeningHour>();

            for (int i = 1; i <= 3; i++)
            {
                model.OpeningHours.Add(new OpeningHour()
                {
                    OpeningHoursDay = i
                });
            }

            model.Timeslot = new List<ClinicHospitalTimeslot>();

            for (int i = 1; i <= 5; i++)
            {
                model.Timeslot.Add(new ClinicHospitalTimeslot()
                {
                    TimeslotIndex = i
                });
            }


            return View(model);
        }

        // POST: ClinicHospitals/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [AuthorizeAdmin(Roles = "SysAdmin")]
        public ActionResult Create(ClinicHospitalCreateModel model)
        {
            if (ModelState.IsValid)
            {
                ClinicHospital addClinicHospital = new ClinicHospital()
                {
                    ClinicHospitalID = model.ClinicHospitalID,
                    ClinicHospitalName = model.ClinicHospitalName,
                    ClinicHospitalAddress = model.ClinicHospitalAddress,
                    ClinicHospitalOpenHours = model.ClinicHospitalOpenHours,
                    ClinicHospitalTel = model.ClinicHospitalTel,
                    ClinicHospitalEmail = model.ClinicHospitalEmail,
                    IsStringOpenHours = model.IsStringOpenHours
                };

                db.ClinicHospitals.Add(addClinicHospital);

                for (int i = 0; i < 3; i++)
                {
                    OpeningHour addOpenHr = new OpeningHour()
                    {
                        OpeningHoursDay = (i + 1),
                        TimeRangeStart = model.OpeningHours[i].TimeRangeStart,
                        TimeRangeEnd = model.OpeningHours[i].TimeRangeEnd,
                        ClinicHospitalID = addClinicHospital.ID
                    };

                    db.OpeningHours.Add(addOpenHr);

                }

                for (int i = 0; i < 5; i++)
                {
                    if (model.Timeslot[i].TimeRangeSlotString != null)
                    {
                        ClinicHospitalTimeslot addNewTimeSlot = new ClinicHospitalTimeslot()
                        {
                            TimeslotIndex = model.Timeslot[i].TimeslotIndex,
                            TimeRangeSlotString = model.Timeslot[i].TimeRangeSlotString,
                            ClinicHospitalID = addClinicHospital.ID
                        };
                        db.ClinicHospitalTimeslots.Add(addNewTimeSlot);
                    }
                    else
                    {
                        ClinicHospitalTimeslot addNewTimeSlot = new ClinicHospitalTimeslot()
                        {
                            TimeslotIndex = model.Timeslot[i].TimeslotIndex,
                            TimeRangeSlotString = "",
                            ClinicHospitalID = addClinicHospital.ID
                        };
                        db.ClinicHospitalTimeslots.Add(addNewTimeSlot);
                    }
                }


                db.SaveChanges();
                return RedirectToAction("Index");
            }
            else
            {
                model.OpeningHours = new List<OpeningHour>();

                for (int i = 1; i <= 3; i++)
                {
                    model.OpeningHours.Add(new OpeningHour()
                    {
                        OpeningHoursDay = i
                    });
                }

                model.Timeslot = new List<ClinicHospitalTimeslot>();

                for (int i = 1; i <= 5; i++)
                {
                    model.Timeslot.Add(new ClinicHospitalTimeslot()
                    {
                        TimeslotIndex = i
                    });
                }
            }

            return View(model);
        }

        //get treatment by hospital
        // GET: ClinicHospitals/Treatments/5
        public ActionResult Treatments(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ClinicHospital clinicHospital = db.ClinicHospitals.Find(id);
            if (clinicHospital == null)
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
                if (hospid != id)
                {
                    return RedirectToAction("Unauthorized", "Account");
                }

            }
            //end check for auth

            //var getTreatmentList = from cht in db.ClinicHospitalTreatments
            //                       where cht.ClinicHospitalID == id
            //                       select cht;

            var someTreatments = from t in db.Treatments
                                 join cht in db.ClinicHospitalTreatments on t.ID equals cht.TreatmentID
                                 join ch in db.ClinicHospitals on cht.ClinicHospitalID equals ch.ID
                                 where cht.ClinicHospitalID == id
                                 select new AddTreatmentsModel()
                                 {
                                     TreatmentID = t.ID,
                                     //TreatmentName = t.TreatmentName,
                                     //TreatmentDesc = t.TreatmentDesc,
                                     //IsFollowUp = t.IsFollowUp,
                                     PriceLow = cht.PriceLow,
                                     PriceHigh = cht.PriceHigh

                                 };


            var allTreatments = from t in db.Treatments
                                select new AddTreatmentsModel()
                                {
                                    TreatmentID = t.ID,
                                    TreatmentName = t.TreatmentName,
                                    TreatmentDesc = t.TreatmentDesc,
                                    IsFollowUp = t.IsFollowUp,


                                };

            List<AddTreatmentsModel> returnList = new List<AddTreatmentsModel>();

            foreach (var treatment in allTreatments)
            {
                bool added = false;

                foreach (var someTreatment in someTreatments)
                {

                    if (someTreatment.TreatmentID == treatment.TreatmentID)
                    {
                        returnList.Add(new AddTreatmentsModel()
                        {
                            TreatmentID = treatment.TreatmentID,
                            TreatmentName = treatment.TreatmentName,
                            TreatmentDesc = treatment.TreatmentDesc,
                            IsFollowUp = treatment.IsFollowUp,
                            PriceLow = someTreatment.PriceLow,
                            PriceHigh = someTreatment.PriceHigh,
                            IsChecked = true

                        });
                        added = true;
                        break;
                    }

                }

                if (!added)
                {
                    returnList.Add(new AddTreatmentsModel()
                    {
                        TreatmentID = treatment.TreatmentID,
                        TreatmentName = treatment.TreatmentName,
                        TreatmentDesc = treatment.TreatmentDesc,
                        IsFollowUp = treatment.IsFollowUp,
                        PriceLow = 0,
                        PriceHigh = 0,
                        IsChecked = false

                    });

                }

            }


            //return View(clinicHospital);



            return View(returnList);
        }


        // GET: ClinicHospitals/EditTreatments/5
        [AuthorizeAdmin(Roles = "SysAdmin, HospAdmin")]
        public ActionResult EditTreatments(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ClinicHospital clinicHospital = db.ClinicHospitals.Find(id);
            if (clinicHospital == null)
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
                if (hospid != id)
                {
                    return RedirectToAction("Unauthorized", "Account");
                }

            }
            //end check for auth

            //var getTreatmentList = from cht in db.ClinicHospitalTreatments
            //                       where cht.ClinicHospitalID == id
            //                       select cht;

            var someTreatments = from t in db.Treatments
                                 join cht in db.ClinicHospitalTreatments on t.ID equals cht.TreatmentID
                                 join ch in db.ClinicHospitals on cht.ClinicHospitalID equals ch.ID
                                 where cht.ClinicHospitalID == id
                                 select new AddTreatmentsModel()
                                 {
                                     TreatmentID = t.ID,
                                     //TreatmentName = t.TreatmentName,
                                     //TreatmentDesc = t.TreatmentDesc,
                                     //IsFollowUp = t.IsFollowUp,
                                     PriceLow = cht.PriceLow,
                                     PriceHigh = cht.PriceHigh

                                 };


            var allTreatments = from t in db.Treatments
                                select new AddTreatmentsModel()
                                {
                                    TreatmentID = t.ID,
                                    TreatmentName = t.TreatmentName,
                                    TreatmentDesc = t.TreatmentDesc,
                                    IsFollowUp = t.IsFollowUp,


                                };

            List<EditTreatmentsModel> returnList = new List<EditTreatmentsModel>();

            foreach (var treatment in allTreatments)
            {
                bool added = false;

                foreach (var someTreatment in someTreatments)
                {

                    if (someTreatment.TreatmentID == treatment.TreatmentID)
                    {
                        returnList.Add(new EditTreatmentsModel()
                        {
                            TreatmentID = treatment.TreatmentID,
                            TreatmentName = treatment.TreatmentName,
                            TreatmentDesc = treatment.TreatmentDesc,
                            IsFollowUp = treatment.IsFollowUp,
                            PriceLow = someTreatment.PriceLow,
                            PriceHigh = someTreatment.PriceHigh,
                            IsChecked = true

                        });
                        added = true;
                        break;
                    }

                }

                if (!added)
                {
                    returnList.Add(new EditTreatmentsModel()
                    {
                        TreatmentID = treatment.TreatmentID,
                        TreatmentName = treatment.TreatmentName,
                        TreatmentDesc = treatment.TreatmentDesc,
                        IsFollowUp = treatment.IsFollowUp,
                        PriceLow = 0,
                        PriceHigh = 0,
                        IsChecked = false

                    });

                }

            }


            //return View(clinicHospital);



            return View(returnList);
            //return View(adminAccounts.ToList());
        }


        // POST: ClinicHospitals/EditTreatments/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [AuthorizeAdmin(Roles = "SysAdmin, HospAdmin")]
        public ActionResult EditTreatments(List<EditTreatmentsModel> editTreatmentsList)
        {

            if (editTreatmentsList == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

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

                if (chkhospid != 0)
                {
                    if (hospid != chkhospid)
                    {
                        return RedirectToAction("Unauthorized", "Account");
                    }

                }
                //end check for auth

                var dellist = from cht in db.ClinicHospitalTreatments
                              join ch in db.ClinicHospitals on cht.ClinicHospitalID equals ch.ID
                              where cht.ClinicHospitalID == hospid
                              select cht;

                db.ClinicHospitalTreatments.RemoveRange(dellist);

                foreach (EditTreatmentsModel editTreatment in editTreatmentsList)
                {
                    if (editTreatment.IsChecked == true)
                    {
                        db.ClinicHospitalTreatments.Add(new ClinicHospitalTreatment()
                        {
                            TreatmentID = editTreatment.TreatmentID,
                            ClinicHospitalID = hospid,
                            PriceLow = editTreatment.PriceLow,
                            PriceHigh = editTreatment.PriceHigh
                        });
                    }


                }


                //db.Entry(clinicHospital).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(editTreatmentsList);
        }

        // GET: ClinicHospitals/Edit/5
        [AuthorizeAdmin(Roles = "SysAdmin, HospAdmin")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ClinicHospital clinicHospital = db.ClinicHospitals.Find(id);
            if (clinicHospital == null)
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
                if (hospid != id)
                {
                    return RedirectToAction("Unauthorized", "Account");
                }

            }
            //end check for auth

            List<OpeningHour> openingHours = new List<OpeningHour>();

            openingHours = (from oh in db.OpeningHours
                            where oh.ClinicHospitalID == clinicHospital.ID
                            select oh).ToList();

            List<ClinicHospitalTimeslotHelperClass> timeslots = new List<ClinicHospitalTimeslotHelperClass>();

            timeslots = (from ts in db.ClinicHospitalTimeslots
                         where ts.ClinicHospitalID == clinicHospital.ID
                         orderby ts.TimeslotIndex ascending
                         select new ClinicHospitalTimeslotHelperClass()
                         {
                             TimeslotIndex = ts.TimeslotIndex,
                             TimeRangeSlotString = ts.TimeRangeSlotString
                         }).ToList();

            ClinicHospitalEditModel returnModel = new ClinicHospitalEditModel()
            {
                ID = clinicHospital.ID,
                ClinicHospitalID = clinicHospital.ClinicHospitalID,
                ClinicHospitalName = clinicHospital.ClinicHospitalName,
                ClinicHospitalAddress = clinicHospital.ClinicHospitalAddress,
                ClinicHospitalOpenHours = clinicHospital.ClinicHospitalOpenHours,
                ClinicHospitalTel = clinicHospital.ClinicHospitalTel,
                ClinicHospitalEmail = clinicHospital.ClinicHospitalEmail,
                IsStringOpenHours = clinicHospital.IsStringOpenHours,
                OpeningHours = openingHours,
                Timeslot = timeslots
            };

            return View(returnModel);
        }

        // POST: ClinicHospitals/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [AuthorizeAdmin(Roles = "SysAdmin, HospAdmin")]
        public ActionResult Edit(ClinicHospitalEditModel model)
        {
            if (ModelState.IsValid)
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
                    if (hospid != model.ID)
                    {
                        return RedirectToAction("Unauthorized", "Account");
                    }

                }
                //end check for auth


                ClinicHospital editClinicHospital = (from ch in db.ClinicHospitals
                                                     where ch.ID == model.ID
                                                     select ch).SingleOrDefault();


                editClinicHospital.ClinicHospitalID = model.ClinicHospitalID;
                editClinicHospital.ClinicHospitalName = model.ClinicHospitalName;
                editClinicHospital.ClinicHospitalAddress = model.ClinicHospitalAddress;
                editClinicHospital.ClinicHospitalOpenHours = model.ClinicHospitalOpenHours;
                editClinicHospital.ClinicHospitalTel = model.ClinicHospitalTel;
                editClinicHospital.ClinicHospitalEmail = model.ClinicHospitalEmail;
                editClinicHospital.IsStringOpenHours = model.IsStringOpenHours;

                db.Entry(editClinicHospital).State = EntityState.Modified;

                List<OpeningHour> editOpeningHour = (from oh in db.OpeningHours
                                                     join ch in db.ClinicHospitals on oh.ClinicHospitalID equals ch.ID
                                                     where ch.ID == model.ID
                                                     select oh).ToList();

                for (int i = 0; i < editOpeningHour.Count; i++)
                {
                    for (int j = 0; j < model.OpeningHours.Count; j++)
                    {
                        if (editOpeningHour[i].OpeningHoursDay == model.OpeningHours[j].OpeningHoursDay)
                        {
                            editOpeningHour[i].TimeRangeStart = model.OpeningHours[j].TimeRangeStart;
                            editOpeningHour[i].TimeRangeEnd = model.OpeningHours[j].TimeRangeEnd;
                            db.Entry(editOpeningHour[i]).State = EntityState.Modified;
                            break;
                        }
                    }

                }

                List<ClinicHospitalTimeslot> editTimeslot = (from ts in db.ClinicHospitalTimeslots
                                                             join ch in db.ClinicHospitals on ts.ClinicHospitalID equals ch.ID
                                                             where ch.ID == model.ID
                                                             select ts).ToList();

                for (int i = 0; i < editTimeslot.Count; i++)
                {
                    for (int j = 0; j < model.Timeslot.Count; j++)
                    {
                        if (editTimeslot[i].TimeslotIndex == model.Timeslot[j].TimeslotIndex)
                        {
                            if (model.Timeslot[j].TimeRangeSlotString != null)
                            {
                                editTimeslot[i].TimeRangeSlotString = model.Timeslot[j].TimeRangeSlotString;
                            }
                            else
                            {
                                editTimeslot[i].TimeRangeSlotString = "";
                            }

                            db.Entry(editTimeslot[i]).State = EntityState.Modified;
                            break;
                        }
                    }

                }

                db.SaveChanges();
                return RedirectToAction("Index");
            }



            return View(model);
        }

        // GET: ClinicHospitals/Delete/5
        [AuthorizeAdmin(Roles = "SysAdmin")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ClinicHospital clinicHospital = db.ClinicHospitals.Find(id);
            if (clinicHospital == null)
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
                if (hospid != id)
                {
                    return RedirectToAction("Unauthorized", "Account");
                }

            }
            //end check for auth

            return View(clinicHospital);
        }

        // POST: ClinicHospitals/Delete/5
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
                if (hospid != id)
                {
                    return RedirectToAction("Unauthorized", "Account");
                }

            }
            //end check for auth

            if (Request.Form["DeleteAll"] != null)
            {
                ClinicHospital clinicHospital = db.ClinicHospitals.Find(id);
                List<ClinicHospitalTimeslot> clinicHospitalTimeslots = (from cht in db.ClinicHospitalTimeslots
                                                                        join ch in db.ClinicHospitals on cht.ClinicHospitalID equals ch.ID
                                                                        where ch.ID == id
                                                                        select cht).ToList();
                List<ClinicHospitalTreatment> clinicHospitalTreatments = (from cht in db.ClinicHospitalTreatments
                                                                          join ch in db.ClinicHospitals on cht.ClinicHospitalID equals ch.ID
                                                                          where ch.ID == id
                                                                          select cht).ToList();
                List<DoctorDentist> doctorDentist = (from dd in db.DoctorDentists
                                                     join ch in db.ClinicHospitals on dd.ClinicHospitalID equals ch.ID
                                                     where ch.ID == id
                                                     select dd).ToList();
                foreach (DoctorDentist doc in doctorDentist)
                {
                    List<DoctorDentistDateBooking> doctorDentistBookings = (from dat in db.DoctorDentistDateBookings
                                                                            join dd in db.DoctorDentists on dat.DoctorDentistID equals dd.ID
                                                                            where dd.ID == doc.ID
                                                                            select dat).ToList();
                    db.DoctorDentistDateBookings.RemoveRange(doctorDentistBookings);
                }
                List<Appointment> appointments = (from apt in db.Appointments
                                                  join ch in db.ClinicHospitals on apt.ClinicHospitalID equals ch.ID
                                                  where ch.ID == id
                                                  select apt).ToList();
                foreach (Appointment apt in appointments)
                {
                    List<AppointmentTreatment> appointmentTreatments = (from at in db.AppointmentTreatments
                                                                        join a in db.Appointments on at.AppointmentID equals a.ID
                                                                        where a.ID == apt.ID
                                                                        select at).ToList();
                    db.AppointmentTreatments.RemoveRange(appointmentTreatments);
                }
                List<AdminAccountClinicHospital> adminAccountClinicHospitals = (from aach in db.AdminAccountClinicHospitals
                                                                                join ch in db.ClinicHospitals on aach.ClinicHospitalID equals ch.ID
                                                                                where ch.ID == id
                                                                                select aach).ToList();
                List<OpeningHour> OpeningHours = (from oh in db.OpeningHours
                                                  join ch in db.ClinicHospitals on oh.ClinicHospitalID equals ch.ID
                                                  where ch.ID == id
                                                  select oh).ToList();
                db.OpeningHours.RemoveRange(OpeningHours);
                db.AdminAccountClinicHospitals.RemoveRange(adminAccountClinicHospitals);
                db.Appointments.RemoveRange(appointments);
                db.DoctorDentists.RemoveRange(doctorDentist);
                db.ClinicHospitalTimeslots.RemoveRange(clinicHospitalTimeslots);
                db.ClinicHospitalTreatments.RemoveRange(clinicHospitalTreatments);
                db.ClinicHospitals.Remove(clinicHospital);
                db.SaveChanges();
                return RedirectToAction("Index", "ClinicHospitals");
            }

            try
            {
                ClinicHospital clinicHospital = db.ClinicHospitals.Find(id);
                db.ClinicHospitals.Remove(clinicHospital);
                db.SaveChanges();
                return RedirectToAction("Index", "ClinicHospitals");
            }
            catch (Exception e)
            {
                SqlException sqle = (SqlException)e.InnerException.InnerException;

                if (sqle.Number == 547)
                {
                    return RedirectToAction("Delete", "ClinicHospitals", new { id = id, error = "547" });
                }
                throw;
            }

            //ClinicHospital clinicHospital = db.ClinicHospitals.Find(id);
            //db.ClinicHospitals.Remove(clinicHospital);
            //db.SaveChanges();
            //return RedirectToAction("Index");
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
