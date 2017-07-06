using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using DAMS_03.Models;
using Newtonsoft.Json;
using System.Web.Mvc;
//using Microsoft.AspNet.Mvc;

namespace DAMS_03.API
{
    public class AdminAccountsController : ApiController
    {
        private DAMS_01Entities db = new DAMS_01Entities();

        // GET: api/AdminAccounts
        public IHttpActionResult GetAdminAccounts()//IQueryable<AdminAccount>
        {
            //return JsonConvert.SerializeObject(db.AdminAccounts).Replace("\"", "");

            //var adminAccounts = new JsonResult
            //{
            //    Data = db.AdminAccounts
            //};

            //db.Configuration.ProxyCreationEnabled = false;


            var adminAccounts = from AdminAccount in db.AdminAccounts
                                select new
                                {
                                    AdminID = AdminAccount.AdminID,
                                    Name = AdminAccount.Name,
                                    Email = AdminAccount.Email,
                                    SecurityLevel = AdminAccount.SecurityLevel
                                };

            //db.Configuration.ProxyCreationEnabled = true;

            //return Json(new { adminAccounts });

            //var returnAdminAccount = { "ID" : 1 };

            //foreach (var i in adminAccounts)
            //{
                

            //}

            return Ok(adminAccounts);

        }

        // GET: api/AdminAccounts/5
        [ResponseType(typeof(AdminAccount))]
        public IHttpActionResult GetAdminAccount(int id)
        {
            AdminAccount adminAccount = db.AdminAccounts.Find(id);
            if (adminAccount == null)
            {
                return NotFound();
            }

            return Ok(adminAccount);
        }

        // PUT: api/AdminAccounts/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutAdminAccount(int id, AdminAccount adminAccount)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != adminAccount.ID)
            {
                return BadRequest();
            }

            db.Entry(adminAccount).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AdminAccountExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/AdminAccounts
        [ResponseType(typeof(AdminAccount))]
        public IHttpActionResult PostAdminAccount(AdminAccount adminAccount)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.AdminAccounts.Add(adminAccount);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = adminAccount.ID }, adminAccount);
        }

        // DELETE: api/AdminAccounts/5
        [ResponseType(typeof(AdminAccount))]
        public IHttpActionResult DeleteAdminAccount(int id)
        {
            AdminAccount adminAccount = db.AdminAccounts.Find(id);
            if (adminAccount == null)
            {
                return NotFound();
            }

            db.AdminAccounts.Remove(adminAccount);
            db.SaveChanges();

            return Ok(adminAccount);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool AdminAccountExists(int id)
        {
            return db.AdminAccounts.Count(e => e.ID == id) > 0;
        }
    }
}