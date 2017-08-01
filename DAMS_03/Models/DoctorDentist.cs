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
    
    public partial class DoctorDentist
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public DoctorDentist()
        {
            this.Appointments = new HashSet<Appointment>();
            this.Appointments1 = new HashSet<Appointment>();
            this.DoctorDentistDateBookings = new HashSet<DoctorDentistDateBooking>();
        }
    
        public int ID { get; set; }

        public string DoctorDentistID { get; set; }
        public string Name { get; set; }
        public int MaxBookings { get; set; }
        public int ClinicHospitalID { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Appointment> Appointments { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Appointment> Appointments1 { get; set; }
        public virtual ClinicHospital ClinicHospital { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<DoctorDentistDateBooking> DoctorDentistDateBookings { get; set; }
    }
}
