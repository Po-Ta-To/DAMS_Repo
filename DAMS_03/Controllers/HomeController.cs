using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DAMS_03.Authorization;

namespace DAMS_03.Controllers
{
    //[Authorize]
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.Title = "Home Page";

            return View();
        }

        [AuthorizeAdmin(Roles = "SysAdmin, HospAdmin, ClerkAdmin")]
        public ActionResult Menu()
        {
            ViewBag.Title = "Home Page";

            return View();
        }

    }
}
