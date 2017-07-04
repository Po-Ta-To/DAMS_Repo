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
    public class DoctorDentistsController : Controller
    {
        private DAMS_01Entities db = new DAMS_01Entities();

        // GET: DoctorDentists
        public ActionResult Index()
        {
            return View(db.DoctorDentists.ToList());
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
            return View(doctorDentist);
        }

        // GET: DoctorDentists/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: DoctorDentists/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,DoctorDentistID,Name")] DoctorDentist doctorDentist)
        {
            if (ModelState.IsValid)
            {
                db.DoctorDentists.Add(doctorDentist);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(doctorDentist);
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
            return View(doctorDentist);
        }

        // POST: DoctorDentists/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,DoctorDentistID,Name")] DoctorDentist doctorDentist)
        {
            if (ModelState.IsValid)
            {
                db.Entry(doctorDentist).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(doctorDentist);
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
