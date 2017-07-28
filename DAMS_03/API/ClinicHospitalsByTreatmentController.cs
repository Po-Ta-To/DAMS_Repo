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
    public class ClinicHospitalsByTreatmentController : ApiController
    {
        private DAMS_01Entities db = new DAMS_01Entities();

        //GET: api/ClinicHospitalsByTreatment/1
        [ResponseType(typeof(ClinicHospital))]
        public IHttpActionResult GetClinicHospitalsByTreatment(int id)
        {
            List<helpReturnClinicHospital> clinicHospitals = (from ClinicHospital in db.ClinicHospitals
                                                                join ClinHospTreat in db.ClinicHospitalTreatments on ClinicHospital.ID equals ClinHospTreat.ClinicHospitalID
                                                                join Treatment in db.Treatments on ClinHospTreat.TreatmentID equals Treatment.ID
                                                                where Treatment.ID == id
                                                                select new helpReturnClinicHospital
                                                                {
                                                                    ID = ClinicHospital.ID,
                                                                    ClinicHospitalName = ClinicHospital.ClinicHospitalName
                                                                }).ToList();

            foreach(helpReturnClinicHospital ch in clinicHospitals)
            {
                var getPrice = (from clinHosp in db.ClinicHospitals
                               join price in db.ClinicHospitalTreatments on clinHosp.ID equals price.ClinicHospitalID
                               join trt in db.Treatments on price.TreatmentID equals trt.ID
                               where clinHosp.ID == ch.ID && trt.ID == id
                               select new
                               {
                                   price.PriceLow,
                                   price.PriceHigh

                               }).SingleOrDefault();

                if (getPrice.PriceLow == getPrice.PriceHigh)
                {
                    ch.Price = getPrice.PriceLow.ToString();
                }
                else
                {
                    ch.Price = getPrice.PriceLow + " - " + getPrice.PriceHigh;
                }
            }// End of foreach()
            return Ok(clinicHospitals);
        } // End of GetClinicHospitalsByTreatment() method
    } // End of ClinicHospitalsByTreatmentController 
} // End of namespace

public class helpReturnClinicHospital
{
    public int ID { get; set; }
    public string ClinicHospitalName { get; set; }
    public string Price { get; set; } 
}
