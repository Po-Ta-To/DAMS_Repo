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
                                                                    ClinicHospitalName = ClinicHospital.ClinicHospitalName,
                                                                    Address = ClinicHospital.ClinicHospitalAddress,
                                                                    Telephone = ClinicHospital.ClinicHospitalTel,
                                                                    Email = ClinicHospital.ClinicHospitalEmail,
                                                                    OpenHours = ClinicHospital.ClinicHospitalOpenHours,
                                                                    IsStringOpenHours = ClinicHospital.IsStringOpenHours
                                                                }).ToList();

            foreach(helpReturnClinicHospital ch in clinicHospitals)
            {

                if (!ch.IsStringOpenHours)
                {
                    List<OpeningHour> openingHours = (from oh in db.OpeningHours
                                                      where oh.ClinicHospitalID == ch.ID
                                                      orderby oh.OpeningHoursDay ascending
                                                      select oh).ToList();

                    string returnOpeningHours = String.Empty;


                    returnOpeningHours += "Monday to Friday\n";
                    if (openingHours[0].TimeRangeStart == new TimeSpan(0) && openingHours[0].TimeRangeEnd == new TimeSpan(0))
                    {
                        returnOpeningHours += "Closed\n\n";
                    }
                    else
                    {
                        returnOpeningHours += openingHours[0].TimeRangeStart.Hours.ToString("00") + ":" + openingHours[0].TimeRangeStart.Minutes.ToString("00") + " - " + openingHours[0].TimeRangeEnd.Hours.ToString("00") + ":" + openingHours[0].TimeRangeEnd.Minutes.ToString("00") + "\n\n";
                    }
                    returnOpeningHours += "Saturday\n";
                    if (openingHours[1].TimeRangeStart == new TimeSpan(0) && openingHours[1].TimeRangeEnd == new TimeSpan(0))
                    {
                        returnOpeningHours += "Closed\n\n";
                    }
                    else
                    {
                        returnOpeningHours += openingHours[1].TimeRangeStart.Hours.ToString("00") + ":" + openingHours[1].TimeRangeStart.Minutes.ToString("00") + " - " + openingHours[1].TimeRangeEnd.Hours.ToString("00") + ":" + openingHours[1].TimeRangeEnd.Minutes.ToString("00") + "\n\n";
                    }
                    returnOpeningHours += "Sundays and Public Holidays\n";
                    if (openingHours[2].TimeRangeStart == new TimeSpan(0) && openingHours[2].TimeRangeEnd == new TimeSpan(0))
                    {
                        returnOpeningHours += "Closed";
                    }
                    else
                    {
                        returnOpeningHours += openingHours[2].TimeRangeStart.Hours.ToString("00") + ":" + openingHours[2].TimeRangeStart.Minutes.ToString("00") + " - " + openingHours[2].TimeRangeEnd.Hours.ToString("00") + ":" + openingHours[2].TimeRangeEnd.Minutes.ToString("00");
                    }
                    ch.OpenHours = returnOpeningHours;
                }

                var getPrice = (from clinHosp in db.ClinicHospitals
                               join price in db.ClinicHospitalTreatments on clinHosp.ID equals price.ClinicHospitalID
                               join trt in db.Treatments on price.TreatmentID equals trt.ID
                               where clinHosp.ID == ch.ID && trt.ID == id
                               select new
                               {
                                   price.PriceLow,
                                   price.PriceHigh

                               }).SingleOrDefault();

                string returnPriceLow = String.Format("{0:C0}", getPrice.PriceLow);
                string returnPriceHigh = String.Format("{0:C0}", getPrice.PriceHigh);

                if (getPrice.PriceLow == getPrice.PriceHigh)
                {
                    ch.Price = returnPriceLow;
                }
                else
                {
                    ch.Price = returnPriceLow + " - " + returnPriceHigh;
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
    public string Address { get; set; }
    public string Telephone { get; set; }
    public string Email { get; set; }
    public string OpenHours{ get; set; }
    public bool IsStringOpenHours { get; set; }
}
