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
    
    public partial class AdminAccount
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public AdminAccount()
        {
            this.AdminAccountClinicHospitals = new HashSet<AdminAccountClinicHospital>();
        }
    
        public int ID { get; set; }
        public string AdminID { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string SecurityLevel { get; set; }
        public string AspNetID { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<AdminAccountClinicHospital> AdminAccountClinicHospitals { get; set; }
        public virtual AspNetUser AspNetUser { get; set; }
    }
}
