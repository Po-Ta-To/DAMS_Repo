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
    [Authorize(Roles = "SysAdmin, HospAdmin, ClerkAdmin")]
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

        // GET: AdminAccounts
        public ActionResult IndexBy(int? id)
        {

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var getAdminAccountsBySecId = from AdminAccounts in db.AdminAccounts
                                          where AdminAccounts.SecurityLevel == id.ToString()
                                          select AdminAccounts;

            return View(getAdminAccountsBySecId.ToList());
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

                    var currentUser = UserManager.FindByName(user.UserName);

                    //var roleresult = UserManager.AddToRole(currentUser.Id, "Admin");

                    switch (model.SecurityLevel)
                    {
                        case 1:
                            UserManager.AddToRole(currentUser.Id, "SysAdmin");
                            break;
                        case 2:
                            UserManager.AddToRole(currentUser.Id, "HospAdmin");
                            break;
                        case 3:
                            UserManager.AddToRole(currentUser.Id, "ClerkAdmin");
                            break;
                        default:
                            UserManager.AddToRole(currentUser.Id, "User");
                            break;
                    }

                    
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

                    //addAdminAccount.ID = model.ID;
                    addAdminAccount.AdminID = model.AdminID;
                    addAdminAccount.Name = model.Name;
                    addAdminAccount.Email = model.Email;
                    addAdminAccount.SecurityLevel = model.SecurityLevel.ToString();
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

            string UserName = (from AspNetUsers in db.AspNetUsers
                               where AspNetUsers.Id == adminAccount.AspNetID
                               select AspNetUsers.UserName).First().ToString();


            AdminAccountEditModel adminAccountEdit = new AdminAccountEditModel();

            adminAccountEdit.ID = adminAccount.ID;
            adminAccountEdit.AdminID = adminAccount.AdminID;
            adminAccountEdit.Name = adminAccount.Name;
            adminAccountEdit.Email = adminAccount.Email;
            adminAccountEdit.SecurityLevel = Int32.Parse(adminAccount.SecurityLevel);

            adminAccountEdit.UserName = UserName;

            return View(adminAccountEdit);
        }

        // POST: AdminAccounts/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(AdminAccountEditModel model)
        {
            if (ModelState.IsValid)
            {

                string aspID = (from AspNetUsers in db.AspNetUsers
                                where AspNetUsers.UserName == model.UserName
                                select AspNetUsers.Id).First().ToString();

                AdminAccount editAdminAccount = (from AdminAccount in db.AdminAccounts
                                                 where AdminAccount.AspNetID == aspID
                                                 select AdminAccount).SingleOrDefault();

                //AdminAccount editAdminAccount = new AdminAccount();

                //editAdminAccount.ID = model.ID;
                editAdminAccount.AdminID = model.AdminID;
                editAdminAccount.Name = model.Name;
                editAdminAccount.Email = model.Email;
                editAdminAccount.SecurityLevel = model.SecurityLevel.ToString();
                //editAdminAccount.AspNetID = aspID;


                db.Entry(editAdminAccount).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(model);
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