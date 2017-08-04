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

            List<HelperReturnTreatment> returnList = new List<HelperReturnTreatment>();

            if (treatments.Count == 0)
            {
                return Ok(returnList);
            }
            else
            {

                foreach (var treatment in treatments)
                {
                    HelperReturnTreatment addTreatment = new HelperReturnTreatment()
                    {
                        ID = treatment.ID,
                        TreatmentName = treatment.TreatmentName,
                        TreatmentDesc = treatment.TreatmentDesc,
                        IsFollowUp = treatment.IsFollowUp
                    };

                    var prices = (from t in db.Treatments
                                  join cht in db.ClinicHospitalTreatments on t.ID equals cht.TreatmentID
                                  where t.ID == addTreatment.ID
                                  select new
                                  {
                                      PriceLow = cht.PriceLow,
                                      PriceHigh = cht.PriceHigh
                                  }).ToList();

                    if (prices.Count == 0)
                    {
                        addTreatment.PriceLow = 0;
                        addTreatment.PriceHigh = 0;
                        addTreatment.Price = "Not Offered";
                        addTreatment.Price_d = "Not Offered";
                        returnList.Add(addTreatment);
                    }
                    else
                    {
                        decimal finalLow = prices[0].PriceLow;
                        decimal finalHigh = prices[0].PriceHigh;
                        decimal getLow = 0;
                        decimal getHigh = 0;

                        foreach (var price in prices)
                        {
                            if (price.PriceLow > price.PriceHigh)
                            {
                                getLow = price.PriceHigh;
                                getHigh = price.PriceLow;
                            }
                            else
                            {
                                getLow = price.PriceLow;
                                getHigh = price.PriceHigh;
                            }

                            if (finalLow > getLow)
                            {
                                finalLow = getLow;
                            }
                            if (finalHigh < getHigh)
                            {
                                finalHigh = getHigh;
                            }

                        }

                        if (getLow == getHigh)
                        {
                            string stringPx = String.Format("{0:C0}", finalLow);
                            string stringPx_d = String.Format("{0:C}", finalLow);
                            addTreatment.Price = stringPx;
                            addTreatment.Price_d = stringPx_d;
                        }
                        else
                        {
                            string stringLow = String.Format("{0:C0}", finalLow);
                            string stringHigh = String.Format("{0:C0}", finalHigh);
                            string stringLow_d = String.Format("{0:C}", finalLow);
                            string stringHigh_d = String.Format("{0:C}", finalHigh);

                            addTreatment.Price = stringLow + " - " + stringHigh;
                            addTreatment.Price_d = stringLow_d + " - " + stringHigh_d;
                        }

                        //addTreatment.Price = "";
                        addTreatment.PriceLow = finalLow;
                        addTreatment.PriceHigh = finalHigh;
                    }

                    returnList.Add(addTreatment);
                }
            }


            return Ok(returnList);
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

        public class HelperReturnTreatment
        {
            public int ID { get; set; }
            public string TreatmentName { get; set; }
            public string TreatmentDesc { get; set; }
            public bool IsFollowUp { get; set; }
            public string Price { get; set; }
            public string Price_d { get; set; }
            public decimal PriceLow { get; set; }
            public decimal PriceHigh { get; set; }
        }

    }
}