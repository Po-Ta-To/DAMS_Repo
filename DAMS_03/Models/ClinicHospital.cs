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
    
    public partial class ClinicHospital
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public ClinicHospital()
        {
            this.Appointments = new HashSet<Appointment>();
            this.Bookings = new HashSet<Booking>();
            this.ClinicHospitalAdvertisements = new HashSet<ClinicHospitalAdvertisement>();
            this.ClinicHospitalTreatments = new HashSet<ClinicHospitalTreatment>();
            this.ClinicHospitalDoctorDentists = new HashSet<ClinicHospitalDoctorDentist>();
            this.ClinicHospitalOpeningHours = new HashSet<ClinicHospitalOpeningHour>();
        }
    
        public int ID { get; set; }
        public string ClinicHospitalID { get; set; }
        public string ClinicHospitalName { get; set; }
        public string ClinicHospitalAddress { get; set; }
        public string ClinicHospitalOpenHours { get; set; }
        public string ClinicHospitalTel { get; set; }
        public string ClinicHospitalEmail { get; set; }
        public string MaxBookings { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Appointment> Appointments { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Booking> Bookings { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ClinicHospitalAdvertisement> ClinicHospitalAdvertisements { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ClinicHospitalTreatment> ClinicHospitalTreatments { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ClinicHospitalDoctorDentist> ClinicHospitalDoctorDentists { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ClinicHospitalOpeningHour> ClinicHospitalOpeningHours { get; set; }
    }
}