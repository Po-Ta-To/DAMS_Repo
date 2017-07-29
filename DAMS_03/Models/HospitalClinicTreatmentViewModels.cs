using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace DAMS_03.Models
{

    public class ClinicHospitalDetailsModel
    {
        [Required]
        public int ID { get; set; }
        [Required]
        [Display(Name = "Organisational ID")]
        public string ClinicHospitalID { get; set; }
        [Required]
        [Display(Name = "Name")]
        public string ClinicHospitalName { get; set; }
        [Required]
        [Display(Name = "Address")]
        public string ClinicHospitalAddress { get; set; }
        [Required]
        [Display(Name = "Opening Hours String")]
        public string ClinicHospitalOpenHours { get; set; }
        [Required]
        [Display(Name = "Tel")]
        public string ClinicHospitalTel { get; set; }
        [Required]
        [Display(Name = "Email")]
        public string ClinicHospitalEmail { get; set; }
        [Required]
        [Display(Name = "Use Opening Hours String")]
        public bool IsStringOpenHours { get; set; }

        [Required]
        //[Display(Name = "")]
        public List<OpeningHour> OpeningHours { get; set; }

        //[Required]
        public List<ClinicHospitalTimeslot> Timeslot { get; set; }

    }

    public class ClinicHospitalCreateModel
    {
        [Required]
        public int ID { get; set; }
        [Required]
        [Display(Name = "Organisational ID")]
        public string ClinicHospitalID { get; set; }
        [Required]
        [Display(Name = "Name")]
        public string ClinicHospitalName { get; set; }
        [Required]
        [Display(Name = "Address")]
        public string ClinicHospitalAddress { get; set; }
        [Required]
        [Display(Name = "Opening Hours String")]
        public string ClinicHospitalOpenHours { get; set; }
        [Required]
        [Display(Name = "Tel")]
        public string ClinicHospitalTel { get; set; }
        [Required]
        [Display(Name = "Email")]
        public string ClinicHospitalEmail { get; set; }
        [Required]
        [Display(Name = "Use Opening Hours String")]
        public bool IsStringOpenHours { get; set; }

        //[Display(Name = "")]
        //public List<System.Web.Mvc.SelectListItem> itemSelection { get; set; }

        public List<OpeningHour> OpeningHours { get; set; }

        //[Required]
        public List<ClinicHospitalTimeslot> Timeslot { get; set; }

    }

    public class ClinicHospitalEditModel
    {
        [Required]
        public int ID { get; set; }
        [Required]
        [Display(Name = "Organisational ID")]
        public string ClinicHospitalID { get; set; }
        [Required]
        [Display(Name = "Name")]
        public string ClinicHospitalName { get; set; }
        [Required]
        [Display(Name = "Address")]
        public string ClinicHospitalAddress { get; set; }
        [Required]
        [Display(Name = "Opening Hours String")]
        public string ClinicHospitalOpenHours { get; set; }
        [Required]
        [Display(Name = "Tel")]
        public string ClinicHospitalTel { get; set; }
        [Required]
        [Display(Name = "Email")]
        public string ClinicHospitalEmail { get; set; }
        [Required]
        [Display(Name = "Use Opening Hours String")]
        public bool IsStringOpenHours { get; set; }


        //[Display(Name = "")]
        //public List<System.Web.Mvc.SelectListItem> itemSelection { get; set; }

        public List<OpeningHour> OpeningHours { get; set; }

        //[Required]
        public List<ClinicHospitalTimeslotHelperClass> Timeslot { get; set; }

    }



    public class AddTreatmentsModel
    {
        //[Required]
        //public int ID { get; set; }

        [Required]
        public int TreatmentID { get; set; }

        [Required]
        public string TreatmentName { get; set; }

        [Required]
        public string TreatmentDesc { get; set; }

        [Required]
        public bool IsFollowUp { get; set; }

        [Required]
        [Display(Name = "Offers Treatment")]
        public bool IsChecked { get; set; }

        [Required]
        public decimal PriceLow { get; set; }

        [Required]
        public decimal PriceHigh { get; set; }



    }

    public class EditTreatmentsModel
    {
        //public List<bool> IsChecked { get; set; }


        [Required]
        public int TreatmentID { get; set; }

        [Required]//may not req for post
        public string TreatmentName { get; set; }

        [Required]//may not req for post
        public string TreatmentDesc { get; set; }

        [Required]//may not req for post
        public bool IsFollowUp { get; set; }

        [Required]
        [Display(Name = "Offers Treatment")]
        public bool IsChecked { get; set; }

        [Required]
        public decimal PriceLow { get; set; }

        [Required]
        public decimal PriceHigh { get; set; }
        
    }

    public class ClinicHospitalTimeslotHelperClass
    {
        [Required]
        public int TimeslotIndex { get; set; }
        
        public string TimeRangeSlotString { get; set; }

    }


}