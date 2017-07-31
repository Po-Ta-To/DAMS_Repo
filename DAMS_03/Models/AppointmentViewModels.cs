using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace DAMS_03.Models
{
    public class AppointmentCreateModel
    {
        [Required]
        public string AppointmentID { get; set; }

        [Required]
        public int UserID { get; set; }

        [Required]
        public int ClinicHospitalID { get; set; }

        //[Required]
        //public int ApprovalState { get; set; }
        
        [Required]
        public System.DateTime PreferredDate { get; set; }

        [Required]
        public int PreferredTime { get; set; }

        //[Required]
        //public int DoctorDentistID { get; set; }

        [Required]
        public int RequestDoctorDentistID { get; set; }

        [Required]
        public string Remarks { get; set; }

        [Required]
        public int[] Treatments { get; set; }
    }

    public class AppointmentDetailViewModel
    {
        public int ID { get; set; }
        [Display(Name = "Appointment ID")]
        public string AppointmentID { get; set; }
        [Display(Name = "User")]
        public string UserName { get; set; }
        public int UserID { get; set; }
        [Display(Name = "Clinic/Hospital Name")]
        public string ClinicHospitalName { get; set; }
        public int ClinicHospitalID { get; set; }
        [Display(Name = "Approval State")]
        public string ApprovalState { get; set; }
        [Display(Name = "Preferred Date")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        public System.DateTime PreferredDate { get; set; }

        [Display(Name = "Preferred Timeslot")]
        public int PreferredTime { get; set; }

        //[Display(Name = "Preferred Timeslot")]
        //public string PreferredTime { get; set; }

        [Display(Name = "Assigned Doctor/Dentist Name")]
        public string DoctorDentistName { get; set; }
        public int? DoctorDentistID { get; set; }
        [Display(Name = "Requested Doctor/Dentist Name")]
        public string RequestDoctorDentistName { get; set; }
        public int? RequestDoctorDentistID { get; set; }
        public string Remarks { get; set; }
        [Display(Name = "Appointment Date")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        public System.DateTime? AppointmentDate { get; set; }

        [Display(Name = "Appointment Timeslot")]
        public int? AppointmentTime { get; set; }

        //[Display(Name = "Appointment Timeslot")]
        //public string AppointmentTime { get; set; }

        public List<Treatment> listOfTreatments { get; set; }

        public string approvalString { get; set; }
    }

    public class AppointmentCreateViewModel
    {
        //public int ID { get; set; }

        [Required]
        [Display(Name = "Appointment ID")]
        public string AppointmentID { get; set; }

        //public string UserName { get; set; }
        public List<System.Web.Mvc.SelectListItem> selectUser { get; set; }

        [Required]
        [Display(Name = "User")]
        public string UserID { get; set; }//

        //public string ClinicHospitalName { get; set; }
        //public List<System.Web.Mvc.SelectListItem> selectClinicHospital { get; set; }

        //[Required]
        //[Display(Name = "Clinic/Hospital Name")]
        //public string ClinicHospitalID { get; set; }//

        [Display(Name = "Approval State")]
        [Range(1, 5)]
        public int ApprovalState { get; set; }

        [Required]
        [Display(Name = "Preferred Date")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        public System.DateTime PreferredDate { get; set; }

        [Required]
        [Display(Name = "Preferred Timeslot")]
        public int PreferredTime { get; set; }

        //public string DoctorDentistName { get; set; }
        public List<System.Web.Mvc.SelectListItem> selectDoctorDentist { get; set; }

        [Display(Name = "Assigned Doctor/Dentist Name")]
        public string DoctorDentistID { get; set; }//

        //public string RequestDoctorDentistName { get; set; }

        [Display(Name = "Requested Doctor/Dentist Name")]
        public string RequestDoctorDentistID { get; set; }//

        [Required]
        public string Remarks { get; set; }

        [Display(Name = "Appointment Date")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        //[Required]
        public System.DateTime AppointmentDate { get; set; }

        [Display(Name = "Appointment Timeslot")]
        public int AppointmentTime { get; set; }

        //public List<Treatment> listOfTreatments { get; set; }

        [Required]
        public List<TreatmentHelperModel> listOfTreatments { get; set; }

    }

    public class AppointmentEditViewModel
    {
        public int ID { get; set; }

        [Required]
        [Display(Name = "Appointment ID")]
        public string AppointmentID { get; set; }

        public string UserName { get; set; }

        public List<System.Web.Mvc.SelectListItem> selectUser { get; set; }

        [Required]
        [Display(Name = "User")]
        public string UserID { get; set; }//

        public string ClinicHospitalName { get; set; }

        public List<System.Web.Mvc.SelectListItem> selectClinicHospital { get; set; }

        [Required]
        [Display(Name = "Clinic/Hospital Name")]
        public string ClinicHospitalID { get; set; }//

        [Display(Name = "Approval State")]
        [Range(1, 5)]
        public string ApprovalState { get; set; }

        [Required]
        [Display(Name = "Preferred Date")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        public System.DateTime PreferredDate { get; set; }

        [Required]
        [Display(Name = "Preferred Timeslot")]
        public int PreferredTime { get; set; }

        public string DoctorDentistName { get; set; }

        public List<System.Web.Mvc.SelectListItem> selectDoctorDentist { get; set; }

        [Display(Name = "Assigned Doctor/Dentist Name")]
        public string DoctorDentistID { get; set; }

        public string RequestDoctorDentistName { get; set; }

        [Display(Name = "Requested Doctor/Dentist Name")]
        public string RequestDoctorDentistID { get; set; }

        [Required]
        public string Remarks { get; set; }

        [Display(Name = "Appointment Date")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        public System.DateTime? AppointmentDate { get; set; }

        [Display(Name = "Appointment Timeslot")]
        public int? AppointmentTime { get; set; }


        public List<TreatmentHelperModel> listOfTreatments { get; set; }
    }

    public class TreatmentHelperModel
    {
        public int ID { get; set; }
        public string TreatmentID { get; set; }
        public string TreatmentName { get; set; }
        public string TreatmentDesc { get; set; }
        public bool IsFollowUp { get; set; }
        public string Price { get; set; }
        public decimal PriceLow { get; set; }
        public decimal PriceHigh { get; set; }
        public bool IsChecked { get; set; }
    }

}