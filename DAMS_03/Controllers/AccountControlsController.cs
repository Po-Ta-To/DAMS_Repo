using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DAMS_03.Controllers
{
    [Authorize(Roles = "SysAdmin, HospAdmin, ClerkAdmin")]
    public class AccountControlsController : Controller
    {
        // GET: AccountControls
        public ActionResult Index()
        {
            return View();
        }
    }
}