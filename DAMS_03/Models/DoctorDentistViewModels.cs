using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;


namespace DAMS_03.Models
{
    public class DoctorDentistCreateModel
    {
        //public int ID { get; set; }

        [Display(Name = "Dentist ID")]
        public string DoctorDentistID { get; set; }

        public string Name { get; set; }

        [Display(Name = "Hospital/Clinic")]
        public List<System.Web.Mvc.SelectListItem> itemSelection { get; set; }

        [Required]
        public string HospClinID { get; set; }

        [Required]
        public int MaxBookings { get; set; }
    }

    public class DoctorDentistDetailModel
    {    
        public int ID { get; set; }

        [Display(Name = "Dentist ID")]
        public string DoctorDentistID { get; set; }

        public string Name { get; set; }

        [Display(Name = "Hospital/Clinic")]
        public string HospClin { get; set; }
        
        public int MaxBookings { get; set; }

    }

    public class DoctorDentistEditModel
    {
        public int ID { get; set; }

        [Display(Name = "Dentist ID")]
        public string DoctorDentistID { get; set; }

        public string Name { get; set; }
        
        //[Required]
        [Display(Name = "Hospital/Clinic")]
        public string HospClin { get; set; }

        [Required]
        public int MaxBookings { get; set; }
    }

}

