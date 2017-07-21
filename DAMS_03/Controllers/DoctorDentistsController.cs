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
    public class DoctorDentistsController : Controller
    {
        private DAMS_01Entities db = new DAMS_01Entities();

        // GET: DoctorDentists
        public ActionResult Index()
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

        // GET: DoctorDentists/Create
        public ActionResult Create()
        {

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

        }

        // POST: DoctorDentists/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(DoctorDentistCreateModel model)
        {
            if (ModelState.IsValid)
            {

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
            return View(doctorDentist);
        }


        // POST: DoctorDentists/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(DoctorDentistEditModel model)
        {
            if (ModelState.IsValid)
            {
                DoctorDentist editDoctorDentist = (from d in db.DoctorDentists
                                                   where d.ID == model.ID
                                                   select d).SingleOrDefault();

                editDoctorDentist.DoctorDentistID = model.DoctorDentistID;
                editDoctorDentist.Name = model.Name;
                editDoctorDentist.MaxBookings = model.MaxBookings;

                db.Entry(editDoctorDentist).State = EntityState.Modified;

                db.SaveChanges();
                return RedirectToAction("Index" , "DoctorDentists");
            }

            return View(model);
        }

        // GET: DoctorDentists/Delete/5
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
            return View(doctorDentist);
        }

        // POST: DoctorDentists/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
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
