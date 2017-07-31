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
    public class DoctorDentistsController : Controller
    {
        private DAMS_01Entities db = new DAMS_01Entities();

        // GET: DoctorDentists
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
                List<DoctorDentistDetailModel> returnListofDoc = new List<DoctorDentistDetailModel>();

                foreach (DoctorDentist doctorDentist in db.DoctorDentists)
                {

                    string returnDocHospName = (from hc in db.ClinicHospitals
                                                join d in db.DoctorDentists on hc.ID equals d.ClinicHospitalID
                                                where d.ID == doctorDentist.ID
                                                select hc.ClinicHospitalName).First().ToString();

                    DoctorDentistDetailModel returnModel = new DoctorDentistDetailModel()
                    {
                        ID = doctorDentist.ID,
                        DoctorDentistID = doctorDentist.DoctorDentistID,
                        Name = doctorDentist.Name,
                        HospClin = returnDocHospName,
                        MaxBookings = doctorDentist.MaxBookings
                    };
                    returnListofDoc.Add(returnModel);
                }

                return View(returnListofDoc);
            }
            else
            {
                List<DoctorDentistDetailModel> returnListofDoc = new List<DoctorDentistDetailModel>();

                returnListofDoc = (from hc in db.ClinicHospitals
                                   join d in db.DoctorDentists on hc.ID equals d.ClinicHospitalID
                                   where hc.ID == hospid
                                   select new DoctorDentistDetailModel()
                                   {
                                       ID = d.ID,
                                       DoctorDentistID = d.DoctorDentistID,
                                       Name = d.Name,
                                       HospClin = hc.ClinicHospitalName,
                                       MaxBookings = d.MaxBookings
                                   }).ToList();

                return View(returnListofDoc);
            }


        }

        // GET: DoctorDentists/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DoctorDentist doctorDentist = db.DoctorDentists.Find(id);
            if (doctorDentist == null)
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
                                   join dd in db.DoctorDentists on ch.ID equals dd.ClinicHospitalID
                                   where dd.ID == id
                                   select ch.ID).SingleOrDefault();
                if (hospid != matchHospid)
                {
                    return RedirectToAction("Unauthorized", "Account");
                }

            }
            //end check for auth

            DoctorDentist returnDoc = (from d in db.DoctorDentists
                                       where d.ID == id
                                       select d).SingleOrDefault();

            string returnDocHospName = (from hc in db.ClinicHospitals
                                        join d in db.DoctorDentists on hc.ID equals d.ClinicHospitalID
                                        where d.ID == id
                                        select hc.ClinicHospitalName).First().ToString();

            DoctorDentistDetailModel returnModel = new DoctorDentistDetailModel()
            {
                ID = returnDoc.ID,
                DoctorDentistID = returnDoc.DoctorDentistID,
                Name = returnDoc.Name,
                HospClin = returnDocHospName,
                MaxBookings = returnDoc.MaxBookings
            };

            return View(returnModel);
        }

        // GET: DoctorDentists/GetByDocDenID/1
        public JsonResult GetByDocDenID(int? id)
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
                                   join dd in db.DoctorDentists on ch.ID equals dd.ClinicHospitalID
                                   where dd.ID == id
                                   select ch.ID).SingleOrDefault();
                if (hospid != matchHospid)
                {
                    Response.StatusCode = (int)HttpStatusCode.Unauthorized;
                    return Json(new { Errormessage = "Unauthorized" });
                }

            }
            //end check for auth

            var dddbList = (from DDDB in db.DoctorDentistDateBookings
                            where DDDB.DoctorDentistID == id
                            select new
                            {
                                DateOfBookings = DDDB.DateOfBookings.ToString(),
                                Bookings = DDDB.Bookings,
                                DoctorDentistID = DDDB.DoctorDentistID
                            }).ToList();

            // Passing over the list(in Json format) from controller to mvc
            return Json(dddbList, JsonRequestBehavior.AllowGet);
        }

        // GET: DoctorDentists/Create
        [AuthorizeAdmin(Roles = "SysAdmin, HospAdmin")]
        public ActionResult Create()
        {

            DoctorDentistCreateModel returnmodel = new DoctorDentistCreateModel();

            returnmodel.itemSelection = new List<SelectListItem>();

            //check for auth
            string userAspId = User.Identity.GetUserId();

            int hospid = (from ch in db.ClinicHospitals
                          join ach in db.AdminAccountClinicHospitals on ch.ID equals ach.ClinicHospitalID
                          join aa in db.AdminAccounts on ach.AdminID equals aa.ID
                          join aspu in db.AspNetUsers on aa.AspNetID equals aspu.Id
                          where aspu.Id == userAspId
                          select ch.ID).SingleOrDefault();
            //end check for auth

            if (User.IsInRole("HospAdmin") || hospid != 0)
            {
                //string userAspId = User.Identity.GetUserId();

                //int hospid = (from ch in db.ClinicHospitals
                //              join ach in db.AdminAccountClinicHospitals on ch.ID equals ach.ClinicHospitalID
                //              join aa in db.AdminAccounts on ach.AdminID equals aa.ID
                //              join aspu in db.AspNetUsers on aa.AspNetID equals aspu.Id
                //              where aspu.Id == userAspId
                //              select ch.ID).SingleOrDefault();

                var listOfHosp = from ClinHosp in db.ClinicHospitals
                                 where ClinHosp.ID == hospid
                                 select new SelectListItem()
                                 {
                                     Value = ClinHosp.ID.ToString(),
                                     Text = ClinHosp.ClinicHospitalName
                                 };
                returnmodel.itemSelection.AddRange(listOfHosp.ToList<SelectListItem>());
            }
            else
            {
                var listOfHosp = from ClinHosp in db.ClinicHospitals
                                 select new SelectListItem()
                                 {
                                     Value = ClinHosp.ID.ToString(),
                                     Text = ClinHosp.ClinicHospitalName
                                 };
                returnmodel.itemSelection.AddRange(listOfHosp.ToList<SelectListItem>());
            }

            //Doctor/Dentist MUST belong to a Hospital/Clinic
            //returnmodel.itemSelection.Add(new SelectListItem()
            //{
            //    Value = "notselected",
            //    Text = " - "
            //});

            return View(returnmodel);

        }

        // POST: DoctorDentists/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [AuthorizeAdmin(Roles = "SysAdmin, HospAdmin")]
        public ActionResult Create(DoctorDentistCreateModel model)
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
                //end check for auth

                if (User.IsInRole("HospAdmin") || hospid != 0)
                {
                    if (Int32.Parse(model.HospClinID) != hospid)
                    {
                        return RedirectToAction("Unauthorized", "Account");
                    }
                }

                DoctorDentist addDoctorDentist = new DoctorDentist()
                {
                    DoctorDentistID = model.DoctorDentistID,
                    Name = model.Name,
                    MaxBookings = model.MaxBookings,
                    ClinicHospitalID = Int32.Parse(model.HospClinID)
                };

                db.DoctorDentists.Add(addDoctorDentist);
                db.SaveChanges();


                //ClinicHospitalDoctorDentist addRelation = new ClinicHospitalDoctorDentist()
                //{
                //    ClinicHospitalID = Int32.Parse(model.HospClinID),
                //    DoctorDentistID = addDoctorDentist.ID
                //};

                //db.ClinicHospitalDoctorDentists.Add(addRelation);
                db.SaveChanges();

                return RedirectToAction("Index", "DoctorDentists");
            }


            //
            DoctorDentistCreateModel returnmodel = new DoctorDentistCreateModel();

            returnmodel.itemSelection = new List<SelectListItem>();

            var listOfHosp = from ClinHosp in db.ClinicHospitals
                             select new SelectListItem()
                             {
                                 Value = ClinHosp.ID.ToString(),
                                 Text = ClinHosp.ClinicHospitalName
                             };

            //Doctor/Dentist MUST belong to a Hospital/Clinic
            //returnmodel.itemSelection.Add(new SelectListItem()
            //{
            //    Value = "notselected",
            //    Text = " - "
            //});

            returnmodel.itemSelection.AddRange(listOfHosp.ToList<SelectListItem>());

            return View(returnmodel);

            //return View(doctorDentist);
        }

        // GET: DoctorDentists/Edit/5
        [AuthorizeAdmin(Roles = "SysAdmin, HospAdmin")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DoctorDentist doctorDentist = db.DoctorDentists.Find(id);
            if (doctorDentist == null)
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
                                   join dd in db.DoctorDentists on ch.ID equals dd.ClinicHospitalID
                                   where dd.ID == id
                                   select ch.ID).SingleOrDefault();
                if (hospid != matchHospid)
                {
                    return RedirectToAction("Unauthorized", "Account");
                }

            }
            //end check for auth

            string hospClinName = (from hc in db.ClinicHospitals
                                   join d in db.DoctorDentists on hc.ID equals d.ClinicHospitalID
                                   where d.ID == id
                                   select hc.ClinicHospitalName).First().ToString();

            DoctorDentistEditModel returnModel = new DoctorDentistEditModel()
            {
                ID = doctorDentist.ID,
                DoctorDentistID = doctorDentist.DoctorDentistID,
                Name = doctorDentist.Name,
                MaxBookings = doctorDentist.MaxBookings,
                HospClin = hospClinName
            };

            return View(returnModel);
        }

        // GET: DoctorDentists/ViewSchedule
        public ActionResult ViewSchedule(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            //Change the "Find" later 
            DoctorDentist doctorDentist = db.DoctorDentists.Find(id);
            if (doctorDentist == null)
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
                                   join dd in db.DoctorDentists on ch.ID equals dd.ClinicHospitalID
                                   where dd.ID == id
                                   select ch.ID).SingleOrDefault();
                if (hospid != matchHospid)
                {
                    return RedirectToAction("Unauthorized", "Account");
                }

            }
            //end check for auth

            return View(doctorDentist);
        }


        // POST: DoctorDentists/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [AuthorizeAdmin(Roles = "SysAdmin, HospAdmin")]
        public ActionResult Edit(DoctorDentistEditModel model)
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
                    int matchHospid = (from ch in db.ClinicHospitals
                                       join dd in db.DoctorDentists on ch.ID equals dd.ClinicHospitalID
                                       where dd.ID == model.ID
                                       select ch.ID).SingleOrDefault();
                    if (hospid != matchHospid)
                    {
                        return RedirectToAction("Unauthorized", "Account");
                    }

                }
                //end check for auth

                DoctorDentist editDoctorDentist = (from d in db.DoctorDentists
                                                   where d.ID == model.ID
                                                   select d).SingleOrDefault();

                editDoctorDentist.DoctorDentistID = model.DoctorDentistID;
                editDoctorDentist.Name = model.Name;
                editDoctorDentist.MaxBookings = model.MaxBookings;

                db.Entry(editDoctorDentist).State = EntityState.Modified;

                db.SaveChanges();
                return RedirectToAction("Index", "DoctorDentists");
            }

            return View(model);
        }

        // GET: DoctorDentists/Delete/5
        [AuthorizeAdmin(Roles = "SysAdmin, HospAdmin")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DoctorDentist doctorDentist = db.DoctorDentists.Find(id);
            if (doctorDentist == null)
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
                                   join dd in db.DoctorDentists on ch.ID equals dd.ClinicHospitalID
                                   where dd.ID == id
                                   select ch.ID).SingleOrDefault();
                if (hospid != matchHospid)
                {
                    return RedirectToAction("Unauthorized", "Account");
                }

            }
            //end check for auth

            return View(doctorDentist);
        }

        // POST: DoctorDentists/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [AuthorizeAdmin(Roles = "SysAdmin, HospAdmin")]
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
                                   join dd in db.DoctorDentists on ch.ID equals dd.ClinicHospitalID
                                   where dd.ID == id
                                   select ch.ID).SingleOrDefault();
                if (hospid != matchHospid)
                {
                    return RedirectToAction("Unauthorized", "Account");
                }

            }
            //end check for auth

            DoctorDentist doctorDentist = db.DoctorDentists.Find(id);
            db.DoctorDentists.Remove(doctorDentist);
            db.SaveChanges();
            return RedirectToAction("Index", "DoctorDentists");
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
