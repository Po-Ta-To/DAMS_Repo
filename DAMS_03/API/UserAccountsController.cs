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
using System.Web.Mvc;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Microsoft.AspNet.Identity;

namespace DAMS_03.API
{
    public class UserAccountsController : ApiController
    {
        private DAMS_01Entities db = new DAMS_01Entities();

        // GET: api/UserAccounts
        public IQueryable<UserAccount> GetUserAccounts()
        {
            return db.UserAccounts;
        }

        // GET: api/UserAccounts/5
        [ResponseType(typeof(UserAccount))]
        public IHttpActionResult GetUserAccount(int id)
        {
            UserAccount userAccount = db.UserAccounts.Find(id);
            if (userAccount == null)
            {
                return NotFound();
            }

            return Ok(userAccount);
        }

        // PUT: api/UserAccounts/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutUserAccount(int id, UserAccount userAccount)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != userAccount.ID)
            {
                return BadRequest();
            }

            db.Entry(userAccount).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserAccountExists(id))
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

        //// POST: api/UserAccounts
        //[ResponseType(typeof(UserAccount))]
        //public IHttpActionResult PostUserAccount(UserAccount userAccount)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }

        //    db.UserAccounts.Add(userAccount);
        //    db.SaveChanges();

        //    return CreatedAtRoute("DefaultApi", new { id = userAccount.ID }, userAccount);
        //}

        // DELETE: api/UserAccounts/5
        [ResponseType(typeof(UserAccount))]
        public IHttpActionResult DeleteUserAccount(int id)
        {
            UserAccount userAccount = db.UserAccounts.Find(id);
            if (userAccount == null)
            {
                return NotFound();
            }

            db.UserAccounts.Remove(userAccount);
            db.SaveChanges();

            return Ok(userAccount);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool UserAccountExists(int id)
        {
            return db.UserAccounts.Count(e => e.ID == id) > 0;
        }


        // POST: api/UserAccounts
        [ResponseType(typeof(UserAccount))]
        public IHttpActionResult PostingUserAccount(UserAccount model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }



            //var user = new ApplicationUser { UserName = userAccount.UserName, Email = userAccount.Email };



            string aspID = (from AspNetUsers in db.AspNetUsers
                            where AspNetUsers.UserName == model.AspNetUser.UserName
                            select AspNetUsers.Id).First().ToString();

            //string aspID = (from AspNetUsers in db.AspNetUsers
            //                where AspNetUsers.Id == userAccount.AspNetID
            //                select AspNetUsers.Id).First().ToString();

            //string aspID = (from AspNetUsers in db.AspNetUsers
            //                where userAccount.AspNetID == AspNetUsers.Id
            //                select AspNetUsers.Id).First().ToString();

            //string aspID = (from UserAccounts in db.UserAccounts
            //                where userAccount.AspNetID == userAccount.AspNetUser.Id
            //                select userAccount.AspNetUser.Id).First().ToString();

            //string aspID = (from AspNetUsers in db.AspNetUsers
            //                where model.AspNetID == AspNetUsers.Id
            //                select model.AspNetID).First().ToString();

            //string aspID = (from AspNetUsers in db.AspNetUsers
            //                where AspNetUsers.UserName == model.UserName
            //                select AspNetUsers.Id).First().ToString();

            UserAccount addUserAccount = new UserAccount();

                addUserAccount.ID = 1;
                addUserAccount.NRIC = model.NRIC;
                addUserAccount.Name = model.Name;
                addUserAccount.DOB = model.DOB;
                addUserAccount.Gender = model.Gender;
                addUserAccount.Mobile = model.Mobile.ToString();
                addUserAccount.Addrress = model.Addrress;
                addUserAccount.AspNetID = aspID;


                db.UserAccounts.Add(addUserAccount);
                db.SaveChanges();
                return CreatedAtRoute("PostApi", new { id = addUserAccount.AspNetUser.Id }, model);


            }

        
        


        
    }
}