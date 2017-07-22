using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using DAMS_03.Models;
using DAMS_03.Authorization;

namespace DAMS_03.Controllers
{
    [AuthorizeAdmin(Roles = "SysAdmin, HospAdmin, ClerkAdmin")]
    public class ClinicHospitalsController : Controller
    {
        private DAMS_01Entities db = new DAMS_01Entities();

        // GET: ClinicHospitals
        public ActionResult Index()
        {
            return View(db.ClinicHospitals.ToList());
        }

        // GET: ClinicHospitals/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ClinicHospital clinicHospital = db.ClinicHospitals.Find(id);
            if (clinicHospital == null)
            {
                return HttpNotFound();
            }

            List<OpeningHour> openingHours = new List<OpeningHour>();

            openingHours = (from oh in db.OpeningHours
                            where oh.ClinicHospitalID == clinicHospital.ID
                            select oh).ToList();


            ClinicHospitalDetailsModel returnModel = new ClinicHospitalDetailsModel()
            {
                ID = clinicHospital.ID,
                ClinicHospitalID = clinicHospital.ClinicHospitalID,
                ClinicHospitalName = clinicHospital.ClinicHospitalName,
                ClinicHospitalAddress = clinicHospital.ClinicHospitalAddress,
                ClinicHospitalOpenHours = clinicHospital.ClinicHospitalOpenHours,
                ClinicHospitalTel = clinicHospital.ClinicHospitalTel,
                ClinicHospitalEmail = clinicHospital.ClinicHospitalEmail,
                IsStringOpenHours = clinicHospital.IsStringOpenHours,
                OpeningHours = openingHours
            };

            return View(returnModel);
        }

        // GET: ClinicHospitals/Create
        public ActionResult Create()
        {
            ClinicHospitalCreateModel model = new ClinicHospitalCreateModel();



            model.OpeningHours = new List<OpeningHour>();

            for (int i = 1; i <= 3; i++)
            {
                model.OpeningHours.Add(new OpeningHour()
                {
                    OpeningHoursDay = i
                });
            }



            return View(model);
        }

        // POST: ClinicHospitals/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ClinicHospitalCreateModel model)
        {
            if (ModelState.IsValid)
            {
                ClinicHospital addClinicHospital = new ClinicHospital()
                {
                    ClinicHospitalID = model.ClinicHospitalID,
                    ClinicHospitalName = model.ClinicHospitalName,
                    ClinicHospitalAddress = model.ClinicHospitalAddress,
                    ClinicHospitalOpenHours = model.ClinicHospitalOpenHours,
                    ClinicHospitalTel = model.ClinicHospitalTel,
                    ClinicHospitalEmail = model.ClinicHospitalEmail,
                    IsStringOpenHours = model.IsStringOpenHours
                };

                db.ClinicHospitals.Add(addClinicHospital);

                for (int i = 0; i < 3; i++)
                {
                    OpeningHour addOpenHr = new OpeningHour()
                    {
                        OpeningHoursDay = (i + 1),
                        TimeRangeStart = model.OpeningHours[i].TimeRangeStart,
                        TimeRangeEnd = model.OpeningHours[i].TimeRangeEnd,
                        ClinicHospitalID = addClinicHospital.ID
                    };

                    db.OpeningHours.Add(addOpenHr);

                }


                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(model);
        }

        //get treatment by hospital
        // GET: ClinicHospitals/Treatments/5
        public ActionResult Treatments(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ClinicHospital clinicHospital = db.ClinicHospitals.Find(id);
            if (clinicHospital == null)
            {
                return HttpNotFound();
            }


            //var getTreatmentList = from cht in db.ClinicHospitalTreatments
            //                       where cht.ClinicHospitalID == id
            //                       select cht;

            var someTreatments = from t in db.Treatments
                                 join cht in db.ClinicHospitalTreatments on t.ID equals cht.TreatmentID
                                 join ch in db.ClinicHospitals on cht.ClinicHospitalID equals ch.ID
                                 where cht.ClinicHospitalID == id
                                 select new AddTreatmentsModel()
                                 {
                                     TreatmentID = t.ID,
                                     //TreatmentName = t.TreatmentName,
                                     //TreatmentDesc = t.TreatmentDesc,
                                     //IsFollowUp = t.IsFollowUp,
                                     Price = cht.Price

                                 };


            var allTreatments = from t in db.Treatments
                                select new AddTreatmentsModel()
                                {
                                    TreatmentID = t.ID,
                                    TreatmentName = t.TreatmentName,
                                    TreatmentDesc = t.TreatmentDesc,
                                    IsFollowUp = t.IsFollowUp,


                                };

            List<AddTreatmentsModel> returnList = new List<AddTreatmentsModel>();

            foreach (var treatment in allTreatments)
            {
                bool added = false;

                foreach (var someTreatment in someTreatments)
                {

                    if (someTreatment.TreatmentID == treatment.TreatmentID)
                    {
                        returnList.Add(new AddTreatmentsModel()
                        {
                            TreatmentID = treatment.TreatmentID,
                            TreatmentName = treatment.TreatmentName,
                            TreatmentDesc = treatment.TreatmentDesc,
                            IsFollowUp = treatment.IsFollowUp,
                            Price = someTreatment.Price,
                            IsChecked = true

                        });
                        added = true;
                        break;
                    }

                }

                if (!added)
                {
                    returnList.Add(new AddTreatmentsModel()
                    {
                        TreatmentID = treatment.TreatmentID,
                        TreatmentName = treatment.TreatmentName,
                        TreatmentDesc = treatment.TreatmentDesc,
                        IsFollowUp = treatment.IsFollowUp,
                        Price = 0,
                        IsChecked = false

                    });

                }

            }


            //return View(clinicHospital);



            return View(returnList);
        }


        // GET: ClinicHospitals/EditTreatments/5
        public ActionResult EditTreatments(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ClinicHospital clinicHospital = db.ClinicHospitals.Find(id);
            if (clinicHospital == null)
            {
                return HttpNotFound();
            }


            //var getTreatmentList = from cht in db.ClinicHospitalTreatments
            //                       where cht.ClinicHospitalID == id
            //                       select cht;

            var someTreatments = from t in db.Treatments
                                 join cht in db.ClinicHospitalTreatments on t.ID equals cht.TreatmentID
                                 join ch in db.ClinicHospitals on cht.ClinicHospitalID equals ch.ID
                                 where cht.ClinicHospitalID == id
                                 select new AddTreatmentsModel()
                                 {
                                     TreatmentID = t.ID,
                                     //TreatmentName = t.TreatmentName,
                                     //TreatmentDesc = t.TreatmentDesc,
                                     //IsFollowUp = t.IsFollowUp,
                                     Price = cht.Price

                                 };


            var allTreatments = from t in db.Treatments
                                select new AddTreatmentsModel()
                                {
                                    TreatmentID = t.ID,
                                    TreatmentName = t.TreatmentName,
                                    TreatmentDesc = t.TreatmentDesc,
                                    IsFollowUp = t.IsFollowUp,


                                };

            List<EditTreatmentsModel> returnList = new List<EditTreatmentsModel>();

            foreach (var treatment in allTreatments)
            {
                bool added = false;

                foreach (var someTreatment in someTreatments)
                {

                    if (someTreatment.TreatmentID == treatment.TreatmentID)
                    {
                        returnList.Add(new EditTreatmentsModel()
                        {
                            TreatmentID = treatment.TreatmentID,
                            TreatmentName = treatment.TreatmentName,
                            TreatmentDesc = treatment.TreatmentDesc,
                            IsFollowUp = treatment.IsFollowUp,
                            Price = someTreatment.Price,
                            IsChecked = true

                        });
                        added = true;
                        break;
                    }

                }

                if (!added)
                {
                    returnList.Add(new EditTreatmentsModel()
                    {
                        TreatmentID = treatment.TreatmentID,
                        TreatmentName = treatment.TreatmentName,
                        TreatmentDesc = treatment.TreatmentDesc,
                        IsFollowUp = treatment.IsFollowUp,
                        Price = 0,
                        IsChecked = false

                    });

                }

            }


            //return View(clinicHospital);



            return View(returnList);
            //return View(adminAccounts.ToList());
        }


        // POST: ClinicHospitals/EditTreatments/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditTreatments(List<EditTreatmentsModel> editTreatmentsList)
        {

            if (editTreatmentsList == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            if (ModelState.IsValid)
            {

                var url = Url.RequestContext.RouteData.Values["id"];
                int hospid = Int32.Parse((string)url);


                var dellist = from cht in db.ClinicHospitalTreatments
                              join ch in db.ClinicHospitals on cht.ClinicHospitalID equals ch.ID
                              where cht.ClinicHospitalID == hospid
                              select cht;

                db.ClinicHospitalTreatments.RemoveRange(dellist);

                foreach (EditTreatmentsModel editTreatment in editTreatmentsList)
                {
                    if (editTreatment.IsChecked == true)
                    {
                        db.ClinicHospitalTreatments.Add(new ClinicHospitalTreatment()
                        {
                            TreatmentID = editTreatment.TreatmentID,
                            ClinicHospitalID = hospid,
                            Price = editTreatment.Price
                        });
                    }


                }


                //db.Entry(clinicHospital).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(editTreatmentsList);
        }



        // GET: ClinicHospitals/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ClinicHospital clinicHospital = db.ClinicHospitals.Find(id);
            if (clinicHospital == null)
            {
                return HttpNotFound();
            }

            List<OpeningHour> openingHours = new List<OpeningHour>();

            openingHours = (from oh in db.OpeningHours
                            where oh.ClinicHospitalID == clinicHospital.ID
                            select oh).ToList();


            ClinicHospitalEditModel returnModel = new ClinicHospitalEditModel()
            {
                ID = clinicHospital.ID,
                ClinicHospitalID = clinicHospital.ClinicHospitalID,
                ClinicHospitalName = clinicHospital.ClinicHospitalName,
                ClinicHospitalAddress = clinicHospital.ClinicHospitalAddress,
                ClinicHospitalOpenHours = clinicHospital.ClinicHospitalOpenHours,
                ClinicHospitalTel = clinicHospital.ClinicHospitalTel,
                ClinicHospitalEmail = clinicHospital.ClinicHospitalEmail,
                IsStringOpenHours = clinicHospital.IsStringOpenHours,
                OpeningHours = openingHours
            };

            return View(returnModel);
        }

        // POST: ClinicHospitals/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(ClinicHospitalEditModel model)
        {
            if (ModelState.IsValid)
            {

                ClinicHospital editClinicHospital = (from ch in db.ClinicHospitals
                                                     where ch.ID == model.ID
                                                     select ch).SingleOrDefault();


                editClinicHospital.ClinicHospitalID = model.ClinicHospitalID;
                editClinicHospital.ClinicHospitalName = model.ClinicHospitalName;
                editClinicHospital.ClinicHospitalAddress = model.ClinicHospitalAddress;
                editClinicHospital.ClinicHospitalOpenHours = model.ClinicHospitalOpenHours;
                editClinicHospital.ClinicHospitalTel = model.ClinicHospitalTel;
                editClinicHospital.ClinicHospitalEmail = model.ClinicHospitalEmail;
                editClinicHospital.IsStringOpenHours = model.IsStringOpenHours;

                db.Entry(editClinicHospital).State = EntityState.Modified;

                List<OpeningHour> editOpeningHour = (from oh in db.OpeningHours
                                                 join ch in db.ClinicHospitals on oh.ClinicHospitalID equals ch.ID
                                                 where ch.ID == model.ID
                                                 select oh).ToList();




                for (int i = 0; i < 3; i++)
                {
                    for (int j = 0; j < 3; j++)
                    {
                        if (editOpeningHour[i].OpeningHoursDay == model.OpeningHours[j].OpeningHoursDay)
                        {
                            editOpeningHour[i].TimeRangeStart = model.OpeningHours[j].TimeRangeStart;
                            editOpeningHour[i].TimeRangeEnd = model.OpeningHours[j].TimeRangeEnd;
                            db.Entry(editOpeningHour[i]).State = EntityState.Modified;
                            break;
                        }
                    }

                }

                
                db.SaveChanges();
                return RedirectToAction("Index");
            }



            return View(model);
        }

        // GET: ClinicHospitals/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ClinicHospital clinicHospital = db.ClinicHospitals.Find(id);
            if (clinicHospital == null)
            {
                return HttpNotFound();
            }
            return View(clinicHospital);
        }

        // POST: ClinicHospitals/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ClinicHospital clinicHospital = db.ClinicHospitals.Find(id);
            db.ClinicHospitals.Remove(clinicHospital);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }



}
