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
using System.Web;

namespace DAMS_03.API
{
    public class OpeningHoursController : ApiController
    {
        private DAMS_01Entities db = new DAMS_01Entities();

        // GET: api/OpeningHours/1
        [ResponseType(typeof(OpeningHour))]
        public IHttpActionResult getOpeningHoursByHCid(int id)
        {
            var openingHours = (from OpeningHour in db.OpeningHours
                                      where OpeningHour.ClinicHospitalID == id
                                      select new
                                      {
                                          OpeningHoursDay = OpeningHour.OpeningHoursDay,
                                          TimeRangeStart = OpeningHour.TimeRangeStart,
                                          TimeRangeEnd = OpeningHour.TimeRangeEnd
                                      }).ToList();

            if (openingHours == null)
            {
                return NotFound();
            }

            return Ok(openingHours);
        }
    }
}
