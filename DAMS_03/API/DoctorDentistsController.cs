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
    public class DoctorDentistsController : ApiController
    {
        private DAMS_01Entities db = new DAMS_01Entities();

        // GET: api/DoctorDentists
        public IQueryable<DoctorDentist> GetDoctorDentists()
        {
            return db.DoctorDentists;
        }

        // GET: api/DoctorDentists/5
        [ResponseType(typeof(DoctorDentist))]
        public IHttpActionResult GetDoctorDentist(int id)
        {
            DoctorDentist doctorDentist = db.DoctorDentists.Find(id);
            if (doctorDentist == null)
            {
                return NotFound();
            }

            return Ok(doctorDentist);
        }

        // PUT: api/DoctorDentists/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutDoctorDentist(int id, DoctorDentist doctorDentist)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != doctorDentist.ID)
            {
                return BadRequest();
            }

            db.Entry(doctorDentist).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DoctorDentistExists(id))
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

        // POST: api/DoctorDentists
        [ResponseType(typeof(DoctorDentist))]
        public IHttpActionResult PostDoctorDentist(DoctorDentist doctorDentist)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.DoctorDentists.Add(doctorDentist);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = doctorDentist.ID }, doctorDentist);
        }

        // DELETE: api/DoctorDentists/5
        [ResponseType(typeof(DoctorDentist))]
        public IHttpActionResult DeleteDoctorDentist(int id)
        {
            DoctorDentist doctorDentist = db.DoctorDentists.Find(id);
            if (doctorDentist == null)
            {
                return NotFound();
            }

            db.DoctorDentists.Remove(doctorDentist);
            db.SaveChanges();

            return Ok(doctorDentist);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool DoctorDentistExists(int id)
        {
            return db.DoctorDentists.Count(e => e.ID == id) > 0;
        }
    }
}