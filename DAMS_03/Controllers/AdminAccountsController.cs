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
using System.Web.Security;

namespace DAMS_03.Controllers
{
    [AuthorizeAdmin(Roles = "SysAdmin, HospAdmin, ClerkAdmin")]
    //[Authorize]
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

            string userAspId = User.Identity.GetUserId();

            int hospid = (from ch in db.ClinicHospitals
                          join ach in db.AdminAccountClinicHospitals on ch.ID equals ach.ClinicHospitalID
                          join aa in db.AdminAccounts on ach.AdminID equals aa.ID
                          join aspu in db.AspNetUsers on aa.AspNetID equals aspu.Id
                          where aspu.Id == userAspId
                          select ch.ID).SingleOrDefault();

            if (hospid == 0)
            {
                return View(db.AdminAccounts.ToList());
            }
            else
            {
                var getAdminAccountsByHospId = from aa in db.AdminAccounts
                                               join ach in db.AdminAccountClinicHospitals on aa.ID equals ach.AdminID
                                               join ch in db.ClinicHospitals on ach.ClinicHospitalID equals ch.ID
                                               where ch.ID == hospid
                                               select aa;

                return View(getAdminAccountsByHospId.ToList());
            }

        }

        // GET: AdminAccounts By Security level
        public ActionResult IndexBy(int? id)
        {

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            string userAspId = User.Identity.GetUserId();

            int hospid = (from ch in db.ClinicHospitals
                          join ach in db.AdminAccountClinicHospitals on ch.ID equals ach.ClinicHospitalID
                          join aa in db.AdminAccounts on ach.AdminID equals aa.ID
                          join aspu in db.AspNetUsers on aa.AspNetID equals aspu.Id
                          where aspu.Id == userAspId
                          select ch.ID).SingleOrDefault();

            if (hospid == 0)
            {
                var getAdminAccountsBySecId = from aa in db.AdminAccounts
                                              where aa.SecurityLevel == id.ToString()
                                              select aa;
                return View(getAdminAccountsBySecId.ToList());
            }
            else
            {
                var getAdminAccountsBySecId = from aa in db.AdminAccounts
                                              join ach in db.AdminAccountClinicHospitals on aa.ID equals ach.AdminID
                                              join ch in db.ClinicHospitals on ach.ClinicHospitalID equals ch.ID
                                              where aa.SecurityLevel == id.ToString() && ch.ID == hospid
                                              select aa;
                return View(getAdminAccountsBySecId.ToList());
            }

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


            //check for auth
            string userAspId = User.Identity.GetUserId();

            int hospid = (from ch in db.ClinicHospitals
                          join ach in db.AdminAccountClinicHospitals on ch.ID equals ach.ClinicHospitalID
                          join aa in db.AdminAccounts on ach.AdminID equals aa.ID
                          join aspu in db.AspNetUsers on aa.AspNetID equals aspu.Id
                          where aspu.Id == userAspId
                          select ch.ID).SingleOrDefault();

            if (hospid != 0)
            {
                int matchHospid = (from ch in db.ClinicHospitals
                                   join ach in db.AdminAccountClinicHospitals on ch.ID equals ach.ClinicHospitalID
                                   join aa in db.AdminAccounts on ach.AdminID equals aa.ID
                                   where aa.ID == id
                                   select ch.ID).SingleOrDefault();
                if (hospid != matchHospid)
                {
                    return RedirectToAction("Unauthorized", "Account");
                }

            }
            //end check for auth


            string UserName = (from AspNetUsers in db.AspNetUsers
                               where AspNetUsers.Id == adminAccount.AspNetID
                               select AspNetUsers.UserName).First().ToString();


            AdminAccountDetailModel adminAccountEdit = new AdminAccountDetailModel();

            adminAccountEdit.ID = adminAccount.ID;
            adminAccountEdit.AdminID = adminAccount.AdminID;
            adminAccountEdit.Name = adminAccount.Name;
            adminAccountEdit.Email = adminAccount.Email;
            //adminAccountEdit.SecurityLevel = adminAccount.SecurityLevel;

            switch (adminAccount.SecurityLevel)
            {
                case "1":
                    adminAccountEdit.SecurityLevel = "System Admin";
                    break;
                case "2":
                    adminAccountEdit.SecurityLevel = "Hospital/Clinic Admin";
                    break;
                case "3":
                    adminAccountEdit.SecurityLevel = "Hospital/Clinic Clerk";
                    break;
                default:
                    adminAccountEdit.SecurityLevel = "Error : not defined";
                    break;
            }

            adminAccountEdit.UserName = UserName;

            try
            {
                string clinhospname = (from ch in db.ClinicHospitals
                                       join ach in db.AdminAccountClinicHospitals on ch.ID equals ach.ClinicHospitalID
                                       join aa in db.AdminAccounts on ach.AdminID equals aa.ID
                                       where aa.ID == id
                                       select ch.ClinicHospitalName).First().ToString();

                adminAccountEdit.HospClin = clinhospname;
            }
            catch (Exception)
            {
                adminAccountEdit.HospClin = " - ";
            }

            return View(adminAccountEdit);
        }


        //[HttpGet]
        // GET: AdminAccounts/Create
        [AuthorizeAdmin(Roles = "SysAdmin, HospAdmin")]
        public ActionResult Create()
        {
            //var tuple = new Tuple<AdminAccount, RegisterViewModel>(new AdminAccount(), new RegisterViewModel());

            //check for auth
            string userAspId = User.Identity.GetUserId();

            int hospid = (from ch in db.ClinicHospitals
                          join ach in db.AdminAccountClinicHospitals on ch.ID equals ach.ClinicHospitalID
                          join aa in db.AdminAccounts on ach.AdminID equals aa.ID
                          join aspu in db.AspNetUsers on aa.AspNetID equals aspu.Id
                          where aspu.Id == userAspId
                          select ch.ID).SingleOrDefault();
            //end check for auth

            AdminAccountCreateModel returnmodel = new AdminAccountCreateModel();

            returnmodel.itemSelection = new List<SelectListItem>();

            if (hospid != 0)
            {
                var listOfHosp = from ClinHosp in db.ClinicHospitals
                                 where ClinHosp.ID == hospid
                                 select new SelectListItem()
                                 {
                                     Value = ClinHosp.ID.ToString(),
                                     Text = ClinHosp.ClinicHospitalName
                                 };
                returnmodel.itemSelection.Add(new SelectListItem()
                {
                    Value = "notselected",
                    Text = " - "
                });
                returnmodel.itemSelection.AddRange(listOfHosp.ToList<SelectListItem>());
            }
            else
            {
                var listOfHosp = from ClinHosp in db.ClinicHospitals
                                 select new SelectListItem()
                                 {
                                     Value = ClinHosp.ID.ToString(),
                                     Text = ClinHosp.ClinicHospitalName
                                 };
                returnmodel.itemSelection.Add(new SelectListItem()
                {
                    Value = "notselected",
                    Text = " - "
                });
                returnmodel.itemSelection.AddRange(listOfHosp.ToList<SelectListItem>());
            }

            return View(returnmodel);
        }

        // POST: AdminAccounts/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [AuthorizeAdmin(Roles = "SysAdmin, HospAdmin")]
        public async Task<ActionResult> Create(AdminAccountCreateModel model)
        {

            if (ModelState.IsValid)
            {

                //Only system admin can create system admin accounts
                if (model.SecurityLevel.Equals("1"))
                {
                    if (!User.IsInRole("SysAdmin"))
                    {
                        return RedirectToAction("Unauthorized", "Account");
                    }
                }
                //check for auth
                string userAspId = User.Identity.GetUserId();

                int hospid = (from ch in db.ClinicHospitals
                              join ach in db.AdminAccountClinicHospitals on ch.ID equals ach.ClinicHospitalID
                              join aa in db.AdminAccounts on ach.AdminID equals aa.ID
                              join aspu in db.AspNetUsers on aa.AspNetID equals aspu.Id
                              where aspu.Id == userAspId
                              select ch.ID).SingleOrDefault();
                if (hospid != 0)
                {
                    if(Int32.Parse(model.HospClinID) != hospid)
                    {
                        return RedirectToAction("Unauthorized", "Account");
                    }
                }
                //end check for auth

                var user = new ApplicationUser { UserName = model.UserName, Email = model.Email };
                var result = await UserManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {

                    var currentUser = UserManager.FindByName(user.UserName);

                    //var roleresult = UserManager.AddToRole(currentUser.Id, "Admin");

                    //var loggedinUser = UserManager.FindById(User.Identity.GetUserId());

                    //string[] roleNames = Roles.GetRolesForUser();

                    //var role = GetRolesForUser().Single();

                    //if (User.IsInRole("Admin")) ;

                    switch (model.SecurityLevel)
                    {
                        case "1":
                            UserManager.AddToRole(currentUser.Id, "SysAdmin");
                            break;
                        case "2":
                            UserManager.AddToRole(currentUser.Id, "HospAdmin");
                            break;
                        case "3":
                            UserManager.AddToRole(currentUser.Id, "ClerkAdmin");
                            break;
                        default:
                            UserManager.AddToRole(currentUser.Id, "User");//
                            break;
                    }


                    //await SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);

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

                    if (!model.HospClinID.Equals("notselected"))
                    {
                        AdminAccountClinicHospital newrelation = new AdminAccountClinicHospital();

                        int currentid = (int)(from adminAccounts in db.AdminAccounts
                                              where adminAccounts.AspNetID == aspID
                                              select adminAccounts.ID).First();

                        newrelation.ClinicHospitalID = Int32.Parse(model.HospClinID);
                        newrelation.AdminID = currentid;
                        db.AdminAccountClinicHospitals.Add(newrelation);
                        db.SaveChanges();
                    }

                    //return RedirectToAction("Index");

                    return RedirectToAction("Index", "AdminAccounts");
                }
                AddErrors(result);


            }


            model.itemSelection = new List<SelectListItem>();

            var listOfHosp = from ClinHosp in db.ClinicHospitals
                             select new SelectListItem()
                             {
                                 Value = ClinHosp.ID.ToString(),
                                 Text = ClinHosp.ClinicHospitalName
                             };

            model.itemSelection.Add(new SelectListItem()
            {
                Value = "notselected",
                Text = " - "
            });

            model.itemSelection.AddRange(listOfHosp.ToList<SelectListItem>());



            return View(model);
        }

        // GET: AdminAccounts/Edit/5
        [AuthorizeAdmin(Roles = "SysAdmin, HospAdmin")]
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

            //check for auth
            string userAspId = User.Identity.GetUserId();

            int hospid = (from ch in db.ClinicHospitals
                          join ach in db.AdminAccountClinicHospitals on ch.ID equals ach.ClinicHospitalID
                          join aa in db.AdminAccounts on ach.AdminID equals aa.ID
                          join aspu in db.AspNetUsers on aa.AspNetID equals aspu.Id
                          where aspu.Id == userAspId
                          select ch.ID).SingleOrDefault();

            if (hospid != 0)
            {
                int matchHospid = (from ch in db.ClinicHospitals
                                   join ach in db.AdminAccountClinicHospitals on ch.ID equals ach.ClinicHospitalID
                                   join aa in db.AdminAccounts on ach.AdminID equals aa.ID
                                   where aa.ID == id
                                   select ch.ID).SingleOrDefault();
                if (hospid != matchHospid)
                {
                    return RedirectToAction("Unauthorized", "Account");
                }

            }
            //end check for auth

            string UserName = (from AspNetUsers in db.AspNetUsers
                               where AspNetUsers.Id == adminAccount.AspNetID
                               select AspNetUsers.UserName).First().ToString();


            AdminAccountEditModel adminAccountEdit = new AdminAccountEditModel();

            adminAccountEdit.ID = adminAccount.ID;
            adminAccountEdit.AdminID = adminAccount.AdminID;
            adminAccountEdit.Name = adminAccount.Name;
            adminAccountEdit.Email = adminAccount.Email;
            //adminAccountEdit.SecurityLevel = adminAccount.SecurityLevel;

            switch (adminAccount.SecurityLevel)
            {
                case "1":
                    adminAccountEdit.SecurityLevelID = "1";
                    adminAccountEdit.SecurityLevel = "System Admin";
                    break;
                case "2":
                    adminAccountEdit.SecurityLevelID = "2";
                    adminAccountEdit.SecurityLevel = "Hospital/Clinic Admin";
                    break;
                case "3":
                    adminAccountEdit.SecurityLevelID = "3";
                    adminAccountEdit.SecurityLevel = "Hospital/Clinic Clerk";
                    break;
                default:
                    adminAccountEdit.SecurityLevel = "Error : not defined";
                    break;
            }

            adminAccountEdit.UserName = UserName;

            try
            {
                string clinhospname = (from ch in db.ClinicHospitals
                                       join ach in db.AdminAccountClinicHospitals on ch.ID equals ach.ClinicHospitalID
                                       join aa in db.AdminAccounts on ach.AdminID equals aa.ID
                                       where aa.ID == id
                                       select ch.ClinicHospitalName).First().ToString();

                adminAccountEdit.HospClin = clinhospname;
            }
            catch (Exception)
            {
                adminAccountEdit.HospClin = " - ";
            }

            return View(adminAccountEdit);
        }

        // POST: AdminAccounts/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [AuthorizeAdmin(Roles = "SysAdmin, HospAdmin")]
        public ActionResult Edit(AdminAccountEditModel model)
        {
            if (ModelState.IsValid)
            {
                //Only system admin can create system admin accounts
                if (model.SecurityLevel.Equals("1"))
                {
                    if (!User.IsInRole("SysAdmin"))
                    {
                        return RedirectToAction("Unauthorized", "Account");
                    }
                }

                //check for auth
                string userAspId = User.Identity.GetUserId();

                int hospid = (from ch in db.ClinicHospitals
                              join ach in db.AdminAccountClinicHospitals on ch.ID equals ach.ClinicHospitalID
                              join aa in db.AdminAccounts on ach.AdminID equals aa.ID
                              join aspu in db.AspNetUsers on aa.AspNetID equals aspu.Id
                              where aspu.Id == userAspId
                              select ch.ID).SingleOrDefault();

                if (hospid != 0)
                {
                    int matchHospid = (from ch in db.ClinicHospitals
                                       join ach in db.AdminAccountClinicHospitals on ch.ID equals ach.ClinicHospitalID
                                       join aa in db.AdminAccounts on ach.AdminID equals aa.ID
                                       where aa.ID == model.ID
                                       select ch.ID).SingleOrDefault();
                    if (hospid != matchHospid)
                    {
                        return RedirectToAction("Unauthorized", "Account");
                    }

                }
                //end check for auth

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
                //db.SaveChanges();

                AspNetUser editAspNetUser = (from anu in db.AspNetUsers
                                             where anu.Id == editAdminAccount.AspNetID
                                             select anu).SingleOrDefault();

                editAspNetUser.Email = model.Email;
                db.Entry(editAspNetUser).State = EntityState.Modified;
                db.SaveChanges();

                return RedirectToAction("Index", "AdminAccounts");
            }
            return View(model);
        }

        // GET: AdminAccounts/Delete/5
        [AuthorizeAdmin(Roles = "SysAdmin, HospAdmin")]
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

            //check for auth
            string userAspId = User.Identity.GetUserId();

            int hospid = (from ch in db.ClinicHospitals
                          join ach in db.AdminAccountClinicHospitals on ch.ID equals ach.ClinicHospitalID
                          join aa in db.AdminAccounts on ach.AdminID equals aa.ID
                          join aspu in db.AspNetUsers on aa.AspNetID equals aspu.Id
                          where aspu.Id == userAspId
                          select ch.ID).SingleOrDefault();

            if (hospid != 0)
            {
                int matchHospid = (from ch in db.ClinicHospitals
                                   join ach in db.AdminAccountClinicHospitals on ch.ID equals ach.ClinicHospitalID
                                   join aa in db.AdminAccounts on ach.AdminID equals aa.ID
                                   where aa.ID == id
                                   select ch.ID).SingleOrDefault();
                if (hospid != matchHospid)
                {
                    return RedirectToAction("Unauthorized", "Account");
                }

            }
            //end check for auth

            string UserName = (from AspNetUsers in db.AspNetUsers
                               where AspNetUsers.Id == adminAccount.AspNetID
                               select AspNetUsers.UserName).First().ToString();


            AdminAccountDetailModel adminAccountEdit = new AdminAccountDetailModel();

            adminAccountEdit.ID = adminAccount.ID;
            adminAccountEdit.AdminID = adminAccount.AdminID;
            adminAccountEdit.Name = adminAccount.Name;
            adminAccountEdit.Email = adminAccount.Email;
            //adminAccountEdit.SecurityLevel = adminAccount.SecurityLevel;

            switch (adminAccount.SecurityLevel)
            {
                case "1":
                    adminAccountEdit.SecurityLevel = "System Admin";
                    break;
                case "2":
                    adminAccountEdit.SecurityLevel = "Hospital/Clinic Admin";
                    break;
                case "3":
                    adminAccountEdit.SecurityLevel = "Hospital/Clinic Clerk";
                    break;
                default:
                    adminAccountEdit.SecurityLevel = "Error : not defined";
                    break;
            }

            adminAccountEdit.UserName = UserName;

            try
            {
                string clinhospname = (from ch in db.ClinicHospitals
                                       join ach in db.AdminAccountClinicHospitals on ch.ID equals ach.ClinicHospitalID
                                       join aa in db.AdminAccounts on ach.AdminID equals aa.ID
                                       where aa.ID == id
                                       select ch.ClinicHospitalName).First().ToString();

                adminAccountEdit.HospClin = clinhospname;
            }
            catch (Exception)
            {
                adminAccountEdit.HospClin = " - ";
            }

            return View(adminAccountEdit);
        }

        // POST: AdminAccounts/Delete/5
        [AuthorizeAdmin(Roles = "SysAdmin, HospAdmin")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {

            //check for auth
            string userAspId = User.Identity.GetUserId();

            int hospid = (from ch in db.ClinicHospitals
                          join ach in db.AdminAccountClinicHospitals on ch.ID equals ach.ClinicHospitalID
                          join aa in db.AdminAccounts on ach.AdminID equals aa.ID
                          join aspu in db.AspNetUsers on aa.AspNetID equals aspu.Id
                          where aspu.Id == userAspId
                          select ch.ID).SingleOrDefault();

            if (hospid != 0)
            {
                int matchHospid = (from ch in db.ClinicHospitals
                                   join ach in db.AdminAccountClinicHospitals on ch.ID equals ach.ClinicHospitalID
                                   join aa in db.AdminAccounts on ach.AdminID equals aa.ID
                                   where aa.ID == id
                                   select ch.ID).SingleOrDefault();
                if (hospid != matchHospid)
                {
                    return RedirectToAction("Unauthorized", "Account");
                }

            }
            //end check for auth

            AdminAccount adminAccount = db.AdminAccounts.Find(id);


            AdminAccountClinicHospital deleteRelationtoCh = (from aach in db.AdminAccountClinicHospitals
                                                             join aa in db.AdminAccounts on aach.AdminID equals aa.ID
                                                             where aa.ID == adminAccount.ID
                                                             select aach).SingleOrDefault();

            AspNetUser deleteRelationToAnu = (from anu in db.AspNetUsers
                                              join aa in db.AdminAccounts on anu.Id equals aa.AspNetID
                                              where aa.ID == adminAccount.ID
                                              select anu).FirstOrDefault(); ;

            //AdminAccountDoctorDentist

            if (deleteRelationtoCh != null)
            {
                db.AdminAccountClinicHospitals.Remove(deleteRelationtoCh);
            }

            if (deleteRelationtoCh != null)
            {
                db.AspNetUsers.Remove(deleteRelationToAnu);
            }

            db.AdminAccounts.Remove(adminAccount);

            db.SaveChanges();
            return RedirectToAction("Index", "AdminAccounts");
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