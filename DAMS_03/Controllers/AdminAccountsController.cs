using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using DAMS_03.Models;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;

namespace DAMS_03.Controllers
{
    [Authorize]
    public class AdminAccountsController : Controller
    {
        private DAMS_01Entities db = new DAMS_01Entities();

        private ApplicationSignInManager _signInManager;
        private ApplicationUserManager _userManager;

        public AdminAccountsController()
        {
            
        }

        public AdminAccountsController(ApplicationUserManager userManager, ApplicationSignInManager signInManager)
        {
            UserManager = userManager;
            SignInManager = signInManager;
        }

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


        //[HttpGet]
        // GET: AdminAccounts/Create
        public ActionResult Create()
        {
            //var tuple = new Tuple<AdminAccount, RegisterViewModel>(new AdminAccount(), new RegisterViewModel());
            return View();
        }

        // POST: AdminAccounts/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(AdminAccountCreateModel model)
        {

            if (ModelState.IsValid)
            {
                
                var user = new ApplicationUser { UserName = model.UserName, Email = model.Email };
                var result = await UserManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    await SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);

                    // For more information on how to enable account confirmation and password reset please visit http://go.microsoft.com/fwlink/?LinkID=320771
                    // Send an email with this link
                    // string code = await UserManager.GenerateEmailConfirmationTokenAsync(user.Id);
                    // var callbackUrl = Url.Action("ConfirmEmail", "Account", new { userId = user.Id, code = code }, protocol: Request.Url.Scheme);
                    // await UserManager.SendEmailAsync(user.Id, "Confirm your account", "Please confirm your account by clicking <a href=\"" + callbackUrl + "\">here</a>");



                    string aspID = (from AspNetUsers in db.AspNetUsers
                                    where AspNetUsers.UserName == model.UserName
                                    select AspNetUsers.Id).First().ToString();

                    AdminAccount addAdminAccount = new AdminAccount();

                    addAdminAccount.ID = model.ID;
                    addAdminAccount.AdminID = model.AdminID;
                    addAdminAccount.Name = model.Name;
                    addAdminAccount.Email = model.Email;
                    addAdminAccount.SecurityLevel = model.SecurityLevel;
                    addAdminAccount.AspNetID = aspID;

                    db.AdminAccounts.Add(addAdminAccount);
                    db.SaveChanges();
                    //return RedirectToAction("Index");


                    return RedirectToAction("Index", "Home");
                }
                AddErrors(result);

                
            }

            return View(model);
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



        #region 
        
        public ApplicationSignInManager SignInManager
        {
            get
            {
                return _signInManager ?? HttpContext.GetOwinContext().Get<ApplicationSignInManager>();
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
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
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

        #endregion

    }

}