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
    [Authorize]
    public class AdminAccountsController : Controller
    {
        private DAMS_01Entities db = new DAMS_01Entities();

        // GET: AdminAccounts
        public ActionResult Index()
        {
            return View(db.AdminAccounts.ToList());
        }

        // GET: AdminAccounts/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AdminAccount adminAccount = db.AdminAccounts.Find(id);
            if (adminAccount == null)
            {
                return HttpNotFound();
            }
            return View(adminAccount);
        }

        // GET: AdminAccounts/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: AdminAccounts/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,AdminID,Name,Email,SecurityLevel")] AdminAccount adminAccount)
        {
            if (ModelState.IsValid)
            {
                db.AdminAccounts.Add(adminAccount);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(adminAccount);
        }

        // GET: AdminAccounts/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AdminAccount adminAccount = db.AdminAccounts.Find(id);
            if (adminAccount == null)
            {
                return HttpNotFound();
            }
            return View(adminAccount);
        }

        // POST: AdminAccounts/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,AdminID,Name,Email,SecurityLevel")] AdminAccount adminAccount)
        {
            if (ModelState.IsValid)
            {
                db.Entry(adminAccount).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(adminAccount);
        }

        // GET: AdminAccounts/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AdminAccount adminAccount = db.AdminAccounts.Find(id);
            if (adminAccount == null)
            {
                return HttpNotFound();
            }
            return View(adminAccount);
        }

        // POST: AdminAccounts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            AdminAccount adminAccount = db.AdminAccounts.Find(id);
            db.AdminAccounts.Remove(adminAccount);
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
