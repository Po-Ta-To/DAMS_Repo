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
using System.Security.Claims;

namespace DAMS_03.API
{
    public class UserAccountByUNController : ApiController
    {
        private DAMS_01Entities db = new DAMS_01Entities();

        //GET: api/UserAccountByUN/5
        [Authorize]
        public IHttpActionResult GetUserAccount()
        {

            ClaimsPrincipal principal = Request.GetRequestContext().Principal as ClaimsPrincipal;
            string username = ClaimsPrincipal.Current.Identity.Name;
            //string alsoName = User.Identity.Name;

            AspNetUser user = (from anu in db.AspNetUsers
                               where anu.UserName == username
                               select anu).SingleOrDefault();

            if (user == null)
            {
                return NotFound();
            }

            var returnUser = from ua in db.UserAccounts
                             join anu in db.AspNetUsers on ua.AspNetID equals anu.Id
                             where anu.UserName == username
                             select new
                             {
                                 ua.ID,
                                 anu.UserName,
                                 anu.Email,
                                 ua.Name,
                                 ua.NRIC,
                                 ua.Gender,
                                 ua.Mobile,
                                 ua.DOB,
                                 Address = ua.Addrress
                             };

            return Ok(returnUser);
        }
        
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
        
    }
}