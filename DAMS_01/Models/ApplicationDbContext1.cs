using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity.EntityFramework;
//using Microsoft.Data.Entity;
//using Microsoft.Data.Entity.Metadata;
using System.Net;
using System.Net.Http;
using System.Web.Http;



namespace DAMS_01.Models
{
    public class ApplicationDbContext1 : IdentityDbContext<ApplicationUser>
    {
    }
}
