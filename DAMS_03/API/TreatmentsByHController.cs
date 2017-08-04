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
    public class TreatmentsByHController : ApiController
    {
        private DAMS_01Entities db = new DAMS_01Entities();

        // GET: api/TreatmentsByH/5
        [ResponseType(typeof(Treatment))]
        public IHttpActionResult GetTreatmentsByHid(int id)
        {
            Treatment treatment = db.Treatments.Find(id);
            if (treatment == null)
            {
                return NotFound();
            }

            List<HelperReturnTreatment> returntreatment = (from Treatment in db.Treatments
                                                           join ClinHospTreat in db.ClinicHospitalTreatments on Treatment.ID equals ClinHospTreat.TreatmentID
                                                           join ClinHosp in db.ClinicHospitals on ClinHospTreat.ClinicHospitalID equals ClinHosp.ID
                                                           where ClinHosp.ID == id
                                                           select new HelperReturnTreatment()
                                                           {
                                                               ID = Treatment.ID,
                                                               TreatmentName = Treatment.TreatmentName,
                                                               TreatmentDesc = Treatment.TreatmentDesc,
                                                               IsFollowUp = Treatment.IsFollowUp
                                                           }).ToList();

            foreach (HelperReturnTreatment t in returntreatment)
            {
                var getprice = (from treat in db.Treatments
                                join price in db.ClinicHospitalTreatments on treat.ID equals price.TreatmentID
                                join hosp in db.ClinicHospitals on price.ClinicHospitalID equals hosp.ID
                                where treat.ID == t.ID && hosp.ID == id
                                select new
                                {
                                    price.PriceLow,
                                    price.PriceHigh

                                }).SingleOrDefault();


                if (getprice.PriceLow == getprice.PriceHigh)
                {
                    string stringPx = String.Format("{0:C0}", getprice.PriceLow.ToString());
                    string stringPx_d = String.Format("{0:C}", getprice.PriceLow.ToString());
                    t.Price = stringPx;
                    t.Price_d = stringPx_d;
                }
                else
                {
                    decimal returnLow;
                    decimal returnHigh;
                    if (getprice.PriceLow > getprice.PriceHigh)
                    {
                        returnLow = getprice.PriceHigh;
                        returnHigh = getprice.PriceLow;
                    }
                    else
                    {
                        returnLow = getprice.PriceLow;
                        returnHigh = getprice.PriceHigh;
                    }
                    string stringLow = String.Format("{0:C0}", returnLow);
                    string stringHigh = String.Format("{0:C0}", returnHigh);
                    string stringLow_d = String.Format("{0:C}", returnLow);
                    string stringHigh_d = String.Format("{0:C}", returnHigh);
                    t.Price = stringLow + " - " + stringHigh;
                    t.Price_d = stringLow_d + " - " + stringHigh_d;
                }

                t.PriceLow = getprice.PriceLow;
                t.PriceHigh = getprice.PriceHigh;
            }

            return Ok(returntreatment);
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
