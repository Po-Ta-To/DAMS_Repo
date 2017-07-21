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

            List<helperReturnTreatment> returntreatment = (from Treatment in db.Treatments
                                                           join ClinHospTreat in db.ClinicHospitalTreatments on Treatment.ID equals ClinHospTreat.TreatmentID
                                                           join ClinHosp in db.ClinicHospitals on ClinHospTreat.ClinicHospitalID equals ClinHosp.ID
                                                           where ClinHosp.ID == id
                                                           select new helperReturnTreatment()
                                                           {
                                                               ID = Treatment.ID,
                                                               TreatmentName = Treatment.TreatmentName,
                                                               TreatmentDesc = Treatment.TreatmentDesc,
                                                               IsFollowUp = Treatment.IsFollowUp
                                                           }).ToList();

            foreach (helperReturnTreatment t in returntreatment)
            {
                decimal getprice = (from treat in db.Treatments
                                    join price in db.ClinicHospitalTreatments on treat.ID equals price.TreatmentID
                                    join hosp in db.ClinicHospitals on price.ClinicHospitalID equals hosp.ID
                                    where treat.ID == t.ID && hosp.ID == id
                                    select price.Price).SingleOrDefault();

                t.Price = getprice;

            }




            return Ok(returntreatment);
        }

    }
}

public class helperReturnTreatment
{
    public int ID { get; set; }
    public string TreatmentName { get; set; }
    public string TreatmentDesc { get; set; }
    public bool IsFollowUp { get; set; }
    public decimal Price { get; set; }
}