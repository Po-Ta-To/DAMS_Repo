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
    public class ClinicHospitalTreatmentPriceController : ApiController
    {
        private DAMS_01Entities db = new DAMS_01Entities();

        // GET: api/ClinicHospitalTreatmentPrice/5
        [ResponseType(typeof(ClinicHospitalTreatment))]
        public IHttpActionResult GetTreatmentHighestLowestPrice(int id)
        {

            var treatments = from ClinicHospitalTreatment in db.ClinicHospitalTreatments
                             where ClinicHospitalTreatment.TreatmentID == id
                             select new
                             {
                                 PriceHigh = ClinicHospitalTreatment.PriceHigh,
                                 PriceLow = ClinicHospitalTreatment.PriceLow
                             };

            var prices = (from treatment in treatments
                          select new
                          {
                              Highest = treatments.Max(clinHospTret => clinHospTret.PriceHigh),
                              Lowest = treatments.Min(clinHospTret => clinHospTret.PriceLow)
                          }).First();

            return Ok(prices);
        }
    }
}
