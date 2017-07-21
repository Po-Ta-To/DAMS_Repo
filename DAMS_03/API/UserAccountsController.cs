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
using System.Threading.Tasks;
using System.Web;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.AspNet.Identity;
//using System.Web.Mvc;

namespace DAMS_03.API
{
    public class UserAccountsController : ApiController
    {
        private DAMS_01Entities db = new DAMS_01Entities();

        private ApplicationSignInManager _signInManager;
        private ApplicationUserManager _userManager;

        // GET: api/UserAccounts
        public IHttpActionResult GetUserAccounts()
        {

            var userAccounts = from UserAccount in db.UserAccounts
                               select new
                               {
                                   UserAccount.ID,
                                   UserAccount.Name,
                                   UserAccount.NRIC,
                                   UserAccount.Gender,
                                   UserAccount.Mobile,
                                   UserAccount.DOB,
                                   Address = UserAccount.Addrress
                               };

            return Ok(userAccounts);
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

            var returnUser = from UserAccount in db.UserAccounts
                             where UserAccount.ID == id
                             select new
                             {
                                 UserAccount.ID,
                                 UserAccount.Name,
                                 UserAccount.NRIC,
                                 UserAccount.Gender,
                                 UserAccount.Mobile,
                                 UserAccount.DOB,
                                 Address = UserAccount.Addrress
                             };

            return Ok(returnUser);
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

        // POST: api/UserAccounts
        [ResponseType(typeof(UserAccount))]
        public async Task<IHttpActionResult> PostUserAccount(UserAccountCreateModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }


            var user = new ApplicationUser { UserName = model.UserName, Email = model.Email };
            var result = await UserManager.CreateAsync(user, model.Password);
            if (result.Succeeded)
            {

                //await SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);

                // For more information on how to enable account confirmation and password reset please visit http://go.microsoft.com/fwlink/?LinkID=320771
                // Send an email with this link
                // string code = await UserManager.GenerateEmailConfirmationTokenAsync(user.Id);
                // var callbackUrl = Url.Action("ConfirmEmail", "Account", new { userId = user.Id, code = code }, protocol: Request.Url.Scheme);
                // await UserManager.SendEmailAsync(user.Id, "Confirm your account", "Please confirm your account by clicking <a href=\"" + callbackUrl + "\">here</a>");

                string aspID = (from AspNetUsers in db.AspNetUsers
                                where AspNetUsers.UserName == model.UserName
                                select AspNetUsers.Id).First().ToString();

                UserAccount addUserAccount = new UserAccount();

                //addUserAccount.ID = 1;
                addUserAccount.NRIC = model.NRIC;
                addUserAccount.Name = model.Name;
                addUserAccount.DOB = model.DOB;
                addUserAccount.Gender = model.Gender;
                addUserAccount.Mobile = model.Mobile.ToString();
                addUserAccount.Addrress = model.Addrress;
                addUserAccount.AspNetID = aspID;

                db.UserAccounts.Add(addUserAccount);
                db.SaveChanges();



                return Ok();

            }

            AddErrors(result);
            return BadRequest(ModelState);

        }

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



        public ApplicationSignInManager SignInManager
        {
            get
            {
                return _signInManager ?? HttpContext.Current.GetOwinContext().Get<ApplicationSignInManager>();
            }
            private set
            {
                _signInManager = value;
            }
        }


        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.Current.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }

        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error);
            }
        }

    }


}