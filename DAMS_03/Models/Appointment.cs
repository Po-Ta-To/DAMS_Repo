//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DAMS_03.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Appointment
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Appointment()
        {
            this.AppointmentTreatments = new HashSet<AppointmentTreatment>();
        }
    
        public int ID { get; set; }
        public string AppointmentID { get; set; }
        public int UserID { get; set; }
        public int ClinicHospitalID { get; set; }
        public int ApprovalState { get; set; }
        public System.DateTime PreferredDate { get; set; }
        public int PreferredTime { get; set; }
        public Nullable<int> DoctorDentistID { get; set; }
        public Nullable<int> RequestDoctorDentistID { get; set; }
        public string Remarks { get; set; }
        public Nullable<System.DateTime> AppointmentDate { get; set; }
        public Nullable<int> AppointmentTime { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<AppointmentTreatment> AppointmentTreatments { get; set; }
        public virtual ClinicHospital ClinicHospital { get; set; }
        public virtual DoctorDentist DoctorDentist { get; set; }
        public virtual DoctorDentist DoctorDentist1 { get; set; }
        public virtual UserAccount UserAccount { get; set; }
    }
}
