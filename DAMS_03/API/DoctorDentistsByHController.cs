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
    public class DoctorDentistsByHController : ApiController
    {
        private DAMS_01Entities db = new DAMS_01Entities();

        //GET: api/DoctorDentistsByH/1
        [ResponseType(typeof(DoctorDentist))]
        public IHttpActionResult GetDocDentistsByCHid (int id)
        {
            var docDenList = (from DoctorDentist in db.DoctorDentists
                              where DoctorDentist.ClinicHospitalID == id
                              select new
                              {
                                  Name = DoctorDentist.Name
                              }).ToList();

            if(docDenList == null)
            {
                return NotFound();
            }

            return Ok(docDenList);
        }
    }
}
