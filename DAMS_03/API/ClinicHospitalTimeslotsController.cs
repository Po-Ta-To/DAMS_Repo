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

namespace DAMS_03.API
{
    public class ClinicHospitalTimeslotsController : ApiController
    {
        private DAMS_01Entities db = new DAMS_01Entities();
        
        // GET: api/ClinicHospitalTimeslots/5
        [ResponseType(typeof(ClinicHospitalTimeslot))]
        public IHttpActionResult GetClinicHospitalTimeslot(int id)
        {
            ClinicHospital clinicHospital = db.ClinicHospitals.Find(id);
            if (clinicHospital == null)
            {
                return NotFound();
            }

            var returnList = (from cht in db.ClinicHospitalTimeslots
                                                       join ch in db.ClinicHospitals on cht.ClinicHospitalID equals ch.ID
                                                       orderby cht.TimeslotIndex ascending
                                                       where ch.ID == id
                                                       select new 
                                                       {
                                                           TimeslotIndex = cht.TimeslotIndex,
                                                           TimeRangeSlotString = cht.TimeRangeSlotString,
                                                       }).ToList();

            return Ok(returnList);
        }
        
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ClinicHospitalTimeslotExists(int id)
        {
            return db.ClinicHospitalTimeslots.Count(e => e.ID == id) > 0;
        }
    }
}