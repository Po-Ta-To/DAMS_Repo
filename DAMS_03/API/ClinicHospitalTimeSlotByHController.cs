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
    public class ClinicHospitalTimeSlotByHController : ApiController
    {
        private DAMS_01Entities db = new DAMS_01Entities();

        // GET: api/ClinicHospitalTimeSlotByH/id
        [ResponseType(typeof(ClinicHospitalTimeslot))]
        public IHttpActionResult GetClinicHospitalTimeSlotByCHid(int id)
        {
            var chTimeSlotList = (from ClinicHospitalTimeslot in db.ClinicHospitalTimeslots
                                  where ClinicHospitalTimeslot.ClinicHospitalID == id
                                  select new
                                  {
                                      TimeslotIndex = ClinicHospitalTimeslot.TimeslotIndex,
                                      TimeRangeSlotString = ClinicHospitalTimeslot.TimeRangeSlotString
                                  }).ToList();

            if(chTimeSlotList == null)
            {
                return NotFound();
            }

            return Ok(chTimeSlotList);
        }
    }
}
