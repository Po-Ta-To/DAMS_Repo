using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace DAMS_03.Models
{


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

        [Display(Name = "Offers Treatment")]
        public bool IsChecked { get; set; }


        public decimal Price { get; set; }

        

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
        public decimal Price { get; set; }



    }


}