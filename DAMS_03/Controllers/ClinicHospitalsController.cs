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

namespace DAMS_03.Controllers
{
    public class ClinicHospitalsController : ApiController
    {
        private DAMS_01Entities db = new DAMS_01Entities();

        // GET: api/ClinicHospitals
        public IQueryable<ClinicHospital> GetClinicHospitals()
        {
            return db.ClinicHospitals;
        }

        // GET: api/ClinicHospitals/5
        [ResponseType(typeof(ClinicHospital))]
        public IHttpActionResult GetClinicHospital(int id)
        {
            ClinicHospital clinicHospital = db.ClinicHospitals.Find(id);
            if (clinicHospital == null)
            {
                return NotFound();
            }

            return Ok(clinicHospital);
        }

        // PUT: api/ClinicHospitals/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutClinicHospital(int id, ClinicHospital clinicHospital)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != clinicHospital.ID)
            {
                return BadRequest();
            }

            db.Entry(clinicHospital).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ClinicHospitalExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/ClinicHospitals
        [ResponseType(typeof(ClinicHospital))]
        public IHttpActionResult PostClinicHospital(ClinicHospital clinicHospital)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.ClinicHospitals.Add(clinicHospital);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = clinicHospital.ID }, clinicHospital);
        }

        // DELETE: api/ClinicHospitals/5
        [ResponseType(typeof(ClinicHospital))]
        public IHttpActionResult DeleteClinicHospital(int id)
        {
            ClinicHospital clinicHospital = db.ClinicHospitals.Find(id);
            if (clinicHospital == null)
            {
                return NotFound();
            }

            db.ClinicHospitals.Remove(clinicHospital);
            db.SaveChanges();

            return Ok(clinicHospital);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ClinicHospitalExists(int id)
        {
            return db.ClinicHospitals.Count(e => e.ID == id) > 0;
        }
    }
}