using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DAMS_03.Authorization;

namespace DAMS_03.Controllers
{
    [AuthorizeAdmin(Roles = "SysAdmin, HospAdmin, ClerkAdmin")]
    public class ListingControlsController : Controller
    {
        // GET: ListingControls
        public ActionResult Index()
        {
            return View();
        }
    }
}