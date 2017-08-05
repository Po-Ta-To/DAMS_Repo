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
    public class ClinicHospitalsController : ApiController
    {
        private DAMS_01Entities db = new DAMS_01Entities();

        // GET: api/ClinicHospitals
        public IHttpActionResult GetClinicHospitals()
        {
            //var clinicHospitals = from ClinicHospital in db.ClinicHospitals
            //                      select new
            //                      {
            //                          ClinicHospital.ID,
            //                          ClinicHospital.ClinicHospitalName
            //                      };

            var clinicHospitals = from ClinicHospital in db.ClinicHospitals
                                  select ClinicHospital;

            List<ClinicHospitalHelperModel> returnList = new List<ClinicHospitalHelperModel>();

            foreach (ClinicHospital clinicHospital in clinicHospitals)
            {
                if (clinicHospital.IsStringOpenHours == true)
                {
                    ClinicHospitalHelperModel returnModel = new ClinicHospitalHelperModel()
                    {
                        ID = clinicHospital.ID,
                        ClinicHospitalID = clinicHospital.ClinicHospitalID,
                        ClinicHospitalName = clinicHospital.ClinicHospitalName,
                        Address = clinicHospital.ClinicHospitalAddress,
                        Telephone = clinicHospital.ClinicHospitalTel,
                        Email = clinicHospital.ClinicHospitalEmail,
                        OpenHours = clinicHospital.ClinicHospitalOpenHours
                    };

                    returnList.Add(returnModel);
                }
                else
                {
                    List<OpeningHour> openingHours = (from oh in db.OpeningHours
                                                      where oh.ClinicHospitalID == clinicHospital.ID
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
                        returnOpeningHours += openingHours[0].TimeRangeStart + " - " + openingHours[0].TimeRangeEnd + "\n\n";
                    }
                    returnOpeningHours += "Saturday\n";
                    if (openingHours[1].TimeRangeStart == new TimeSpan(0) && openingHours[1].TimeRangeEnd == new TimeSpan(0))
                    {
                        returnOpeningHours += "Closed\n\n";
                    }
                    else
                    {
                        returnOpeningHours += openingHours[1].TimeRangeStart + " - " + openingHours[1].TimeRangeEnd + "\n\n";
                    }
                    returnOpeningHours += "Sundays and Public Holidays\n";
                    if (openingHours[2].TimeRangeStart == new TimeSpan(0) && openingHours[2].TimeRangeEnd == new TimeSpan(0))
                    {
                        returnOpeningHours += "Closed";
                    }
                    else
                    {
                        returnOpeningHours += openingHours[2].TimeRangeStart + " - " + openingHours[2].TimeRangeEnd + "\n\n";
                    }
                    
                    ClinicHospitalHelperModel returnModel = new ClinicHospitalHelperModel()
                    {
                        ID = clinicHospital.ID,
                        ClinicHospitalID = clinicHospital.ClinicHospitalID,
                        ClinicHospitalName = clinicHospital.ClinicHospitalName,
                        Address = clinicHospital.ClinicHospitalAddress,
                        Telephone = clinicHospital.ClinicHospitalTel,
                        Email = clinicHospital.ClinicHospitalEmail,
                        OpenHours = returnOpeningHours
                    };

                    returnList.Add(returnModel);
                }//end of if else
            }//end of loop

            return Ok(returnList);

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

            List<OpeningHour> openingHours = new List<OpeningHour>();

            openingHours = (from oh in db.OpeningHours
                            where oh.ClinicHospitalID == clinicHospital.ID
                            orderby oh.OpeningHoursDay ascending
                            select oh).ToList();


            ClinicHospitalDetailsModel detailedModel = new ClinicHospitalDetailsModel()
            {
                ID = clinicHospital.ID,
                ClinicHospitalID = clinicHospital.ClinicHospitalID,
                ClinicHospitalName = clinicHospital.ClinicHospitalName,
                ClinicHospitalAddress = clinicHospital.ClinicHospitalAddress,
                ClinicHospitalOpenHours = clinicHospital.ClinicHospitalOpenHours,
                ClinicHospitalTel = clinicHospital.ClinicHospitalTel,
                ClinicHospitalEmail = clinicHospital.ClinicHospitalEmail,
                IsStringOpenHours = clinicHospital.IsStringOpenHours,
                //OpeningHours = openingHours
            };



            if (detailedModel.IsStringOpenHours == true)
            {
                var returnModel = new
                {
                    ClinicHospitalID = clinicHospital.ClinicHospitalID,
                    ClinicHospitalName = clinicHospital.ClinicHospitalName,
                    ClinicHospitalAddress = clinicHospital.ClinicHospitalAddress,
                    ClinicHospitalTel = clinicHospital.ClinicHospitalTel,
                    ClinicHospitalEmail = clinicHospital.ClinicHospitalEmail,
                    ClinicHospitalOpenHours = clinicHospital.ClinicHospitalOpenHours
                };

                return Ok(returnModel);

            }
            else
            {
                //string openinghours = "Monday to Friday " + openingHours[0].TimeRangeStart + " - " + openingHours[0].TimeRangeEnd + "\n" +
                //    "Saturday " + openingHours[1].TimeRangeStart + " - " + openingHours[1].TimeRangeEnd + "\n" +
                //    "Sundays and Public Holidays " + openingHours[2].TimeRangeStart + " - " + openingHours[2].TimeRangeEnd + "";

                string openinghours = String.Empty;


                openinghours += "<b>Monday to Friday</b>\n";
                if (openingHours[0].TimeRangeStart == new TimeSpan(0) && openingHours[0].TimeRangeEnd == new TimeSpan(0))
                {
                    openinghours += "Closed\n\n";
                }
                else
                {
                    openinghours += openingHours[0].TimeRangeStart + " - " + openingHours[0].TimeRangeEnd + "\n\n";
                }
                openinghours += "<b>Saturday</b>\n";
                if (openingHours[1].TimeRangeStart == new TimeSpan(0) && openingHours[1].TimeRangeEnd == new TimeSpan(0))
                {
                    openinghours += "Closed\n\n";
                }
                else
                {
                    openinghours += openingHours[1].TimeRangeStart + " - " + openingHours[1].TimeRangeEnd + "\n\n";
                }
                openinghours += "<b>Sundays and Public Holidays</b>\n";
                if (openingHours[2].TimeRangeStart == new TimeSpan(0) && openingHours[2].TimeRangeEnd == new TimeSpan(0))
                {
                    openinghours += "Closed\n\n";
                }
                else
                {
                    openinghours += openingHours[2].TimeRangeStart + " - " + openingHours[2].TimeRangeEnd + "\n\n";
                }

                var returnModel = new
                {
                    ClinicHospitalID = clinicHospital.ClinicHospitalID,
                    ClinicHospitalName = clinicHospital.ClinicHospitalName,
                    ClinicHospitalAddress = clinicHospital.ClinicHospitalAddress,
                    ClinicHospitalTel = clinicHospital.ClinicHospitalTel,
                    ClinicHospitalEmail = clinicHospital.ClinicHospitalEmail,
                    ClinicHospitalOpenHours = openinghours
                };

                return Ok(returnModel);
            }

            //return BadRequest();
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

        private class ClinicHospitalHelperModel
        {
            public int ID { get; set; }
            public string ClinicHospitalID { get; set; }
            public string ClinicHospitalName { get; set; }
            public string Address { get; set; }
            public string Telephone { get; set; }
            public string Email { get; set; }
            public string OpenHours { get; set; }
        }

    }
}