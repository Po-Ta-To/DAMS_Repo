using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using DAMS_03.Models;

namespace DAMS_03.Controllers
{
    [Authorize(Roles = "SysAdmin, HospAdmin, ClerkAdmin")]
    public class ClinicHospitalsController : Controller
    {
        private DAMS_01Entities db = new DAMS_01Entities();

        // GET: ClinicHospitals
        public ActionResult Index()
        {
            return View(db.ClinicHospitals.ToList());
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
            return View(clinicHospital);
        }

        // GET: ClinicHospitals/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ClinicHospitals/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,ClinicHospitalID,ClinicHospitalName,ClinicHospitalAddress,ClinicHospitalOpenHours,ClinicHospitalTel,ClinicHospitalEmail,MaxBookings")] ClinicHospital clinicHospital)
        {
            if (ModelState.IsValid)
            {
                db.ClinicHospitals.Add(clinicHospital);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(clinicHospital);
        }

        // GET: ClinicHospitals/Edit/5
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
            return View(clinicHospital);
        }

        // POST: ClinicHospitals/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,ClinicHospitalID,ClinicHospitalName,ClinicHospitalAddress,ClinicHospitalOpenHours,ClinicHospitalTel,ClinicHospitalEmail,MaxBookings")] ClinicHospital clinicHospital)
        {
            if (ModelState.IsValid)
            {
                db.Entry(clinicHospital).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(clinicHospital);
        }

        // GET: ClinicHospitals/Delete/5
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
            return View(clinicHospital);
        }

        // POST: ClinicHospitals/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ClinicHospital clinicHospital = db.ClinicHospitals.Find(id);
            db.ClinicHospitals.Remove(clinicHospital);
            db.SaveChanges();
            return RedirectToAction("Index");
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
