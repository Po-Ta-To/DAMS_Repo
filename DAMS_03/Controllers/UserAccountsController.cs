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
using DAMS_03.Authorization;

namespace DAMS_03.Controllers
{
    [AuthorizeAdmin(Roles = "SysAdmin, HospAdmin, ClerkAdmin")]
    public class UserAccountsController : Controller
    {
        private DAMS_01Entities db = new DAMS_01Entities();

        private ApplicationSignInManager _signInManager;
        private ApplicationUserManager _userManager;

        public UserAccountsController()
        {

        }

        public UserAccountsController(ApplicationUserManager userManager, ApplicationSignInManager signInManager)
        {
            UserManager = userManager;
            SignInManager = signInManager;
        }

        // GET: UserAccounts
        public ActionResult Index()
        {
            return View(db.UserAccounts.ToList());
        }

        // GET: UserAccounts/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UserAccount userAccount = db.UserAccounts.Find(id);
            if (userAccount == null)
            {
                return HttpNotFound();
            }

            var AspNetUser = (from AspNetUsers in db.AspNetUsers
                              where AspNetUsers.Id == userAccount.AspNetID
                              select new
                              {
                                  AspNetUsers.UserName,
                                  AspNetUsers.Email
                              }).First();


            UserAccountEditModel userAccountEdit = new UserAccountEditModel();

            userAccountEdit.ID = userAccount.ID;
            userAccountEdit.Name = userAccount.Name;
            userAccountEdit.Email = AspNetUser.Email;
            userAccountEdit.NRIC = userAccount.NRIC;
            userAccountEdit.DOB = userAccount.DOB;
            userAccountEdit.Gender = userAccount.Gender;
            userAccountEdit.Mobile = Int32.Parse(userAccount.Mobile);
            userAccountEdit.Addrress = userAccount.Addrress;

            userAccountEdit.UserName = AspNetUser.UserName;


            return View(userAccountEdit);
        }

        // GET: UserAccounts/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: UserAccounts/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(UserAccountCreateModel model)
        {
            if (ModelState.IsValid)
            {
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
                    var currentUser = UserManager.FindByName(user.UserName);

                    UserManager.AddToRole(currentUser.Id, "User");

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
                    addUserAccount.IsDeleted = false;

                    db.UserAccounts.Add(addUserAccount);
                    db.SaveChanges();
                    return RedirectToAction("Index");

                    //return RedirectToAction("Index", "Home");
                }
                AddErrors(result);
            }

            return View(model);
        }

        // POST : api/AddUserAccountMobile
        [Route("api/AddUserAccountMobile")]
        public string AddUserAccount()
        {
            return "";
        }

        // GET: UserAccounts/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UserAccount userAccount = db.UserAccounts.Find(id);
            if (userAccount == null)
            {
                return HttpNotFound();
            }

            var AspNetUser = (from AspNetUsers in db.AspNetUsers
                              where AspNetUsers.Id == userAccount.AspNetID
                              select new
                              {
                                  AspNetUsers.UserName,
                                  AspNetUsers.Email
                              }).First();


            UserAccountEditModel userAccountEdit = new UserAccountEditModel();

            userAccountEdit.ID = userAccount.ID;
            userAccountEdit.Name = userAccount.Name;
            userAccountEdit.Email = AspNetUser.Email;
            userAccountEdit.NRIC = userAccount.NRIC;
            userAccountEdit.DOB = userAccount.DOB;
            userAccountEdit.Gender = userAccount.Gender;
            userAccountEdit.Mobile = Int32.Parse(userAccount.Mobile);
            userAccountEdit.Addrress = userAccount.Addrress;

            userAccountEdit.UserName = AspNetUser.UserName;


            return View(userAccountEdit);
        }

        // POST: UserAccounts/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(UserAccountEditModel model)//[Bind(Include = "ID")] 
        {
            if (ModelState.IsValid)
            {
                string aspID = (from AspNetUsers in db.AspNetUsers
                                where AspNetUsers.UserName == model.UserName
                                select AspNetUsers.Id).First().ToString();

                UserAccount editUserAccount = (from UserAccount in db.UserAccounts
                                               where UserAccount.AspNetID == aspID
                                               select UserAccount).SingleOrDefault();

                //UserAccount editUserAccount = new UserAccount();

                //editUserAccount.ID = model.ID;
                //editUserAccount.AspNetID = aspID;

                editUserAccount.ID = model.ID;
                editUserAccount.Name = model.Name;
                editUserAccount.NRIC = model.NRIC;
                editUserAccount.NRIC = model.NRIC;
                editUserAccount.DOB = model.DOB;
                editUserAccount.Gender = model.Gender;
                editUserAccount.Mobile = model.Mobile.ToString();
                editUserAccount.Addrress = model.Addrress;


                AspNetUser editAspNetUser = (from AspNetUser in db.AspNetUsers
                                             where AspNetUser.Id == aspID
                                             select AspNetUser).SingleOrDefault();

                editAspNetUser.Email = model.Email;

                db.Entry(editUserAccount).State = EntityState.Modified;
                db.Entry(editAspNetUser).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(model);
        }

        // GET: UserAccounts/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UserAccount userAccount = db.UserAccounts.Find(id);
            if (userAccount == null)
            {
                return HttpNotFound();
            }
            return View(userAccount);
        }

        // POST: UserAccounts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            UserAccount userAccount = db.UserAccounts.Find(id);

            userAccount.IsDeleted = true;
            
            db.Entry(userAccount).State = EntityState.Modified;
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
