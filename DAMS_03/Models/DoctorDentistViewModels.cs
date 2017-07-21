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
        public string DoctorDentistID { get; set; }
        public string Name { get; set; }


        [Display(Name = "Hospital/Clinic")]
        public List<System.Web.Mvc.SelectListItem> itemSelection { get; set; }

        [Required]
        public string HospClinID { get; set; }

    }
    
}

