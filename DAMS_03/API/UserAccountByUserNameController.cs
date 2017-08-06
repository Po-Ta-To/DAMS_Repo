using DAMS_03.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;

namespace DAMS_03.API
{
    public class UserAccountByUserNameController : ApiController
    {

        private DAMS_01Entities db = new DAMS_01Entities();

        // GET: api/UserAccountByUserName/5
        [ResponseType(typeof(UserAccount))]
        public IHttpActionResult GetUserAccountByUserName(int username)
        {
            //UserAccount userAccount = db.UserAccounts.Find(id);
            //if (userAccount == null)
            //{
            //    return NotFound();
            //}
            return Ok();
            //AspNetUser user = (from anu in db.AspNetUsers
            //                   where anu.UserName == username
            //                   select anu).SingleOrDefault();

            //if (user == null)
            //{
            //    return NotFound();
            //}

            //var returnUser = from ua in db.UserAccounts
            //                 join anu in db.AspNetUsers on ua.AspNetID equals anu.Id
            //                 where anu.UserName == username
            //                 select new
            //                 {
            //                     ua.ID,
            //                     anu.UserName,
            //                     ua.Name,
            //                     ua.NRIC,
            //                     ua.Gender,
            //                     ua.Mobile,
            //                     ua.DOB,
            //                     Address = ua.Addrress
            //                 };

            //return Ok(returnUser);
        }

    }
}
