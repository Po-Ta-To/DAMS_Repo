using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DAMS_01.Models
{
    public class Appointment
    {
        public int ID_ { get; set; }

        public String AppointmentID { get; set; }

        public String UserID { get; set; }

        public String ApprovalState { get; set; }
        public DateTime PrefferedDate { get; set; }
        public String PreferredTime { get; set; }
        public int DoctorDentistID { get; set; }
        public int RequestDoctorID { get; set; }
        public String Remarks { get; set; }
        public DateTime? ConfirmedDate { get; set; }
        public int ConfirmedTime { get; set; }


        public string CreatedById { get; set; }
        public string UpdatedById { get; set; }
        public string DeletedById { get; set; }

        public ApplicationUser CreatedBy { get; set; }
        public ApplicationUser UpdatedBy { get; set; }
        public ApplicationUser DeletedBy { get; set; }


        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public DateTime? DeletedAt { get; set; }
    }
}