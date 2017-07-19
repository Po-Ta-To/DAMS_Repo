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
            var appointments = db.Appointments.Include(a => a.ClinicHospital).Include(a => a.DoctorDentist).Include(a => a.DoctorDentist1).Include(a => a.UserAccount);
            return View(appointments.ToList());
        }

        // GET: Appointment Requests
        public ActionResult IndexBy()
        {
            // If ApprovalState was 2 for "Appointment Requests"
            var getAppointmentsBySecId = from Appointments in db.Appointments
                                          where Appointments.ApprovalState == 2
                                          select Appointments;

            return View(getAppointmentsBySecId.ToList());
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
            return View(appointment);
        }

        // GET: Appointments/Create
        public ActionResult Create()
        {
            ViewBag.ClinicHospitalID = new SelectList(db.ClinicHospitals, "ID", "ClinicHospitalID");
            ViewBag.DoctorDentistID = new SelectList(db.DoctorDentists, "ID", "DoctorDentistID");
            ViewBag.RequestDoctorDentistID = new SelectList(db.DoctorDentists, "ID", "DoctorDentistID");
            ViewBag.UserID = new SelectList(db.UserAccounts, "ID", "ID");
            return View();
        }

        // POST: Appointments/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,AppointmentID,UserID,ClinicHospitalID,ApprovalState,PreferredDate,PreferredTime,DoctorDentistID,RequestDoctorDentistID,Remarks,AppointmentDate,AppointmentTime")] Appointment appointment)
        {
            if (ModelState.IsValid)
            {
                db.Appointments.Add(appointment);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ClinicHospitalID = new SelectList(db.ClinicHospitals, "ID", "ClinicHospitalID", appointment.ClinicHospitalID);
            ViewBag.DoctorDentistID = new SelectList(db.DoctorDentists, "ID", "DoctorDentistID", appointment.DoctorDentistID);
            ViewBag.RequestDoctorDentistID = new SelectList(db.DoctorDentists, "ID", "DoctorDentistID", appointment.RequestDoctorDentistID);
            ViewBag.UserID = new SelectList(db.UserAccounts, "ID", "ID", appointment.UserID);
            return View(appointment);
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
            ViewBag.ClinicHospitalID = new SelectList(db.ClinicHospitals, "ID", "ClinicHospitalID", appointment.ClinicHospitalID);
            ViewBag.DoctorDentistID = new SelectList(db.DoctorDentists, "ID", "DoctorDentistID", appointment.DoctorDentistID);
            ViewBag.RequestDoctorDentistID = new SelectList(db.DoctorDentists, "ID", "DoctorDentistID", appointment.RequestDoctorDentistID);
            ViewBag.UserID = new SelectList(db.UserAccounts, "ID", "ID", appointment.UserID);
            return View(appointment);
        }

        // POST: Appointments/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,AppointmentID,UserID,ClinicHospitalID,ApprovalState,PreferredDate,PreferredTime,DoctorDentistID,RequestDoctorDentistID,Remarks,AppointmentDate,AppointmentTime")] Appointment appointment)
        {
            if (ModelState.IsValid)
            {
                db.Entry(appointment).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ClinicHospitalID = new SelectList(db.ClinicHospitals, "ID", "ClinicHospitalID", appointment.ClinicHospitalID);
            ViewBag.DoctorDentistID = new SelectList(db.DoctorDentists, "ID", "DoctorDentistID", appointment.DoctorDentistID);
            ViewBag.RequestDoctorDentistID = new SelectList(db.DoctorDentists, "ID", "DoctorDentistID", appointment.RequestDoctorDentistID);
            ViewBag.UserID = new SelectList(db.UserAccounts, "ID", "ID", appointment.UserID);
            return View(appointment);
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
