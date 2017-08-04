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
using System.Web.Http.Results;

namespace DAMS_03.API
{
    public class TreatmentsController : ApiController
    {
        private DAMS_01Entities db = new DAMS_01Entities();

        // GET: api/
        public IHttpActionResult GetTreatments()
        {
            var treatments = (from Treatment in db.Treatments
                              select new
                              {
                                  ID = Treatment.ID,
                                  TreatmentName = Treatment.TreatmentName,
                                  TreatmentDesc = Treatment.TreatmentDesc,
                                  IsFollowUp = Treatment.IsFollowUp
                              }).ToList();

            return Ok(treatments);
        }

        // GET: api/Treatments/5
        [ResponseType(typeof(Treatment))]
        public IHttpActionResult GetTreatment(int id)
        {
            Treatment treatment = db.Treatments.Find(id);
            if (treatment == null)
            {
                return NotFound();
            }

            var returntreatment = from Treatment in db.Treatments
                                  where Treatment.ID == id
                                  select new
                                  {
                                      Treatment.ID,
                                      Treatment.TreatmentName,
                                      Treatment.TreatmentDesc,
                                      Treatment.IsFollowUp
                                  };

            return Ok(returntreatment);
        }

        //// GET: api/TreatmentsByHid/5
        //[ResponseType(typeof(Treatment))]
        //[Route("byhid/{id:int}")]
        //public IHttpActionResult GetTreatmentsByHid(int id)
        //{
        //    Treatment treatment = db.Treatments.Find(id);
        //    if (treatment == null)
        //    {
        //        return NotFound();
        //    }
            
        //    var returntreatment = from Treatment in db.Treatments
        //                          join ClinHospTreat in db.ClinicHospitalTreatments on Treatment.ID equals ClinHospTreat.TreatmentID
        //                          join ClinHosp in db.ClinicHospitals on ClinHospTreat.ClinicHospitalID equals ClinHosp.ID
        //                          where ClinHosp.ID == id
        //                          select new
        //                          {
        //                              Treatment.ID,
        //                              Treatment.TreatmentName,
        //                              Treatment.TreatmentDesc,
        //                              Treatment.IsFollowUp
        //                          };

        //    return Ok(returntreatment);
        //}

        // PUT: api/Treatments/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutTreatment(int id, Treatment treatment)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != treatment.ID)
            {
                return BadRequest();
            }

            db.Entry(treatment).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TreatmentExists(id))
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

        // POST: api/Treatments
        [ResponseType(typeof(Treatment))]
        public IHttpActionResult PostTreatment(Treatment treatment)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Treatments.Add(treatment);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = treatment.ID }, treatment);
        }

        // DELETE: api/Treatments/5
        [ResponseType(typeof(Treatment))]
        public IHttpActionResult DeleteTreatment(int id)
        {
            Treatment treatment = db.Treatments.Find(id);
            if (treatment == null)
            {
                return NotFound();
            }

            db.Treatments.Remove(treatment);
            db.SaveChanges();

            return Ok(treatment);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool TreatmentExists(int id)
        {
            return db.Treatments.Count(e => e.ID == id) > 0;
        }
    }
}