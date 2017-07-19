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
            return View(clinicHospital);
        }

        // GET: ClinicHospitals/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ClinicHospitals/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,ClinicHospitalID,ClinicHospitalName,ClinicHospitalAddress,ClinicHospitalOpenHours,ClinicHospitalTel,ClinicHospitalEmail,MaxBookings")] ClinicHospital clinicHospital)
        {
            if (ModelState.IsValid)
            {
                db.ClinicHospitals.Add(clinicHospital);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(clinicHospital);
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
                    if(editTreatment.IsChecked == true)
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
            return View(clinicHospital);
        }

        // POST: ClinicHospitals/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,ClinicHospitalID,ClinicHospitalName,ClinicHospitalAddress,ClinicHospitalOpenHours,ClinicHospitalTel,ClinicHospitalEmail,MaxBookings")] ClinicHospital clinicHospital)
        {
            if (ModelState.IsValid)
            {
                db.Entry(clinicHospital).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(clinicHospital);
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
